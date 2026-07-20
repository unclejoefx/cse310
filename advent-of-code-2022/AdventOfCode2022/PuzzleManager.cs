// PuzzleManager.cs - Manager class for handling all Advent of Code puzzles
// Author: Uwa Joseph Uwa
// Date: July 2026
// Purpose: Manages puzzle instances, handles file loading, and tracks statistics
// Demonstrates class usage, file I/O, and collections in C#.

using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2022
{
    /// <summary>
    /// Structure to track puzzle statistics.
    /// Demonstrates struct for organizing related statistical data.
    /// </summary>
    public struct PuzzleStats
    {
        public int TotalPuzzles;         // Total number of puzzles available
        public int SolvedPuzzles;        // Number of puzzles completed
        public DateTime LastRun;         // When puzzles were last run
        public double CompletionRate;    // Percentage of puzzles completed

        /// <summary>
        /// Calculates and updates the completion rate.
        /// </summary>
        public void UpdateCompletionRate()
        {
            if (TotalPuzzles > 0)
            {
                // Expression: calculate percentage
                CompletionRate = (double)SolvedPuzzles / TotalPuzzles * 100;
            }
            else
            {
                CompletionRate = 0;
            }
        }
    }

    /// <summary>
    /// PuzzleManager class handles the lifecycle of all puzzle solutions.
    /// This class manages puzzle instances, loads input files, and tracks progress.
    /// </summary>
    public class PuzzleManager
    {
        // Dictionary to store puzzle instances indexed by day number
        private Dictionary<int, Puzzle> puzzles;

        // Statistics tracking structure
        private PuzzleStats stats;

        // Path to the inputs directory
        private string inputsPath;

        /// <summary>
        /// Constructor initializes the puzzle manager.
        /// Creates instances of all available puzzles and sets up paths.
        /// </summary>
        public PuzzleManager()
        {
            // Initialize the puzzles dictionary
            puzzles = new Dictionary<int, Puzzle>();

            // Initialize statistics
            stats = new PuzzleStats();
            stats.TotalPuzzles = 2;  // We have Day 1 and Day 2 implemented
            stats.SolvedPuzzles = 0;

            // Set up the inputs directory path
            // Navigate to the inputs folder relative to the executable
            inputsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "inputs");

            // If that doesn't exist, try the development path
            if (!Directory.Exists(inputsPath))
            {
                inputsPath = Path.Combine(Directory.GetCurrentDirectory(), "inputs");
            }

            // Register all available puzzles
            RegisterPuzzles();

            Console.WriteLine($"PuzzleManager initialized with {puzzles.Count} puzzles.");
            Console.WriteLine($"Input files directory: {Path.GetFullPath(inputsPath)}");
        }

        /// <summary>
        /// Registers all available puzzle instances.
        /// Creates and adds each puzzle to the dictionary.
        /// </summary>
        private void RegisterPuzzles()
        {
            // Create Day 1 puzzle - demonstrates polymorphism
            // Day01 extends Puzzle, but is stored as Puzzle type
            puzzles[1] = new Day01();

            // Create Day 2 puzzle - also demonstrates polymorphism
            puzzles[2] = new Day02();
        }

        /// <summary>
        /// Runs a specific puzzle by day number.
        /// Handles input file loading and puzzle execution.
        /// </summary>
        /// <param name="day">The day number of the puzzle to run</param>
        public void RunPuzzle(int day)
        {
            // Conditional: Check if puzzle exists
            if (puzzles.ContainsKey(day))
            {
                Puzzle puzzle = puzzles[day];

                // Build the input file path
                string inputFile = Path.Combine(inputsPath, $"day{day:D2}.txt");

                // Load input from file (demonstrates file I/O)
                bool inputLoaded = puzzle.LoadInput(inputFile);

                if (inputLoaded)
                {
                    // Run the puzzle (polymorphic call)
                    puzzle.Run();

                    // Update statistics
                    if (puzzle.IsSolved)
                    {
                        stats.SolvedPuzzles++;
                        stats.LastRun = DateTime.Now;
                        stats.UpdateCompletionRate();
                    }
                }
                else
                {
                    // Handle missing input file - create sample data
                    Console.WriteLine("\nWould you like to use sample data instead? (y/n): ");
                    string? response = Console.ReadLine();

                    if (response?.ToLower() == "y")
                    {
                        CreateSampleInput(day);
                        // Try loading again
                        if (puzzle.LoadInput(inputFile))
                        {
                            puzzle.Run();
                            if (puzzle.IsSolved)
                            {
                                stats.SolvedPuzzles++;
                                stats.LastRun = DateTime.Now;
                                stats.UpdateCompletionRate();
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"\n✗ Error: Day {day} puzzle is not available.");
                Console.WriteLine("  Available days: 1, 2");
            }
        }

        /// <summary>
        /// Runs all registered puzzles sequentially.
        /// </summary>
        public void RunAllPuzzles()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║      RUNNING ALL PUZZLES               ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            // Ensure input files exist
            EnsureInputFilesExist();

            // Loop through all registered puzzles
            foreach (int day in puzzles.Keys)
            {
                RunPuzzle(day);
                Console.WriteLine();  // Add spacing between puzzles
            }

            Console.WriteLine("\n═══════════════════════════════════════════════════════");
            Console.WriteLine("  All puzzles completed!");
            DisplayStatistics();
        }

        /// <summary>
        /// Displays statistics about puzzle completion.
        /// Shows solved puzzles, completion rate, and last run time.
        /// </summary>
        public void DisplayStatistics()
        {
            // Update completion rate before displaying
            stats.UpdateCompletionRate();

            Console.WriteLine("\n┌──────────────────────────────────────────┐");
            Console.WriteLine("│           PUZZLE STATISTICS              │");
            Console.WriteLine("├──────────────────────────────────────────┤");
            Console.WriteLine($"│  Total Puzzles Available: {stats.TotalPuzzles,-14}│");
            Console.WriteLine($"│  Puzzles Solved:          {stats.SolvedPuzzles,-14}│");
            Console.WriteLine($"│  Completion Rate:         {stats.CompletionRate:F1}%{"",-11}│");

            // Conditional: Show last run time if puzzles have been run
            if (stats.LastRun != DateTime.MinValue)
            {
                Console.WriteLine($"│  Last Run:                {stats.LastRun:g,-14}│");
            }

            Console.WriteLine("└──────────────────────────────────────────┘");

            // Display individual puzzle status
            Console.WriteLine("\n  Puzzle Status:");
            foreach (var kvp in puzzles)
            {
                string status = kvp.Value.IsSolved ? "✓ Solved" : "○ Not Run";
                Console.WriteLine($"    Day {kvp.Key}: {kvp.Value.PuzzleName} - {status}");
            }
        }

        /// <summary>
        /// Ensures input files exist, creating sample data if necessary.
        /// Demonstrates file writing in C#.
        /// </summary>
        private void EnsureInputFilesExist()
        {
            // Create inputs directory if it doesn't exist
            if (!Directory.Exists(inputsPath))
            {
                Directory.CreateDirectory(inputsPath);
                Console.WriteLine($"Created inputs directory: {inputsPath}");
            }

            // Check and create sample input files
            CreateSampleInput(1);
            CreateSampleInput(2);
        }

        /// <summary>
        /// Creates sample input data for a specific puzzle day.
        /// Demonstrates file writing using File.WriteAllText.
        /// </summary>
        /// <param name="day">The day number to create input for</param>
        private void CreateSampleInput(int day)
        {
            string filePath = Path.Combine(inputsPath, $"day{day:D2}.txt");

            // Only create if file doesn't exist
            if (!File.Exists(filePath))
            {
                // Ensure directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? inputsPath);

                string content = "";

                // Conditional: Generate appropriate sample data for each day
                if (day == 1)
                {
                    // Day 1: Calorie Counting sample data
                    // Each group of numbers represents one elf's food items
                    content = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";
                }
                else if (day == 2)
                {
                    // Day 2: Rock Paper Scissors sample data
                    // Format: Opponent_Move Your_Response
                    content = @"A Y
B X
C Z";
                }

                // Write the content to file (File I/O demonstration)
                File.WriteAllText(filePath, content);
                Console.WriteLine($"✓ Created sample input file: {Path.GetFileName(filePath)}");
            }
        }
    }
}
