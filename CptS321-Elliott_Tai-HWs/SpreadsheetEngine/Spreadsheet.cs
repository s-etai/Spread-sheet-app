// <copyright file="Spreadsheet.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Conain 2D array of cells.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// Keep track of commands with undo and redo stack.
        /// </summary>
        private CommandManeger commandManeger;

        /// <summary>
        /// 2D array to store all the cells in the spread sheet.
        /// </summary>
        private SpreadsheetCell[,] cellArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// Set the size of the 2D array and initialze cells.
        /// </summary>
        /// <param name="newRowCount">Number of rows in the sheet.</param>
        /// <param name="newColumnCount">Number of columns in the sheet.</param>
        public Spreadsheet(int newRowCount, int newColumnCount)
        {
            this.commandManeger = new CommandManeger();
            this.commandManeger.CommandExecuted += this.CommandExecutedHandler;
            this.cellArray = new SpreadsheetCell[newRowCount, newColumnCount];

            for (int row = 0; row < this.RowCount; row++)
            {
                for (int col = 0; col < this.ColumnCount; col++)
                {
                    this.cellArray[row, col] = new SpreadsheetCell(row, col); // initalize each cell.
                    this.cellArray[row, col].PropertyChanged += this.OnCellPropertyChanged; // subscribe to event.
                }
            }
        }

        /// <summary>
        /// Event thing as shown in class slides
        /// </summary>
        public event PropertyChangedEventHandler? CellPropertyChanged;

        /// <summary>
        /// To pass command changed from command maneger to form1.
        /// </summary>
        public event EventHandler<CommandExecutedArgs>? CommandExecuted;

        /// <summary>
        /// Gets rowCount.
        /// </summary>
        public int RowCount { get => this.cellArray.GetLength(0); }

        /// <summary>
        /// Gets columnCount.
        /// </summary>
        public int ColumnCount { get => this.cellArray.GetLength(1); }

        /// <summary>
        /// Call AddCommand() of maneger.
        /// </summary>
        /// <param name="command">Command to be added.</param>
        public void AddCommand(ICommand command)
        {
            this.commandManeger.AddCommand(command);
        }

        /// <summary>
        /// Call Undo() of cammand maneger.
        /// </summary>
        public void Undo()
        {
            this.commandManeger.Undo();
        }

        /// <summary>
        /// Call Redo() of the cammand maneger.
        /// </summary>
        public void Redo()
        {
            this.commandManeger.Redo();
        }

        /// <summary>
        /// Returns the cell at a given location.
        /// </summary>
        /// <param name="row">Row of wanted cell.</param>
        /// <param name="column">Column of wanted cell.</param>
        /// <returns>Cell if it exists.</returns>
        public Cell? GetCell(int row, int column)
        {
            if (row < 0)
            {
                throw new ArgumentOutOfRangeException("Row must be >= 0.", nameof(row));
            }

            if (column < 0)
            {
                throw new ArgumentException("Column must be >= 0.", nameof(column));
            }

            if (row < this.RowCount && column < this.ColumnCount)
            {
                return this.cellArray[row, column];
            }

            return null;
        }

        /// <summary>
        /// Run the demo sesified in instructions.
        /// </summary>
        public void RunDemo()
        {
            for (int i = 0; i < 50; i++) // 50 random cells populated with normal text.
            {
                Random rand = new Random();
                int row = rand.Next(0, 49);
                int column = rand.Next(0, 25);
                this.cellArray[row, column].Text = "Test";
            }

            for (int row = 0; row < 50; row++) // All of column B populated with normal text.
            {
                this.cellArray[row, 1].Text = "This is cell B" + (row + 1);
            }

            for (int row = 0; row < 50; row++) // All of column A set equal to the equivilant cell in B.
            {
                this.cellArray[row, 0].Text = "=B" + (row + 1);
            }
        }

        /// <summary>
        /// Load spreadsheet from xml at passed stream.
        /// </summary>
        /// <param name="stream">The stream of load file from ui.</param>
        public void Load(Stream stream)
        {
            // Reset all cells in the spreadsheet.
            foreach (var cell in this.cellArray)
            {
                cell.Text = null;
                cell.BGColor = 4294967295;
            }

            XDocument doc = XDocument.Load(stream);

            // For each Cell element in the file, set the spreadsheet cell the the values in the xml.
            foreach (XElement xmlCell in doc.Descendants("Cell"))
            {
                string? name = xmlCell.Attribute("Name")?.Value;
                string? text = xmlCell.Element("Text")?.Value;
                string? bgcolor = xmlCell.Element("BGColor")?.Value;

                if (name != null && text != null && bgcolor != null)
                {
                    var sheetCell = this.GetCell(name);
                    sheetCell.Text = text;
                    sheetCell.BGColor = uint.Parse(bgcolor);
                }
                else
                {
                    throw new InvalidDataException("Missing Attribute or Element in <Cell>");
                }
            }

            this.commandManeger.ClearStacks();
        }

        /// <summary>
        /// Save the non defalt cells in a xml, save with the stream passed.
        /// </summary>
        /// <param name="stream">File stream from ui layer.</param>
        public void Save(Stream stream)
        {
            XElement root = new XElement("Spreadsheet");
            XDocument doc = new XDocument(root); // Create new XDocument with root spreadsheet.

            // Iterate through the whole spreadsheet adding the non defalt cells to the xml file.
            foreach (var cell in this.cellArray)
            {
                if (cell.Text != null || cell.BGColor != 4294967295)
                {
                    string cellName = ((char)('A' + cell.ColumnIndex)).ToString() + (cell.RowIndex + 1);
                    root.Add(
                        new XElement(
                            "Cell",
                            new XAttribute("Name", cellName),
                            new XElement("Text", cell.Text),
                            new XElement("BGColor", cell.BGColor)));
                }
            }

            doc.Save(stream);
        }

        /// <summary>
        /// Pass command changed event from command maneger to form1.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Info.</param>
        private void CommandExecutedHandler(object? sender, CommandExecutedArgs e)
        {
            this.CommandExecuted?.Invoke(this, e);
        }

        /// <summary>
        /// Event for outside world that is subscribed to each cells event.
        /// </summary>
        /// <param name="sender">Cell.</param>
        /// <param name="e">Cell info.</param>
        private void OnCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // This if got rid of a style cop, with the is.
            if (sender is not SpreadsheetCell changedCell)
            {
                return;
            }

            if (sender is Cell cell)
            {
                if (e.PropertyName == "Text") // If a cell text chenges evaluate the cell to find and set value.
                {
                    changedCell.Evaluate(this);
                }

                if (e.PropertyName == "Value") // If Cell value changes fire event to ui for ui cell chenge.
                {
                    this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
                }

                if (e.PropertyName == "BGColor") // If Cell color changes fire event to ui for ui cell chenge.
                {
                    this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("BGColor"));
                }
            }
        }

        /// <summary>
        /// takes location string (like A34) and retruns the value at the cell in that location.
        /// </summary>
        /// <param name="location">Location of refernced cell.</param>
        /// <returns>Value of that cell.</returns>
        private Cell GetCell(string location)
        {
            char columnLetter = location[0];
            int row = int.Parse(location.Substring(1));

            int columnNumber = (int)columnLetter;
            columnNumber -= 'A'; // column A is in the 0 column of the 2D array.

            row--; // row numbers are off by one from the index in the array.

            // This got rid of a style cop by returning an empty string if a cell is null.
            var cell = this.GetCell(row, columnNumber);
            if (cell != null)
            {
                return cell;
            }
            else
            {
                throw new ArgumentException("No cell at location", nameof(location));
            }
        }

        /// <summary>
        /// We need cell objects, so this private class inherits from Cell.
        /// </summary>
        private class SpreadsheetCell : Cell
        {
            /// <summary>
            /// Reference to the spreadsheet so cellAtLocation() can be used.
            /// </summary>
            private Spreadsheet? spreadsheetRef;

            /// <summary>
            /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
            /// </summary>
            /// <param name="newRow">Row where the new cell is located.</param>
            /// <param name="newColumn">Column where the new cell is located.</param>
            public SpreadsheetCell(int newRow, int newColumn)
                : base(newRow, newColumn)
            {
            }

            /// <summary>
            /// Gets or Sets value, This value property must be setable and is not in cell.
            /// </summary>
            public new string? Value
            {
                get => base.Value;
                set => base.Value = value;
            }

            /// <summary>
            /// Using the text in the cell calculate and set cell value.
            /// </summary>
            /// <param name="spreadsheet">The spreadsheet so cellAtLocation() can be used.</param>
            public void Evaluate(Spreadsheet spreadsheet)
            {
                // Unsubscribe from everything to prevent errors.
                foreach (Cell update in this.referencedCells)
                {
                    update.PropertyChanged -= this.OnCellPropertyChangedForRefrence;
                }

                this.referencedCells.Clear(); // Clear referenced cells set so that the set onlu contains referenced cells in the current formula.
                this.spreadsheetRef = spreadsheet;
                if (this.Text == null || this.Text == string.Empty)
                {
                    this.Value = string.Empty;
                }
                else if (this.Text.StartsWith("=")) // If the cell starts with '=' use expression tree to evaluate the expression.
                {
                    try
                    {
                        string? cellExpression = this.Text.Substring(1); // string without '='
                        ExpressionTree expressionTree = new ExpressionTree(cellExpression); // Expression tree to evaluate the expression.
                        List<string> variableNames = expressionTree.GetVariableNames(); // list of all references to other cells (A1, B5, ...).
                        foreach (string cellName in variableNames) // Set each variable in the tree to the value of the cell referenced.
                        {
                            if (this.spreadsheetRef.GetCell(cellName) == this)
                            {
                                throw new SelfReferenceException("A cell cannot reference itself.");
                            }

                            if (double.TryParse(this.spreadsheetRef.GetCell(cellName).Value, out var cellValueAtLocation))
                            {
                                expressionTree.SetVariable(cellName, cellValueAtLocation);
                                this.referencedCells.Add(this.spreadsheetRef.GetCell(cellName));
                            }
                            else
                            {
                                expressionTree.SetVariable(cellName, 0);
                                this.referencedCells.Add(this.spreadsheetRef.GetCell(cellName));
                            }
                        }

                        this.Value = expressionTree.Evaluate().ToString(); // Set the value of the cell to the result of the evaluated tree.

                        // Subscribe to all cells referenced so this cell can be reevaluated when referenced cells change.
                        foreach (Cell update in this.referencedCells)
                        {
                            update.PropertyChanged += this.OnCellPropertyChangedForRefrence;
                        }

                        // Check for circular reference and throw error if detected.
                        if (this.HasCircularReference())
                        {
                            throw new CircularReferenceException("A cell cannot reference itself.");
                        }
                    }

                    // Display self reference error if self reference exeption is thrown.
                    catch (SelfReferenceException)
                    {
                        this.Value = "!(Self Reference)";
                    }

                    // Display circular reference error if that exception is thrown.
                    catch (CircularReferenceException)
                    {
                        this.Value = "!(Circular Reference)";
                    }

                    // Display bed reference error if any othere exeption is thrown.
                    catch
                    {
                        this.Value = "!(Bad Reference)";
                    }
                }
                else
                {
                    this.Value = this.Text; // Set value to text if not an expression.
                }
            }

            /// <summary>
            /// Checks if current cell has a circular reference by calling the recersive function.
            /// </summary>
            /// <returns>If this cell has a circular reference.</returns>
            private bool HasCircularReference()
            {
                return this.HasCircularReference(this, this, new HashSet<Cell>());
            }

            /// <summary>
            /// Recuersivley deturmine if the current cell has a circular reference.
            /// </summary>
            /// <param name="startCell">This cell.</param>
            /// <param name="currentCell">The cell we a checking.</param>
            /// <param name="visited">Set of all cells visited so far.</param>
            /// <param name="isFirstCall">Is this the first call of the function.</param>
            /// <returns>If there is a circular reference.</returns>
            private bool HasCircularReference(Cell startCell, Cell currentCell, HashSet<Cell> visited, bool isFirstCall = true)
            {
                if (!isFirstCall && startCell == currentCell)
                {
                    return true;
                }

                if (visited.Contains(currentCell))
                {
                    return false;
                }

                visited.Add(currentCell);

                foreach (var dep in currentCell.GetDependancies())
                {
                    if (this.HasCircularReference(startCell, dep, visited, false))
                    {
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Event handeler to reevaluate the text in a node when a referenced cell changes value.
            /// </summary>
            /// <param name="sender">Changed cell.</param>
            /// <param name="e">Changed cell information.</param>
            private void OnCellPropertyChangedForRefrence(object? sender, PropertyChangedEventArgs e)
            {
                if (this.spreadsheetRef != null)
                {
                    // Do not reevaluate this cell if the cell invoking the event has a circular reference.
                    Cell? senderCell = sender as Cell;
                    if (senderCell != null && senderCell.Value != "!(Circular Reference)")
                    {
                        this.Evaluate(this.spreadsheetRef);
                    }
                }
            }
        }
    }
}
