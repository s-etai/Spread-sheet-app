// <copyright file="ExpressionTree.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using SpreadsheetEngine.Nodes;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Contains functionality for the expression tree, used to evaluate equations.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Dictonary to store variable name value pairs.
        /// </summary>
        private Dictionary<string, double> treeVariables = new Dictionary<string, double>();

        /// <summary>
        /// Root node of the tree.
        /// </summary>
        private Node? root = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
    /// Builds tree in memory with the expression argument.
    /// </summary>
    /// <param name="expression">The expression to be turned into the expression tree.</param>
        public ExpressionTree(string expression)
        {
            this.BuildExpressionTree(expression);
        }

        /// <summary>
        /// Sets the value in the dictionary of the coresponding key passed.
        /// </summary>
        /// <param name="variableName">The the key for the value we want to change.</param>
        /// <param name="variableValue">The value we want to chage.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.treeVariables[variableName] = variableValue;
        }

        /// <summary>
        /// Evaluate the expression tree.
        /// </summary>
        /// <returns>A double that is the result of evaluating the expression tree.</returns>
        public double Evaluate()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException("Expression tree root is null.");
            }

            return this.root.Evaluate(this.treeVariables);
        }

        /// <summary>
        /// Give the outside world a list of variable names so that variables can be set.
        /// </summary>
        /// <returns>List of all the variable names.</returns>
        public List<string> GetVariableNames()
        {
            var keys = this.treeVariables.Keys;
            List<string> names = keys.ToList();
            return names;
        }

        /// <summary>
        /// Build an expression tree by converting the expression to postfix using shunting yard,
        /// then construct the tree using post fix stack evaluation.
        /// </summary>
        /// <param name="expression">Expression in infix.</param>
        /// <exception cref="ArgumentNullException">Throw if expresion is empty.</exception>
        /// <exception cref="ArgumentException">Throw if expression does not use binary operators correctly.</exception>
        private void BuildExpressionTree(string expression)
        {
            // If expression is empty, throw exeption.
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException("expression cannot be null.", nameof(expression));
            }

            List<string> tokensInfix = this.TokenizeExpression(expression);

            // Throw exeptions if there are parenthesis mismatches in the infix expression.
            this.ParenthesisMismatchCheck(tokensInfix);

            // Parse expression string into a list of constants, variables, or operators in postfix.
            List<string> tokensPostfix = this.ToPostfix(tokensInfix);

            // Stack for building tree from post fix expression.
            Stack<Node> evaluationStack = new Stack<Node>();

            // Loop through the token list adding constant and variables straight to the stack.
            // Add operator node to stack with popping top two nodes for the children of the oprator node.
            for (int i = 0; i < tokensPostfix.Count; i++)
            {
                // If the token is a constant push a constant node to the stack.
                if (this.IsConstant(tokensPostfix[i]))
                {
                    evaluationStack.Push(NodeFactory.CreateNode(tokensPostfix[i]));
                }

                // If token is a variable, add to the dictonary and add not to the stack.
                else if (this.IsVariable(tokensPostfix[i]))
                {
                    if (!this.treeVariables.ContainsKey(tokensPostfix[i]))
                    {
                        this.treeVariables.Add(tokensPostfix[i], 0);
                    }

                    evaluationStack.Push(NodeFactory.CreateNode(tokensPostfix[i]));
                }

                // only tokens left must be operators.
                else
                {
                    // Throw exeption if expression does not use binary operators correctly.
                    if (evaluationStack.Count < 2)
                    {
                        throw new ArgumentException("Expression must follow pattern: operand(operator operand)*");
                    }

                    // The top node on the stack becoms the right childe of the operator node.
                    Node rightChild = evaluationStack.Pop();

                    // the second to the top becoms the left child of the operator node.
                    Node leftChild = evaluationStack.Pop();

                    // Push new operator node to the top of the stack.
                    evaluationStack.Push(NodeFactory.CreateNode(tokensPostfix[i], leftChild, rightChild));
                }
            }

            // Throw exeption if expression does not use binary operators correctly.
            if (evaluationStack.Count != 1)
            {
                throw new ArgumentException("Expression must follow pattern: operand(operator operand)*");
            }

            // The single node that is in the stack is the root of the tree, set this to root.
            this.root = evaluationStack.Pop();
        }

        /// <summary>
        /// Return a list of constants, variables, parenthesis, and operators in the order they were in the expression string.
        /// </summary>
        /// <param name="expression">Expression string.</param>
        /// <returns>List of tokens.</returns>
        private List<string> TokenizeExpression(string expression)
        {
            // Use regular expression to parse the string.
            string pattern = @"\d+(\.\d+)?|[a-zA-Z][a-zA-Z0-9]*|[+\-*/()]";
            MatchCollection matches = Regex.Matches(expression, pattern);
            List<string> result = new List<string>();
            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }

            return result;
        }

        /// <summary>
        /// Deturmine if a token is a constant.
        /// </summary>
        /// <param name="token">Token in string form.</param>
        /// <returns>Bool if token is a constant.</returns>
        private bool IsConstant(string token)
        {
            return double.TryParse(token, out double value);
        }

        /// <summary>
        /// Deturmine if token is a variable by checking if the first char is a alphebetical character.
        /// </summary>
        /// <param name="token">Token in the form of a string.</param>
        /// <returns>Bool if token is a variable or not.</returns>
        private bool IsVariable(string token)
        {
            return char.IsLetter(token[0]);
        }

        /// <summary>
        /// Turn a infix expression into a postfix expression using shunting yard.
        /// Working with lists of tokens from parse function.
        /// </summary>
        /// <param name="infixExpression">The infix expression as a list of strings.</param>
        /// <returns>The expression in postfix as a list of strings.</returns>
        private List<string> ToPostfix(List<string> infixExpression)
        {
            // Store the pesidence for each operator.
            // populate with refection.
            Dictionary<char, int> operatorPrecedence = new Dictionary<char, int>();

            // Get all operator nodes in the assembily.
            var operatorNodeType = typeof(OperatorNode);
            var operatorTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(operatorNodeType) && !t.IsAbstract);

            // For each add the symbol and precedence to the dictionary.
            foreach (var operatorType in operatorTypes)
            {
                // Get variables from the class.
                var operatorProp = operatorType.GetProperty("Operator", BindingFlags.Public | BindingFlags.Static);
                var precedenceProp = operatorType.GetProperty("Precedence", BindingFlags.Public | BindingFlags.Static);

                object? opObj = operatorProp?.GetValue(null);
                object? precedenceObj = precedenceProp?.GetValue(null);

                // Add the variables to the dictionary.
                if (opObj is char op && precedenceObj is int precedence)
                {
                    operatorPrecedence[op] = precedence;
                }
            }

            // The operator stack in the shunting yard method.
            Stack<char> operatorStack = new Stack<char>();

            // The output string
            List<string> postfixExpression = new List<string>();

            // Go through the whole infix list doing the shunting yard method.
            // number and variable go straight into the output, operators push and pop from stack depending on precedence.
            for (int i = 0; i < infixExpression.Count; i++)
            {
                // When encountered, Constants or variables get added to the result list no mater what.
                if (this.IsConstant(infixExpression[i]) || this.IsVariable(infixExpression[i]))
                {
                    postfixExpression.Add(infixExpression[i]);
                }

                // "(" Always gets pushed onto the stack when encountered.
                else if (infixExpression[i] == "(")
                {
                    operatorStack.Push(infixExpression[i][0]);
                }

                // When a ")" is encountered the operators stack is poped into the result intill a ")" is encountered in the stack.
                else if (infixExpression[i] == ")")
                {
                    string topOperator = operatorStack.Pop().ToString();
                    while (topOperator != "(")
                    {
                        postfixExpression.Add(topOperator);
                        topOperator = operatorStack.Pop().ToString();
                    }
                }

                // At this point in the is else the token must be an operator.
                else
                {
                    // Pop operators of higher or equal precedence off the stack and into the result.
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '(' && operatorPrecedence[operatorStack.Peek()] >= operatorPrecedence[infixExpression[i][0]])
                    {
                        postfixExpression.Add(operatorStack.Pop().ToString());
                    }

                    // Push the new operator to the stack.
                    operatorStack.Push(infixExpression[i][0]);
                }
            }

            // Pop the rest of the stack to the result expression.
            while (operatorStack.Count > 0)
            {
                postfixExpression.Add(operatorStack.Pop().ToString());
            }

            return postfixExpression;
        }

        /// <summary>
        /// Throw exeption if parenthesis are mismathched.
        /// </summary>
        /// <param name="infixExpression">Infix Expression as a list of strings.</param>
        /// <exception cref="ArgumentException">Throw if there is a mismatch.</exception>
        private void ParenthesisMismatchCheck(List<string> infixExpression)
        {
            int unMatchedOpens = 0; // The number of "(" that have not yet been paired with a ")"

            // Go through the whole expression looking for parenthesis.
            for (int i = 0; i < infixExpression.Count; i++)
            {
                if (infixExpression[i] == "(")
                {
                    unMatchedOpens++;
                }

                if (infixExpression[i] == ")")
                {
                    unMatchedOpens--;
                }

                // It the int ever goes negitive the parenthesis are mismatched,
                // ")(" would result in unMatchedOpens being 0 at the end but this is a mismatch.
                if (unMatchedOpens < 0)
                {
                    throw new ArgumentException("Unmatched closing parenthesis");
                }
            }

            // If there a parenthesis with out a mate there is a mismatch.
            if (unMatchedOpens > 0)
            {
                throw new ArgumentException("Unmatched opening parenthesis");
            }
        }
    }
}
