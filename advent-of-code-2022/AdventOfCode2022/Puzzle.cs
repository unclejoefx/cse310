// Puzzle.cs - Abstract base class for all Advent of Code puzzles
// Author: Uwa Joseph Uwa
// Date: July 2026
// Purpose: Demonstrates inheritance using abstract classes in C#
// All puzzle solutions extend this base class to ensure consistent structure.

using System;
using System.IO;

namespace AdventOfCode2022
{
    /// <summary>
    /// Abstract base class that defines the structure for all puzzle solutions.
    /// This demonstrates the use of 'abstract' keyword for inheritance in C#.
    /// Each day's puzzle must extend this class and implement the abstract methods.
    /// </summary>
    public abstract class Puzzle
    {
        // Protected fields accessible to derived classes
        protected int dayNumber;        // The day number of the puzzle (1-25)
        protected string puzzleName;    // Human-readable name of the puzzle
        protected string[] inputLines;  // Array to store input data from file
        protected bool isSolved;        // Flag to track if puzzle has been solved

        /// <summary>
        /// Constructor for the Puzzle base class.
        /// Initializes common properties for all puzzles.
        /// </summary>
        /// <param name="day">The day number of the puzzle</param>
        /// <param name="name">The name of the puzzle</param>
        protected Puzzle(int day, string name)
        {
            this.dayNumber = day;
            this.puzzleName = name;
            this.inputLines = Array.Empty<string>();
            this.isSolved = false;
        }

        /// <summary>
        /// Property to get the day number of the puzzle.
        /// Demonstrates C# property getter syntax.
        /// </summary>
        public int DayNumber
        {
            get { return dayNumber; }
        }

        /// <summary>
        /// Property to get the puzzle name.
        /// </summary>
        public string PuzzleName
        {
            get { return puzzleName; }
        }

        /// <summary>
        /// Property to check if the puzzle has been solved.
        /// </summary>
        public bool IsSolved
        {
            get { return isSolved; }
        }

        /// <summary>
        /// Loads input data from a file for the puzzle.
        /// Demonstrates file I/O operations in C# using File.ReadAllLines.
        /// </summary>
        /// <param name="filePath">Path to the input file</param>
        /// <returns>True if file loaded successfully, false otherwise</returns>
        public bool LoadInput(string filePath)
        {
            // Check if the file exists before attempting to read
            if (File.Exists(filePath))
            {
                // Read all lines from the file into the inputLines array
                inputLines = File.ReadAllLines(filePath);
                Console.WriteLine($"✓ Loaded {inputLines.Length} lines from {Path.GetFileName(filePath)}");
                return true;
            }
            else
            {
                // Handle the case where the input file is not found
                Console.WriteLine($"✗ Error: Input file not found: {filePath}");
                return false;
            }
        }

        /// <summary>
        /// Abstract method that each puzzle must implement to solve Part 1.
        /// The 'abstract' keyword means this method has no body in the base class.
        /// </summary>
        /// <returns>The solution to Part 1 as a string</returns>
        public abstract string SolvePart1();

        /// <summary>
        /// Abstract method that each puzzle must implement to solve Part 2.
        /// </summary>
        /// <returns>The solution to Part 2 as a string</returns>
        public abstract string SolvePart2();

        /// <summary>
        /// Virtual method to display the puzzle header.
        /// Virtual methods can be overridden by derived classes if needed.
        /// </summary>
        public virtual void DisplayHeader()
        {
            Console.WriteLine();
            Console.WriteLine($"═══════════════════════════════════════════════════════");
            Console.WriteLine($"  Day {dayNumber}: {puzzleName}");
            Console.WriteLine($"═══════════════════════════════════════════════════════");
        }

        /// <summary>
        /// Runs both parts of the puzzle and displays results.
        /// This method uses the template method pattern.
        /// </summary>
        public void Run()
        {
            // Display the puzzle header
            DisplayHeader();

            // Solve and display Part 1
            Console.WriteLine("\n▶ Part 1:");
            string part1Result = SolvePart1();
            Console.WriteLine($"  Answer: {part1Result}");

            // Solve and display Part 2
            Console.WriteLine("\n▶ Part 2:");
            string part2Result = SolvePart2();
            Console.WriteLine($"  Answer: {part2Result}");

            // Mark puzzle as solved
            isSolved = true;
            Console.WriteLine($"\n✓ Day {dayNumber} completed successfully!");
        }
    }
}
