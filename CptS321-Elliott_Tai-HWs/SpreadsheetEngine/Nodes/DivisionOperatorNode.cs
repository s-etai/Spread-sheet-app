// <copyright file="DivisionOperatorNode.cs" company="Elliott Tai 11844538">
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
    /// Node for the division operator.
    /// </summary>
    internal class DivisionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionOperatorNode"/> class.
        /// </summary>
        /// <param name="newOperator">New operator.</param>
        /// <param name="newLeft">left child.</param>
        /// <param name="newRight">right child.</param>
        public DivisionOperatorNode(Node newLeft, Node newRight)
            : base(newLeft, newRight)
        {
        }

        /// <summary>
        /// Gets precedence of operator.
        /// </summary>
        public static int Precedence { get; } = 1;

        /// <summary>
        /// Gets the symbol assosiated with this node.
        /// </summary>
        public static char Operator { get; } = '/';

        /// <summary>
        /// Get the result of the division of the 2 sub trees.
        /// </summary>
        /// <param name="treeVariables">Dictionary not used.</param>
        /// <returns>The result of division.</returns>
        public override double Evaluate(Dictionary<string, double> treeVariables)
        {
            return this.Left.Evaluate(treeVariables) / this.Right.Evaluate(treeVariables);
        }
    }
}
