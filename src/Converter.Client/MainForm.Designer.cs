namespace Converter.Client
{
    partial class MainForm
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
            panel1 = new Panel();
            panel2 = new Panel();
            lblAppName = new Label();
            panel3 = new Panel();
            labelDisplay = new Label();
            btnReset = new Button();
            comboBoxLanguage = new ComboBox();
            btnConvert = new Button();
            labelLanguage = new Label();
            textBoxAmount = new TextBox();
            labelAmount = new Label();
            panel4 = new Panel();
            btnExit = new Button();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(874, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(79, 8);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaption;
            panel2.Controls.Add(lblAppName);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(1142, 97);
            panel2.TabIndex = 1;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblAppName.Location = new Point(342, 20);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(511, 65);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "Dollar Text Converter";
            lblAppName.TextAlign = ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ActiveCaption;
            panel3.Controls.Add(labelDisplay);
            panel3.Controls.Add(btnReset);
            panel3.Controls.Add(comboBoxLanguage);
            panel3.Controls.Add(btnConvert);
            panel3.Controls.Add(labelLanguage);
            panel3.Controls.Add(textBoxAmount);
            panel3.Controls.Add(labelAmount);
            panel3.Location = new Point(12, 115);
            panel3.Name = "panel3";
            panel3.Size = new Size(1142, 483);
            panel3.TabIndex = 2;
            // 
            // labelDisplay
            // 
            labelDisplay.AutoSize = true;
            labelDisplay.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelDisplay.Location = new Point(72, 261);
            labelDisplay.Name = "labelDisplay";
            labelDisplay.Size = new Size(0, 30);
            labelDisplay.TabIndex = 5;
            // 
            // btnReset
            // 
            btnReset.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnReset.Location = new Point(598, 192);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(283, 48);
            btnReset.TabIndex = 1;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBoxLanguage.FormattingEnabled = true;
            comboBoxLanguage.Location = new Point(484, 125);
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.Size = new Size(395, 38);
            comboBoxLanguage.TabIndex = 4;
            // 
            // btnConvert
            // 
            btnConvert.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnConvert.Location = new Point(279, 192);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(283, 48);
            btnConvert.TabIndex = 0;
            btnConvert.Text = "Convert";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // labelLanguage
            // 
            labelLanguage.AutoSize = true;
            labelLanguage.BorderStyle = BorderStyle.Fixed3D;
            labelLanguage.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelLanguage.Location = new Point(361, 131);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new Size(117, 32);
            labelLanguage.TabIndex = 3;
            labelLanguage.Text = "Language:";
            // 
            // textBoxAmount
            // 
            textBoxAmount.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxAmount.Location = new Point(484, 44);
            textBoxAmount.Name = "textBoxAmount";
            textBoxAmount.PlaceholderText = "0";
            textBoxAmount.Size = new Size(397, 35);
            textBoxAmount.TabIndex = 2;
            textBoxAmount.TextChanged += textBoxAmount_TextChanged;
            // 
            // labelAmount
            // 
            labelAmount.AutoSize = true;
            labelAmount.BorderStyle = BorderStyle.Fixed3D;
            labelAmount.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelAmount.Location = new Point(342, 44);
            labelAmount.Name = "labelAmount";
            labelAmount.Size = new Size(136, 32);
            labelAmount.TabIndex = 1;
            labelAmount.Text = "Amount ($):";
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.ActiveCaption;
            panel4.Controls.Add(btnExit);
            panel4.Location = new Point(12, 604);
            panel4.Name = "panel4";
            panel4.Size = new Size(1142, 97);
            panel4.TabIndex = 3;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnExit.Location = new Point(425, 27);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(283, 48);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            ControlBox = false;
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label lblAppName;
        private Panel panel4;
        private TextBox textBoxAmount;
        private Label labelAmount;
        private ComboBox comboBoxLanguage;
        private Label labelLanguage;
        private Button btnExit;
        private Button btnReset;
        private Button btnConvert;
        private Label labelDisplay;
    }
}
