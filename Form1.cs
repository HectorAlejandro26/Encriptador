using System.Text;

namespace ProyectoMetodos;

public partial class Encriptador : Form
{
	internal bool IsStartable
	{
		get
		{
			string dirPath = Path.GetDirectoryName(SaveOnEntry.Text) ?? "";
			return KeyEntry.Text.Length > 0
				&& File.Exists(OpenOnEntry.Text)
				&& !string.IsNullOrEmpty(dirPath) && Directory.Exists(dirPath);
		}
	}

	public Encriptador() => InitializeComponent();

	private void OpenOnBtn_Click(object sender, EventArgs e)
	{
		using OpenFileDialog ofd = new();

		ofd.Title = "Seleccionar archivo";
		ofd.Filter = "Archivos de texto|*.txt";

		if (ofd.ShowDialog() == DialogResult.OK)
			OpenOnEntry.Text = ofd.FileName;
	}
	private void OpenOnEntry_TextChanged(object sender, EventArgs e)
	{
		OpenOnEntry.ForeColor = File.Exists(OpenOnEntry.Text)
			? SystemColors.WindowText
			: Color.Red;

		StartBtn.Enabled = IsStartable;
	}

	private void SaveOnBtn_Click(object sender, EventArgs e)
	{
		using SaveFileDialog sfd = new();

		sfd.Title = "Guardar archivo";
		sfd.Filter = "Archivos de texto|*.txt";
		sfd.FileName = "encriptado.txt";

		if (sfd.ShowDialog() == DialogResult.OK)
			SaveOnEntry.Text = sfd.FileName;
	}
	private void SaveOnEntry_TextChanged(object sender, EventArgs e)
	{
		string dirPath = Path.GetDirectoryName(SaveOnEntry.Text) ?? "";
		SaveOnEntry.ForeColor = !string.IsNullOrEmpty(dirPath) && Directory.Exists(dirPath)
			? SystemColors.WindowText
			: Color.Red;

		StartBtn.Enabled = IsStartable;
	}

	private void KeyEntry_TextChanged(object sender, EventArgs e) =>
		StartBtn.Enabled = IsStartable;

	private void StartBtn_Click(object sender, EventArgs e)
	{
		string text = File.ReadAllText(OpenOnEntry.Text, Encoding.UTF8);
		string key = KeyEntry.Text;
		bool encode = MD5KeyBtn.Checked;

		string output = CallPython.Run(FuncPy.Decrypt, text, key, encode);

		File.WriteAllText(SaveOnEntry.Text, output, Encoding.UTF8);
	}
}
