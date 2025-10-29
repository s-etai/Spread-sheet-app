// <copyright file="Form1.cs" company="Elliott Tai 11844538">
// Copyright (c) Elliott Tai 11844538. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Drawing;
using System.Xml.Linq;
using SpreadsheetEngine;

namespace Spreadsheet_Elliott_Tai
{
    /// <summary>
    /// Create instance of spreadsheet and run demo.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Spreadsheet that will be used in the app.
        /// </summary>
        private SpreadsheetEngine.Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// set data grig view to correct size.
        /// Initialize spreadsheet object and subscribe to event.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
            this.spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            this.spreadsheet.CellPropertyChanged += this.UIUpdateCellPropertyChanged; // Subscribe to event from speadsheet class.
            this.spreadsheet.CommandExecuted += this.CommandExecutedHandler;
            this.dataGridView1.CellBeginEdit += this.DataGridViewCellBeginEdit; // Subscribe to CellBeginEdit for showing text when grid cell is edited.
            this.dataGridView1.CellEndEdit += this.DataGridViewCellEndEdit; // Subscribe to CellEndEdit to set the text of the cell in spreadsheet object.
        }

        /// <summary>
        /// Display cell test and not value when cell is being edit.
        /// </summary>
        /// <param name="sender">Spreadsheet grid object.</param>
        /// <param name="e">Information.</param>
        private void DataGridViewCellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            Cell? cell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);

            if (cell != null)
            {
                this.dataGridView1[cell.ColumnIndex, cell.RowIndex].Value = cell.Text;
            }
        }

        /// <summary>
        /// Change text field of cell to user input when editing is done.
        /// </summary>
        /// <param name="sender">Spreadsheet grid object.</param>
        /// <param name="e">Information.</param>
        private void DataGridViewCellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            string newText = this.dataGridView1[e.ColumnIndex, e.RowIndex].Value?.ToString() ?? string.Empty;
            Cell? cell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);
            if (cell != null)
            {
                if (cell.Text != null)
                {
                    var command = new TextChangeCommand(cell, newText, cell.Text);
                    this.spreadsheet.AddCommand(command);
                }
                else
                {
                    var command = new TextChangeCommand(cell, newText, string.Empty);
                    this.spreadsheet.AddCommand(command);
                }
            }
        }

        /// <summary>
        /// Make columns A-Z and rows 1-50.
        /// </summary>
        private void InitializeDataGrid()
        {
            // Clear manuly added columns.
            this.dataGridView1.Columns.Clear();

            // Make comlumns A - Z with Add().
            for (char c = 'A'; c <= 'Z'; c++)
            {
                this.dataGridView1.Columns.Add(c.ToString(), c.ToString());
            }

            // Make 50 rows
            for (int i = 1; i <= 50; i++)
            {
                int rowNumber = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[rowNumber].HeaderCell.Value = i.ToString();
            }
        }

        /// <summary>
        /// Update the undo and redo buttons to the correct name and enable and disable.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Info.</param>
        private void CommandExecutedHandler(object? sender, CommandExecutedArgs e)
        {
            this.undoToolStripMenuItem.Enabled = e.CanUndo;
            this.redoToolStripMenuItem.Enabled = e.CanRedo;
            this.undoToolStripMenuItem.Text = e.UndoName;
            this.redoToolStripMenuItem.Text = e.RedoName;
        }

        /// <summary>
        /// Event to change ui when ever cell is changed.
        /// </summary>
        /// <param name="sender">CellObject.</param>
        /// <param name="e">CellObject information.</param>
        private void UIUpdateCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is Cell cell)
            {
                if (e.PropertyName == "Value")
                {
                    this.dataGridView1[cell.ColumnIndex, cell.RowIndex].Value = cell.Value;
                }

                if (e.PropertyName == "BGColor")
                {
                    this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = Color.FromArgb((int)cell.BGColor);
                }
            }
        }

        /// <summary>
        /// Run demo on button click.
        /// </summary>
        /// <param name="sender">Events stuff.</param>
        /// <param name="e">Event Stuff.</param>
        private void Demo_Click(object sender, EventArgs e)
        {
            this.spreadsheet.RunDemo();
        }

        /// <summary>
        /// Change color of selected cells to the selected color.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Info.</param>
        private void SetColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK) // Color dialog display and color selected.
            {
                List<ICommand> changedCellCommands = new List<ICommand>(); // list of commands for each cell color change.
                Color selectedColor = this.colorDialog1.Color;

                // For each cell selected, create it's color change command and add it to the list of commands.
                foreach (DataGridViewCell gridCell in this.dataGridView1.SelectedCells)
                {
                    var cell = this.spreadsheet.GetCell(gridCell.RowIndex, gridCell.ColumnIndex);
                    if (cell != null)
                    {
                        var cellChangedCommand = new CellColorChangeCommand(cell, (uint)selectedColor.ToArgb(), cell.BGColor);
                        changedCellCommands.Add(cellChangedCommand);
                    }
                }

                // Create the group command holding all the individual commands and pass to the command maneger.
                // The command will execute when added.
                var groupCommand = new GroupColorChangedCommand(changedCellCommands);
                this.spreadsheet.AddCommand(groupCommand);
            }
        }

        /// <summary>
        /// When the undo button is clicked, call Undo() of the command maneger.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Information.</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.Undo();
        }

        /// <summary>
        /// When redo is clicked, call the Redo() of the command maneger.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Info.</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.Redo();
        }

        /// <summary>
        /// Open the open file dialog when the user clicks "Load file".
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Info.</param>
        private void LoadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    this.spreadsheet.Load(stream);
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// Open the save file dialog when the user clicks "Save file".
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">Info.</param>
        private void SaveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    this.spreadsheet.Save(stream);
                    stream.Close();
                }
            }
        }
    }
}