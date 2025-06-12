using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Campus_Security_Staffs_Management_System;
using MySql.Data.MySqlClient;

namespace StaffSecuritySystem
{
    public partial class UC_Monitor : UserControl
    {
        //private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2
        public UC_Monitor()
        {
            InitializeComponent();
            this.Load += UC_Monitor_Load;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy"; // hoặc "dddd, dd MMMM yyyy" nếu muốn kèm thứ
        }

        private void UC_Monitor_Load(object sender, EventArgs e)
        {
            LoadStaffStatus();
        }

        private void LoadStaffStatus()
        {
            dgvMonitor.Rows.Clear();

            using (var conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();

                // Lấy dữ liệu lịch trực hôm nay và tên địa điểm
                string query = @"
                SELECT r.user_id, u.username, r.date, r.start_time, r.end_time, p.name AS place_name
                FROM routine r
                JOIN users u ON r.user_id = u.id
                JOIN places p ON r.place_id = p.id
                WHERE r.date = @selectedDate;
                ";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@selectedDate", dateTimePicker1.Value.Date);  // Lọc theo ngày trong dateTimePicker1
                var reader = cmd.ExecuteReader();

                var routineList = new List<(int userId, string name, string placeName, DateTime date, TimeSpan start, TimeSpan end)>();

                while (reader.Read())
                {
                    int userId = reader.GetInt32("user_id");
                    string name = reader.GetString("username");
                    string placeName = reader.GetString("place_name");
                    DateTime date = reader.GetDateTime("date");
                    TimeSpan start = reader.GetTimeSpan("start_time");
                    TimeSpan end = reader.GetTimeSpan("end_time");

                    routineList.Add((userId, name, placeName, date, start, end));
                }

                reader.Close();

                // Lấy danh sách nhân viên đang nghỉ phép hôm nay
                var onLeaveUsers = new HashSet<int>();

                string leaveQuery = @"
                SELECT id FROM leave_requests 
                WHERE request_date = @selectedDate AND status = 'Approved';
                ";

                var leaveCmd = new MySqlCommand(leaveQuery, conn);
                leaveCmd.Parameters.AddWithValue("@selectedDate", dateTimePicker1.Value.Date);  // Lọc theo ngày trong dateTimePicker1
                var leaveReader = leaveCmd.ExecuteReader();

                while (leaveReader.Read())
                {
                    onLeaveUsers.Add(leaveReader.GetInt32("user_id"));
                }

                leaveReader.Close();

                // Xử lý trạng thái
                var now = DateTime.Now.TimeOfDay;
                var addedUserIds = new HashSet<int>();

                foreach (var item in routineList)
                {
                    string status;

                    if (onLeaveUsers.Contains(item.userId))
                        status = "On Leave";
                    else if (IsWithinDuty(now, item.start, item.end))
                        status = "On Duty";
                    else
                        status = "Off Duty";

                    // Thêm vào DataGridView theo đúng thứ tự: id, username, place name, date, start_time, end_time, status
                    var rowIndex = dgvMonitor.Rows.Add(item.userId, item.name, item.placeName, item.date.ToString("yyyy-MM-dd"), item.start.ToString(@"hh\:mm"), item.end.ToString(@"hh\:mm"), status);

                    // Thay đổi màu nền ô nếu trạng thái là "On Duty"
                    if (status == "On Duty")
                    {
                        dgvMonitor.Rows[rowIndex].Cells["status"].Style.BackColor = Color.Green; // Tô màu xanh lá cây
                    }

                    addedUserIds.Add(item.userId);
                }

                // Thêm những nhân viên không có ca hôm nay vào (Off Duty)
                string allUsersQuery = "SELECT id, username FROM users WHERE role = 'security'";
                var allCmd = new MySqlCommand(allUsersQuery, conn);
                var allReader = allCmd.ExecuteReader();

                while (allReader.Read())
                {
                    int uid = allReader.GetInt32("id");
                    string uname = allReader.GetString("username");

                    if (!addedUserIds.Contains(uid))
                    {
                        string status = onLeaveUsers.Contains(uid) ? "On Leave" : "Off Duty";
                        var rowIndex = dgvMonitor.Rows.Add(uid, uname, "-", "-", "-", "-", status);

                        // Thay đổi màu nền ô nếu trạng thái là "On Duty"
                        if (status == "On Duty")
                        {
                            dgvMonitor.Rows[rowIndex].Cells["status"].Style.BackColor = Color.Green; // Tô màu xanh lá cây
                        }
                    }
                }

                allReader.Close();
            }
        }


        private bool IsWithinDuty(TimeSpan now, TimeSpan start, TimeSpan end)
        {
            if (start < end)
            {
                return now >= start && now <= end;
            }
            else
            {
                // Qua đêm: ví dụ 17:45 - 06:00
                return now >= start || now <= end;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadStaffStatus(); // Gọi lại hàm để lọc theo ngày mới
        }
    }
}
