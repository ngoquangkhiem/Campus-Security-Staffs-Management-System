namespace Campus_Security_Staffs_Management_System
{
    partial class UC_CreateRoutine
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
            this.btnSaveRoutine = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dgvRoutinePreview = new System.Windows.Forms.DataGridView();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoutinePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveRoutine
            // 
            this.btnSaveRoutine.Location = new System.Drawing.Point(366, 323);
            this.btnSaveRoutine.Name = "btnSaveRoutine";
            this.btnSaveRoutine.Size = new System.Drawing.Size(144, 36);
            this.btnSaveRoutine.TabIndex = 4;
            this.btnSaveRoutine.Text = "Save Routine";
            this.btnSaveRoutine.UseVisualStyleBackColor = true;
            this.btnSaveRoutine.Click += new System.EventHandler(this.btnSaveRoutine_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(378, 19);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(145, 34);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dgvRoutinePreview
            // 
            this.dgvRoutinePreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoutinePreview.Location = new System.Drawing.Point(12, 70);
            this.dgvRoutinePreview.Name = "dgvRoutinePreview";
            this.dgvRoutinePreview.RowHeadersWidth = 62;
            this.dgvRoutinePreview.RowTemplate.Height = 28;
            this.dgvRoutinePreview.Size = new System.Drawing.Size(853, 247);
            this.dgvRoutinePreview.TabIndex = 6;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(12, 21);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(304, 26);
            this.dtpStartDate.TabIndex = 3;
            // 
            // UC_CreateRoutine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSaveRoutine);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.dgvRoutinePreview);
            this.Controls.Add(this.dtpStartDate);
            this.Name = "UC_CreateRoutine";
            this.Size = new System.Drawing.Size(868, 372);
            this.Load += new System.EventHandler(this.UC_CreateRoutine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoutinePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaveRoutine;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGridView dgvRoutinePreview;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
    }
}
