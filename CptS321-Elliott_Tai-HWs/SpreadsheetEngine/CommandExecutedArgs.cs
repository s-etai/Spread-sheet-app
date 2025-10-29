// <copyright file="CommandExecutedArgs.cs" company="Elliott Tai 11844538">
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
    /// Contain the information for the ui to update undo and redo button state.
    /// </summary>
    public class CommandExecutedArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutedArgs"/> class.
        /// </summary>
        /// <param name="canUndo">If undo should be enabled.</param>
        /// <param name="canRedo">If redo should be enabled.</param>
        /// <param name="undoName">Name of undo button.</param>
        /// <param name="redoName">Name of redo button.</param>
        public CommandExecutedArgs(bool canUndo, bool canRedo, string undoName, string redoName)
        {
            this.CanUndo = canUndo;
            this.CanRedo = canRedo;
            this.UndoName = undoName;
            this.RedoName = redoName;
        }

        /// <summary>
        /// Gets a value indicating whether the undo button should be enabled.
        /// </summary>
        public bool CanUndo { get; }

        /// <summary>
        /// Gets a value indicating whether the redo button should be enabled.
        /// </summary>
        public bool CanRedo { get; }

        /// <summary>
        /// Gets the name of the undo button.
        /// </summary>
        public string UndoName { get; }

        /// <summary>
        /// Gets the name of the redo button.
        /// </summary>
        public string RedoName { get; }
    }
}
