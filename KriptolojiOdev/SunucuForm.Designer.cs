namespace KriptolojiOdev
{
    partial class SunucuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            serverLog = new RichTextBox();
            button1 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // serverLog
            // 
            serverLog.Location = new Point(367, 12);
            serverLog.Name = "serverLog";
            serverLog.Size = new Size(775, 426);
            serverLog.TabIndex = 0;
            serverLog.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(12, 47);
            button1.Name = "button1";
            button1.Size = new Size(349, 29);
            button1.TabIndex = 1;
            button1.Text = "Sunucuyu Başlat";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(49, 9);
            label1.Name = "label1";
            label1.Size = new Size(280, 35);
            label1.TabIndex = 2;
            label1.Text = "Kriptoloji Server Sunucu";
            // 
            // SunucuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1154, 450);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(serverLog);
            Name = "SunucuForm";
            Text = "Sunucu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox serverLog;
        private Button button1;
        private Label label1;
    }
}