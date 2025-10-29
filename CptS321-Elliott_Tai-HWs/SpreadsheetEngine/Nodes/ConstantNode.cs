// <copyright file="ConstantNode.cs" company="Elliott Tai 11844538">
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
    /// Node that will contain constants.
    /// </summary>
    internal class ConstantNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value">Constant to be stored.</param>
        public ConstantNode(double value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the constant (might never use get or set?).
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Simpily returns the constant, never using the dictonary.
        /// </summary>
        /// <param name="treeVariables">Does nothing with this.</param>
        /// <returns>Its value.</returns>
        public override double Evaluate(Dictionary<string, double> treeVariables)
        {
            return this.Value;
        }
    }
}
