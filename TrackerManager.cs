using System;

namespace DBMWEB
{
    public static class TrackerManager
    {
        public static void Run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Personal Trackers ---");
                Console.WriteLine("Menu:");
                Console.WriteLine("[1] Carbon Footprint Tracker");
                Console.WriteLine("[2] Weekly Spending Tracker");
                Console.WriteLine("[3] Savings Calculator");
                Console.WriteLine("[4] Grade Categorizer");
                Console.WriteLine("[5] Inventory Summary");
                Console.WriteLine("[6] Back to Main");
                Console.Write("Enter option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CarbonFootprint();
                        break;
                    case "2":
                        WeeklySpending();
                        break;
                    case "3":
                        SavingsCalculator();
                        break;
                    case "4":
                        GradeCategorizer();
                        break;
                    case "5":
                        InventorySummary();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void CarbonFootprint()
        {
            Console.WriteLine("\n-- Carbon Footprint Tracker --");
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.Write("Enter miles driven this week: ");
            int milesDriven = int.Parse(Console.ReadLine());
            double carbonPerMile = 0.404;
            double totalImpact = milesDriven * carbonPerMile;
            Console.WriteLine($"\nHello {userName}, your estimated carbon footprint is: {totalImpact:F2} kg of CO2.");
        }

        static void WeeklySpending()
        {
            Console.WriteLine("\n-- Weekly Spending Tracker --");
            double[] dailySpending = new double[5];
            double totalSpending = 0.0;
            double budgetLimit = 100.0;

            for (int i = 0; i < dailySpending.Length; i++)
            {
                Console.Write($"Enter spending for day {i + 1}: ");
                dailySpending[i] = double.Parse(Console.ReadLine());
                totalSpending += dailySpending[i];
            }

            Console.WriteLine($"\nTotal spending: {totalSpending:F2}");
            Console.WriteLine(totalSpending > budgetLimit ? 
                $"Warning: Over budget by {totalSpending - budgetLimit:F2}" : 
                "Congratulations: Within budget!");
        }

        static void SavingsCalculator()
        {
            Console.Write("Enter monthly savings: ");
            decimal monthlySavings = decimal.Parse(Console.ReadLine());
            Console.Write("Enter annual interest rate (e.g., 0.05): ");
            double interestRate = double.Parse(Console.ReadLine());

            decimal principal = monthlySavings * 12 * 5;
            decimal totalSavings = principal * (1m + (decimal)interestRate);
            Console.WriteLine($"\nAfter 5 years, total savings: {totalSavings:C}");
        }

        static void GradeCategorizer()
        {
            Console.WriteLine("\n-- Grade Categorizer --");
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.Write("Enter numeric grade (0-100): ");
            int grade = int.Parse(Console.ReadLine());

            if (grade < 0 || grade > 100)
            {
                Console.WriteLine("Error: Grade must be 0-100.");
                return;
            }

            string category = grade >= 90 ? "Excellent" :
                             grade >= 80 ? "Very Good" :
                             grade >= 70 ? "Satisfactory" : "Re-take Required";
            Console.WriteLine($"{userName}: {category}");
        }

        static void InventorySummary()
        {
            Console.WriteLine("\n-- Inventory Summary --");
            int[] stockLevels = new int[5];
            int totalStock = 0;

            for (int i = 0; i < stockLevels.Length; i++)
            {
                Console.Write($"Enter stock for Product {i + 1}: ");
                stockLevels[i] = int.Parse(Console.ReadLine());
            }

            foreach (int stock in stockLevels)
            {
                totalStock += stock;
                Console.WriteLine($"Stock: {stock}");
            }

            double average = (double)totalStock / stockLevels.Length;
            Console.WriteLine($"Total: {totalStock}, Average: {average:F2}");
        }
    }
}
