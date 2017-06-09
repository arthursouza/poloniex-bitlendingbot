namespace BitLendingBot.App
{
    partial class AppForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppForm));
            this.botLog = new System.Windows.Forms.TextBox();
            this.mainMenu = new System.Windows.Forms.ToolStrip();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // botLog
            // 
            this.botLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.botLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.botLog.ForeColor = System.Drawing.SystemColors.Menu;
            this.botLog.Location = new System.Drawing.Point(12, 28);
            this.botLog.Multiline = true;
            this.botLog.Name = "botLog";
            this.botLog.ReadOnly = true;
            this.botLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.botLog.Size = new System.Drawing.Size(684, 511);
            this.botLog.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(708, 25);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "toolStrip1";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 551);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.botLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AppForm";
            this.Text = "Bit Lending Bot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox botLog;
        private System.Windows.Forms.ToolStrip mainMenu;
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.Timer timer;
    }
}

