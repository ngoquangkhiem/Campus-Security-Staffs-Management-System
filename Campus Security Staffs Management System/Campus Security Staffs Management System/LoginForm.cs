using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Campus_Security_Staffs_Management_System;
using CampusSecurityApp;
using MySql.Data.MySqlClient; //1
using SecurityManagementSystem; 

namespace CampusSecuritySystem
{
    public partial class Form1 : Form
    {
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2
        public Form1()
        {
            InitializeComponent();
            this.Shown += new EventHandler(Form1_Shown);
           
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            //string connStr = "server=localhost;user=root;password=141102;database=staff_system;";
            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString)) //3
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, role FROM users WHERE username=@username AND password=@password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idIndex = reader.GetOrdinal("id");
                            int roleIndex = reader.GetOrdinal("role");

                            int userId = reader.GetInt32(idIndex);
                            string roleStr = reader.GetString(roleIndex);

                            if (roleStr == "manager")
                            {
                                ManagerDashboard mgrForm = new ManagerDashboard();
                                mgrForm.Show();
                                this.Hide();
                            }
                            else if (roleStr == "security")
                            {
                                SecurityDashboard secForm = new SecurityDashboard(userId); // truyền userId ở đây
                                secForm.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            lbMessage.Visible = true;
                            lbMessage.Text = "Wrong username or password!";
                            lbMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Connection error: " + ex.Message);
                }

            }
        }

        private void btnGoToRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide(); // Ẩn form login
        }

        public void ClearFields()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
