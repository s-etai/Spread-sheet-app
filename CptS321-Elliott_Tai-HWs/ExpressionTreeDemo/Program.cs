// <copyright file="Program.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System.Transactions;

namespace ExpressionTreeDemo
{
    /// <summary>
    /// Contains all expession tree demo functionality.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Main for Expression tree demo.
        /// </summary>
        private static void Main()
        {
            int userOption = -1; // Store user input.
            string? expression = "A1+12+C1"; // string to store expression, set to a defalt expression.
            var expressionTree = new SpreadsheetEngine.ExpressionTree(expression);

            // Outer do while prints menue after every option selection till 4 is entered.
            do
            {
                // ask for input till valide option input is entered.
                do
                {
                    PrintMenu(expression);
                    string? userInput = Console.ReadLine();
                    int.TryParse(userInput, out userOption); // If try parse fails, userOption is test to 0 so the loop happens.
                }
                while (userOption < 1 || userOption > 4);

                switch (userOption)
                {
                    case 1:
                        Console.Write("Enter new expression: ");
                        expression = Console.ReadLine();
                        if (expression == null)
                        {
                            throw new InvalidOperationException("Expression is null.");
                        }

                        expressionTree = new SpreadsheetEngine.ExpressionTree(expression);
                        break;
                    case 2:
                        Console.Write("Enter variable name: ");
                        string? newName = Console.ReadLine();
                        Console.Write("Entervariable value: ");
                        double.TryParse(Console.ReadLine(), out double newValue);
                        if (newName == null)
                        {
                            throw new InvalidOperationException("newValue is null");
                        }

                        expressionTree.SetVariable(newName, newValue);
                        break;
                    case 3:
                        Console.WriteLine(expressionTree.Evaluate());
                        break;
                }
            }
            while (userOption != 4);
        }

        /// <summary>
        /// Print menu with current expression.
        /// </summary>
        /// <param name="currentExpression">The current expression.</param>
        private static void PrintMenu(string currentExpression)
        {
            Console.WriteLine("Menu (current expression= " + currentExpression + ")");
            Console.WriteLine("1 = Enter a new expression");
            Console.WriteLine("2 = Set a variable value");
            Console.WriteLine("3 = Evaluate tree");
            Console.WriteLine("4 = Quit");
        }
    }
}