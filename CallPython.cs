using System.Diagnostics;
using System.Text;

namespace ProyectoMetodos;

[Flags]
public enum ResultType
{
	Error = 0b0000,
	Encrypted = 0b0001,
	Decrypted = 0b0010,
	Ambiguos = 0b0100,
	Success = Encrypted | Decrypted,
	Failure = Error | Ambiguos
}
internal static class CallPython
{
	private readonly static string PythonPath = Path.Combine(
		AppDomain.CurrentDomain.BaseDirectory,
		".venv", "Scripts", "python.exe"
	);
	private readonly static string SrcPath = Path.Combine(
		AppDomain.CurrentDomain.BaseDirectory,
		"main.py"
	);
	private readonly static string VenvPath = Path.Combine(
		AppDomain.CurrentDomain.BaseDirectory,
        ".venv", "Scripts", "activate.bat"
    );

	internal static string Run(string text, string key, string encode) => Run(text, key, encode, out _);
	internal static string Run(string text, string key, string encode, out ResultType resultType)
	{
		if (!File.Exists(PythonPath))
			throw new FileNotFoundException($"No se encontró Python en {PythonPath}");

		if (!File.Exists(SrcPath))
			throw new FileNotFoundException($"No se encontró el script main.py en {SrcPath}");

        if (!ActivateVenv())
        {
            resultType = ResultType.Error;
            throw new InvalidOperationException("No se pudo activar el entorno virtual");
        }

        ProcessStartInfo psi = new()
		{
			FileName = PythonPath,
			Arguments = $"{SrcPath} \"{text}\" \"{key}\" {encode.ToLower()}",
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			CreateNoWindow = true,
			StandardOutputEncoding = Encoding.UTF8,
			StandardErrorEncoding = Encoding.UTF8,
            WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
        };

		Debug.WriteLine($"Ejecutando el comando: {psi.FileName} {psi.Arguments}");

		using Process p = new() { StartInfo = psi };
		p.Start();
		string stdout = p.StandardOutput.ReadToEnd();
		string stderr = p.StandardError.ReadToEnd();

		p.WaitForExit();

		resultType = p.ExitCode switch
		{
			0 => ResultType.Encrypted,
            1 => ResultType.Decrypted,
            2 => ResultType.Ambiguos,
			_ => ResultType.Error
        };

		if (resultType.HasFlag(ResultType.Failure))
			throw new InvalidOperationException("Error en Python: " + stderr);

		return stdout;
	}
    private static bool ActivateVenv()
    {
        try
        {
            // Verificar si el entorno virtual ya está activado
            string? virtualEnvPath = Environment.GetEnvironmentVariable("VIRTUAL_ENV");
            if (!string.IsNullOrEmpty(virtualEnvPath))
            {
                Debug.WriteLine($"El entorno virtual ya está activado: {virtualEnvPath}");
                return true;
            }

            if (!File.Exists(VenvPath))
            {
                Debug.WriteLine($"No se encontró el script de activación en {VenvPath}");
                return false;
            }

            // Crear un proceso para activar el entorno virtual
            var activateProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C \"{VenvPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                }
            };

            activateProcess.Start();
            string output = activateProcess.StandardOutput.ReadToEnd();
            string error = activateProcess.StandardError.ReadToEnd();
            activateProcess.WaitForExit();

            if (activateProcess.ExitCode != 0)
            {
                Debug.WriteLine($"Error al activar el entorno virtual: {error}");
                return false;
            }

            Debug.WriteLine($"Entorno virtual activado correctamente. Output: {output}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Excepción al activar el entorno virtual: {ex.Message}");
            return false;
        }
    }
}
