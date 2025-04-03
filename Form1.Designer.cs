namespace ProyectoMetodos
{
    partial class Encriptador
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			KeyEntry = new TextBox();
			KeyLabel = new Label();
			MD5KeyBtn = new RadioButton();
			SHA256KeyBtn = new RadioButton();
			MethodKeyLabel = new Label();
			OpenOnBtn = new Button();
			OpenOnEntry = new TextBox();
			SaveOnBtn = new Button();
			SaveOnEntry = new TextBox();
			StartBtn = new Button();
			SuspendLayout();
			// 
			// KeyEntry
			// 
			KeyEntry.Location = new Point(12, 27);
			KeyEntry.Name = "KeyEntry";
			KeyEntry.Size = new Size(310, 23);
			KeyEntry.TabIndex = 0;
			KeyEntry.TextChanged += KeyEntry_TextChanged;
			// 
			// KeyLabel
			// 
			KeyLabel.AutoSize = true;
			KeyLabel.Location = new Point(12, 9);
			KeyLabel.Name = "KeyLabel";
			KeyLabel.Size = new Size(36, 15);
			KeyLabel.TabIndex = 1;
			KeyLabel.Text = "Clave";
			// 
			// MD5KeyBtn
			// 
			MD5KeyBtn.AutoSize = true;
			MD5KeyBtn.Checked = true;
			MD5KeyBtn.Location = new Point(12, 71);
			MD5KeyBtn.Name = "MD5KeyBtn";
			MD5KeyBtn.Size = new Size(50, 19);
			MD5KeyBtn.TabIndex = 2;
			MD5KeyBtn.TabStop = true;
			MD5KeyBtn.Text = "MD5";
			MD5KeyBtn.UseVisualStyleBackColor = true;
			// 
			// SHA256KeyBtn
			// 
			SHA256KeyBtn.AutoSize = true;
			SHA256KeyBtn.Location = new Point(12, 96);
			SHA256KeyBtn.Name = "SHA256KeyBtn";
			SHA256KeyBtn.Size = new Size(71, 19);
			SHA256KeyBtn.TabIndex = 3;
			SHA256KeyBtn.TabStop = true;
			SHA256KeyBtn.Text = "SHA-256";
			SHA256KeyBtn.UseVisualStyleBackColor = true;
			// 
			// MethodKeyLabel
			// 
			MethodKeyLabel.AutoSize = true;
			MethodKeyLabel.Location = new Point(12, 53);
			MethodKeyLabel.Name = "MethodKeyLabel";
			MethodKeyLabel.Size = new Size(192, 15);
			MethodKeyLabel.TabIndex = 4;
			MethodKeyLabel.Text = "Metodo de encriptacion de la clave";
			// 
			// OpenOnBtn
			// 
			OpenOnBtn.Location = new Point(8, 127);
			OpenOnBtn.Name = "OpenOnBtn";
			OpenOnBtn.Size = new Size(75, 23);
			OpenOnBtn.TabIndex = 5;
			OpenOnBtn.Text = "Abrir...";
			OpenOnBtn.UseVisualStyleBackColor = true;
			OpenOnBtn.Click += OpenOnBtn_Click;
			// 
			// OpenOnEntry
			// 
			OpenOnEntry.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			OpenOnEntry.Location = new Point(89, 127);
			OpenOnEntry.Name = "OpenOnEntry";
			OpenOnEntry.Size = new Size(233, 23);
			OpenOnEntry.TabIndex = 6;
			OpenOnEntry.TextChanged += OpenOnEntry_TextChanged;
			// 
			// SaveOnBtn
			// 
			SaveOnBtn.Location = new Point(8, 156);
			SaveOnBtn.Name = "SaveOnBtn";
			SaveOnBtn.Size = new Size(75, 23);
			SaveOnBtn.TabIndex = 5;
			SaveOnBtn.Text = "Guardar...";
			SaveOnBtn.UseVisualStyleBackColor = true;
			SaveOnBtn.Click += SaveOnBtn_Click;
			// 
			// SaveOnEntry
			// 
			SaveOnEntry.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			SaveOnEntry.Location = new Point(89, 156);
			SaveOnEntry.Name = "SaveOnEntry";
			SaveOnEntry.Size = new Size(233, 23);
			SaveOnEntry.TabIndex = 6;
			SaveOnEntry.TextChanged += SaveOnEntry_TextChanged;
			// 
			// StartBtn
			// 
			StartBtn.Enabled = false;
			StartBtn.Location = new Point(137, 185);
			StartBtn.Name = "StartBtn";
			StartBtn.Size = new Size(75, 23);
			StartBtn.TabIndex = 5;
			StartBtn.Text = "Iniciar";
			StartBtn.UseVisualStyleBackColor = true;
			StartBtn.Click += StartBtn_Click;
			// 
			// Encriptador
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(334, 220);
			Controls.Add(SaveOnEntry);
			Controls.Add(StartBtn);
			Controls.Add(SaveOnBtn);
			Controls.Add(OpenOnEntry);
			Controls.Add(OpenOnBtn);
			Controls.Add(MethodKeyLabel);
			Controls.Add(SHA256KeyBtn);
			Controls.Add(MD5KeyBtn);
			Controls.Add(KeyLabel);
			Controls.Add(KeyEntry);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Name = "Encriptador";
			Text = "Encriptador";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox KeyEntry;
		private Label KeyLabel;
		private RadioButton MD5KeyBtn;
		private RadioButton SHA256KeyBtn;
		private Label MethodKeyLabel;
		private Button OpenOnBtn;
		private TextBox OpenOnEntry;
		private Button SaveOnBtn;
		private TextBox SaveOnEntry;
		private Button StartBtn;
	}
}
