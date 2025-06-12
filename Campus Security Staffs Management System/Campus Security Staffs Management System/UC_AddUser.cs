using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Campus_Security_Staffs_Management_System
{
    public partial class UC_AddUser : UserControl
    {
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2
        private bool isResetting = false;

        public UC_AddUser()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.UserName.Leave += new EventHandler(this.UserName_Leave);
                this.CP.TextChanged += new EventHandler(this.CP_TextChanged);
            }

            Role.Items.Add("Manager");
            Role.Items.Add("Security");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (isResetting) return;

            string username = UserName.Text.Trim();
            string password = Password.Text;
            string confirmPassword = CP.Text;
            string role = Role.SelectedItem != null ? Role.SelectedItem.ToString() : "";
            string email = txtEmail.Text.Trim(); // ✅ Lấy giá trị email

            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(role) ||
                string.IsNullOrWhiteSpace(email)) // ✅ Kiểm tra email
            {
                MessageBox.Show("Please enter complete information!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (UA.ForeColor == Color.Red)
            {
                MessageBox.Show("Username already exists. Please choose another one!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MB.ForeColor == Color.Red)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
                {
                    conn.Open();

                    string query = "INSERT INTO users (username, password, role, email) VALUES (@u, @p, @r, @e)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password); // Gợi ý: mã hóa sau
                    cmd.Parameters.AddWithValue("@r", role);
                    cmd.Parameters.AddWithValue("@e", email); // ✅ Thêm tham số email
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset form
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            isResetting = true;
            UserName.Text = "";
            Password.Text = "";
            CP.Text = "";
            txtEmail.Text = "";
            Role.SelectedIndex = -1;
            UA.Text = "";
            MB.Text = "";
            isResetting = false;
        }

        private void UserName_Leave(object sender, EventArgs e)
        {
            if (isResetting) return;

            string username = UserName.Text.Trim();
            //string connStr = "server=localhost;user=root;password=141102;database=staff_system;";

            if (string.IsNullOrEmpty(username))
            {
                UA.Text = "";
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @u";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@u", username);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        UA.Text = "Username already exists";
                        UA.ForeColor = Color.Red;
                    }
                    else
                    {
                        UA.Text = "Username available";
                        UA.ForeColor = Color.Green;
                    }
                }
            }
            catch (Exception)
            {
                UA.Text = "Error checking username";
                UA.ForeColor = Color.Red;
            }
        }

        private void CP_TextChanged(object sender, EventArgs e)
        {
            if (isResetting) return;

            if (Password.Text != CP.Text)
            {
                MB.Text = "Passwords do not match";
                MB.ForeColor = Color.Red;
            }
            else
            {
                MB.Text = "Passwords match";
                MB.ForeColor = Color.Green;
            }
        }
    }
}