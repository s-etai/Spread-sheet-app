// <copyright file="NodeFactory.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine.Nodes;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Contains function that returns node bases on argument string.
    /// </summary>
    internal static class NodeFactory
    {
        /// <summary>
        /// To store symbol class pairs of the operator nodes in the assembily.
        /// </summary>
        private static Dictionary<char, Type> operatorNodeTypes = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes static members of the <see cref="NodeFactory"/> class.
        /// look throug all the operator nodes and populate the dictionary with the symboly and it's class.
        /// </summary>
        static NodeFactory()
        {
            // Get all the operator nodes in the assembily.
            var operatorNodeBaseType = typeof(OperatorNode);
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && !t.IsAbstract && operatorNodeBaseType.IsAssignableFrom(t));

            // Add each to the dictionary.
            foreach (var type in types)
            {
                var operatorProperty = type.GetProperty("Operator", BindingFlags.Public | BindingFlags.Static);
                if (operatorProperty != null && operatorProperty.GetValue(null) is char op)
                {
                    operatorNodeTypes[op] = type;
                }
            }
        }

        /// <summary>
        /// Return node based on string.
        /// </summary>
        /// <param name="token">Token to be made into node.</param>
        /// <returns>Constant node of VariableNode.</returns>
        /// <exception cref="ArgumentException">Exeption if token is not costant or variable.</exception>
        public static Node CreateNode(string token)
        {
            if (double.TryParse(token, out double value))
            {
                return new ConstantNode(value);
            }
            else if (char.IsLetter(token[0]))
            {
                return new VariableNode(token);
            }
            else
            {
                throw new ArgumentException("Should pass constant or variable");
            }
        }

        /// <summary>
        /// Depending on the symbol passed make the assosiated operator node.
        /// </summary>
        /// <param name="token">The symbol.</param>
        /// <param name="left">Left child node.</param>
        /// <param name="right">Right child node.</param>
        /// <returns>Instance of operator node.</returns>
        /// <exception cref="InvalidOperationException">Exeption is the constructor is not gotten.</exception>
        public static Node CreateNode(string token, Node left, Node right)
        {
            // Get the correct operator node based on the symbol passed.
            Type type = operatorNodeTypes[token[0]];

            // Get the constructor of the Operator node that is assosiated with this symbol.
            ConstructorInfo? nodeConstuctor = type.GetConstructor(new[] { typeof(Node), typeof(Node) });
            if (nodeConstuctor == null)
            {
                throw new InvalidOperationException();
            }

            // Return the instance of the operator node created.
            return (Node)nodeConstuctor.Invoke(new object[] { left, right });
        }
    }
}
