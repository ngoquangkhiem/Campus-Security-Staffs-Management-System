namespace Campus_Security_Staffs_Management_System
{
    partial class UC_Delete
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
            this.btnDel = new System.Windows.Forms.Button();
            this.userCheck = new System.Windows.Forms.Label();
            this.UD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(251, 178);
            this.btnDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(149, 41);
            this.btnDel.TabIndex = 7;
            this.btnDel.Text = "Delete";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // userCheck
            // 
            this.userCheck.AutoSize = true;
            this.userCheck.Location = new System.Drawing.Point(489, 91);
            this.userCheck.Name = "userCheck";
            this.userCheck.Size = new System.Drawing.Size(0, 20);
            this.userCheck.TabIndex = 6;
            // 
            // UD
            // 
            this.UD.Location = new System.Drawing.Point(251, 84);
            this.UD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UD.Name = "UD";
            this.UD.Size = new System.Drawing.Size(198, 26);
            this.UD.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter Username";
            // 
            // UC_Delete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.userCheck);
            this.Controls.Add(this.UD);
            this.Controls.Add(this.label1);
            this.Name = "UC_Delete";
            this.Size = new System.Drawing.Size(640, 286);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label userCheck;
        private System.Windows.Forms.TextBox UD;
        private System.Windows.Forms.Label label1;
    }
}
