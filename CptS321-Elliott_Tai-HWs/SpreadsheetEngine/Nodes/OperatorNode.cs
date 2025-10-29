// <copyright file="OperatorNode.cs" company="Elliott Tai 11844538">
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
    /// Conatian and operation and the two child nodes.
    /// </summary>
    internal abstract class OperatorNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="newOperator">Char for operator.</param>
        /// <param name="newLeft">Left node.</param>
        /// <param name="newRight">Right node.</param>
        public OperatorNode(Node newLeft, Node newRight)
        {
            this.Left = newLeft;
            this.Right = newRight;
        }

        /// <summary>
        /// Gets or sets the left child node.
        /// </summary>
        public Node Left { get; set; }

        /// <summary>
        /// Gets or sets the right child node.
        /// </summary>
        public Node Right { get; set; }

        /// <summary>
        /// Returns the result of doing the operation on the left and right child node.
        /// </summary>
        /// <param name="treeVariables">Does nothing with this.</param>
        /// <returns>The result of the operation on the left and right child node.</returns>
        public override abstract double Evaluate(Dictionary<string, double> treeVariables);
    }
}
