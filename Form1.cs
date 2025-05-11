using System.Diagnostics;
using System.Text;

namespace ProyectoMetodos;

public partial class Encriptador : Form
{
    private static readonly Size defaultSize = new(350, 259);
    private static readonly Size postProcessSize = new(350, 459);
    private const int Sep = 20;

    private string input = string.Empty;
    private string result = string.Empty;

    private bool IsFinalized = false;

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
 
    public Encriptador()
    {
        InitializeComponent();
        Size = defaultSize;
        ArrowPictureBox.Size = new(Sep, Sep);

        TextPanel.Size = new(defaultSize.Width, postProcessSize.Height - defaultSize.Height);

        InputTextBox.Width = postProcessSize.Width / 2 - Sep;
        ResultTextBox.Width = postProcessSize.Width / 2 - Sep;

        Point locationImg = new(
            TextPanel.Width / 2 - ArrowPictureBox.Width,
            TextPanel.Height / 2 - ArrowPictureBox.Height
        );
        ArrowPictureBox.Location = locationImg;
        Debug.WriteLine($"Location: {locationImg}");

#if DEBUG
        KeyEntry.Text = "hola";
        OpenOnEntry.Text = @"C:\Users\alex2\Desktop\test.txt";
        SaveOnEntry.Text = @"C:\Users\alex2\Desktop\test_1.txt";
#endif
    }

    public void AddPanel()
    {
        if (!IsFinalized)
        {
            Size = postProcessSize;
            TextPanel.Visible = true;
            IsFinalized = true;
        }
        InputTextBox.Text = input;
        ResultTextBox.Text = result;
    }
    public void RemovePanel()
    {
        if (IsFinalized)
        {
            Size = defaultSize;
            TextPanel.Visible = false;
            IsFinalized = false;
        }
        InputTextBox.Text = string.Empty;
        ResultTextBox.Text = string.Empty;	
    }

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
        input = File.ReadAllText(OpenOnEntry.Text, Encoding.UTF8);
        string key = KeyEntry.Text;
        string encode = MD5KeyBtn.Checked ? "md5" : "sha256";
        ResultType rType;

        try
        {
            result = CallPython.Run(
                input,
                key,
                encode,
                out rType
            );
        }
        catch (Exception ex)
        {
            

            MessageBox.Show(
                "Error: " + ex.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            RemovePanel();

            return;
        }

        File.WriteAllText(SaveOnEntry.Text, result, Encoding.UTF8);

        AddPanel();

        MessageBox.Show(
            $"Proceso completado, {(rType.HasFlag(ResultType.Encrypted) ? "encriptado" : "desencriptado")} y archivo guardado en:\n{SaveOnEntry.Text}",
            "Éxito",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }

    private void CancelBtn_Click(object sender, EventArgs e) => Close();
}
