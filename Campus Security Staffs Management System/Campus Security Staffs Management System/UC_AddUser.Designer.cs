using System;

namespace Campus_Security_Staffs_Management_System
{
    partial class UC_AddUser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MB = new System.Windows.Forms.Label();
            this.CPlabel = new System.Windows.Forms.Label();
            this.UA = new System.Windows.Forms.Label();
            this.Role = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CP = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MB
            // 
            this.MB.AutoSize = true;
            this.MB.Location = new System.Drawing.Point(338, 224);
            this.MB.Name = "MB";
            this.MB.Size = new System.Drawing.Size(0, 20);
            this.MB.TabIndex = 24;
            // 
            // CPlabel
            // 
            this.CPlabel.AutoSize = true;
            this.CPlabel.Location = new System.Drawing.Point(97, 190);
            this.CPlabel.Name = "CPlabel";
            this.CPlabel.Size = new System.Drawing.Size(141, 20);
            this.CPlabel.TabIndex = 23;
            this.CPlabel.Text = "Comfirm Password";
            // 
            // UA
            // 
            this.UA.AutoSize = true;
            this.UA.Location = new System.Drawing.Point(530, 51);
            this.UA.Name = "UA";
            this.UA.Size = new System.Drawing.Size(0, 20);
            this.UA.TabIndex = 22;
            // 
            // Role
            // 
            this.Role.FormattingEnabled = true;
            this.Role.Location = new System.Drawing.Point(250, 259);
            this.Role.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Role.Name = "Role";
            this.Role.Size = new System.Drawing.Size(136, 28);
            this.Role.TabIndex = 21;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(450, 294);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(176, 43);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Role";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "User name";
            // 
            // CP
            // 
            this.CP.Location = new System.Drawing.Point(248, 184);
            this.CP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CP.Name = "CP";
            this.CP.PasswordChar = '*';
            this.CP.Size = new System.Drawing.Size(257, 26);
            this.CP.TabIndex = 14;
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(250, 140);
            this.Password.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(257, 26);
            this.Password.TabIndex = 15;
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(250, 47);
            this.UserName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(257, 26);
            this.UserName.TabIndex = 16;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(250, 92);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(196, 26);
            this.txtEmail.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 25;
            this.label5.Text = "Email";
            // 
            // UC_AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MB);
            this.Controls.Add(this.CPlabel);
            this.Controls.Add(this.UA);
            this.Controls.Add(this.Role);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CP);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.UserName);
            this.Name = "UC_AddUser";
            this.Size = new System.Drawing.Size(696, 370);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void UC_AddUser_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Label MB;
        private System.Windows.Forms.Label CPlabel;
        private System.Windows.Forms.Label UA;
        private System.Windows.Forms.ComboBox Role;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CP;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label5;
    }
}
