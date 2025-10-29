// <copyright file="CellReferenceTests.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine;

namespace Spreadsheet_Elliott_Tai_Tests
{
    /// <summary>
    /// Contain tests for hw 7.
    /// </summary>
    internal class CellReferenceTests
    {
        /// <summary>
        /// Test if cell value is same as text for standard text not expression.
        /// </summary>
        [Test]
        public void TextOnlyTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(1, 1); // Get a reference to a cell in the tree.
            if (testCell != null)
            {
                testCell.Text = "test text";
                Assert.That(testCell.Value, Is.EqualTo("test text"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test an expression that is a single constant.
        /// </summary>
        [Test]
        public void SingleConstantExpressionTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(1, 1);
            if (testCell != null)
            {
                testCell.Text = "1";
                Assert.That(testCell.Value, Is.EqualTo("1"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// test expression evaluation with multiple constants.
        /// </summary>
        [Test]
        public void MultipleConstantExpressionTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(1, 1);
            if (testCell != null)
            {
                testCell.Text = "=1+2*3";
                Assert.That(testCell.Value, Is.EqualTo("7"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test if one cell can reference another.
        /// </summary>
        [Test]
        public void SimpleReferenceTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(1, 1);
            SpreadsheetEngine.Cell? testCell2 = testSheet.GetCell(1, 2);
            if (testCell != null && testCell2 != null)
            {
                testCell.Text = "10";
                testCell2.Text = "=B2*2";
                Assert.That(testCell2.Value, Is.EqualTo("20"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test if cell referencing another cell changes when the referenced cell is changed.
        /// </summary>
        [Test]
        public void ReferenceUpdateTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(1, 1);
            SpreadsheetEngine.Cell? testCell2 = testSheet.GetCell(1, 2);
            if (testCell != null && testCell2 != null)
            {
                testCell.Text = "10";
                testCell2.Text = "=B2*2";
                testCell.Text = "5";
                Assert.That(testCell2.Value, Is.EqualTo("10"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test if a variable can be used multiple times in one expression.
        /// </summary>
        [Test]
        public void DuplicateVariableExpressionTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(1, 1);
            SpreadsheetEngine.Cell? testCell2 = testSheet.GetCell(1, 2);
            if (testCell != null && testCell2 != null)
            {
                testCell.Text = "10";
                testCell2.Text = "=B2+B2";
                Assert.That(testCell2.Value, Is.EqualTo("20"));
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
