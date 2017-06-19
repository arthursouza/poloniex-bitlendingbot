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
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDollarTotal = new System.Windows.Forms.TextBox();
            this.txtDollarYear = new System.Windows.Forms.TextBox();
            this.txtDollarMonth = new System.Windows.Forms.TextBox();
            this.txtDollarWeek = new System.Windows.Forms.TextBox();
            this.txtDollarDay = new System.Windows.Forms.TextBox();
            this.txtDollarHour = new System.Windows.Forms.TextBox();
            this.txtTotalBTC = new System.Windows.Forms.TextBox();
            this.txtYearBtc = new System.Windows.Forms.TextBox();
            this.txtMonthBTC = new System.Windows.Forms.TextBox();
            this.txtWeekBTC = new System.Windows.Forms.TextBox();
            this.txtDayBTC = new System.Windows.Forms.TextBox();
            this.txtHourBtc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBTCDollar = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // botLog
            // 
            this.botLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.botLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.botLog.ForeColor = System.Drawing.SystemColors.Menu;
            this.botLog.Location = new System.Drawing.Point(12, 138);
            this.botLog.MinimumSize = new System.Drawing.Size(251, 213);
            this.botLog.Multiline = true;
            this.botLog.Name = "botLog";
            this.botLog.ReadOnly = true;
            this.botLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.botLog.Size = new System.Drawing.Size(563, 371);
            this.botLog.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtBTCDollar);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtDollarTotal);
            this.panel1.Controls.Add(this.txtDollarYear);
            this.panel1.Controls.Add(this.txtDollarMonth);
            this.panel1.Controls.Add(this.txtDollarWeek);
            this.panel1.Controls.Add(this.txtDollarDay);
            this.panel1.Controls.Add(this.txtDollarHour);
            this.panel1.Controls.Add(this.txtTotalBTC);
            this.panel1.Controls.Add(this.txtYearBtc);
            this.panel1.Controls.Add(this.txtMonthBTC);
            this.panel1.Controls.Add(this.txtWeekBTC);
            this.panel1.Controls.Add(this.txtDayBTC);
            this.panel1.Controls.Add(this.txtHourBtc);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.MaximumSize = new System.Drawing.Size(563, 120);
            this.panel1.MinimumSize = new System.Drawing.Size(563, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 120);
            this.panel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "H";
            // 
            // txtDollarTotal
            // 
            this.txtDollarTotal.Location = new System.Drawing.Point(49, 87);
            this.txtDollarTotal.Name = "txtDollarTotal";
            this.txtDollarTotal.Size = new System.Drawing.Size(83, 20);
            this.txtDollarTotal.TabIndex = 13;
            // 
            // txtDollarYear
            // 
            this.txtDollarYear.Location = new System.Drawing.Point(473, 38);
            this.txtDollarYear.Name = "txtDollarYear";
            this.txtDollarYear.Size = new System.Drawing.Size(83, 20);
            this.txtDollarYear.TabIndex = 12;
            // 
            // txtDollarMonth
            // 
            this.txtDollarMonth.Location = new System.Drawing.Point(364, 38);
            this.txtDollarMonth.Name = "txtDollarMonth";
            this.txtDollarMonth.Size = new System.Drawing.Size(83, 20);
            this.txtDollarMonth.TabIndex = 11;
            // 
            // txtDollarWeek
            // 
            this.txtDollarWeek.Location = new System.Drawing.Point(257, 38);
            this.txtDollarWeek.Name = "txtDollarWeek";
            this.txtDollarWeek.Size = new System.Drawing.Size(83, 20);
            this.txtDollarWeek.TabIndex = 10;
            // 
            // txtDollarDay
            // 
            this.txtDollarDay.Location = new System.Drawing.Point(147, 38);
            this.txtDollarDay.Name = "txtDollarDay";
            this.txtDollarDay.Size = new System.Drawing.Size(83, 20);
            this.txtDollarDay.TabIndex = 9;
            // 
            // txtDollarHour
            // 
            this.txtDollarHour.Location = new System.Drawing.Point(49, 35);
            this.txtDollarHour.Name = "txtDollarHour";
            this.txtDollarHour.Size = new System.Drawing.Size(71, 20);
            this.txtDollarHour.TabIndex = 8;
            // 
            // txtTotalBTC
            // 
            this.txtTotalBTC.Location = new System.Drawing.Point(49, 61);
            this.txtTotalBTC.Name = "txtTotalBTC";
            this.txtTotalBTC.Size = new System.Drawing.Size(83, 20);
            this.txtTotalBTC.TabIndex = 7;
            // 
            // txtYearBtc
            // 
            this.txtYearBtc.Location = new System.Drawing.Point(473, 9);
            this.txtYearBtc.Name = "txtYearBtc";
            this.txtYearBtc.Size = new System.Drawing.Size(83, 20);
            this.txtYearBtc.TabIndex = 6;
            // 
            // txtMonthBTC
            // 
            this.txtMonthBTC.Location = new System.Drawing.Point(364, 9);
            this.txtMonthBTC.Name = "txtMonthBTC";
            this.txtMonthBTC.Size = new System.Drawing.Size(83, 20);
            this.txtMonthBTC.TabIndex = 5;
            // 
            // txtWeekBTC
            // 
            this.txtWeekBTC.Location = new System.Drawing.Point(257, 9);
            this.txtWeekBTC.Name = "txtWeekBTC";
            this.txtWeekBTC.Size = new System.Drawing.Size(83, 20);
            this.txtWeekBTC.TabIndex = 4;
            // 
            // txtDayBTC
            // 
            this.txtDayBTC.Location = new System.Drawing.Point(147, 9);
            this.txtDayBTC.Name = "txtDayBTC";
            this.txtDayBTC.Size = new System.Drawing.Size(83, 20);
            this.txtDayBTC.TabIndex = 3;
            // 
            // txtHourBtc
            // 
            this.txtHourBtc.Location = new System.Drawing.Point(49, 9);
            this.txtHourBtc.Name = "txtHourBtc";
            this.txtHourBtc.Size = new System.Drawing.Size(71, 20);
            this.txtHourBtc.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "D";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "W";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(343, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "M";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(453, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "T";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "T";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(453, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Y";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(343, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "M";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(236, 41);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "W";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(126, 38);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "D";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(28, 38);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "H";
            // 
            // txtBTCDollar
            // 
            this.txtBTCDollar.Location = new System.Drawing.Point(204, 87);
            this.txtBTCDollar.Name = "txtBTCDollar";
            this.txtBTCDollar.Size = new System.Drawing.Size(100, 20);
            this.txtBTCDollar.TabIndex = 26;
            this.txtBTCDollar.TextChanged += new System.EventHandler(this.txtBTCDollar_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(136, 90);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 13);
            this.label15.TabIndex = 27;
            this.label15.Text = "USD / BTC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "btc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "usd";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(3, 90);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "usd";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 64);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(22, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "btc";
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 521);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.botLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(596, 560);
            this.Name = "AppForm";
            this.Text = "Bit Lending Bot";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox botLog;
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtTotalBTC;
        private System.Windows.Forms.TextBox txtYearBtc;
        private System.Windows.Forms.TextBox txtMonthBTC;
        private System.Windows.Forms.TextBox txtWeekBTC;
        private System.Windows.Forms.TextBox txtDayBTC;
        private System.Windows.Forms.TextBox txtHourBtc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDollarTotal;
        private System.Windows.Forms.TextBox txtDollarYear;
        private System.Windows.Forms.TextBox txtDollarMonth;
        private System.Windows.Forms.TextBox txtDollarWeek;
        private System.Windows.Forms.TextBox txtDollarDay;
        private System.Windows.Forms.TextBox txtDollarHour;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBTCDollar;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

