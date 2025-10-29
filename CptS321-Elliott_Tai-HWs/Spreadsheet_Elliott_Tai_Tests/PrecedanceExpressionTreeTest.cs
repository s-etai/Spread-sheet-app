// <copyright file="PrecedanceExpressionTreeTest.cs" company="Elliott Tai 11844538">
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
    /// Contains tests for pecedence.
    /// </summary>
    internal class PrecedanceExpressionTreeTest
    {
        /// <summary>
        /// Test cases where precedence matters and use parenthesis.
        /// </summary>
        /// <param name="expression">Test expression.</param>
        /// <returns>Expected result.</returns>
        [Test]
        [TestCase("1+2*3", ExpectedResult = 7.0)]
        [TestCase("(1+2)*3", ExpectedResult = 9.0)]
        [TestCase("((1+2))*3", ExpectedResult = 9.0)]
        [TestCase("((1+2)*3)", ExpectedResult = 9.0)]
        [TestCase("((((1+2)*(3))))", ExpectedResult = 9.0)]
        [TestCase("(2+3)/(3-1)+4", ExpectedResult = 6.5)]
        [TestCase("(1)", ExpectedResult = 1)]
        public double PrecedanceTest(string expression)
        {
            SpreadsheetEngine.ExpressionTree exp = new SpreadsheetEngine.ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Test setting variables in expression with parenthesis.
        /// </summary>
        [Test]
        public void VariablesInParenthesisTest()
        {
            string testExpression = "(a+1)/(3-1)";
            SpreadsheetEngine.ExpressionTree exp = new SpreadsheetEngine.ExpressionTree(testExpression);
            exp.SetVariable("a", 3);
            string testResult = exp.Evaluate().ToString();
            Assert.That(testResult, Is.EqualTo("2"));
        }

        /// <summary>
        /// Test that an exeption is thrown if parenthesis are mismatched.
        /// </summary>
        [Test]
        public void ParenthesesMismatchTest()
        {
            string testExpression = "(4+3";
            Assert.Throws<System.ArgumentException>(() => new SpreadsheetEngine.ExpressionTree(testExpression));
        }
    }
}
