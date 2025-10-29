// <copyright file="AdditionOperatorNode.cs" company="Elliott Tai 11844538">
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
    /// Node for the addition operator.
    /// </summary>
    internal class AdditionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionOperatorNode"/> class.
        /// </summary>
        /// <param name="newOperator">New operator.</param>
        /// <param name="newLeft">left child.</param>
        /// <param name="newRight">right child.</param>
        public AdditionOperatorNode(Node newLeft, Node newRight)
            : base(newLeft, newRight)
        {
        }

        /// <summary>
        /// Gets precedence of operator.
        /// </summary>
        public static int Precedence { get; } = 0;

        /// <summary>
        /// Gets the symbol assosiated with this node.
        /// </summary>
        public static char Operator { get; } = '+';

        /// <summary>
        /// Return the sum of the sub trees.
        /// </summary>
        /// <param name="treeVariables">The dictionary is not used.</param>
        /// <returns>Sum.</returns>
        public override double Evaluate(Dictionary<string, double> treeVariables)
        {
            return this.Left.Evaluate(treeVariables) + this.Right.Evaluate(treeVariables);
        }
    }
}
