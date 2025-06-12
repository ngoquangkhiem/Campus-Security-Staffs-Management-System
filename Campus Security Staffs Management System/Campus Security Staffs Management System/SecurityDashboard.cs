using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Drawing;
using DocumentFormat.OpenXml.Office.Word;
using Campus_Security_Staffs_Management_System;

namespace SecurityManagementSystem
{
    public partial class SecurityDashboard : Form
    {
        //private Panel panelPersonalRoutine;
        private Label labelTitle;
        //private DataGridView dgvPersonalRoutine;
        private int _userId;
        private object userId;
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2

        public SecurityDashboard(int userId)
        {
            InitializeComponent();
            _userId = userId;
            //InitializePersonalRoutinePanel();
            LoadUserProfile();
            LoadPersonalRoutine();
            LoadAvatar();
            LoadRoutineStatus();
            LoadLeaveRequests();
            label14.Text = DateTime.Now.ToString("MMMM yyyy"); // Ví dụ: May 2025

        }

        private void LoadUserProfile()
        {
            //string connStr = "server=localhost;user id=root;password=141102;database=staff_system;SslMode=none;";
            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT username, id, role, email FROM users WHERE id = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", _userId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        label6.Text = reader.GetString("username");
                        label7.Text = reader["id"].ToString();
                        label8.Text = reader.GetString("role");
                        label9.Text = reader.GetString("email");
                    }
                }
            }
        }

        private void LoadPersonalRoutine()
        {
            dgvPersonalRoutine.Columns.Clear();
            dgvPersonalRoutine.Rows.Clear();

            dgvPersonalRoutine.Columns.Add("PlaceName", "Place");
            dgvPersonalRoutine.Columns.Add("Date", "Date");
            dgvPersonalRoutine.Columns.Add("StartTime", "Start Time");
            dgvPersonalRoutine.Columns.Add("EndTime", "End Time");

            var routines = Database.GetRoutineByUserId(_userId);

            foreach (var r in routines)
            {
                dgvPersonalRoutine.Rows.Add(
                    r.PlaceName,
                    r.Date.ToString("yyyy-MM-dd"),
                    r.StartTime,
                    r.EndTime
                );
            }
        }


        private void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("userId hiện tại: " + _userId.ToString());
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh đại diện";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;

                try
                {
                    // Hiển thị ảnh trong PictureBox
                    Image img = Image.FromFile(selectedImagePath);
                    pictureAvatar.Image = img;
                    pictureAvatar.SizeMode = PictureBoxSizeMode.Zoom;

                    // Đọc file ảnh thành mảng byte[]
                    byte[] imageBytes = File.ReadAllBytes(selectedImagePath);

                    // Lưu byte[] vào cột avatar (LONGBLOB) trong bảng users
                    //string connStr = "server=localhost;user=root;password=141102;database=staff_system;";
                    using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
                    {
                        conn.Open();
                        string query = "UPDATE users SET avatar = @avatar WHERE id = @userId";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.Add("@avatar", MySqlDbType.LongBlob).Value = imageBytes;
                            cmd.Parameters.Add("@userId", MySqlDbType.Int32).Value = _userId;
                            //cmd.ExecuteNonQuery();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Profile picture updated successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No record was updated. Please check the userId!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating the image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadAvatar()
        {
            //string connStr = "server=localhost;user=root;password=141102;database=staff_system;";
            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT avatar FROM users WHERE id = @userId";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", _userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            byte[] imageBytes = (byte[])reader["avatar"];
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                Image avatarImage = Image.FromStream(ms);
                                pictureAvatar.SizeMode = PictureBoxSizeMode.Zoom; // Đảm bảo không bị zoom méo
                                pictureAvatar.Image = avatarImage;
                            }
                        }
                    }
                }
            }
        }

        // Định nghĩa lớp Routine
        public class Routine
        {
            public string PlaceName { get; set; }
            public DateTime Date { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
        }

        // Lớp Database để kết nối DB và lấy dữ liệu
        public static class Database
        {
            //private static string connectionString = "server=localhost;user id=root;password=141102;database=staff_system;";

            public static List<Routine> GetRoutineByUserId(int userId)
            {
                List<Routine> routines = new List<Routine>();

                using (var conn = new MySqlConnection(DBConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                    SELECT p.name AS PlaceName, r.date, r.start_time, r.end_time
                    FROM routine r
                    JOIN places p ON r.place_id = p.id
                    WHERE r.user_id = @userId
                    ORDER BY r.date, r.start_time";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            routines.Add(new Routine
                            {
                                PlaceName = reader.GetString("PlaceName"),
                                Date = reader.GetDateTime("date"),
                                StartTime = reader.GetTimeSpan("start_time").ToString(@"hh\:mm\:ss"),
                                EndTime = reader.GetTimeSpan("end_time").ToString(@"hh\:mm\:ss")
                            });
                        }
                    }
                }

                return routines;
            }
        }


        private void LoadRoutineStatus()
        {
            try
            {
                int tongCa = 0, daLam = 0, soNgayNghi = 0;
                string trangThai = "No shift";
                DateTime today = DateTime.Today;
                TimeSpan nowTime = DateTime.Now.TimeOfDay;
                DateTime firstDay = new DateTime(today.Year, today.Month, 1);
                DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

                //string connStr = "server=localhost;user=root;password=141102;database=staff_system;";
                using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
                {
                    conn.Open();

                    // 1. Lấy tổng số ca trong tháng và số ca đã làm
                    string queryStat = @"
                        SELECT date, start_time, end_time
                        FROM routine 
                        WHERE user_id = @userId 
                          AND date BETWEEN @firstDay AND @lastDay";

                    using (MySqlCommand cmd = new MySqlCommand(queryStat, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", _userId);
                        cmd.Parameters.AddWithValue("@firstDay", firstDay);
                        cmd.Parameters.AddWithValue("@lastDay", lastDay);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tongCa++;
                                DateTime date = reader.GetDateTime("date");
                                TimeSpan start = reader.GetTimeSpan("start_time");
                                TimeSpan end = reader.GetTimeSpan("end_time");

                                if (date < today)
                                {
                                    daLam++;
                                }
                                else if (date == today && nowTime > end)
                                {
                                    daLam++;
                                }
                            }
                        }
                    }

                    // 2. Lấy số ngày nghỉ được duyệt trong tháng
                    string queryLeave = @"
                        SELECT COUNT(*) 
                        FROM leave_requests 
                        WHERE user_id = @userId 
                          AND status = 'Approved'
                          AND request_date BETWEEN @firstDay AND @lastDay";

                    using (MySqlCommand cmd = new MySqlCommand(queryLeave, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", _userId);
                        cmd.Parameters.AddWithValue("@firstDay", firstDay);  // Đã khai báo ở đầu hàm
                        cmd.Parameters.AddWithValue("@lastDay", lastDay);    // Đã khai báo ở đầu hàm
                        soNgayNghi = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 3. Lấy ca hôm nay (nếu có)
                    string queryToday = @"
                        SELECT start_time, end_time 
                        FROM routine 
                        WHERE user_id = @userId 
                          AND date = CURDATE() 
                        LIMIT 1";

                    TimeSpan? startCa = null, endCa = null;

                    using (MySqlCommand cmd = new MySqlCommand(queryToday, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", _userId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                startCa = reader.GetTimeSpan("start_time");
                                endCa = reader.GetTimeSpan("end_time");
                            }
                        }
                    }

                    // 4. Xác định trạng thái hôm nay
                    if (startCa != null && endCa != null)
                    {
                        string queryCheckNghi = @"
                    SELECT 1 
                    FROM leave_requests 
                    WHERE user_id = @userId 
                      AND status = 'Approved' 
                      AND request_date = CURDATE()
                    LIMIT 1";

                        using (MySqlCommand cmdCheck = new MySqlCommand(queryCheckNghi, conn))
                        {
                            cmdCheck.Parameters.AddWithValue("@userId", _userId);
                            object result = cmdCheck.ExecuteScalar();

                            if (result != null)
                            {
                                trangThai = "Scheduled shift - On leave";
                            }
                            else if (nowTime >= startCa && nowTime <= endCa)
                            {
                                trangThai = "Scheduled shift - On duty";
                            }
                            else
                            {
                                trangThai = "Scheduled shift - Off duty";
                            }
                        }
                    }
                    else
                    {
                        trangThai = "No shift";
                    }
                }

                // Hiển thị kết quả
                int luong = daLam * 300000;
                label17.Text = $"Status: {trangThai}";
                label18.Text = $"Total shifts: {tongCa}";
                label19.Text = $"Completed: {daLam}";
                label20.Text = $"Off: {soNgayNghi}";
                label21.Text = $"Estimated salary: {luong:N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading status: " + ex.Message);
            }
        }


        private void LoadLeaveRequests()
        {
            dgvLeaveRequests.Columns.Clear();
            dgvLeaveRequests.Rows.Clear();
            dgvLeaveRequests.Columns.Add("Date", "Leave date");
            dgvLeaveRequests.Columns.Add("TimeRange", "Leave shift");
            dgvLeaveRequests.Columns.Add("Reason", "Reason");
            dgvLeaveRequests.Columns.Add("Status", "Status");

            //string connStr = "server=localhost;user id=root;password=141102;database=staff_system;SslMode=none;";
            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT request_date, shift_start, shift_end, reason, status 
                    FROM leave_requests 
                    WHERE user_id = @userId 
                    ORDER BY request_date DESC";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", _userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string timeRange = $"{TimeSpan.Parse(reader["shift_start"].ToString()):hh\\:mm} - {TimeSpan.Parse(reader["shift_end"].ToString()):hh\\:mm}";
                            dgvLeaveRequests.Rows.Add(
                                Convert.ToDateTime(reader["request_date"]).ToString("yyyy-MM-dd"),
                                timeRange,
                                reader["reason"].ToString(),
                                reader["status"].ToString()
                            );
                        }
                    }
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpLeaveDate.Value.Date;
            string reason = txtReason.Text.Trim();
            string shiftType = cmbShift.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(reason) || string.IsNullOrEmpty(shiftType))
            {
                MessageBox.Show("Please enter all required information.", "Err", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác định giờ bắt đầu/kết thúc theo loại ca
            TimeSpan shiftStart, shiftEnd;
            if (shiftType == "Day Shift")
            {
                shiftStart = new TimeSpan(5, 45, 0);
                shiftEnd = new TimeSpan(18, 0, 0);
            }
            else // Night Shift
            {
                shiftStart = new TimeSpan(17, 45, 0);
                shiftEnd = new TimeSpan(6, 0, 0);
            }

            //string connStr = "server=localhost;user id=root;password=141102;database=staff_system;SslMode=none;";
            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                string query = @"INSERT INTO leave_requests (user_id, request_date, shift_start, shift_end, reason)
                     VALUES (@userId, @date, @start, @end, @reason)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", _userId);
                    cmd.Parameters.AddWithValue("@date", selectedDate);
                    cmd.Parameters.AddWithValue("@start", shiftStart);
                    cmd.Parameters.AddWithValue("@end", shiftEnd);
                    cmd.Parameters.AddWithValue("@reason", reason);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Leave request submitted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadLeaveRequests(); // Reload lại bảng
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn dashboard

            // Tạo lại LoginForm mới (đảm bảo form trống)
            CampusSecuritySystem.Form1 loginForm = new CampusSecuritySystem.Form1();
            loginForm.Show();

            this.Close(); // Đóng dashboard sau khi mở login
        }
    }
    
}
