// Day01.cs - Advent of Code 2022 Day 1: Calorie Counting
// Author: Uwa Joseph Uwa
// Date: July 2026
// Purpose: Solves Day 1 puzzle - finding elves carrying the most calories
// This class EXTENDS the abstract Puzzle class to demonstrate inheritance.

using System;
using System.Collections.Generic;

namespace AdventOfCode2022
{
    /// <summary>
    /// Structure to represent an Elf's inventory.
    /// Demonstrates the use of 'struct' in C# (similar to unions in concept).
    /// Structures are value types that can hold related data together.
    /// </summary>
    public struct ElfInventory
    {
        // Fields to store elf data
        public int ElfNumber;           // The elf's identifier (1-indexed)
        public int TotalCalories;       // Total calories this elf is carrying
        public int ItemCount;           // Number of food items the elf has

        /// <summary>
        /// Constructor for ElfInventory struct.
        /// </summary>
        /// <param name="number">The elf's number</param>
        /// <param name="calories">Total calories</param>
        /// <param name="items">Number of items</param>
        public ElfInventory(int number, int calories, int items)
        {
            ElfNumber = number;
            TotalCalories = calories;
            ItemCount = items;
        }

        /// <summary>
        /// Override ToString for easy display of elf information.
        /// </summary>
        /// <returns>Formatted string with elf details</returns>
        public override string ToString()
        {
            return $"Elf #{ElfNumber}: {TotalCalories:N0} calories ({ItemCount} items)";
        }
    }

    /// <summary>
    /// Day 1 puzzle solution class.
    /// This class EXTENDS the abstract Puzzle class using inheritance.
    /// The colon (:) syntax is C#'s way of indicating class inheritance (similar to 'extends' in Java).
    /// </summary>
    public class Day01 : Puzzle  // Day01 extends Puzzle - demonstrates inheritance
    {
        // Private field to store processed elf inventory data
        private List<ElfInventory> elves;

        /// <summary>
        /// Constructor that calls the base class constructor.
        /// The 'base' keyword is used to call the parent class constructor.
        /// </summary>
        public Day01() : base(1, "Calorie Counting")
        {
            // Initialize the list to store elf inventories
            elves = new List<ElfInventory>();
        }

        /// <summary>
        /// Parses the input data and creates ElfInventory structures.
        /// Demonstrates loops, conditionals, and expressions.
        /// </summary>
        private void ParseInput()
        {
            // Clear any existing data
            elves.Clear();

            // Variables to track current elf being processed
            int currentElfNumber = 1;
            int currentCalories = 0;
            int currentItemCount = 0;

            // Loop through each line of input
            for (int i = 0; i < inputLines.Length; i++)
            {
                string line = inputLines[i];

                // Conditional: Check if line is empty (separates elves)
                if (string.IsNullOrWhiteSpace(line))
                {
                    // Create a new ElfInventory struct and add to list
                    ElfInventory elf = new ElfInventory(currentElfNumber, currentCalories, currentItemCount);
                    elves.Add(elf);

                    // Reset for next elf
                    currentElfNumber++;
                    currentCalories = 0;
                    currentItemCount = 0;
                }
                else
                {
                    // Parse the calorie value and add to current total
                    // Expression: converting string to int and adding
                    if (int.TryParse(line, out int calories))
                    {
                        currentCalories += calories;  // Expression: addition assignment
                        currentItemCount++;           // Expression: increment
                    }
                }
            }

            // Don't forget the last elf (file may not end with blank line)
            if (currentCalories > 0)
            {
                ElfInventory lastElf = new ElfInventory(currentElfNumber, currentCalories, currentItemCount);
                elves.Add(lastElf);
            }

            Console.WriteLine($"  Parsed {elves.Count} elves from input data.");
        }

        /// <summary>
        /// Override of abstract method SolvePart1.
        /// Finds the elf carrying the most calories.
        /// </summary>
        /// <returns>The maximum calories carried by any single elf</returns>
        public override string SolvePart1()
        {
            // Parse input data if not already done
            if (elves.Count == 0)
            {
                ParseInput();
            }

            // Variable to track maximum calories found
            int maxCalories = 0;
            int maxElfNumber = 0;

            // Loop through all elves to find the maximum
            foreach (ElfInventory elf in elves)
            {
                // Conditional: Check if this elf has more calories than current max
                if (elf.TotalCalories > maxCalories)
                {
                    maxCalories = elf.TotalCalories;
                    maxElfNumber = elf.ElfNumber;
                }
            }

            Console.WriteLine($"  Elf #{maxElfNumber} is carrying the most calories.");
            return maxCalories.ToString("N0");  // Format with thousands separator
        }

        /// <summary>
        /// Override of abstract method SolvePart2.
        /// Finds the total calories of the top 3 elves.
        /// </summary>
        /// <returns>Sum of calories from the top 3 elves</returns>
        public override string SolvePart2()
        {
            // Parse input data if not already done
            if (elves.Count == 0)
            {
                ParseInput();
            }

            // Sort elves by calories in descending order
            // Using a custom comparison function (lambda expression)
            List<ElfInventory> sortedElves = new List<ElfInventory>(elves);
            sortedElves.Sort((a, b) => b.TotalCalories.CompareTo(a.TotalCalories));

            // Variable to accumulate top 3 calories
            int topThreeTotal = 0;

            // Display the top 3 elves
            Console.WriteLine("  Top 3 elves carrying the most calories:");

            // Loop through top 3 (or fewer if less than 3 elves)
            int count = Math.Min(3, sortedElves.Count);
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"    {i + 1}. {sortedElves[i]}");
                topThreeTotal += sortedElves[i].TotalCalories;  // Expression: accumulate total
            }

            return topThreeTotal.ToString("N0");  // Format with thousands separator
        }

        /// <summary>
        /// Override of virtual method to customize the header display.
        /// Demonstrates that virtual methods can be overridden.
        /// </summary>
        public override void DisplayHeader()
        {
            // Call the base class implementation first
            base.DisplayHeader();

            // Add additional puzzle-specific information
            Console.WriteLine("  Puzzle: Find which elf is carrying the most calories.");
            Console.WriteLine("  Each elf's inventory is separated by a blank line.");
        }
    }
}
