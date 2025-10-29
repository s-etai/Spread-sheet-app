// <copyright file="Node.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Nodes
{
    /// <summary>
    /// Abstract node class for expression tree nodes.
    /// </summary>
    internal abstract class Node
    {
        /// <summary>
        /// Function that retunrs the value of the node (result of tree up to this point).
        /// </summary>
        /// <param name="treeVariables">The dictionary of varible name value pairs.</param>
        /// <returns>Result of tree at this node.</returns>
        public abstract double Evaluate(Dictionary<string, double> treeVariables);
    }
}
