// <copyright file="UndoTests.cs" company="Elliott Tai 11844538">
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
    /// Contains test for undo and redo.
    /// </summary>
    internal class UndoTests
    {
        /// <summary>
        /// Test single text undo.
        /// </summary>
        [Test]
        public void SimpleUndoTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            var testCell = testSheet.GetCell(1, 1);
            if (testCell != null)
            {
                var textSetCommand = new TextChangeCommand(testCell, "10", string.Empty);
                testSheet.AddCommand(textSetCommand);
                testSheet.Undo();
                Assert.That(testCell.Text, Is.EqualTo(string.Empty));
            }
        }

        /// <summary>
        /// Test single redo after undo.
        /// </summary>
        [Test]
        public void SimpleRedoTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            var testCell = testSheet.GetCell(1, 1);
            if (testCell != null)
            {
                var textSetCommand = new TextChangeCommand(testCell, "10", string.Empty);
                testSheet.AddCommand(textSetCommand);
                testSheet.Undo();
                testSheet.Redo();
                Assert.That(testCell.Text, Is.EqualTo("10"));
            }
        }

        /// <summary>
        /// Test if command can be run after undo.
        /// </summary>
        [Test]
        public void CommandAfterUndoTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            var testCell = testSheet.GetCell(1, 1);
            if (testCell != null)
            {
                var textSetCommand = new TextChangeCommand(testCell, "10", string.Empty);
                testSheet.AddCommand(textSetCommand);
                testSheet.Undo();
                textSetCommand = new TextChangeCommand(testCell, "20", string.Empty);
                testSheet.AddCommand(textSetCommand);
                Assert.That(testCell.Text, Is.EqualTo("20"));
            }
        }

        /// <summary>
        /// Test if exeption is thrown when undo is called and the undo stack is empty.
        /// </summary>
        [Test]
        public void UndoOnEmptyStackTest()
        {
            SpreadsheetEngine.Spreadsheet testSheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            var testCell = testSheet.GetCell(1, 1);
            if (testCell != null)
            {
                var textSetCommand = new TextChangeCommand(testCell, "10", string.Empty);
                testSheet.AddCommand(textSetCommand);
                testSheet.Undo();
                Assert.Throws<InvalidOperationException>(() => testSheet.Undo());
            }
        }
    }
}
