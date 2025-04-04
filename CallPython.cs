using System.Diagnostics;
using System.Text;

namespace ProyectoMetodos;

internal static class CallPython
{
	private readonly static string PythonPath = Path.Combine(
		AppDomain.CurrentDomain.BaseDirectory,
		"venv", "Scripts", "python.exe"
	);
	private readonly static string SrcPath = Path.Combine(
		AppDomain.CurrentDomain.BaseDirectory,
		"main.py"
	);

	internal static string Run(FuncPy f, string text, string key, bool encode)
	{
		if (!File.Exists(PythonPath))
			throw new FileNotFoundException($"No se encontró Python en {PythonPath}");

		if (!File.Exists(SrcPath))
			throw new FileNotFoundException($"No se encontró el script main.py en {SrcPath}");

		ProcessStartInfo psi = new()
		{
			FileName = PythonPath,
			Arguments = $"{SrcPath} {(f == FuncPy.Encrypt).ToString().ToLower()} \"{text}\" \"{key}\" {encode.ToString().ToLower()}",
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			CreateNoWindow = true,
			StandardOutputEncoding = Encoding.UTF8,
			StandardErrorEncoding = Encoding.UTF8
		};

		Debug.WriteLine($"Ejecutando el comando: {psi.FileName} {psi.Arguments}");

		using Process p = new() { StartInfo = psi };
		p.Start();
		string stdout = p.StandardOutput.ReadToEnd();
		string stderr = p.StandardError.ReadToEnd();

		p.WaitForExit();

		if (!string.IsNullOrWhiteSpace(stderr))
			throw new InvalidOperationException("Error en Python: " + stderr);

		return stdout[..^2];
	}
}

internal enum FuncPy
{
	Encrypt,
	Decrypt
}
