// <copyright file="CellColorChangeCommand.cs" company="Elliott Tai 11844538">
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
    /// Command for changing the color of a single cell.
    /// </summary>
    public class CellColorChangeCommand : ICommand
    {
        /// <summary>
        /// Ref to the cell that need to be changed.
        /// </summary>
        private Cell changedCell;

        /// <summary>
        /// The color of the cell before the change.
        /// </summary>
        private uint oldColor;

        /// <summary>
        /// The new color of the cell.
        /// </summary>
        private uint newColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellColorChangeCommand"/> class.
        /// </summary>
        /// <param name="changedCell">Ref to changed cell.</param>
        /// <param name="newColor">New color of cell.</param>
        /// <param name="oldColor">The color of the cell before the change.</param>
        public CellColorChangeCommand(Cell changedCell, uint newColor, uint oldColor)
        {
            this.changedCell = changedCell;
            this.oldColor = oldColor;
            this.newColor = newColor;
        }

        /// <summary>
        /// Set the cell color to the new color.
        /// </summary>
        public void Execute()
        {
            this.changedCell.BGColor = this.newColor;
        }

        /// <summary>
        /// Set the cell's color to what is was before the change.
        /// </summary>
        public void UnExecute()
        {
            this.changedCell.BGColor = this.oldColor;
        }
    }
}
