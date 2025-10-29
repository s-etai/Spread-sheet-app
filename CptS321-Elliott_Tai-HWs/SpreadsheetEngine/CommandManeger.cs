// <copyright file="CommandManeger.cs" company="Elliott Tai 11844538">
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
    /// Manage the commands with undo and redo stack.
    /// </summary>
    public class CommandManeger
    {
        /// <summary>
        /// Hold commands with most recently executed on top.
        /// </summary>
        private Stack<ICommand> undoStack = new Stack<ICommand>();

        /// <summary>
        /// Hold  commands with most recently undone on top.
        /// </summary>
        private Stack<ICommand> redoStack = new Stack<ICommand>();

        /// <summary>
        /// Event to set iu undo and redo buttons to the correct state.
        /// </summary>
        public event EventHandler<CommandExecutedArgs>? CommandExecuted;

        /// <summary>
        /// Add a command to the top of the undo stack and execute it.
        /// </summary>
        /// <param name="newCommand">The command being added.</param>
        public void AddCommand(ICommand newCommand)
        {
            this.redoStack.Clear(); // Clear redo stack when spreadsheet is changed.
            newCommand.Execute();
            this.undoStack.Push(newCommand);
            this.CommandExecutedInvoke();
        }

        /// <summary>
        /// Undo the most recently executed command by popping off the undo stack, unexecuting, and pushing to redo stack.
        /// </summary>
        public void Undo()
        {
            if (this.undoStack.Count == 0)
            {
                throw new InvalidOperationException("Cannot undo: Undo stack is empty.");
            }

            var command = this.undoStack.Pop();
            command.UnExecute();
            this.redoStack.Push(command);
            this.CommandExecutedInvoke();
        }

        /// <summary>
        /// Empty the undo and redo stack.
        /// </summary>
        public void ClearStacks()
        {
            this.undoStack.Clear();
            this.redoStack.Clear();
            this.CommandExecutedInvoke();
        }

        /// <summary>
        /// Reexecut the most resent undone command by popping from the redo stack, executing, and pushing to redo.
        /// </summary>
        public void Redo()
        {
            if (this.redoStack.Count == 0)
            {
                throw new InvalidOperationException("Cannot redo: Redo stack is empty.");
            }

            var command = this.redoStack.Pop();
            command.Execute();
            this.undoStack.Push(command);
            this.CommandExecutedInvoke();
        }

        /// <summary>
        /// Invoke the event to change ui buttons.
        /// </summary>
        private void CommandExecutedInvoke()
        {
            bool canUndo = this.undoStack.Count > 0;
            bool canRedo = this.redoStack.Count > 0;
            string undoName;
            string redoName;

            if (canUndo)
            {
                undoName = "Undo " + this.undoStack.Peek().GetType().Name;
            }
            else
            {
                undoName = "No undo available";
            }

            if (canRedo)
            {
                redoName = "Redo " + this.redoStack.Peek().GetType().Name;
            }
            else
            {
                redoName = "No redo available";
            }

            this.CommandExecuted?.Invoke(this, new CommandExecutedArgs(canUndo, canRedo, undoName, redoName));
        }
    }
}
