// <copyright file="VariableNode.cs" company="Elliott Tai 11844538">
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
    /// Store the string key of a variable.
    /// </summary>
    internal class VariableNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name">Variable name from expression.</param>
        public VariableNode(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Return the value assosiated with this node's key in the dictionary.
        /// </summary>
        /// <param name="treeVariables">The dictonary where name value pairs are stored.</param>
        /// <returns>The value associated with this node's name.</returns>
        public override double Evaluate(Dictionary<string, double> treeVariables)
        {
            return treeVariables[this.Name];
        }
    }
}
