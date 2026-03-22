using System;
using MySql.Data.MySqlClient;

namespace DBMWEB
{
    class Program
    {
        // Database connection string for SystemDB
        static string systemConnectionString = "Server=localhost; Database=SystemDB; Uid=root; port=1108;";

        static void Main(string[] args)
        {
            // Shows app title and description
            Console.WriteLine("=== DBMWEB Integrated Application ===");
            Console.WriteLine("Unified Clinic/Student Management & Personal Trackers\n");

            // Keeps main menu running until exit
            bool running = true;
            while (running)
            {
                // main
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("[1] Clinic Management");
                Console.WriteLine("[2] Student Management");
                Console.WriteLine("[3] Personal Trackers");
                Console.WriteLine("[4] Test DB Connections");
                Console.WriteLine("[5] Exit");
                Console.Write("Enter option: ");
                // Urayun na djay user and input
                string option = Console.ReadLine();

                // Checks user's choice and runs the matching section
                switch (option)
                {
                    case "1":
                        ClinicManager.Run(systemConnectionString);
                        break;
                    case "2":
                        StudentManager.Run(systemConnectionString);
                        break;
                    case "3":
                        TrackerManager.Run();
                        break;
                    case "4":
                        TestConnections();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            // Final message before app closes
            Console.WriteLine("\nThank you for using DBMWEB. Press any key to exit...");
            Console.ReadKey();
        }

        static void TestConnections()
        {
            // Starts database connection test
            Console.WriteLine("\n--- Testing Connections ---");
            TestConnection(systemConnectionString, "SystemDB");
        }

        static void TestConnection(string connStr, string dbName)
        {
            // Tries to connect to database tapos ipakita na no success ono saan
            using var conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                Console.WriteLine($"[SUCCESS] {dbName} connected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {dbName}: {ex.Message}");
            }
        }
    }
}

/*STUDENT SYSTEM
static void DisplayStudentsByCourse(string connectionString)
{
    using var conn = new MySqlConnection(connectionString);
    try
    {
        conn.Open();

        Console.Write("Enter course: ");
        string course = Console.ReadLine();

        string query = "SELECT * FROM Student WHERE Course = @course ORDER BY YearLevel ASC";

        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@course", course);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["StudentID"]}, Name: {reader["FirstName"]} {reader["LastName"]}, Year: {reader["YearLevel"]}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}*/
// SAMPLE SYSTEMS
/*CLINIC SYSTEM 
Using Join naman toh
static void DisplayFullPrescription(string connectionString)
{
    using var conn = new MySqlConnection(connectionString);
    try
    {
        conn.Open();

        string query = @"
        SELECT p.PrescriptionID, p.PatientName,
               d.FirstName, d.LastName,
               m.MedicineName
        FROM Prescription p
        JOIN Doctor d ON p.DoctorID = d.DoctorID
        JOIN Medicine m ON p.MedicineID = m.MedicineID";

        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                $"PrescriptionID: {reader["PrescriptionID"]}, " +
                $"Patient: {reader["PatientName"]}, " +
                $"Doctor: {reader["FirstName"]} {reader["LastName"]}, " +
                $"Medicine: {reader["MedicineName"]}"
            );
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}*/
