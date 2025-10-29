// <copyright file="ICommand.cs" company="Elliott Tai 11844538">
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
    /// Interface for commands, with Execute() and UnExecute().
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute the action.
        /// </summary>
        void Execute();

        /// <summary>
        /// Reverse the Execut().
        /// </summary>
        void UnExecute();
    }
}
