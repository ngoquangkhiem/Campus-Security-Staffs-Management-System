using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ClosedXML.Excel;
using System.Collections.Generic;

namespace Campus_Security_Staffs_Management_System
{
    public partial class UC_Salary : UserControl
    {
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2
        public UC_Salary()
        {
            InitializeComponent();

            // Khởi tạo các cột cho dgvSalary đúng số lượng cột và tên cột
            dgvSalary.Columns.Clear();
            dgvSalary.Columns.Add("UserId", "User ID");
            dgvSalary.Columns.Add("UserName", "User Name");
            dgvSalary.Columns.Add("TotalShiftCount", "Total Shifts");
            dgvSalary.Columns.Add("CompletedShiftCount", "Completed Shifts");
            dgvSalary.Columns.Add("LeaveDays", "Approved Leave Days");
            dgvSalary.Columns.Add("TotalSalary", "Total Salary (VND)");
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                dgvSalary.Rows.Clear();

                DateTime selectedMonth = dtpMonth.Value;
                DateTime firstDay = new DateTime(selectedMonth.Year, selectedMonth.Month, 1);
                DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
                TimeSpan nowTime = DateTime.Now.TimeOfDay;
                int salaryPerShift = 300000;

                //string connStr = "server=localhost;user=root;password=141102;database=staff_system;";

                using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
                {
                    conn.Open();

                    string queryUsers = @"
                        SELECT DISTINCT u.id, u.username 
                        FROM users u
                        JOIN routine r ON u.id = r.user_id
                        WHERE r.date BETWEEN @firstDay AND @lastDay
                    ";

                    MySqlCommand cmdUsers = new MySqlCommand(queryUsers, conn);
                    cmdUsers.Parameters.AddWithValue("@firstDay", firstDay);
                    cmdUsers.Parameters.AddWithValue("@lastDay", lastDay);

                    using (MySqlDataReader readerUsers = cmdUsers.ExecuteReader())
                    {
                        var userList = new List<(int userId, string username)>();
                        while (readerUsers.Read())
                        {
                            userList.Add((readerUsers.GetInt32("id"), readerUsers.GetString("username")));
                        }
                        readerUsers.Close();

                        foreach (var user in userList)
                        {
                            int tongCa = 0, daLam = 0, soNgayNghi = 0;

                            string queryRoutine = @"
                                SELECT date, start_time, end_time
                                FROM routine
                                WHERE user_id = @userId AND date BETWEEN @firstDay AND @lastDay
                            ";

                            MySqlCommand cmdRoutine = new MySqlCommand(queryRoutine, conn);
                            cmdRoutine.Parameters.AddWithValue("@userId", user.userId);
                            cmdRoutine.Parameters.AddWithValue("@firstDay", firstDay);
                            cmdRoutine.Parameters.AddWithValue("@lastDay", lastDay);

                            using (MySqlDataReader readerRoutine = cmdRoutine.ExecuteReader())
                            {
                                while (readerRoutine.Read())
                                {
                                    tongCa++;
                                    DateTime date = readerRoutine.GetDateTime("date");
                                    TimeSpan end = readerRoutine.GetTimeSpan("end_time");

                                    if (date < DateTime.Today)
                                    {
                                        daLam++;
                                    }
                                    else if (date == DateTime.Today && nowTime > end)
                                    {
                                        daLam++;
                                    }
                                }
                                readerRoutine.Close();
                            }

                            string queryLeave = @"
                                SELECT COUNT(*)
                                FROM leave_requests
                                WHERE user_id = @userId AND status = 'Approved' AND request_date BETWEEN @firstDay AND @lastDay
                            ";

                            MySqlCommand cmdLeave = new MySqlCommand(queryLeave, conn);
                            cmdLeave.Parameters.AddWithValue("@userId", user.userId);
                            cmdLeave.Parameters.AddWithValue("@firstDay", firstDay);
                            cmdLeave.Parameters.AddWithValue("@lastDay", lastDay);

                            soNgayNghi = Convert.ToInt32(cmdLeave.ExecuteScalar());

                            int luong = daLam * salaryPerShift;
                            string luongFormatted = luong.ToString("N0");

                            dgvSalary.Rows.Add(user.userId, user.username, tongCa, daLam, soNgayNghi, luongFormatted);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating salary: " + ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvSalary.Rows.Count == 0)
            {
                MessageBox.Show("No data to export!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx",
                FileName = $"SalaryReport_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Salary Report");

                        for (int i = 0; i < dgvSalary.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dgvSalary.Columns[i].HeaderText;
                            worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        }

                        for (int i = 0; i < dgvSalary.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvSalary.Columns.Count; j++)
                            {
                                worksheet.Cell(i + 2, j + 1).Value = dgvSalary.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        workbook.SaveAs(sfd.FileName);
                        MessageBox.Show("Export file successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
