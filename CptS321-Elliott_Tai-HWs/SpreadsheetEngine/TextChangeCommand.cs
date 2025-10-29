// <copyright file="TextChangeCommand.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Command to change the text of a cell.
    /// </summary>
    public class TextChangeCommand : ICommand
    {
        /// <summary>
        /// Ref to the cell being changed.
        /// </summary>
        private Cell cell;

        /// <summary>
        /// The new cell text.
        /// </summary>
        private string newText;

        /// <summary>
        /// The cell text before the change.
        /// </summary>
        private string oldText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextChangeCommand"/> class.
        /// </summary>
        /// <param name="cell">Ref to cell.</param>
        /// <param name="newText">New cell text.</param>
        /// <param name="oldText">Cell text before the change.</param>
        public TextChangeCommand(Cell cell, string newText, string oldText)
        {
            this.cell = cell;
            this.newText = newText;
            this.oldText = oldText;
        }

        /// <summary>
        /// Set the cell's text the the new text.
        /// </summary>
        public void Execute()
        {
            this.cell.Text = this.newText;
        }

        /// <summary>
        /// Revert the cell text to what it was before the change.
        /// </summary>
        public void UnExecute()
        {
            this.cell.Text = this.oldText;
        }
    }
}
