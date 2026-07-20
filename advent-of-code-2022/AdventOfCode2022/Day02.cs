// Day02.cs - Advent of Code 2022 Day 2: Rock Paper Scissors
// Author: Uwa Joseph Uwa
// Date: July 2026
// Purpose: Solves Day 2 puzzle - calculating Rock Paper Scissors tournament score
// This class EXTENDS the abstract Puzzle class to demonstrate inheritance.

using System;
using System.Collections.Generic;

namespace AdventOfCode2022
{
    /// <summary>
    /// Structure to represent a single round of Rock Paper Scissors.
    /// Demonstrates struct usage for organizing game round data.
    /// </summary>
    public struct GameRound
    {
        // Fields for the round
        public int RoundNumber;         // Round identifier
        public char OpponentMove;       // A, B, or C
        public char YourMove;           // X, Y, or Z
        public int Score;               // Score for this round

        /// <summary>
        /// Constructor for GameRound struct.
        /// </summary>
        public GameRound(int round, char opponent, char you)
        {
            RoundNumber = round;
            OpponentMove = opponent;
            YourMove = you;
            Score = 0;  // Will be calculated later
        }
    }

    /// <summary>
    /// Day 2 puzzle solution class.
    /// This class EXTENDS the abstract Puzzle class using C# inheritance syntax.
    /// In C#, the colon (:) is equivalent to 'extends' in Java.
    /// </summary>
    public class Day02 : Puzzle  // Day02 extends Puzzle - inheritance
    {
        // Private list to store all game rounds
        private List<GameRound> rounds;

        /// <summary>
        /// Constructor calling the base class constructor.
        /// </summary>
        public Day02() : base(2, "Rock Paper Scissors")
        {
            rounds = new List<GameRound>();
        }

        /// <summary>
        /// Calculates the shape score based on your choice.
        /// Rock = 1, Paper = 2, Scissors = 3
        /// </summary>
        /// <param name="choice">X (Rock), Y (Paper), or Z (Scissors)</param>
        /// <returns>Score for the shape chosen</returns>
        private int GetShapeScore(char choice)
        {
            // Use conditional expressions to determine score
            if (choice == 'X') return 1;      // Rock
            else if (choice == 'Y') return 2;  // Paper
            else if (choice == 'Z') return 3;  // Scissors
            else return 0;  // Invalid choice
        }

        /// <summary>
        /// Determines the outcome score for a round (Part 1 interpretation).
        /// Opponent: A = Rock, B = Paper, C = Scissors
        /// You: X = Rock, Y = Paper, Z = Scissors
        /// </summary>
        /// <param name="opponent">Opponent's move</param>
        /// <param name="you">Your move</param>
        /// <returns>0 for loss, 3 for draw, 6 for win</returns>
        private int GetOutcomeScore(char opponent, char you)
        {
            // Convert moves to numbers for easier comparison
            // A/X = 0 (Rock), B/Y = 1 (Paper), C/Z = 2 (Scissors)
            int oppMove = opponent - 'A';
            int yourMove = you - 'X';

            // Check for draw
            if (oppMove == yourMove)
            {
                return 3;  // Draw
            }

            // Check for win using modular arithmetic
            // Rock beats Scissors, Paper beats Rock, Scissors beats Paper
            // If (yourMove - oppMove + 3) % 3 == 1, you win
            int difference = (yourMove - oppMove + 3) % 3;
            if (difference == 1)
            {
                return 6;  // Win
            }
            else
            {
                return 0;  // Loss
            }
        }

        /// <summary>
        /// Determines what move to make based on desired outcome (Part 2).
        /// X = need to lose, Y = need to draw, Z = need to win
        /// </summary>
        /// <param name="opponent">Opponent's move (A, B, C)</param>
        /// <param name="outcome">Desired outcome (X, Y, Z)</param>
        /// <returns>Your move as X, Y, or Z</returns>
        private char DetermineMove(char opponent, char outcome)
        {
            int oppMove = opponent - 'A';  // 0, 1, or 2

            // Calculate what move to make based on desired outcome
            if (outcome == 'Y')
            {
                // Need to draw - play same shape
                return (char)('X' + oppMove);
            }
            else if (outcome == 'X')
            {
                // Need to lose - play the losing shape
                // Losing shape is (oppMove + 2) % 3
                int losingMove = (oppMove + 2) % 3;
                return (char)('X' + losingMove);
            }
            else  // outcome == 'Z'
            {
                // Need to win - play the winning shape
                // Winning shape is (oppMove + 1) % 3
                int winningMove = (oppMove + 1) % 3;
                return (char)('X' + winningMove);
            }
        }

        /// <summary>
        /// Gets the outcome score directly from the second column (Part 2).
        /// X = lose (0), Y = draw (3), Z = win (6)
        /// </summary>
        /// <param name="outcome">X, Y, or Z</param>
        /// <returns>Outcome score</returns>
        private int GetOutcomeScoreDirect(char outcome)
        {
            if (outcome == 'X') return 0;       // Lose
            else if (outcome == 'Y') return 3;  // Draw
            else return 6;                      // Win
        }

        /// <summary>
        /// Override of abstract method SolvePart1.
        /// Interprets the second column as your shape choice.
        /// </summary>
        /// <returns>Total score for the tournament</returns>
        public override string SolvePart1()
        {
            // Variable to accumulate total score
            int totalScore = 0;
            int roundCount = 0;

            // Loop through each line of input
            foreach (string line in inputLines)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Parse the round - format is "A X" (opponent move, your move)
                string[] parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    char opponent = parts[0][0];
                    char you = parts[1][0];

                    // Calculate score for this round using expressions
                    int shapeScore = GetShapeScore(you);
                    int outcomeScore = GetOutcomeScore(opponent, you);
                    int roundScore = shapeScore + outcomeScore;  // Expression: addition

                    totalScore += roundScore;  // Expression: accumulation
                    roundCount++;
                }
            }

            Console.WriteLine($"  Played {roundCount} rounds of Rock Paper Scissors.");
            return totalScore.ToString("N0");
        }

        /// <summary>
        /// Override of abstract method SolvePart2.
        /// Interprets the second column as the desired outcome.
        /// X = lose, Y = draw, Z = win
        /// </summary>
        /// <returns>Total score when following the strategy</returns>
        public override string SolvePart2()
        {
            int totalScore = 0;
            int roundCount = 0;

            // Loop through each line of input
            foreach (string line in inputLines)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Parse the round
                string[] parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    char opponent = parts[0][0];
                    char desiredOutcome = parts[1][0];

                    // Determine what move to make based on desired outcome
                    char yourMove = DetermineMove(opponent, desiredOutcome);

                    // Calculate score for this round
                    int shapeScore = GetShapeScore(yourMove);
                    int outcomeScore = GetOutcomeScoreDirect(desiredOutcome);
                    int roundScore = shapeScore + outcomeScore;

                    totalScore += roundScore;
                    roundCount++;
                }
            }

            Console.WriteLine($"  Following the strategy for {roundCount} rounds.");
            return totalScore.ToString("N0");
        }

        /// <summary>
        /// Override of virtual DisplayHeader method.
        /// </summary>
        public override void DisplayHeader()
        {
            base.DisplayHeader();
            Console.WriteLine("  Puzzle: Calculate your Rock Paper Scissors tournament score.");
            Console.WriteLine("  Part 1: Second column is your choice (X=Rock, Y=Paper, Z=Scissors)");
            Console.WriteLine("  Part 2: Second column is outcome (X=Lose, Y=Draw, Z=Win)");
        }
    }
}
