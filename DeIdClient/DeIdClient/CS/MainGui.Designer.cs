using System;
using System.Windows.Forms;

namespace WTextAnnotator
{
    partial class MainGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textArea = new System.Windows.Forms.RichTextBox();
            this.loadTextButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAnnotateWord = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonSort = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.startCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonImportDict = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textArea
            // 
            this.textArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textArea.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textArea.Location = new System.Drawing.Point(3, 53);
            this.textArea.Name = "textArea";
            this.textArea.Size = new System.Drawing.Size(682, 454);
            this.textArea.TabIndex = 0;
            this.textArea.Text = "";
            // 
            // loadTextButton
            // 
            this.loadTextButton.Location = new System.Drawing.Point(9, 9);
            this.loadTextButton.Name = "loadTextButton";
            this.loadTextButton.Size = new System.Drawing.Size(126, 34);
            this.loadTextButton.TabIndex = 1;
            this.loadTextButton.Text = "Load Text";
            this.loadTextButton.UseVisualStyleBackColor = true;
            this.loadTextButton.Click += new System.EventHandler(this.buttonLoadText_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.textArea, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1059, 489);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonImportDict);
            this.panel1.Controls.Add(this.buttonAnnotateWord);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.loadTextButton);
            this.panel1.Controls.Add(this.buttonSort);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(634, 44);
            this.panel1.TabIndex = 1;
            // 
            // buttonAnnotateWord
            // 
            this.buttonAnnotateWord.Location = new System.Drawing.Point(441, 9);
            this.buttonAnnotateWord.Name = "buttonAnnotateWord";
            this.buttonAnnotateWord.Size = new System.Drawing.Size(88, 32);
            this.buttonAnnotateWord.TabIndex = 4;
            this.buttonAnnotateWord.Text = "Run";
            this.buttonAnnotateWord.UseVisualStyleBackColor = true;
            this.buttonAnnotateWord.Click += new System.EventHandler(this.buttonAnnotateWord_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(331, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 26);
            this.textBox1.TabIndex = 3;
            // 
            // buttonSort
            // 
            this.buttonSort.Location = new System.Drawing.Point(535, 5);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(81, 35);
            this.buttonSort.TabIndex = 5;
            this.buttonSort.Text = "Sort";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.startCol,
            this.endCol,
            this.sourceCol,
            this.timeCol,
            this.textCol});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(691, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(365, 454);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // startCol
            // 
            this.startCol.HeaderText = "Start";
            this.startCol.Name = "startCol";
            this.startCol.Width = 40;
            // 
            // endCol
            // 
            this.endCol.HeaderText = "End";
            this.endCol.Name = "endCol";
            this.endCol.Width = 40;
            // 
            // sourceCol
            // 
            this.sourceCol.HeaderText = "Source";
            this.sourceCol.Name = "sourceCol";
            this.sourceCol.Width = 50;
            // 
            // timeCol
            // 
            this.timeCol.HeaderText = "Time";
            this.timeCol.Name = "timeCol";
            this.timeCol.Width = 80;
            // 
            // textCol
            // 
            this.textCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.textCol.HeaderText = "Text";
            this.textCol.Name = "textCol";
            // 
            // buttonImportDict
            // 
            this.buttonImportDict.Location = new System.Drawing.Point(141, 8);
            this.buttonImportDict.Name = "buttonImportDict";
            this.buttonImportDict.Size = new System.Drawing.Size(150, 35);
            this.buttonImportDict.TabIndex = 6;
            this.buttonImportDict.Text = "Import Dictionary";
            this.buttonImportDict.UseVisualStyleBackColor = true;
            this.buttonImportDict.Click += new System.EventHandler(this.buttonImportDict_Click);
            // 
            // MainGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 489);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainGui";
            this.Text = "UAB Text Annotator";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

  



        #endregion

        public System.Windows.Forms.RichTextBox textArea;
        public System.Windows.Forms.Button loadTextButton;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button buttonAnnotateWord;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.DataGridView dataGridView1;
        public Button buttonSort;
        public DataGridViewTextBoxColumn startCol;
        public DataGridViewTextBoxColumn endCol;
        public DataGridViewTextBoxColumn sourceCol;
        public DataGridViewTextBoxColumn timeCol;
        public DataGridViewTextBoxColumn textCol;
        private Button buttonImportDict;
    }
}

