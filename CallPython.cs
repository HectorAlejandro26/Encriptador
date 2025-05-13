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
	private readonly static string ExePath = Path.Combine(
		AppDomain.CurrentDomain.BaseDirectory,
		"HillCipher.exe"
	);

	internal static string Run(string text, string key, string encode) => Run(text, key, encode, out _);
	internal static string Run(string text, string key, string encode, out ResultType resultType)
	{
		if (!File.Exists(ExePath))
			throw new FileNotFoundException($"No se encontró el ejecutable {ExePath}");

        ProcessStartInfo psi = new()
		{
			FileName = ExePath,
			Arguments = $"\"{text}\" \"{key}\" {encode.ToLower()}",
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
}
