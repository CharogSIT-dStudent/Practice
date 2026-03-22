using System;
using MySql.Data.MySqlClient;

namespace DBMWEB
{
    public static class ClinicManager
    {
        public static void Run(string connectionString)
        {
            // Keeps the clinic menu showing and working until user picks exit
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Clinic Management ---");
                Console.WriteLine("Menu:");
                Console.WriteLine("[1] Insert Data");
                Console.WriteLine("[2] Create Prescription");
                Console.WriteLine("[3] View Prescriptions");
                Console.WriteLine("[4] View Tables");
                Console.WriteLine("[5] Back to Main");
                Console.Write("Enter option: ");
                string option = Console.ReadLine();

                // Looks at user's choice and runs the matching action
                switch (option)
                {
                    case "1":
                        // Adds new doctor or medicine to the database
                        InsertData(connectionString);
                        break;
                    case "2":
                        // Creates a new prescription record
                        CreatePrescription(connectionString);
                        break;
                    case "3":
                        // Shows all prescriptions linking patients, doctors, and medicines
                        ViewPrescriptions(connectionString);
                        break;
                    case "4":
                        // Shows all rows from the chosen table
                        ViewTables(connectionString);
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void InsertData(string connectionString)
        {
            Console.WriteLine("\nChoose table:");
            Console.WriteLine("1. Doctor");
            Console.WriteLine("2. Medicine");
            string choice = Console.ReadLine();

            using var conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();

                if (choice == "1")
                {
                // Gets doctor's details from user input
                    Console.Write("First Name: ");
                    string first = Console.ReadLine();
                    Console.Write("Last Name: ");
                    string last = Console.ReadLine();
                    Console.Write("Specialty: ");
                    string spec = Console.ReadLine();

                    // Adds the doctor to the database using safe placeholders
                    string query = "INSERT INTO Doctor (FirstName, LastName, Specialty) VALUES (@f, @l, @s)";
                    using var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@f", first);
                    cmd.Parameters.AddWithValue("@l", last);
                    cmd.Parameters.AddWithValue("@s", spec);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Doctor added.");
                }
                else if (choice == "2")
                {
                    // Gets medicine details from user input
                    Console.Write("Medicine Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Dosage: ");
                    string dosage = Console.ReadLine();
                    Console.Write("Price: ");
                    decimal price = decimal.Parse(Console.ReadLine());

                    // Adds the medicine to the database using safe placeholders
                    string query = "INSERT INTO Medicine (MedicineName, Dosage, Price) VALUES (@n, @d, @p)";
                    using var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@n", name);
                    cmd.Parameters.AddWithValue("@d", dosage);
                    cmd.Parameters.AddWithValue("@p", price);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Medicine added.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }
        }

        static void CreatePrescription(string connectionString)
        {
            using var conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                // Gets IDs and patient name for the prescription
                Console.Write("Doctor ID: ");
                string doctor = Console.ReadLine();
                Console.Write("Medicine ID: ");
                string medicine = Console.ReadLine();
                Console.Write("Patient Name: ");
                string patient = Console.ReadLine();

                // Adds prescription with current date/time automatically
                string query = "INSERT INTO Prescription (DoctorID, MedicineID, PatientName, PrescriptionDate) VALUES (@d, @m, @p, NOW())";
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@d", doctor);
                cmd.Parameters.AddWithValue("@m", medicine);
                cmd.Parameters.AddWithValue("@p", patient);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Prescription saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }
        }

        static void ViewPrescriptions(string connectionString)
        {
            using var conn = new MySqlConnection(connectionString);
            // Special query that combines prescription info with doctor and medicine details (JOIN)
            string query = @"SELECT p.PrescriptionID, p.PatientName, d.LastName, d.Specialty, m.MedicineName 
                             FROM Prescription p 
                             JOIN Doctor d ON p.DoctorID = d.DoctorID 
                             JOIN Medicine m ON p.MedicineID = m.MedicineID";
            try
            {
                conn.Open();
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();
                Console.WriteLine("\n--- PRESCRIPTION LIST ---");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["PrescriptionID"]} | Patient: {reader["PatientName"]} | Doctor: {reader["LastName"]} | Specialty: {reader["Specialty"]} | Medicine: {reader["MedicineName"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }
        }

        static void ViewTables(string connectionString)
        {
            Console.WriteLine("\nSelect table: 1. Doctor | 2. Medicine | 3. Prescription");
            string choice = Console.ReadLine();
            string table = choice == "1" ? "Doctor" : choice == "2" ? "Medicine" : "Prescription";

            using var conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = $"SELECT * FROM {table}";
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();
                Console.WriteLine($"\n--- {table} TABLE ---");
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + " | ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] " + ex.Message);
            }
        }
    }
}
