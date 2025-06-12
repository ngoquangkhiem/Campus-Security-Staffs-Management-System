using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Campus_Security_Staffs_Management_System
{
    public partial class UC_Update : UserControl
    {
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2
        //private string connStr = "server=localhost;user=root;password=141102;database=staff_system;";
        private string originalUsername = null;

        public UC_Update()
        {
            InitializeComponent();

            cbrole.Items.AddRange(new string[] { "Manager", "Security" });

            lblstatus.Text = "*";
            lblstatus.ForeColor = Color.Black;
            lblstatus.Font = new Font("Segoe UI", 9);

            // Gỡ sự kiện trước khi gán để tránh bị gắn trùng
            search.TextChanged -= search_TextChanged;
            search.TextChanged += search_TextChanged;

            btnUpdate.Click -= btnUpdate_Click;
            btnUpdate.Click += btnUpdate_Click;
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            string inputUsername = search.Text.Trim();

            if (string.IsNullOrEmpty(inputUsername))
            {
                lblstatus.Text = "*";
                lblstatus.ForeColor = Color.Black;
                lblstatus.Font = new Font("Segoe UI", 9);
                ClearFields();
                originalUsername = null;
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM users WHERE BINARY username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", inputUsername);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            originalUsername = reader["username"].ToString();
                            username.Text = reader["username"].ToString();
                            password.Text = reader["password"].ToString();
                            email.Text = reader["email"].ToString();

                            // Gán role khớp với ComboBox
                            string roleValue = reader["role"].ToString();
                            cbrole.SelectedIndex = cbrole.FindStringExact(roleValue);

                            lblstatus.Text = "✅ User found. You can now update details.";
                            lblstatus.ForeColor = Color.Green;
                            lblstatus.Font = new Font(lblstatus.Font, FontStyle.Bold);
                        }
                        else
                        {
                            lblstatus.Text = "❌ User not found.";
                            lblstatus.ForeColor = Color.Red;
                            lblstatus.Font = new Font(lblstatus.Font, FontStyle.Bold);
                            ClearFields();
                            originalUsername = null;
                        }
                        lblstatus.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblstatus.Text = "⚠️ Database connection error.";
                    lblstatus.ForeColor = Color.Orange;
                    lblstatus.Font = new Font(lblstatus.Font, FontStyle.Bold);
                    lblstatus.Visible = true;
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(originalUsername))
            {
                MessageBox.Show("Please search for and find a user before updating.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newUsername = username.Text.Trim();
            string newPassword = password.Text.Trim();
            string newEmail = email.Text.Trim();
            string newRole = cbrole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(newUsername))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(newRole))
            {
                MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                try
                {
                    conn.Open();

                    if (newUsername != originalUsername)
                    {
                        string checkUserQuery = "SELECT COUNT(*) FROM users WHERE BINARY username = @newUsername";
                        MySqlCommand checkCmd = new MySqlCommand(checkUserQuery, conn);
                        checkCmd.Parameters.AddWithValue("@newUsername", newUsername);
                        if ((long)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("This username already exists. Please choose a different one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Kiểm tra có thay đổi gì không
                    string checkChangeQuery = "SELECT * FROM users WHERE BINARY username = @original";
                    MySqlCommand checkChangeCmd = new MySqlCommand(checkChangeQuery, conn);
                    checkChangeCmd.Parameters.AddWithValue("@original", originalUsername);

                    using (MySqlDataReader reader = checkChangeCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool unchanged =
                                newUsername == reader["username"].ToString() &&
                                newPassword == reader["password"].ToString() &&
                                newEmail == reader["email"].ToString() &&
                                newRole == reader["role"].ToString();

                            if (unchanged)
                            {
                                MessageBox.Show("No changes detected. Nothing was updated.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lblstatus.Text = "⚠️ No changes made.";
                                lblstatus.ForeColor = Color.Orange;
                                lblstatus.Visible = true;
                                return;
                            }
                        }
                    }

                    // Cập nhật
                    string updateQuery = "UPDATE users SET username = @uname, password = @pwd, email = @mail, role = @r WHERE BINARY username = @original";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);

                    updateCmd.Parameters.AddWithValue("@uname", newUsername);
                    updateCmd.Parameters.AddWithValue("@pwd", newPassword);
                    updateCmd.Parameters.AddWithValue("@mail", newEmail);
                    updateCmd.Parameters.AddWithValue("@r", newRole);
                    updateCmd.Parameters.AddWithValue("@original", originalUsername);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("✅ User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblstatus.Text = "✅ Update successful.";
                        lblstatus.ForeColor = Color.Green;

                        originalUsername = newUsername;
                        search.Text = newUsername;
                    }
                    else
                    {
                        MessageBox.Show("❌ Update failed. No matching user or nothing changed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblstatus.Text = "❌ Update failed.";
                        lblstatus.ForeColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblstatus.Text = "⚠️ Update error.";
                    lblstatus.ForeColor = Color.Orange;
                }
                finally
                {
                    lblstatus.Font = new Font(lblstatus.Font, FontStyle.Bold);
                    lblstatus.Visible = true;
                }
            }
        }

        private void ClearFields()
        {
            username.Text = "";
            password.Text = "";
            email.Text = "";
            cbrole.SelectedIndex = -1;
        }

        
    }
}
