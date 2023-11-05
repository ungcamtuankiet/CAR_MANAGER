
namespace CarSalesWinApp
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stripMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stripMenuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuManage = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuCar = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuManu = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuCustomerInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuNewInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuStatistic = new System.Windows.Forms.ToolStripMenuItem();
            this.stripMenuCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuFile,
            this.stripMenuManage,
            this.stripMenuStatistic});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // stripMenuFile
            // 
            this.stripMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuLogin,
            this.toolStripSeparator1,
            this.stripMenuLogout,
            this.stripMenuExit});
            this.stripMenuFile.Name = "stripMenuFile";
            this.stripMenuFile.Size = new System.Drawing.Size(46, 24);
            this.stripMenuFile.Text = "File";
            // 
            // stripMenuLogin
            // 
            this.stripMenuLogin.Name = "stripMenuLogin";
            this.stripMenuLogin.Size = new System.Drawing.Size(169, 26);
            this.stripMenuLogin.Text = "Login";
            this.stripMenuLogin.Click += new System.EventHandler(this.stripMenuLogin_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // stripMenuLogout
            // 
            this.stripMenuLogout.Name = "stripMenuLogout";
            this.stripMenuLogout.Size = new System.Drawing.Size(169, 26);
            this.stripMenuLogout.Text = "Logout";
            this.stripMenuLogout.Click += new System.EventHandler(this.stripMenuLogout_Click);
            // 
            // stripMenuExit
            // 
            this.stripMenuExit.Name = "stripMenuExit";
            this.stripMenuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.stripMenuExit.Size = new System.Drawing.Size(169, 26);
            this.stripMenuExit.Text = "Exit";
            this.stripMenuExit.Click += new System.EventHandler(this.stripMenuExit_Click);
            // 
            // stripMenuManage
            // 
            this.stripMenuManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuProduct,
            this.stripMenuInvoice,
            this.stripMenuCustomer});
            this.stripMenuManage.Name = "stripMenuManage";
            this.stripMenuManage.Size = new System.Drawing.Size(77, 24);
            this.stripMenuManage.Text = "Manage";
            // 
            // stripMenuProduct
            // 
            this.stripMenuProduct.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuCar,
            this.stripMenuManu});
            this.stripMenuProduct.Name = "stripMenuProduct";
            this.stripMenuProduct.Size = new System.Drawing.Size(219, 26);
            this.stripMenuProduct.Text = "Manage Product";
            // 
            // stripMenuCar
            // 
            this.stripMenuCar.Name = "stripMenuCar";
            this.stripMenuCar.Size = new System.Drawing.Size(186, 26);
            this.stripMenuCar.Text = "Cars";
            this.stripMenuCar.Click += new System.EventHandler(this.stripMenuCar_Click);
            // 
            // stripMenuManu
            // 
            this.stripMenuManu.Name = "stripMenuManu";
            this.stripMenuManu.Size = new System.Drawing.Size(186, 26);
            this.stripMenuManu.Text = "Manufacturers";
            this.stripMenuManu.Click += new System.EventHandler(this.stripMenuManu_Click);
            // 
            // stripMenuInvoice
            // 
            this.stripMenuInvoice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuCustomerInvoice,
            this.stripMenuNewInvoice});
            this.stripMenuInvoice.Name = "stripMenuInvoice";
            this.stripMenuInvoice.Size = new System.Drawing.Size(219, 26);
            this.stripMenuInvoice.Text = "Manage Invoices";
            // 
            // stripMenuCustomerInvoice
            // 
            this.stripMenuCustomerInvoice.Name = "stripMenuCustomerInvoice";
            this.stripMenuCustomerInvoice.Size = new System.Drawing.Size(212, 26);
            this.stripMenuCustomerInvoice.Text = "Customer Invoices";
            this.stripMenuCustomerInvoice.Click += new System.EventHandler(this.stripMenuCustomerInvoice_Click);
            // 
            // stripMenuNewInvoice
            // 
            this.stripMenuNewInvoice.Name = "stripMenuNewInvoice";
            this.stripMenuNewInvoice.Size = new System.Drawing.Size(212, 26);
            this.stripMenuNewInvoice.Text = "New Invoice";
            this.stripMenuNewInvoice.Click += new System.EventHandler(this.stripMenuNewInvoice_Click);
            // 
            // stripMenuCustomer
            // 
            this.stripMenuCustomer.Name = "stripMenuCustomer";
            this.stripMenuCustomer.Size = new System.Drawing.Size(219, 26);
            this.stripMenuCustomer.Text = "Manage Customers";
            this.stripMenuCustomer.Click += new System.EventHandler(this.stripMenuCustomer_Click);
            // 
            // stripMenuStatistic
            // 
            this.stripMenuStatistic.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripMenuCreate});
            this.stripMenuStatistic.Name = "stripMenuStatistic";
            this.stripMenuStatistic.Size = new System.Drawing.Size(75, 24);
            this.stripMenuStatistic.Text = "Statistic";
            // 
            // stripMenuCreate
            // 
            this.stripMenuCreate.Name = "stripMenuCreate";
            this.stripMenuCreate.Size = new System.Drawing.Size(224, 26);
            this.stripMenuCreate.Text = "Create";
            this.stripMenuCreate.Click += new System.EventHandler(this.stripMenuCreate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stripMenuFile;
        private System.Windows.Forms.ToolStripMenuItem stripMenuLogin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem stripMenuLogout;
        private System.Windows.Forms.ToolStripMenuItem stripMenuExit;
        private System.Windows.Forms.ToolStripMenuItem stripMenuManage;
        private System.Windows.Forms.ToolStripMenuItem stripMenuProduct;
        private System.Windows.Forms.ToolStripMenuItem stripMenuInvoice;
        private System.Windows.Forms.ToolStripMenuItem stripMenuCar;
        private System.Windows.Forms.ToolStripMenuItem stripMenuManu;
        private System.Windows.Forms.ToolStripMenuItem stripMenuCustomer;
        private System.Windows.Forms.ToolStripMenuItem stripMenuCustomerInvoice;
        private System.Windows.Forms.ToolStripMenuItem stripMenuNewInvoice;
        private System.Windows.Forms.ToolStripMenuItem stripMenuStatistic;
        private System.Windows.Forms.ToolStripMenuItem stripMenuCreate;
    }
}