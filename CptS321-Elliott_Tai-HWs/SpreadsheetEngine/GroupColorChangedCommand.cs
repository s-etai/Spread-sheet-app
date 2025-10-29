// <copyright file="GroupColorChangedCommand.cs" company="Elliott Tai 11844538">
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
    /// Command to run all the individual color change commands assosiated with the color change of selected cells.
    /// </summary>
    public class GroupColorChangedCommand : ICommand
    {
        /// <summary>
        /// list of individial cell color change commands.
        /// </summary>
        private List<ICommand> commands = new List<ICommand>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupColorChangedCommand"/> class.
        /// </summary>
        /// <param name="newCommands">List of cell color change commands.</param>
        public GroupColorChangedCommand(List<ICommand> newCommands)
        {
            this.commands = newCommands;
        }

        /// <summary>
        /// Run the execut of all the commands in the list.
        /// </summary>
        public void Execute()
        {
            foreach (var command in this.commands)
            {
                command.Execute();
            }
        }

        /// <summary>
        /// Run the UnExecute of all the commands in the list.
        /// </summary>
        public void UnExecute()
        {
            foreach (var command in this.commands)
            {
                command.UnExecute();
            }
        }
    }
}
