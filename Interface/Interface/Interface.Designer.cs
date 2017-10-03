namespace Interface
{
    partial class Interface
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
            this.panelLogin = new System.Windows.Forms.Panel();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelSelectSession = new System.Windows.Forms.Panel();
            this.panelWorkSession = new System.Windows.Forms.Panel();
            this.panelStudent3 = new System.Windows.Forms.Panel();
            this.labelStatus3 = new System.Windows.Forms.Label();
            this.labelStudent3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panelStudent2 = new System.Windows.Forms.Panel();
            this.labelStatus2 = new System.Windows.Forms.Label();
            this.labelStudent2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panelStudent1 = new System.Windows.Forms.Panel();
            this.labelStatus1 = new System.Windows.Forms.Label();
            this.labelStudent1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonTurnOn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxSelectSession = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.panelLogin.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelSelectSession.SuspendLayout();
            this.panelWorkSession.SuspendLayout();
            this.panelStudent3.SuspendLayout();
            this.panelStudent2.SuspendLayout();
            this.panelStudent1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.buttonLogin);
            this.panelLogin.Controls.Add(this.label2);
            this.panelLogin.Controls.Add(this.label1);
            this.panelLogin.Controls.Add(this.textBoxPassword);
            this.panelLogin.Controls.Add(this.textBoxUsername);
            this.panelLogin.Location = new System.Drawing.Point(348, 409);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(116, 116);
            this.panelLogin.TabIndex = 0;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(32, 85);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(50, 23);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBoxPassword.Location = new System.Drawing.Point(7, 59);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxPassword.TabIndex = 1;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBoxUsername.Location = new System.Drawing.Point(7, 20);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(100, 20);
            this.textBoxUsername.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(337, 308);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelSelectSession);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(329, 282);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Teacher";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelSelectSession
            // 
            this.panelSelectSession.Controls.Add(this.panelWorkSession);
            this.panelSelectSession.Controls.Add(this.label3);
            this.panelSelectSession.Controls.Add(this.listBoxSelectSession);
            this.panelSelectSession.Location = new System.Drawing.Point(0, 0);
            this.panelSelectSession.Name = "panelSelectSession";
            this.panelSelectSession.Size = new System.Drawing.Size(329, 282);
            this.panelSelectSession.TabIndex = 0;
            // 
            // panelWorkSession
            // 
            this.panelWorkSession.Controls.Add(this.panelStudent3);
            this.panelWorkSession.Controls.Add(this.panelStudent2);
            this.panelWorkSession.Controls.Add(this.panelStudent1);
            this.panelWorkSession.Controls.Add(this.buttonTurnOn);
            this.panelWorkSession.Enabled = false;
            this.panelWorkSession.Location = new System.Drawing.Point(0, 0);
            this.panelWorkSession.Name = "panelWorkSession";
            this.panelWorkSession.Size = new System.Drawing.Size(329, 201);
            this.panelWorkSession.TabIndex = 1;
            // 
            // panelStudent3
            // 
            this.panelStudent3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStudent3.Controls.Add(this.labelStatus3);
            this.panelStudent3.Controls.Add(this.labelStudent3);
            this.panelStudent3.Controls.Add(this.label10);
            this.panelStudent3.Location = new System.Drawing.Point(221, 35);
            this.panelStudent3.Name = "panelStudent3";
            this.panelStudent3.Size = new System.Drawing.Size(103, 64);
            this.panelStudent3.TabIndex = 3;
            // 
            // labelStatus3
            // 
            this.labelStatus3.AutoSize = true;
            this.labelStatus3.Location = new System.Drawing.Point(4, 38);
            this.labelStatus3.Name = "labelStatus3";
            this.labelStatus3.Size = new System.Drawing.Size(0, 13);
            this.labelStatus3.TabIndex = 2;
            // 
            // labelStudent3
            // 
            this.labelStudent3.AutoSize = true;
            this.labelStudent3.Location = new System.Drawing.Point(7, 21);
            this.labelStudent3.Name = "labelStudent3";
            this.labelStudent3.Size = new System.Drawing.Size(0, 13);
            this.labelStudent3.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Student PC";
            // 
            // panelStudent2
            // 
            this.panelStudent2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStudent2.Controls.Add(this.labelStatus2);
            this.panelStudent2.Controls.Add(this.labelStudent2);
            this.panelStudent2.Controls.Add(this.label7);
            this.panelStudent2.Location = new System.Drawing.Point(112, 35);
            this.panelStudent2.Name = "panelStudent2";
            this.panelStudent2.Size = new System.Drawing.Size(103, 64);
            this.panelStudent2.TabIndex = 2;
            // 
            // labelStatus2
            // 
            this.labelStatus2.AutoSize = true;
            this.labelStatus2.Location = new System.Drawing.Point(4, 38);
            this.labelStatus2.Name = "labelStatus2";
            this.labelStatus2.Size = new System.Drawing.Size(0, 13);
            this.labelStatus2.TabIndex = 2;
            // 
            // labelStudent2
            // 
            this.labelStudent2.AutoSize = true;
            this.labelStudent2.Location = new System.Drawing.Point(7, 21);
            this.labelStudent2.Name = "labelStudent2";
            this.labelStudent2.Size = new System.Drawing.Size(0, 13);
            this.labelStudent2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Student PC";
            // 
            // panelStudent1
            // 
            this.panelStudent1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStudent1.Controls.Add(this.labelStatus1);
            this.panelStudent1.Controls.Add(this.labelStudent1);
            this.panelStudent1.Controls.Add(this.label4);
            this.panelStudent1.Location = new System.Drawing.Point(3, 35);
            this.panelStudent1.Name = "panelStudent1";
            this.panelStudent1.Size = new System.Drawing.Size(103, 64);
            this.panelStudent1.TabIndex = 1;
            // 
            // labelStatus1
            // 
            this.labelStatus1.AutoSize = true;
            this.labelStatus1.Location = new System.Drawing.Point(4, 38);
            this.labelStatus1.Name = "labelStatus1";
            this.labelStatus1.Size = new System.Drawing.Size(0, 13);
            this.labelStatus1.TabIndex = 2;
            // 
            // labelStudent1
            // 
            this.labelStudent1.AutoSize = true;
            this.labelStudent1.Location = new System.Drawing.Point(7, 21);
            this.labelStudent1.Name = "labelStudent1";
            this.labelStudent1.Size = new System.Drawing.Size(0, 13);
            this.labelStudent1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Student PC";
            // 
            // buttonTurnOn
            // 
            this.buttonTurnOn.Location = new System.Drawing.Point(3, 6);
            this.buttonTurnOn.Name = "buttonTurnOn";
            this.buttonTurnOn.Size = new System.Drawing.Size(79, 23);
            this.buttonTurnOn.TabIndex = 0;
            this.buttonTurnOn.Text = "Start Session";
            this.buttonTurnOn.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Select Session:";
            // 
            // listBoxSelectSession
            // 
            this.listBoxSelectSession.FormattingEnabled = true;
            this.listBoxSelectSession.Location = new System.Drawing.Point(3, 23);
            this.listBoxSelectSession.Name = "listBoxSelectSession";
            this.listBoxSelectSession.Size = new System.Drawing.Size(321, 251);
            this.listBoxSelectSession.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(329, 282);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Admin";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create new Session";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelLogin);
            this.MaximizeBox = false;
            this.Name = "Interface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelSelectSession.ResumeLayout(false);
            this.panelSelectSession.PerformLayout();
            this.panelWorkSession.ResumeLayout(false);
            this.panelStudent3.ResumeLayout(false);
            this.panelStudent3.PerformLayout();
            this.panelStudent2.ResumeLayout(false);
            this.panelStudent2.PerformLayout();
            this.panelStudent1.ResumeLayout(false);
            this.panelStudent1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panelSelectSession;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxSelectSession;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panelWorkSession;
        private System.Windows.Forms.Button buttonTurnOn;
        private System.Windows.Forms.Panel panelStudent3;
        private System.Windows.Forms.Label labelStatus3;
        private System.Windows.Forms.Label labelStudent3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panelStudent2;
        private System.Windows.Forms.Label labelStatus2;
        private System.Windows.Forms.Label labelStudent2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelStudent1;
        private System.Windows.Forms.Label labelStatus1;
        private System.Windows.Forms.Label labelStudent1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}