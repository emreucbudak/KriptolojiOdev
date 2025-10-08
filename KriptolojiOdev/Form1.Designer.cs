namespace KriptolojiOdev
{
    partial class Form1
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
            textBox2 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            clientLog = new RichTextBox();
            SuspendLayout();
            // 
            // textBox2
            // 
            textBox2.Location = new Point(206, 145);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(246, 27);
            textBox2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(49, 145);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 4;
            label3.Text = "Metin : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(49, 197);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 5;
            label4.Text = "Çıktı : ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(206, 197);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(246, 27);
            textBox1.TabIndex = 6;
            // 
            // button2
            // 
            button2.Location = new Point(49, 246);
            button2.Name = "button2";
            button2.Size = new Size(116, 28);
            button2.TabIndex = 8;
            button2.Text = "Sezar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(192, 246);
            button3.Name = "button3";
            button3.Size = new Size(116, 28);
            button3.TabIndex = 9;
            button3.Text = "Vigenere";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(336, 246);
            button4.Name = "button4";
            button4.Size = new Size(116, 28);
            button4.TabIndex = 10;
            button4.Text = "Substitiuion";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(49, 306);
            button5.Name = "button5";
            button5.Size = new Size(116, 28);
            button5.TabIndex = 11;
            button5.Text = "Affine";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // clientLog
            // 
            clientLog.Location = new Point(529, 13);
            clientLog.Name = "clientLog";
            clientLog.Size = new Size(524, 423);
            clientLog.TabIndex = 12;
            clientLog.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1065, 450);
            Controls.Add(clientLog);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Name = "Form1";
            Text = "Encryptor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private RichTextBox clientLog;
    }
}
