// <copyright file="CircularReferenceTests.cs" company="Elliott Tai 11844538">
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
    /// Contains test for circular references.
    /// </summary>
    internal class CircularReferenceTests
    {
        /// <summary>
        /// Test if invalid cell name is handled correctly.
        /// </summary>
        [Test]
        public void BadCellNameTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(0, 0);
            if (testCell != null)
            {
                testCell.Text = "=Ba";
                Assert.That(testCell.Value, Is.EqualTo("!(Bad Reference)"));
            }
        }

        /// <summary>
        /// Test if reference to cell not in the range of the sheet is handled.
        /// </summary>
        [Test]
        public void CellBeyondRangeTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(0, 0);
            if (testCell != null)
            {
                testCell.Text = "=Z12345";
                Assert.That(testCell.Value, Is.EqualTo("!(Bad Reference)"));
            }
        }

        /// <summary>
        /// Test that if a cell references it self that error is displayed.
        /// </summary>
        [Test]
        public void SelfReferenceTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCell = testSheet.GetCell(0, 0);
            if (testCell != null)
            {
                testCell.Text = "=A1";
                Assert.That(testCell.Value, Is.EqualTo("!(Self Reference)"));
            }
        }

        /// <summary>
        /// Test that error is displayed in the cell that causes a circular reference.
        /// </summary>
        [Test]
        public void CircularReferenceTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCellA1 = testSheet.GetCell(0, 0);
            SpreadsheetEngine.Cell? testCellB1 = testSheet.GetCell(0, 1);
            if (testCellA1 != null && testCellB1 != null)
            {
                testCellA1.Text = "=B1";
                testCellB1.Text = "=A1";
                Assert.That(testCellB1.Value, Is.EqualTo("!(Circular Reference)"));
            }
        }

        /// <summary>
        /// Test that circular Reference error is displayed when a complex circular reference is created.
        /// </summary>
        [Test]
        public void ComplexCircularReferenceTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCellA1 = testSheet.GetCell(0, 0);
            SpreadsheetEngine.Cell? testCellB1 = testSheet.GetCell(0, 1);
            SpreadsheetEngine.Cell? testCellC1 = testSheet.GetCell(0, 2);
            if (testCellA1 != null && testCellB1 != null && testCellC1 != null)
            {
                testCellA1.Text = "=B1*3+C1+4";
                testCellB1.Text = "=C1+100";
                testCellC1.Text = "=100*(3/2+A1)-B1*2";
                Assert.That(testCellC1.Value, Is.EqualTo("!(Circular Reference)"));
            }
        }

        /// <summary>
        /// Test that a reference to a cell with no numerical value sets that variable to 0 in expression tree.
        /// </summary>
        [Test]
        public void RefToCellWithNoNumericalValueTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.Cell? testCellA1 = testSheet.GetCell(0, 0);
            SpreadsheetEngine.Cell? testCellB1 = testSheet.GetCell(0, 1);
            if (testCellA1 != null && testCellB1 != null)
            {
                testCellA1.Text = "not a numerical value";
                testCellB1.Text = "=A1+4";
                Assert.That(testCellB1.Value, Is.EqualTo("4"));
            }
        }
    }
}
