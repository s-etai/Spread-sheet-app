namespace Spreadsheet_Elliott_Tai
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            Demo = new Button();
            colorDialog1 = new ColorDialog();
            menuStrip1 = new MenuStrip();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            cellToolStripMenuItem = new ToolStripMenuItem();
            setColorToolStripMenuItem = new ToolStripMenuItem();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadFileToolStripMenuItem = new ToolStripMenuItem();
            saveFileToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 33);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 33;
            dataGridView1.Size = new Size(800, 417);
            dataGridView1.TabIndex = 0;
            // 
            // Demo
            // 
            Demo.Location = new Point(676, 12);
            Demo.Name = "Demo";
            Demo.Size = new Size(112, 34);
            Demo.TabIndex = 1;
            Demo.Text = "Demo";
            Demo.UseVisualStyleBackColor = true;
            Demo.Click += Demo_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { editToolStripMenuItem, cellToolStripMenuItem, fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 33);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(58, 29);
            editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(158, 34);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += UndoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(158, 34);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += RedoToolStripMenuItem_Click;
            // 
            // cellToolStripMenuItem
            // 
            cellToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { setColorToolStripMenuItem });
            cellToolStripMenuItem.Name = "cellToolStripMenuItem";
            cellToolStripMenuItem.Size = new Size(56, 29);
            cellToolStripMenuItem.Text = "Cell";
            // 
            // setColorToolStripMenuItem
            // 
            setColorToolStripMenuItem.Name = "setColorToolStripMenuItem";
            setColorToolStripMenuItem.Size = new Size(184, 34);
            setColorToolStripMenuItem.Text = "Set color";
            setColorToolStripMenuItem.Click += SetColorToolStripMenuItem_Click;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadFileToolStripMenuItem, saveFileToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // loadFileToolStripMenuItem
            // 
            loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            loadFileToolStripMenuItem.Size = new Size(270, 34);
            loadFileToolStripMenuItem.Text = "Load File";
            loadFileToolStripMenuItem.Click += LoadFileToolStripMenuItem_Click;
            // 
            // saveFileToolStripMenuItem
            // 
            saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            saveFileToolStripMenuItem.Size = new Size(270, 34);
            saveFileToolStripMenuItem.Text = "Save File";
            saveFileToolStripMenuItem.Click += SaveFileToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Demo);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button Demo;
        private ColorDialog colorDialog1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem cellToolStripMenuItem;
        private ToolStripMenuItem setColorToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadFileToolStripMenuItem;
        private ToolStripMenuItem saveFileToolStripMenuItem;
    }
}