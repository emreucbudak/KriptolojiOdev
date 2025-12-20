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
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            clientLog = new RichTextBox();
            label1 = new Label();
            textBox3 = new TextBox();
            groupBox1 = new GroupBox();
            textBox4 = new TextBox();
            textBox1 = new TextBox();
            label4 = new Label();
            label2 = new Label();
            button27 = new Button();
            textBox7 = new TextBox();
            label7 = new Label();
            button22 = new Button();
            button21 = new Button();
            button19 = new Button();
            button13 = new Button();
            button12 = new Button();
            button11 = new Button();
            button10 = new Button();
            button9 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox2
            // 
            textBox2.Location = new Point(169, 95);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(246, 27);
            textBox2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 102);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 4;
            label3.Text = "Metin : ";
            // 
            // button2
            // 
            button2.Location = new Point(19, 339);
            button2.Name = "button2";
            button2.Size = new Size(58, 28);
            button2.TabIndex = 8;
            button2.Text = "Sezar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(83, 339);
            button3.Name = "button3";
            button3.Size = new Size(80, 28);
            button3.TabIndex = 9;
            button3.Text = "Vigenere";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(169, 339);
            button4.Name = "button4";
            button4.Size = new Size(102, 28);
            button4.TabIndex = 10;
            button4.Text = "Substitiuion";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(277, 339);
            button5.Name = "button5";
            button5.Size = new Size(58, 28);
            button5.TabIndex = 11;
            button5.Text = "Affine";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // clientLog
            // 
            clientLog.Location = new Point(522, 13);
            clientLog.Name = "clientLog";
            clientLog.Size = new Size(762, 664);
            clientLog.TabIndex = 12;
            clientLog.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 240);
            label1.Name = "label1";
            label1.Size = new Size(144, 20);
            label1.TabIndex = 13;
            label1.Text = "Şifrelemeler için Key";
            label1.Click += label1_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(169, 237);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(246, 27);
            textBox3.TabIndex = 14;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(button27);
            groupBox1.Controls.Add(textBox7);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(button22);
            groupBox1.Controls.Add(button21);
            groupBox1.Controls.Add(button19);
            groupBox1.Controls.Add(button13);
            groupBox1.Controls.Add(button12);
            groupBox1.Controls.Add(button11);
            groupBox1.Controls.Add(button10);
            groupBox1.Controls.Add(button9);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(471, 665);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Encrypt";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(169, 190);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(246, 27);
            textBox4.TabIndex = 30;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(169, 148);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(246, 27);
            textBox1.TabIndex = 29;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 197);
            label4.Name = "label4";
            label4.Size = new Size(82, 20);
            label4.TabIndex = 28;
            label4.Text = "Private Key";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 148);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 27;
            label2.Text = "Public Key";
            // 
            // button27
            // 
            button27.Location = new Point(202, 408);
            button27.Name = "button27";
            button27.Size = new Size(143, 29);
            button27.TabIndex = 26;
            button27.Text = "Manuel DES";
            button27.UseVisualStyleBackColor = true;
            button27.Click += button27_Click;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(169, 286);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(246, 27);
            textBox7.TabIndex = 24;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 289);
            label7.Name = "label7";
            label7.Size = new Size(95, 20);
            label7.TabIndex = 23;
            label7.Text = "Opsiyonel IV:";
            // 
            // button22
            // 
            button22.Location = new Point(346, 408);
            button22.Name = "button22";
            button22.Size = new Size(69, 29);
            button22.TabIndex = 22;
            button22.Text = "DES";
            button22.UseVisualStyleBackColor = true;
            button22.Click += button22_Click;
            // 
            // button21
            // 
            button21.Location = new Point(131, 408);
            button21.Name = "button21";
            button21.Size = new Size(65, 29);
            button21.TabIndex = 21;
            button21.Text = "AES";
            button21.UseVisualStyleBackColor = true;
            button21.Click += button21_Click;
            // 
            // button19
            // 
            button19.Location = new Point(19, 408);
            button19.Name = "button19";
            button19.Size = new Size(94, 29);
            button19.TabIndex = 20;
            button19.Text = "Tren Rayı";
            button19.UseVisualStyleBackColor = true;
            button19.Click += button19_Click;
            // 
            // button13
            // 
            button13.Location = new Point(241, 408);
            button13.Name = "button13";
            button13.Size = new Size(50, 29);
            button13.TabIndex = 19;
            button13.Text = "Hill";
            button13.UseVisualStyleBackColor = true;
            button13.Click += button13_Click;
            // 
            // button12
            // 
            button12.Location = new Point(241, 373);
            button12.Name = "button12";
            button12.Size = new Size(94, 29);
            button12.TabIndex = 18;
            button12.Text = "Pigpen";
            button12.UseVisualStyleBackColor = true;
            button12.Click += button12_Click;
            // 
            // button11
            // 
            button11.Location = new Point(131, 373);
            button11.Name = "button11";
            button11.Size = new Size(94, 29);
            button11.TabIndex = 17;
            button11.Text = "Polybius";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button10
            // 
            button10.Location = new Point(19, 373);
            button10.Name = "button10";
            button10.Size = new Size(94, 29);
            button10.TabIndex = 16;
            button10.Text = "Columnar";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // button9
            // 
            button9.Location = new Point(346, 342);
            button9.Name = "button9";
            button9.Size = new Size(62, 25);
            button9.TabIndex = 15;
            button9.Text = "ROTA";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1287, 682);
            Controls.Add(groupBox1);
            Controls.Add(clientLog);
            Name = "Form1";
            Text = "Encryptor";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBox2;
        private Label label3;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private RichTextBox clientLog;
        private Label label1;
        private TextBox textBox3;
        private GroupBox groupBox1;
        private Button button13;
        private Button button12;
        private Button button11;
        private Button button10;
        private Button button9;
        private Button button19;
        private Button button22;
        private Button button21;
        private TextBox textBox7;
        private Label label7;
        private Button button27;
        private TextBox textBox4;
        private TextBox textBox1;
        private Label label4;
        private Label label2;
    }
}
