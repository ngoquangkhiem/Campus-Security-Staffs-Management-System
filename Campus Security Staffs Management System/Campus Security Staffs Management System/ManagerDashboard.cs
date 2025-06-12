using StaffSecuritySystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //1

namespace Campus_Security_Staffs_Management_System
{
    public partial class ManagerDashboard : Form
    {
        public ManagerDashboard()
        {
            InitializeComponent();
        }

        private void LoadForm(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        private void btnViewRequests_Click(object sender, EventArgs e)
        {
            LoadForm(new UC_ViewRequests());
        }

        private void btnCreateRoutine_Click(object sender, EventArgs e)
        {
            LoadForm(new UC_CreateRoutine());
        }

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            LoadForm(new UC_Monitor());
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            LoadForm(new UC_Salary());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn dashboard

            // Tạo lại LoginForm mới (đảm bảo form trống)
            CampusSecuritySystem.Form1 loginForm = new CampusSecuritySystem.Form1();
            loginForm.Show();

            this.Close(); // Đóng dashboard sau khi mở login
        }


        private void ManagerDashboard_Load(object sender, EventArgs e)
        {

        }

        private void AddUser_Click(object sender, EventArgs e)
        {
            LoadForm(new UC_ManageUser());
        }

       
    }
}
