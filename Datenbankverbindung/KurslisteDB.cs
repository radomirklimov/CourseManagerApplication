using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Datenbankverbindung
{
    internal class KurslisteDB
    {
        private readonly string _connectionString;

        public KurslisteDB()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Atiwora"]?.ConnectionString
            ?? throw new InvalidOperationException("Connection string 'Atiwora' not found in configuration.");
        }

        private OracleConnection GetConnection()
        {
            var conn = new OracleConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public Table GetAllRooms()
        {
            try
            {
                using var conn = GetConnection();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM B3_Pr_Room";

                using var reader = cmd.ExecuteReader();

                Table rooms = new Table("Room");

                while (reader.Read())
                {
                    var row = new Row();
                    row.AddField(new TextField("Name", reader["RNR"]?.ToString() ?? string.Empty));
                    row.AddField(new NumberField("Size", Convert.ToDouble(reader["ROOM_SIZE"])));
                    row.AddField(new NumberField("Capacity", Convert.ToInt32(reader["MAXCAPACITY"])));
                    rooms.AddRow(row);
                }

                return rooms;
            }
            catch (OracleException ex)
            {
                throw new ApplicationException("Error fetching room data from the database.", ex);
            }
        }

        public Table GetCoursesByRoom(string rnr)
        {
            try
            {
                using var conn = GetConnection();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM B3_Pr_Course WHERE rnr = :rnr";
                cmd.Parameters.Add(new OracleParameter("rnr", rnr));

                using var reader = cmd.ExecuteReader();
                Table courses = new Table("Course");

                while (reader.Read())
                {
                    var row = new Row();
                    row.AddField(new NumberField("cnr", Convert.ToDouble(reader["CNR"])));
                    row.AddField(new TextField("Name", reader["NAME"]?.ToString() ?? string.Empty));
                    row.AddField(new NumberField("Duration", Convert.ToInt32(reader["DURATION"])));
                    row.AddField(new NumberField("Price", Convert.ToDouble(reader["PRICE"])));
                    row.AddField(new BoolField("Booked", reader["BOOKED"].ToString() == "T"));

                    courses.AddRow(row);
                }

                return courses;
            }
            catch (OracleException ex)
            {
                throw new ApplicationException($"Error fetching courses for room '{rnr}'.", ex);
            }
        }

        public void AddNewRoom(Row room)
        {
            try
            {
                using var conn = GetConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = @"
                    INSERT INTO B3_Pr_Room (rnr, room_size, maxCapacity)
                    VALUES (:rnr, :room_size, :maxCapacity)";

                cmd.Parameters.Add(new OracleParameter("rnr", room.Fields[0].Value));
                cmd.Parameters.Add(new OracleParameter("room_size", Convert.ToDouble(room.Fields[1].Value)));
                cmd.Parameters.Add(new OracleParameter("maxCapacity", Convert.ToInt32(room.Fields[2].Value)));

                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                throw new ApplicationException("Error inserting new room into the database.", ex);
            }
        }

        public void AddNewCourse(Row course)
        {
            if (course == null || course.Fields.Count < 5)
                throw new ArgumentException("Course data is incomplete.");

            try
            {
                using var conn = GetConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = @"
                INSERT INTO B3_Pr_Course (rnr, name, duration, price, booked)
                VALUES (:rnr, :name, :duration, :price, :booked)";

                object bookedValue = (bool)course.Fields[4].Value ? "Y" : "N";

                cmd.Parameters.Add(new OracleParameter("rnr", course.Fields[0].Value));
                cmd.Parameters.Add(new OracleParameter("name", course.Fields[1].Value));
                cmd.Parameters.Add(new OracleParameter("duration", course.Fields[2].Value));
                cmd.Parameters.Add(new OracleParameter("price", course.Fields[3].Value));
                cmd.Parameters.Add(new OracleParameter("booked", bookedValue));

                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                throw new ApplicationException("Error inserting new course into the database.", ex);
            }
        }

        public void DeleteRoomByPKeyAndItsCourses(string rnr)
        {
            try
            {
                using var conn = GetConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM B3_Pr_Course WHERE rnr = :rnr";
                cmd.Parameters.Add(new OracleParameter("rnr", rnr));
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                cmd.CommandText = "DELETE FROM B3_Pr_Room WHERE rnr = :rnr";
                cmd.Parameters.Add(new OracleParameter("rnr", rnr));

                int affected = cmd.ExecuteNonQuery();
                if (affected == 0)
                    throw new InvalidOperationException($"No room found with RNR = '{rnr}'.");

            }
            catch (OracleException ex)
            {
                throw new ApplicationException("Error deleting room from the database.", ex);
            }
        }

        public void DeleteCourseByPKey(string cnr)
        {
            try
            {
                using var conn = GetConnection();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM B3_Pr_Course WHERE cnr = :cnr";
                cmd.Parameters.Add(new OracleParameter("cnr", cnr));

                int affected = cmd.ExecuteNonQuery();
                if (affected == 0)
                    throw new InvalidOperationException($"No course found with CNR = '{cnr}'.");
            }
            catch (OracleException ex)
            {
                throw new ApplicationException("Error deleting course from the database.", ex);
            }
        }
    }
}
