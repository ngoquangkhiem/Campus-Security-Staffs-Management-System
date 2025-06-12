using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Campus_Security_Staffs_Management_System
{
    public partial class UC_Delete : UserControl
    {
        private Timer inputDelayTimer;
        private bool userExists = false;
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2

        public UC_Delete()
        {
            InitializeComponent();

            // Debounce timer
            inputDelayTimer = new Timer();
            inputDelayTimer.Interval = 300; // 300ms
            inputDelayTimer.Tick += InputDelayTimer_Tick;

            // Sự kiện người dùng gõ vào ô nhập
            UD.TextChanged += UD_TextChanged;
        }

        private void UD_TextChanged(object sender, EventArgs e)
        {
            inputDelayTimer.Stop();
            inputDelayTimer.Start();
        }

        private void InputDelayTimer_Tick(object sender, EventArgs e)
        {
            inputDelayTimer.Stop();
            CheckUserExists();
        }

        private void CheckUserExists()
        {
            string username = UD.Text.Trim();
            userExists = false;

            if (string.IsNullOrEmpty(username))
            {
                userCheck.Text = "";
                return;
            }

            //string connStr = "server=localhost;user=root;password=141102;database=staff_system;";
            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE BINARY username = @username";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        userCheck.Text = "✅ User found.";
                        userCheck.ForeColor = Color.Green;
                        userExists = true;
                    }
                    else
                    {
                        userCheck.Text = "❌ User not found.";
                        userCheck.ForeColor = Color.Red;
                    }
                }
                catch
                {
                    userCheck.Text = "⚠️ Error checking user.";
                    userCheck.ForeColor = Color.Orange;
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string usernameToDelete = UD.Text.Trim();

            if (string.IsNullOrEmpty(usernameToDelete))
            {
                MessageBox.Show("Please enter a username.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!userExists)
            {
                MessageBox.Show("User not found. Please check again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //string connStr = "server=localhost;user=root;password=141102;database=staff_system;";
            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                try
                {
                    conn.Open();

                    var confirm = MessageBox.Show($"Are you sure you want to delete user '{usernameToDelete}'?",
                                                  "Confirm Delete",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

                    if (confirm != DialogResult.Yes)
                        return;

                    string deleteQuery = "DELETE FROM users WHERE BINARY username = @username";
                    MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@username", usernameToDelete);

                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        userCheck.Text = "✅ User deleted.";
                        userCheck.ForeColor = Color.Green;
                        UD.Clear();
                        userExists = false;

                        MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        userCheck.Text = "❌ Failed to delete user.";
                        userCheck.ForeColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
