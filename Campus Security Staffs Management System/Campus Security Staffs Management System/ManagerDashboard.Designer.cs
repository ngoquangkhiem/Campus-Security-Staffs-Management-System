namespace Campus_Security_Staffs_Management_System
{
    partial class ManagerDashboard
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
            this.btnViewRequests = new System.Windows.Forms.Button();
            this.btnCreateRoutine = new System.Windows.Forms.Button();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.btnSalary = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnViewRequests
            // 
            this.btnViewRequests.Location = new System.Drawing.Point(39, 86);
            this.btnViewRequests.Name = "btnViewRequests";
            this.btnViewRequests.Size = new System.Drawing.Size(144, 52);
            this.btnViewRequests.TabIndex = 0;
            this.btnViewRequests.Text = "View Requests";
            this.btnViewRequests.UseVisualStyleBackColor = true;
            this.btnViewRequests.Click += new System.EventHandler(this.btnViewRequests_Click);
            // 
            // btnCreateRoutine
            // 
            this.btnCreateRoutine.Location = new System.Drawing.Point(220, 86);
            this.btnCreateRoutine.Name = "btnCreateRoutine";
            this.btnCreateRoutine.Size = new System.Drawing.Size(144, 52);
            this.btnCreateRoutine.TabIndex = 0;
            this.btnCreateRoutine.Text = "Create Routine";
            this.btnCreateRoutine.UseVisualStyleBackColor = true;
            this.btnCreateRoutine.Click += new System.EventHandler(this.btnCreateRoutine_Click);
            // 
            // btnMonitor
            // 
            this.btnMonitor.Location = new System.Drawing.Point(411, 86);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(144, 52);
            this.btnMonitor.TabIndex = 0;
            this.btnMonitor.Text = "Monitor";
            this.btnMonitor.UseVisualStyleBackColor = true;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // btnSalary
            // 
            this.btnSalary.Location = new System.Drawing.Point(600, 86);
            this.btnSalary.Name = "btnSalary";
            this.btnSalary.Size = new System.Drawing.Size(144, 52);
            this.btnSalary.TabIndex = 0;
            this.btnSalary.Text = "Salary";
            this.btnSalary.UseVisualStyleBackColor = true;
            this.btnSalary.Click += new System.EventHandler(this.btnSalary_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(817, 23);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(108, 37);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(39, 155);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(886, 384);
            this.panelMain.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Manager Dashboard";
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(781, 86);
            this.btnAddUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(144, 52);
            this.btnAddUser.TabIndex = 3;
            this.btnAddUser.Text = "Manage User";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.AddUser_Click);
            // 
            // ManagerDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 567);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnSalary);
            this.Controls.Add(this.btnMonitor);
            this.Controls.Add(this.btnCreateRoutine);
            this.Controls.Add(this.btnViewRequests);
            this.Name = "ManagerDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManagerDashboard";
            this.Load += new System.EventHandler(this.ManagerDashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnViewRequests;
        private System.Windows.Forms.Button btnCreateRoutine;
        private System.Windows.Forms.Button btnMonitor;
        private System.Windows.Forms.Button btnSalary;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddUser;
    }
}