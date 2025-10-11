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
            label1 = new Label();
            textBox3 = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label5 = new Label();
            textBox4 = new TextBox();
            label2 = new Label();
            label6 = new Label();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            button1 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // textBox2
            // 
            textBox2.Location = new Point(177, 36);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(246, 27);
            textBox2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(34, 43);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 4;
            label3.Text = "Metin : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 147);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 5;
            label4.Text = "Çıktı : ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(177, 140);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(246, 27);
            textBox1.TabIndex = 6;
            // 
            // button2
            // 
            button2.Location = new Point(34, 209);
            button2.Name = "button2";
            button2.Size = new Size(116, 28);
            button2.TabIndex = 8;
            button2.Text = "Sezar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(177, 209);
            button3.Name = "button3";
            button3.Size = new Size(116, 28);
            button3.TabIndex = 9;
            button3.Text = "Vigenere";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(321, 209);
            button4.Name = "button4";
            button4.Size = new Size(116, 28);
            button4.TabIndex = 10;
            button4.Text = "Substitiuion";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(34, 269);
            button5.Name = "button5";
            button5.Size = new Size(116, 28);
            button5.TabIndex = 11;
            button5.Text = "Affine";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // clientLog
            // 
            clientLog.Location = new Point(522, 13);
            clientLog.Name = "clientLog";
            clientLog.Size = new Size(732, 584);
            clientLog.TabIndex = 12;
            clientLog.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 99);
            label1.Name = "label1";
            label1.Size = new Size(44, 20);
            label1.TabIndex = 13;
            label1.Text = "Key : ";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(177, 96);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(246, 27);
            textBox3.TabIndex = 14;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Location = new Point(12, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(471, 314);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Encrypt";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button8);
            groupBox2.Controls.Add(button7);
            groupBox2.Controls.Add(button6);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(textBox6);
            groupBox2.Controls.Add(textBox5);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textBox4);
            groupBox2.Controls.Add(label5);
            groupBox2.Location = new Point(12, 333);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(471, 291);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = "Decrypt";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 41);
            label5.Name = "label5";
            label5.Size = new Size(54, 20);
            label5.TabIndex = 0;
            label5.Text = "Metin :";
            label5.Click += label5_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(168, 38);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(255, 27);
            textBox4.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 101);
            label2.Name = "label2";
            label2.Size = new Size(40, 20);
            label2.TabIndex = 2;
            label2.Text = "Key :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(28, 153);
            label6.Name = "label6";
            label6.Size = new Size(45, 20);
            label6.TabIndex = 3;
            label6.Text = "Çıktı :";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(168, 98);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(255, 27);
            textBox5.TabIndex = 4;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(168, 146);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(255, 27);
            textBox6.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(34, 199);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 6;
            button1.Text = "Sezar";
            button1.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(177, 199);
            button6.Name = "button6";
            button6.Size = new Size(94, 29);
            button6.TabIndex = 7;
            button6.Text = "Vigenere";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(329, 199);
            button7.Name = "button7";
            button7.Size = new Size(108, 29);
            button7.TabIndex = 8;
            button7.Text = "Substitiuion";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new Point(34, 256);
            button8.Name = "button8";
            button8.Size = new Size(94, 29);
            button8.TabIndex = 9;
            button8.Text = "Affine";
            button8.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1287, 842);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(clientLog);
            Name = "Form1";
            Text = "Encryptor";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
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
        private Label label1;
        private TextBox textBox3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label5;
        private TextBox textBox6;
        private TextBox textBox5;
        private Label label6;
        private Label label2;
        private TextBox textBox4;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button1;
    }
}
