// Program.cs - Main entry point for Advent of Code 2022 Solver
// Author: Uwa Joseph Uwa
// Date: July 2026
// Purpose: Demonstrates C# programming by solving Advent of Code 2022 puzzles
// This program showcases variables, expressions, conditionals, loops, functions,
// classes, structures, inheritance, and file I/O operations.

using System;

namespace AdventOfCode2022
{
    /// <summary>
    /// Main program class that serves as the entry point for the Advent of Code solver.
    /// This class handles user interaction and puzzle execution.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method - entry point of the application.
        /// Displays a menu and allows users to select which puzzle to solve.
        /// </summary>
        /// <param name="args">Command line arguments (not used)</param>
        static void Main(string[] args)
        {
            // Display welcome message with program information
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║       ADVENT OF CODE 2022 SOLVER - C# Edition          ║");
            Console.WriteLine("║           Author: Uwa Joseph Uwa                       ║");
            Console.WriteLine("║           CSE 310 - Applied Programming                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            // Create the puzzle manager to handle all puzzle operations
            PuzzleManager manager = new PuzzleManager();

            // Variable to track if the user wants to continue
            bool continueRunning = true;

            // Main program loop - keeps running until user chooses to exit
            while (continueRunning)
            {
                // Display menu options to the user
                DisplayMenu();

                // Get user input and process their choice
                string? userInput = Console.ReadLine();

                // Use conditional to handle different menu choices
                if (userInput == "1")
                {
                    // Run Day 1 puzzle: Calorie Counting
                    manager.RunPuzzle(1);
                }
                else if (userInput == "2")
                {
                    // Run Day 2 puzzle: Rock Paper Scissors
                    manager.RunPuzzle(2);
                }
                else if (userInput == "3")
                {
                    // Run all puzzles sequentially
                    manager.RunAllPuzzles();
                }
                else if (userInput == "4")
                {
                    // Display statistics about solved puzzles
                    manager.DisplayStatistics();
                }
                else if (userInput == "5")
                {
                    // Exit the program
                    continueRunning = false;
                    Console.WriteLine("\nThank you for using the Advent of Code 2022 Solver!");
                    Console.WriteLine("Happy Coding!");
                }
                else
                {
                    // Handle invalid input
                    Console.WriteLine("\n⚠ Invalid option. Please enter a number between 1 and 5.");
                }

                // Add spacing between iterations for readability
                if (continueRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Displays the main menu options to the user.
        /// This function organizes menu display logic separately from main.
        /// </summary>
        static void DisplayMenu()
        {
            Console.WriteLine("\n┌──────────────────────────────────────┐");
            Console.WriteLine("│           MAIN MENU                  │");
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.WriteLine("│  1. Solve Day 1: Calorie Counting    │");
            Console.WriteLine("│  2. Solve Day 2: Rock Paper Scissors │");
            Console.WriteLine("│  3. Run All Puzzles                  │");
            Console.WriteLine("│  4. View Statistics                  │");
            Console.WriteLine("│  5. Exit                             │");
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.Write("\nEnter your choice (1-5): ");
        }
    }
}
