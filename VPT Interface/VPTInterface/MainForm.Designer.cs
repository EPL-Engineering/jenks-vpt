namespace VPTInterface
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mmFileShowLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mmFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.fileBrowser = new KLib.Controls.FileBrowser();
            this.backgroundColorBox = new KLib.Controls.KColorBox();
            this.distanceNumeric = new KLib.Controls.KNumericBox();
            this.widthNumeric = new KLib.Controls.KNumericBox();
            this.monitorNumeric = new KLib.Controls.KNumericBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.openButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(244, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mmFileShowLog,
            this.toolStripSeparator1,
            this.mmFileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mmFileShowLog
            // 
            this.mmFileShowLog.Name = "mmFileShowLog";
            this.mmFileShowLog.Size = new System.Drawing.Size(123, 22);
            this.mmFileShowLog.Text = "Show &log";
            this.mmFileShowLog.Click += new System.EventHandler(this.mmFileShowLog_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
            // 
            // mmFileExit
            // 
            this.mmFileExit.Name = "mmFileExit";
            this.mmFileExit.Size = new System.Drawing.Size(123, 22);
            this.mmFileExit.Text = "E&xit";
            this.mmFileExit.Click += new System.EventHandler(this.mmFileExit_Click);
            // 
            // fileBrowser
            // 
            this.fileBrowser.AutoSize = true;
            this.fileBrowser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fileBrowser.DefaultFolder = null;
            this.fileBrowser.FileMustExist = false;
            this.fileBrowser.Filter = null;
            this.fileBrowser.FoldersOnly = false;
            this.fileBrowser.HideFolder = true;
            this.fileBrowser.Location = new System.Drawing.Point(102, 49);
            this.fileBrowser.Name = "fileBrowser";
            this.fileBrowser.ReadOnly = false;
            this.fileBrowser.ShowSaveButton = false;
            this.fileBrowser.Size = new System.Drawing.Size(121, 21);
            this.fileBrowser.TabIndex = 1;
            this.fileBrowser.UseEllipsis = false;
            this.fileBrowser.Value = "";
            this.fileBrowser.ValueChanged += new System.EventHandler(this.fileBrowser_ValueChanged);
            // 
            // backgroundColorBox
            // 
            this.backgroundColorBox.Location = new System.Drawing.Point(102, 76);
            this.backgroundColorBox.Name = "backgroundColorBox";
            this.backgroundColorBox.Size = new System.Drawing.Size(88, 22);
            this.backgroundColorBox.TabIndex = 2;
            this.backgroundColorBox.Value = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.backgroundColorBox.ValueAsUInt = ((uint)(4294967295u));
            this.backgroundColorBox.ValueChanged += new System.EventHandler(this.backgroundColorBox_ValueChanged);
            // 
            // distanceNumeric
            // 
            this.distanceNumeric.AllowInf = false;
            this.distanceNumeric.AutoSize = true;
            this.distanceNumeric.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.distanceNumeric.ClearOnDisable = false;
            this.distanceNumeric.FloatValue = 0F;
            this.distanceNumeric.IntValue = 0;
            this.distanceNumeric.IsInteger = false;
            this.distanceNumeric.Location = new System.Drawing.Point(102, 113);
            this.distanceNumeric.MaxCoerce = false;
            this.distanceNumeric.MaximumSize = new System.Drawing.Size(15000, 16);
            this.distanceNumeric.MaxValue = 1.7976931348623157E+308D;
            this.distanceNumeric.MinCoerce = false;
            this.distanceNumeric.MinimumSize = new System.Drawing.Size(8, 16);
            this.distanceNumeric.MinValue = 0D;
            this.distanceNumeric.Name = "distanceNumeric";
            this.distanceNumeric.Size = new System.Drawing.Size(88, 16);
            this.distanceNumeric.TabIndex = 3;
            this.distanceNumeric.TextFormat = "K4";
            this.distanceNumeric.ToolTip = "";
            this.distanceNumeric.Units = "";
            this.distanceNumeric.Value = 0D;
            this.distanceNumeric.ValueChanged += new System.EventHandler(this.distanceNumeric_ValueChanged);
            // 
            // widthNumeric
            // 
            this.widthNumeric.AllowInf = false;
            this.widthNumeric.AutoSize = true;
            this.widthNumeric.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.widthNumeric.ClearOnDisable = false;
            this.widthNumeric.FloatValue = 0F;
            this.widthNumeric.IntValue = 0;
            this.widthNumeric.IsInteger = false;
            this.widthNumeric.Location = new System.Drawing.Point(102, 147);
            this.widthNumeric.MaxCoerce = false;
            this.widthNumeric.MaximumSize = new System.Drawing.Size(15000, 16);
            this.widthNumeric.MaxValue = 1.7976931348623157E+308D;
            this.widthNumeric.MinCoerce = false;
            this.widthNumeric.MinimumSize = new System.Drawing.Size(8, 16);
            this.widthNumeric.MinValue = 0D;
            this.widthNumeric.Name = "widthNumeric";
            this.widthNumeric.Size = new System.Drawing.Size(88, 16);
            this.widthNumeric.TabIndex = 4;
            this.widthNumeric.TextFormat = "K4";
            this.widthNumeric.ToolTip = "";
            this.widthNumeric.Units = "";
            this.widthNumeric.Value = 0D;
            this.widthNumeric.ValueChanged += new System.EventHandler(this.widthNumeric_ValueChanged);
            // 
            // monitorNumeric
            // 
            this.monitorNumeric.AllowInf = false;
            this.monitorNumeric.AutoSize = true;
            this.monitorNumeric.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.monitorNumeric.ClearOnDisable = false;
            this.monitorNumeric.FloatValue = 0F;
            this.monitorNumeric.IntValue = 0;
            this.monitorNumeric.IsInteger = true;
            this.monitorNumeric.Location = new System.Drawing.Point(102, 180);
            this.monitorNumeric.MaxCoerce = false;
            this.monitorNumeric.MaximumSize = new System.Drawing.Size(15000, 16);
            this.monitorNumeric.MaxValue = 1.7976931348623157E+308D;
            this.monitorNumeric.MinCoerce = true;
            this.monitorNumeric.MinimumSize = new System.Drawing.Size(8, 16);
            this.monitorNumeric.MinValue = 0D;
            this.monitorNumeric.Name = "monitorNumeric";
            this.monitorNumeric.Size = new System.Drawing.Size(88, 16);
            this.monitorNumeric.TabIndex = 5;
            this.monitorNumeric.TextFormat = "K4";
            this.monitorNumeric.ToolTip = "";
            this.monitorNumeric.Units = "";
            this.monitorNumeric.Value = 0D;
            this.monitorNumeric.ValueChanged += new System.EventHandler(this.monitorNumeric_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Background";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Distance (cm)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 150);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Width (cm)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 183);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Monitor";
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(30, 228);
            this.openButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(88, 37);
            this.openButton.TabIndex = 11;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(122, 228);
            this.closeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(88, 37);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 284);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monitorNumeric);
            this.Controls.Add(this.widthNumeric);
            this.Controls.Add(this.distanceNumeric);
            this.Controls.Add(this.backgroundColorBox);
            this.Controls.Add(this.fileBrowser);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.closeButton);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "VPT Interface";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mmFileShowLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mmFileExit;
        private KLib.Controls.FileBrowser fileBrowser;
        private KLib.Controls.KColorBox backgroundColorBox;
        private KLib.Controls.KNumericBox distanceNumeric;
        private KLib.Controls.KNumericBox widthNumeric;
        private KLib.Controls.KNumericBox monitorNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button closeButton;
    }
}

