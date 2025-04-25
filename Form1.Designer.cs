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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Encriptador));
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
            CancelBtn = new Button();
            TextPanel = new Panel();
            ArrowPictureBox = new PictureBox();
            ResultTextBox = new TextBox();
            InputTextBox = new TextBox();
            TextPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ArrowPictureBox).BeginInit();
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
            MD5KeyBtn.TabIndex = 1;
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
            SHA256KeyBtn.TabIndex = 2;
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
            OpenOnBtn.TabIndex = 3;
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
            OpenOnEntry.TabIndex = 4;
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
            StartBtn.Location = new Point(247, 185);
            StartBtn.Name = "StartBtn";
            StartBtn.Size = new Size(75, 23);
            StartBtn.TabIndex = 7;
            StartBtn.Text = "Iniciar";
            StartBtn.UseVisualStyleBackColor = true;
            StartBtn.Click += StartBtn_Click;
            // 
            // CancelBtn
            // 
            CancelBtn.Location = new Point(166, 185);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 8;
            CancelBtn.Text = "Cancelar";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // TextPanel
            // 
            TextPanel.BorderStyle = BorderStyle.Fixed3D;
            TextPanel.Controls.Add(ArrowPictureBox);
            TextPanel.Controls.Add(ResultTextBox);
            TextPanel.Controls.Add(InputTextBox);
            TextPanel.Dock = DockStyle.Bottom;
            TextPanel.Location = new Point(0, 220);
            TextPanel.Name = "TextPanel";
            TextPanel.Size = new Size(334, 200);
            TextPanel.TabIndex = 9;
            TextPanel.Visible = false;
            // 
            // ArrowPictureBox
            // 
            ArrowPictureBox.BackColor = Color.Transparent;
            ArrowPictureBox.Image = Properties.Resources.arrow_right;
            ArrowPictureBox.Location = new Point(153, 76);
            ArrowPictureBox.Name = "ArrowPictureBox";
            ArrowPictureBox.Size = new Size(24, 24);
            ArrowPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            ArrowPictureBox.TabIndex = 2;
            ArrowPictureBox.TabStop = false;
            // 
            // ResultTextBox
            // 
            ResultTextBox.BackColor = SystemColors.ControlLightLight;
            ResultTextBox.BorderStyle = BorderStyle.None;
            ResultTextBox.Dock = DockStyle.Right;
            ResultTextBox.Location = new Point(183, 0);
            ResultTextBox.Multiline = true;
            ResultTextBox.Name = "ResultTextBox";
            ResultTextBox.ReadOnly = true;
            ResultTextBox.ScrollBars = ScrollBars.Vertical;
            ResultTextBox.Size = new Size(147, 196);
            ResultTextBox.TabIndex = 1;
            // 
            // InputTextBox
            // 
            InputTextBox.BackColor = SystemColors.ControlLightLight;
            InputTextBox.BorderStyle = BorderStyle.None;
            InputTextBox.Dock = DockStyle.Left;
            InputTextBox.Location = new Point(0, 0);
            InputTextBox.Multiline = true;
            InputTextBox.Name = "InputTextBox";
            InputTextBox.ReadOnly = true;
            InputTextBox.ScrollBars = ScrollBars.Vertical;
            InputTextBox.Size = new Size(147, 196);
            InputTextBox.TabIndex = 0;
            // 
            // Encriptador
            // 
            AcceptButton = StartBtn;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = CancelBtn;
            ClientSize = new Size(334, 420);
            Controls.Add(TextPanel);
            Controls.Add(SaveOnEntry);
            Controls.Add(CancelBtn);
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Encriptador";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Encriptador";
            TextPanel.ResumeLayout(false);
            TextPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ArrowPictureBox).EndInit();
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
		private Button CancelBtn;
		private Panel TextPanel;
		private TextBox InputTextBox;
		private TextBox ResultTextBox;
        private PictureBox ArrowPictureBox;
    }
}
