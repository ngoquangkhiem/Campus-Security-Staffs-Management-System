using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Campus_Security_Staffs_Management_System;
using CampusSecuritySystem;
using MySql.Data.MySqlClient;
using SecurityManagementSystem;

namespace CampusSecurityApp
{
    public partial class RegisterForm : Form
    {
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2
        public RegisterForm()
        {
            InitializeComponent();
        }

    

        private void txtConfirm_TextChanged(object sender, EventArgs e)
        {
            lbMatch.Visible = true;

            if (txtPassword.Text == txtConfirm.Text)
            {
                lbMatch.Text = "Passwords match";
                lbMatch.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbMatch.Text = "Passwords do not match";
                lbMatch.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirm = txtConfirm.Text.Trim();
            string email = txtEmail.Text.Trim(); // Thêm dòng này

            if (username == "" || password == "" || confirm == "" || email == "")
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
                {
                    conn.Open();

                    // Kiểm tra username đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userCount > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose another.");
                        return;
                    }

                    // Chèn người dùng mới, bao gồm cả email
                    string insertQuery = "INSERT INTO users (username, password, role, email) VALUES (@username, @password, 'security', @email)";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@username", username);
                    insertCmd.Parameters.AddWithValue("@password", password); // Gợi ý: hash sau
                    insertCmd.Parameters.AddWithValue("@email", email); // Thêm dòng này
                    insertCmd.ExecuteNonQuery();

                    // Lấy userId vừa tạo
                    string getIdQuery = "SELECT id FROM users WHERE username = @username";
                    MySqlCommand getIdCmd = new MySqlCommand(getIdQuery, conn);
                    getIdCmd.Parameters.AddWithValue("@username", username);
                    int newUserId = Convert.ToInt32(getIdCmd.ExecuteScalar());

                    MessageBox.Show("Registration successful!");
                    SecurityDashboard secForm = new SecurityDashboard(newUserId); // Truyền userId
                    secForm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide(); // Ẩn form register
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
