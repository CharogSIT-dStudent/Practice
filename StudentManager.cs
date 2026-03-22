using System;
using MySql.Data.MySqlClient;

namespace DBMWEB
{
    public static class StudentManager
    {
        //ready-to-use task that takes some text to work with na hindi na kailangan may ibabalik pa
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
                  //pauses and hihintayin nya yong user input 
                string option = Console.ReadLine();
                //option selection
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

            //ag alala ti info idjay database at nagchecheck kung connected
        static void TestConnection(string connectionString)
        {
            //This line opens a connection to a MySQL database using the info in connectionString. 
            // coon ti nagan ti connection, ag close no nalpas tayo idjay output or yaz
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
          //ag alala ti info idjay database at nagchecheck kung connected
        static void InsertStudent(string connectionString)
        {
            //This line opens a connection to a MySQL database using the info in connectionString. 
            // coon ti nagan ti connection, ag close no nalpas tayo idjay output or yaz
            using var conn = new MySqlConnection(connectionString);
            try
            {
                //input
                // conn.open() ti agstastart ti conversation
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
               static void DisplayStudents(string connectionString) //ag alala ti info idjay database at nagchecheck kung connected
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
                    Console.WriteLine($"ID: {reader["STUDENTID"]}, FirstName: {reader["FIRSTNAME"]}, LastName: {reader["LASTNAME"]}, Course: {reader["COURSE"]}, Year Level: {reader["YEARLEVEL"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }
        }
    }
}
