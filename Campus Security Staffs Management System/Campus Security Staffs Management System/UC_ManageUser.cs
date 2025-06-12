using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campus_Security_Staffs_Management_System
{
    public partial class UC_ManageUser : UserControl
    {
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2
        public UC_ManageUser()
        {
            InitializeComponent();
        }
        private void LoadForm(UserControl uc)
        {
            panelUser.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelUser.Controls.Add(uc);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           LoadForm(new UC_AddUser());
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            LoadForm(new UC_Delete());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            LoadForm(new UC_Update());
        }
    }
}
