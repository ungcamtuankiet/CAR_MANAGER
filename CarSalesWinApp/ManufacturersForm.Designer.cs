
namespace CarSalesWinApp
{
    partial class ManufacturersForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridManu = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stripMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.stripManu = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridManu)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.gridManu);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 452);
            this.panel1.TabIndex = 0;
            // 
            // gridManu
            // 
            this.gridManu.AllowUserToAddRows = false;
            this.gridManu.AllowUserToDeleteRows = false;
            this.gridManu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridManu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridManu.Location = new System.Drawing.Point(0, 28);
            this.gridManu.Name = "gridManu";
            this.gridManu.ReadOnly = true;
            this.gridManu.RowHeadersWidth = 51;
            this.gridManu.RowTemplate.Height = 29;
            this.gridManu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridManu.Size = new System.Drawing.Size(442, 424);
            this.gridManu.TabIndex = 1;
            this.gridManu.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridManu_CellDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuFile,
            this.stripManu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(442, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // stripMenuFile
            // 
            this.stripMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuExit});
            this.stripMenuFile.Name = "stripMenuFile";
            this.stripMenuFile.Size = new System.Drawing.Size(46, 24);
            this.stripMenuFile.Text = "File";
            // 
            // stripMenuExit
            // 
            this.stripMenuExit.Name = "stripMenuExit";
            this.stripMenuExit.Size = new System.Drawing.Size(116, 26);
            this.stripMenuExit.Text = "Exit";
            this.stripMenuExit.Click += new System.EventHandler(this.stripMenuExit_Click);
            // 
            // stripManu
            // 
            this.stripManu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuNew,
            this.stripMenuDelete});
            this.stripManu.Name = "stripManu";
            this.stripManu.Size = new System.Drawing.Size(117, 24);
            this.stripManu.Text = "Manufacturers";
            // 
            // stripMenuNew
            // 
            this.stripMenuNew.Name = "stripMenuNew";
            this.stripMenuNew.Size = new System.Drawing.Size(168, 26);
            this.stripMenuNew.Text = "New";
            this.stripMenuNew.Click += new System.EventHandler(this.stripMenuNew_Click);
            // 
            // stripMenuDelete
            // 
            this.stripMenuDelete.Name = "stripMenuDelete";
            this.stripMenuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.stripMenuDelete.Size = new System.Drawing.Size(168, 26);
            this.stripMenuDelete.Text = "Delete";
            this.stripMenuDelete.Click += new System.EventHandler(this.stripMenuDelete_Click);
            // 
            // ManufacturersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 450);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ManufacturersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manufacturers";
            this.Activated += new System.EventHandler(this.ManufacturersForm_Activated);
            this.Load += new System.EventHandler(this.ManufacturersForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridManu)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stripMenuFile;
        private System.Windows.Forms.ToolStripMenuItem stripMenuExit;
        private System.Windows.Forms.DataGridView gridManu;
        private System.Windows.Forms.ToolStripMenuItem stripManu;
        private System.Windows.Forms.ToolStripMenuItem stripMenuNew;
        private System.Windows.Forms.ToolStripMenuItem stripMenuDelete;
    }
}