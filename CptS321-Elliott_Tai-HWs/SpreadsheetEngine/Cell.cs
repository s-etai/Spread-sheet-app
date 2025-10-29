// <copyright file="Cell.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Formats.Asn1;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Store user input text, displayed value, and it's row and column.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Store all cells that this cell references.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        protected HashSet<Cell> referencedCells = new HashSet<Cell>();
#pragma warning restore SA1401 // Fields should be private

        /// <summary>
        /// Store the user input text.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private, Protected in assigment instructions.
        protected string? text;
#pragma warning restore SA1401 // Fields should be private

        /// <summary>
        /// Store the value to be displayed. Protected in instructions.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        protected string? value;
#pragma warning restore SA1401 // Fields should be private

        /// <summary>
        /// Background color of a cell.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        protected uint bgColor = 0xFFFFFFFF;
#pragma warning restore SA1401 // Fields should be private

        /// <summary>
        /// Store the row of the cell, read only.
        /// </summary>
        private int rowIndex;

        /// <summary>
        /// Store the column of the cell, read only.
        /// </summary>
        private int columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// set row and column the only set.
        /// </summary>
        /// <param name="newRow"> The row of the cell.</param>
        /// <param name="newColumn">The column of the new cell.</param>
        public Cell(int newRow, int newColumn)
        {
            this.rowIndex = newRow;
            this.columnIndex = newColumn;
        }

        /// <summary>
        /// Event thing as shown in class slides.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets or Sets text, when set (if text changed, event fire).
        /// </summary>
        public string? Text
        {
            get => this.text;

            set
            {
                this.text = value;
                this.CellPropertyChanged(nameof(this.Text));
            }
        }

        /// <summary>
        /// Gets or Sets value, fires event on set.
        /// </summary>
        public string? Value
        {
            get => this.value;
            protected set
            {
                this.value = value;
                this.CellPropertyChanged(nameof(this.Value));
            }
        }

        /// <summary>
        /// Gets or sets the background color of the cell.
        /// </summary>
        public uint BGColor
        {
            get => this.bgColor;
            set
            {
                this.bgColor = value;
                this.CellPropertyChanged(nameof(this.BGColor));
            }
        }

        /// <summary>
        /// Gets rowindex, only get no set.
        /// </summary>
        public int RowIndex // property
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets columnIndex, only get no set.
        /// </summary>
        public int ColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Return the set of dependancies.
        /// </summary>
        /// <returns>Set of depandancies.</returns>
        public HashSet<Cell> GetDependancies()
        {
            return this.referencedCells;
        }

        /// <summary>
        /// Fire event based on passed name.
        /// </summary>
        /// <param name="name">Name of proporty changed.</param>
        private void CellPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}