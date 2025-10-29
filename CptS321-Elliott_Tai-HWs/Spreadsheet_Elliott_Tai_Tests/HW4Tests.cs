// <copyright file="HW4Tests.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

namespace Spreadsheet_Elliott_Tai_Tests
{
    /// <summary>
    /// Contians tests for the spreadsheet app.
    /// </summary>
    public class HW4Tests
    {
        /// <summary>
        /// Test GetCell method in a normal case.
        /// </summary>
        [Test]
        public void GetCellNormalTest()
        {
            var spreadsheet = new SpreadsheetEngine.Spreadsheet(10, 10);
            var cell = spreadsheet.GetCell(1, 1);

            Assert.IsNotNull(cell);
        }

        /// <summary>
        /// test GetCell() with cell not in array.
        /// </summary>
        [Test]
        public void GetCellOutOfRangeTest()
        {
            var spreadsheet = new SpreadsheetEngine.Spreadsheet(10, 10);
            var cell = spreadsheet.GetCell(100, 100);

            Assert.IsNull(cell);
        }

        /// <summary>
        /// Test GetCell() with negitive row.
        /// </summary>
        [Test]
        public void GetCellNegitiveRowTest()
        {
            var spreadsheet = new SpreadsheetEngine.Spreadsheet(10, 10);
            Assert.Throws<ArgumentOutOfRangeException>(() => spreadsheet.GetCell(-1, -1));
        }
    }
}