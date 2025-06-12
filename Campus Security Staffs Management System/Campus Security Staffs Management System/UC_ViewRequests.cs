using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Campus_Security_Staffs_Management_System
{
    public partial class UC_ViewRequests : UserControl
    {
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2
        //string connStr = "server=localhost;user=root;password=141102;database=staff_system;";

        public UC_ViewRequests()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            lr.id, 
                            u.username AS staff_name, 
                            lr.request_date, 
                            lr.reason, 
                            lr.status
                        FROM leave_requests lr
                        JOIN users u ON lr.user_id = u.id
                        WHERE lr.status = 'Pending'";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns["id"].HeaderText = "Request ID";
                    dataGridView1.Columns["staff_name"].HeaderText = "Staff Name";
                    dataGridView1.Columns["request_date"].HeaderText = "Request Date";
                    dataGridView1.Columns["reason"].HeaderText = "Reason";
                    dataGridView1.Columns["status"].HeaderText = "Status";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading requests: " + ex.Message);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateStatus("Approved");
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            UpdateStatus("Declined");
        }

        private void UpdateStatus(string newStatus)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int requestId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
                    {
                        conn.Open();
                        string query = "UPDATE leave_requests SET status = @status WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.Parameters.AddWithValue("@id", requestId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Request updated.");
                    LoadRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating request: " + ex.Message);
                }
            }
        }
    }
}
