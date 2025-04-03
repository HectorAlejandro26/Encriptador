using System.Diagnostics;
using System.Text;

namespace ProyectoMetodos;

internal static class CallPython
{
	private const string PythonPath = "python.exe";
	private const string SrcPath = "main.py";

	internal static string Run(FuncPy f, string text, string key, bool encode)
	{
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

		using Process p = new() { StartInfo = psi };
		p.Start();
		string stdout = p.StandardOutput.ReadToEnd();
		string stderr = p.StandardError.ReadToEnd();

		p.WaitForExit();

		if (!string.IsNullOrWhiteSpace(stderr))
			throw new Exception("Error en Python: " + stderr);

		return stdout;
	}
}

internal enum FuncPy
{
	Encrypt,
	Decrypt
}
