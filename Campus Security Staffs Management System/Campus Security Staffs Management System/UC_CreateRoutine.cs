// UC_CreateRoutine.cs - Full Implementation
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Campus_Security_Staffs_Management_System
{
    public partial class UC_CreateRoutine : UserControl
    {
        private MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString); //2
        private int staffIndex = 0;
        public UC_CreateRoutine()
        {
            InitializeComponent();
            //CenterToParent();
            dgvRoutinePreview.Columns.Add("UserId", "User ID");
            dgvRoutinePreview.Columns.Add("UserName", "User Name");
            dgvRoutinePreview.Columns.Add("PlaceId", "Place ID");
            dgvRoutinePreview.Columns.Add("PlaceName", "Place Name");
            dgvRoutinePreview.Columns.Add("DutyDate", "Duty Date");
            dgvRoutinePreview.Columns.Add("StartTime", "Start Time");
            dgvRoutinePreview.Columns.Add("EndTime", "End Time");
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value.Date;
            int days = 7;

            List<User> securityStaffs = Database.GetSecurityStaffs();
            List<Place> places = Database.GetPlaces();

            dgvRoutinePreview.Rows.Clear();

            for (int day = 0; day < days; day++)
            {
                DateTime currentDate = startDate.AddDays(day);
                var assignedMorning = new HashSet<int>();
                var assignedNight = new HashSet<int>();

                foreach (var place in places)
                {
                    // Gán nhân viên cho ca sáng: 05:45 - 18:00
                    User morningStaff = null;
                    for (int i = 0; i < securityStaffs.Count; i++)
                    {
                        var candidate = securityStaffs[(staffIndex + i) % securityStaffs.Count];
                        if (!assignedMorning.Contains(candidate.Id))
                        {
                            morningStaff = candidate;
                            staffIndex = (staffIndex + i + 1) % securityStaffs.Count;
                            assignedMorning.Add(candidate.Id);
                            break;
                        }
                    }

                    if (morningStaff != null)
                    {
                        dgvRoutinePreview.Rows.Add(
                            morningStaff.Id, morningStaff.Name,
                            place.Id, place.Places,
                            currentDate.ToString("yyyy-MM-dd"),
                            "05:45", "18:00"
                        );
                    }

                    // Gán nhân viên cho ca tối: 17:45 - 06:00 hôm sau
                    User nightStaff = null;
                    for (int i = 0; i < securityStaffs.Count; i++)
                    {
                        var candidate = securityStaffs[(staffIndex + i) % securityStaffs.Count];
                        if (!assignedNight.Contains(candidate.Id))
                        {
                            nightStaff = candidate;
                            staffIndex = (staffIndex + i + 1) % securityStaffs.Count;
                            assignedNight.Add(candidate.Id);
                            break;
                        }
                    }

                    if (nightStaff != null)
                    {
                        dgvRoutinePreview.Rows.Add(
                            nightStaff.Id, nightStaff.Name,
                            place.Id, place.Places,
                            currentDate.ToString("yyyy-MM-dd"),
                            "17:45", "06:00"
                        );
                    }
                }
            }
        }



        private void btnSaveRoutine_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvRoutinePreview.Rows)
            {
                if (row.IsNewRow) continue;

                int userId = Convert.ToInt32(row.Cells["UserId"].Value);
                int placeId = Convert.ToInt32(row.Cells["PlaceId"].Value);
                DateTime date = DateTime.Parse(row.Cells["DutyDate"].Value.ToString());
                string startTime = row.Cells["StartTime"].Value.ToString();
                string endTime = row.Cells["EndTime"].Value.ToString();

                Database.InsertRoutine(userId, placeId, date, startTime, endTime);
            }

            MessageBox.Show("Routine created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UC_CreateRoutine_Load(object sender, EventArgs e)
        {

        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Place
    {
        public int Id { get; set; }
        public string Places { get; set; }
    }

    public static class Database
    {
        //private static string connectionString = "server=localhost;user=root;password=141102;database=staff_system;";

        public static List<User> GetSecurityStaffs()
        {
            List<User> users = new List<User>();

            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT id, username FROM users WHERE role = 'security'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("username")
                        });
                    }
                }
            }

            return users;
        }

        public static List<Place> GetPlaces()
        {
            List<Place> places = new List<Place>();

            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                string query = "SELECT id, name FROM places";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        places.Add(new Place
                        {
                            Id = reader.GetInt32("id"),
                            Places = reader.GetString("name")
                        });
                    }
                }
            }

            return places;
        }

        public static void InsertRoutine(int userId, int placeId, DateTime date, string startTime, string endTime)
        {
            using (MySqlConnection conn = new MySqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO routine (user_id, place_id, date, start_time, end_time) VALUES (@userId, @placeId, @date, @startTime, @endTime)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@placeId", placeId);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@startTime", startTime);
                cmd.Parameters.AddWithValue("@endTime", endTime);
                cmd.ExecuteNonQuery();
            }
        }

        internal static MySqlConnection GetConnection()
        {
            return new MySqlConnection(DBConfig.ConnectionString);
        }
    }
}
