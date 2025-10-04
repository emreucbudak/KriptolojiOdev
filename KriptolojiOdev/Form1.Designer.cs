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
            label1 = new Label();
            connectionState = new Label();
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
            label5 = new Label();
            openServer = new Label();
            serverLog = new RichTextBox();
            SuspendLayout();
            // 
            // textBox2
            // 
            textBox2.Location = new Point(169, 187);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(246, 27);
            textBox2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 110);
            label1.Name = "label1";
            label1.Size = new Size(169, 20);
            label1.TabIndex = 2;
            label1.Text = "Server Bağlantı Durumu:";
            // 
            // connectionState
            // 
            connectionState.AutoSize = true;
            connectionState.Location = new Point(251, 110);
            connectionState.Name = "connectionState";
            connectionState.Size = new Size(59, 20);
            connectionState.TabIndex = 3;
            connectionState.Text = "DeAktif";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 187);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 4;
            label3.Text = "Metin : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 239);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 5;
            label4.Text = "Çıktı : ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(169, 239);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(246, 27);
            textBox1.TabIndex = 6;
            // 
            // connection
            // 
            connection.Location = new Point(169, 288);
            connection.Name = "connection";
            connection.Size = new Size(116, 28);
            connection.TabIndex = 7;
            connection.Text = "Bağlantı";
            connection.UseVisualStyleBackColor = true;
            connection.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(317, 349);
            button2.Name = "button2";
            button2.Size = new Size(116, 28);
            button2.TabIndex = 8;
            button2.Text = "Sezar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(317, 288);
            button3.Name = "button3";
            button3.Size = new Size(116, 28);
            button3.TabIndex = 9;
            button3.Text = "Vigenere";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(12, 349);
            button4.Name = "button4";
            button4.Size = new Size(116, 28);
            button4.TabIndex = 10;
            button4.Text = "Substitiuion";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(169, 349);
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
            startButton.Location = new Point(12, 288);
            startButton.Name = "startButton";
            startButton.Size = new Size(116, 28);
            startButton.TabIndex = 13;
            startButton.Text = "Başlat";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 151);
            label5.Name = "label5";
            label5.Size = new Size(107, 20);
            label5.TabIndex = 14;
            label5.Text = "Server Durumu";
            // 
            // openServer
            // 
            openServer.AutoSize = true;
            openServer.Location = new Point(251, 151);
            openServer.Name = "openServer";
            openServer.Size = new Size(51, 20);
            openServer.TabIndex = 15;
            openServer.Text = "Kapalı";
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
            Controls.Add(openServer);
            Controls.Add(label5);
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
            Controls.Add(connectionState);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Name = "Form1";
            Text = "Encryptor";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox2;
        private Label label1;
        private Label connectionState;
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
        private Label label5;
        private Label openServer;
        private RichTextBox serverLog;
    }
}
