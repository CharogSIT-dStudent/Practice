using System;
using MySql.Data.MySqlClient;

namespace DBMWEB
{
    public static class StudentManager
    {
        public static void Run(string connectionString)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Student Management ---");
                Console.WriteLine("Menu:");
                Console.WriteLine("[1] Test Connection");
                Console.WriteLine("[2] Insert Student");
                Console.WriteLine("[3] Display Students");
                Console.WriteLine("[4] Back to Main");
                Console.Write("Enter option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        TestConnection(connectionString);
                        break;
                    case "2":
                        InsertStudent(connectionString);
                        break;
                    case "3":
                        DisplayStudents(connectionString);
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void TestConnection(string connectionString)
        {
            using var conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                Console.WriteLine("[SUCCESS] StudentDB connected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }
        }

        static void InsertStudent(string connectionString)
        {
            using var conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                Console.Write("First Name: ");
                string firstName = Console.ReadLine();
                Console.Write("Last Name: ");
                string lastName = Console.ReadLine();
                Console.Write("Course: ");
                string course = Console.ReadLine();
                Console.Write("Year Level: ");
                int yearLevel = int.Parse(Console.ReadLine());

                string query = "INSERT INTO Student (FIRSTNAME, LASTNAME, COURSE, YEARLEVEL) VALUES (@fn, @ln, @c, @yl)";
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@fn", firstName);
                cmd.Parameters.AddWithValue("@ln", lastName);
                cmd.Parameters.AddWithValue("@c", course);
                cmd.Parameters.AddWithValue("@yl", yearLevel);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine($"{rows} student(s) added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }
        }

        static void DisplayStudents(string connectionString)
        {
            using var conn = new MySqlConnection(connectionString);
            string query = "SELECT * FROM Student";
            try
            {
                conn.Open();
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();
                Console.WriteLine("\n--- Student List ---");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["STUDENT_ID"]}, FirstName: {reader["FIRSTNAME"]}, LastName: {reader["LASTNAME"]}, Course: {reader["COURSE"]}, Year Level: {reader["YEARLEVEL"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }
        }
    }
}
