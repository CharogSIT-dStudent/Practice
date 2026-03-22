using System;
using MySql.Data.MySqlClient;

namespace DBMWEB
{
    class Program
    {
        static string clinicConnectionString = "Server=localhost; Database=SystemDB; Uid=root; port=1108;";
        static string studentConnectionString = "server=127.0.0.1;database=SystemDB;Uid=root;port=1108;";

        static void Main(string[] args)
        {
            Console.WriteLine("=== DBMWEB Integrated Application ===");
            Console.WriteLine("Unified Clinic/Student Management & Personal Trackers\n");

            bool running = true;
            while (running)
            {
                // menu
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("[1] Clinic Management");
                Console.WriteLine("[2] Student Management");
                Console.WriteLine("[3] Personal Trackers");
                Console.WriteLine("[4] Test DB Connections");
                Console.WriteLine("[5] Exit");
                Console.Write("Enter option: ");
                //pauses and hihintayin nya yong user input 
                string option = Console.ReadLine();

                    //option selection
                switch (option)
                {
                    case "1":
                        ClinicManager.Run(clinicConnectionString);
                        break;
                    case "2":
                        StudentManager.Run(studentConnectionString);
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
            Console.WriteLine("\nThank you for using DBMWEB. Press any key to exit...");
            Console.ReadKey();
        }

        static void TestConnections()
        {
            Console.WriteLine("\n--- Testing Connections ---");
            TestConnection(clinicConnectionString, "ClinicDB");
            TestConnection(studentConnectionString, "StudentDB");
        }

        static void TestConnection(string connStr, string dbName)
        {
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
