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
            connection = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            clientLog = new RichTextBox();
            startButton = new Button();
            serverLog = new RichTextBox();
            SuspendLayout();
            // 
            // textBox2
            // 
            textBox2.Location = new Point(200, 83);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(246, 27);
            textBox2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(43, 83);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 4;
            label3.Text = "Metin : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(43, 135);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 5;
            label4.Text = "Çıktı : ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(200, 135);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(246, 27);
            textBox1.TabIndex = 6;
            // 
            // connection
            // 
            connection.Location = new Point(200, 184);
            connection.Name = "connection";
            connection.Size = new Size(116, 28);
            connection.TabIndex = 7;
            connection.Text = "Bağlantı";
            connection.UseVisualStyleBackColor = true;
            connection.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(362, 184);
            button2.Name = "button2";
            button2.Size = new Size(116, 28);
            button2.TabIndex = 8;
            button2.Text = "Sezar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(362, 245);
            button3.Name = "button3";
            button3.Size = new Size(116, 28);
            button3.TabIndex = 9;
            button3.Text = "Vigenere";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(43, 245);
            button4.Name = "button4";
            button4.Size = new Size(116, 28);
            button4.TabIndex = 10;
            button4.Text = "Substitiuion";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(200, 245);
            button5.Name = "button5";
            button5.Size = new Size(116, 28);
            button5.TabIndex = 11;
            button5.Text = "Affine";
            button5.UseVisualStyleBackColor = true;
            // 
            // clientLog
            // 
            clientLog.Location = new Point(529, 13);
            clientLog.Name = "clientLog";
            clientLog.Size = new Size(420, 423);
            clientLog.TabIndex = 12;
            clientLog.Text = "";
            // 
            // startButton
            // 
            startButton.Location = new Point(43, 184);
            startButton.Name = "startButton";
            startButton.Size = new Size(116, 28);
            startButton.TabIndex = 13;
            startButton.Text = "Başlat";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // serverLog
            // 
            serverLog.Location = new Point(955, 12);
            serverLog.Name = "serverLog";
            serverLog.Size = new Size(420, 423);
            serverLog.TabIndex = 16;
            serverLog.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1385, 450);
            Controls.Add(serverLog);
            Controls.Add(startButton);
            Controls.Add(clientLog);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(connection);
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
        private Button connection;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private RichTextBox clientLog;
        private Button startButton;
        private RichTextBox serverLog;
    }
}
