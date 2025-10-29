// <copyright file="BasicExpressionTreeTests.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet_Elliott_Tai_Tests
{
    /// <summary>
    /// Conains test for the expression tree.
    /// </summary>
    internal class BasicExpressionTreeTests
    {
        /// <summary>
        /// Test if the expresion tree evaluates correctly with one operator and 2 constants.
        /// </summary>
        [Test]
        public void SingleOperatorTest()
        {
            string testExpression = "2+2";
            SpreadsheetEngine.ExpressionTree exp = new SpreadsheetEngine.ExpressionTree(testExpression);
            string testResult = exp.Evaluate().ToString();
            Assert.That(testResult, Is.EqualTo("4"));
        }

        /// <summary>
        /// Test if evaluation works on an expression with multiple operators.
        /// </summary>
        [Test]
        public void MultipleOperatorTest()
        {
            string testExpression = "2+2+2";
            SpreadsheetEngine.ExpressionTree exp = new SpreadsheetEngine.ExpressionTree(testExpression);
            string testResult = exp.Evaluate().ToString();
            Assert.That(testResult, Is.EqualTo("6"));
        }

        /// <summary>
        /// Test if the tree is just a single constant that the constant is returned when evaluated.
        /// </summary>
        [Test]
        public void SingleConstantTest()
        {
            string testExpression = "2";
            SpreadsheetEngine.ExpressionTree exp = new SpreadsheetEngine.ExpressionTree(testExpression);
            string testResult = exp.Evaluate().ToString();
            Assert.That(testResult, Is.EqualTo("2"));
        }

        /// <summary>
        /// Test if a variable alone returns its value when evaluated.
        /// </summary>
        [Test]
        public void SingleVariableTest()
        {
            string testExpression = "TestVariable";
            SpreadsheetEngine.ExpressionTree exp = new SpreadsheetEngine.ExpressionTree(testExpression);
            string testResult = exp.Evaluate().ToString();
            Assert.That(testResult, Is.EqualTo("0"));
        }

        /// <summary>
        /// Test if set variable changes variable value correctly.
        /// </summary>
        [Test]
        public void SetVariableTest()
        {
            string testExpression = "TestVariable";
            SpreadsheetEngine.ExpressionTree exp = new SpreadsheetEngine.ExpressionTree(testExpression);
            exp.SetVariable("TestVariable", 2);
            string testResult = exp.Evaluate().ToString();
            Assert.That(testResult, Is.EqualTo("2"));
        }

        /// <summary>
        /// Test expression with constant and variable.
        /// </summary>
        [Test]
        public void MixedExpressionTest()
        {
            string testExpression = "TestVariable+2";
            SpreadsheetEngine.ExpressionTree exp = new SpreadsheetEngine.ExpressionTree(testExpression);
            exp.SetVariable("TestVariable", 2);
            string testResult = exp.Evaluate().ToString();
            Assert.That(testResult, Is.EqualTo("4"));
        }

        /// <summary>
        /// Test that exeption is thrown when expression is empty.
        /// </summary>
        [Test]
        public void EmptyExpressionTest()
        {
            string testExpression = string.Empty;
            Assert.Throws<ArgumentNullException>(() => new SpreadsheetEngine.ExpressionTree(testExpression));
        }

        /// <summary>
        /// Test that exeption is thrown when expression starts with an operator.
        /// </summary>
        [Test]
        public void StartsWithOperatorTest()
        {
            string testExpression = "+2+2";
            Assert.Throws<ArgumentException>(() => new SpreadsheetEngine.ExpressionTree(testExpression));
        }

        /// <summary>
        /// Test that an exeption is thrown when expression ends with an operator.
        /// </summary>
        [Test]
        public void EndsWithOperatorTest()
        {
            string testExpression = "2+2+";
            Assert.Throws<ArgumentException>(() => new SpreadsheetEngine.ExpressionTree(testExpression));
        }

        /// <summary>
        /// Test that exeption is thrown when there are operators one after another.
        /// </summary>
        [Test]
        public void ConsecutiveOperatorsTest()
        {
            string testExpression = "2+2++2";
            Assert.Throws<ArgumentException>(() => new SpreadsheetEngine.ExpressionTree(testExpression));
        }

        /// <summary>
        /// Test that an exeption is thrown when there are multiple operands in a row.
        /// </summary>
        [Test]
        public void ConsecutiveOperandsTest()
        {
            string testExpression = "2+2A+2";
            Assert.Throws<ArgumentException>(() => new SpreadsheetEngine.ExpressionTree(testExpression));
        }
    }
}
