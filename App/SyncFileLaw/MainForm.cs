#region Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
/*
   Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
   All rights are reserved.

   Permission to use, copy, modify, and distribute this software 
   for any purpose and without any fee is hereby granted, 
   provided this notice is included in its entirety in the 
   documentation and in the source files.
  
   This software and any related documentation is provided "as is" 
   without any warranty of any kind, either express or implied, 
   including, without limitation, the implied warranties of 
   merchantibility or fitness for a particular purpose. The entire 
   risk arising out of use or performance of the software remains 
   with you. 
   
   In no event shall Richard Schneider, Black Hen Limited, or their agents 
   be liable for any cost, loss, or damage incurred due to the 
   use, malfunction, or misuse of the software or the inaccuracy 
   of the documentation associated with the software. 
*/
#endregion

using BlackHen.Threading;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Configuration;
using System.Text.RegularExpressions;
using Devshock.Net;
namespace SyncMediaFile
{
	/// <summary>
	///  A WorkQueue sample.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label4;
      private IContainer components;
      private System.Windows.Forms.TextBox maxThreads;
      private System.Windows.Forms.TextBox concurrentLimit;
      private System.Windows.Forms.TextBox minThreads;
      private System.Windows.Forms.TextBox txtAmount;

      private WorkQueue work;

      private System.Windows.Forms.ContextMenu fsmMenu;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem reset;
      private System.Windows.Forms.MenuItem pause;
      private System.Windows.Forms.MenuItem resume;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.Button produceWork;
      private int[] stats;
      private TimeSpan refreshInterval;
      private Button btnStop;
      private DateTime nextRefreshTime;
      private TextBox txtLog;
      private CheckBox chkShowLog;

      private int m_ThreadRunning = 0;
      private int m_MaxWorkQueue = 5;
      private int m_QueueNotDone = 10;
      private bool isRunning = false;
      private int m_SleepTime = 1000;
      private int m_CountLog = 0;
      private int m_CountLog_LoadData = 0;
      private int m_CountLog_Error = 0;
      private int m_MaxCountLog = 200;
      private short m_MaxRow = 50;

      private GroupBox groupBox2;
      private TextBox txtFailing;
      private Label label9;
      private TextBox txtRunning;
      private TextBox txtCompleted;
      private TextBox txtQueued;
      private Label label5;
      private TextBox txtScheduled;
      private Label label6;
      private Label label7;
      private Label label8;
      private TextBox txtErrorLog;
      private Label label10;
      private TextBox txtWait;
      private Label label11;
      private TextBox txtLoadDataLog;
      private Label label12;
      private string str;
      private int lenginsert = 0;
      private Button btnSync;
      private CheckBox chkScanDB;
        private Button button1;
        internal SockServer m_SockServer = new SockServer();

      public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            if (ConfigurationManager.AppSettings["MaxRow"] != null) txtAmount.Text = ConfigurationManager.AppSettings["MaxRow"].ToString();
            if (ConfigurationManager.AppSettings["SleepTime"] != null) txtWait.Text = ConfigurationManager.AppSettings["SleepTime"].ToString();

            m_MaxRow = Convert.ToInt16(txtAmount.Text);
            m_SleepTime = Convert.ToInt32(txtWait.Text);

            nextRefreshTime = DateTime.Now;
            refreshInterval = TimeSpan.FromSeconds(0.50);

            stats = new int[6];
            //Queue for default
            work = new WorkQueue();
            work.AllWorkCompleted += new EventHandler(work_AllWorkCompleted);
            work.WorkerException += new ResourceExceptionEventHandler(work_WorkerException);
            work.ChangedWorkItemState += new ChangedWorkItemStateEventHandler(work_ChangedWorkItemState);
            ((WorkThreadPool)work.WorkerPool).MaxThreads = Int32.Parse(maxThreads.Text);
            ((WorkThreadPool)work.WorkerPool).MinThreads = Int32.Parse(minThreads.Text);
            work.ConcurrentLimit = Int32.Parse(concurrentLimit.Text);

        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fsmMenu = new System.Windows.Forms.ContextMenu();
            this.reset = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.pause = new System.Windows.Forms.MenuItem();
            this.resume = new System.Windows.Forms.MenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtWait = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.maxThreads = new System.Windows.Forms.TextBox();
            this.concurrentLimit = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.minThreads = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowLog = new System.Windows.Forms.CheckBox();
            this.produceWork = new System.Windows.Forms.Button();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFailing = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRunning = new System.Windows.Forms.TextBox();
            this.txtCompleted = new System.Windows.Forms.TextBox();
            this.txtQueued = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtScheduled = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtErrorLog = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLoadDataLog = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSync = new System.Windows.Forms.Button();
            this.chkScanDB = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fsmMenu
            // 
            this.fsmMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.reset,
            this.menuItem1,
            this.pause,
            this.resume});
            // 
            // reset
            // 
            this.reset.Index = 0;
            this.reset.Text = "Reset Counts";
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "-";
            // 
            // pause
            // 
            this.pause.Index = 2;
            this.pause.Text = "Pause";
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // resume
            // 
            this.resume.Enabled = false;
            this.resume.Index = 3;
            this.resume.Text = "Resume";
            this.resume.Click += new System.EventHandler(this.resume_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtWait);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.maxThreads);
            this.groupBox1.Controls.Add(this.concurrentLimit);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.minThreads);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 133);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Worker management";
            // 
            // txtWait
            // 
            this.txtWait.Location = new System.Drawing.Point(113, 110);
            this.txtWait.Name = "txtWait";
            this.txtWait.Size = new System.Drawing.Size(43, 20);
            this.txtWait.TabIndex = 12;
            this.txtWait.Text = "60000";
            this.txtWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtWait.Leave += new System.EventHandler(this.txtWait_Leave);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(24, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "Wait time (ms)";
            // 
            // maxThreads
            // 
            this.maxThreads.Location = new System.Drawing.Point(113, 64);
            this.maxThreads.Name = "maxThreads";
            this.maxThreads.Size = new System.Drawing.Size(43, 20);
            this.maxThreads.TabIndex = 5;
            this.maxThreads.Text = "5";
            this.maxThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maxThreads.Leave += new System.EventHandler(this.maxThreads_Leave);
            // 
            // concurrentLimit
            // 
            this.concurrentLimit.Location = new System.Drawing.Point(113, 87);
            this.concurrentLimit.Name = "concurrentLimit";
            this.concurrentLimit.Size = new System.Drawing.Size(43, 20);
            this.concurrentLimit.TabIndex = 1;
            this.concurrentLimit.Text = "100";
            this.concurrentLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.concurrentLimit.Leave += new System.EventHandler(this.concurrentLimit_Leave);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(113, 19);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(43, 20);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.Text = "200";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.Leave += new System.EventHandler(this.txtAmount_Leave);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Amount";
            // 
            // minThreads
            // 
            this.minThreads.Location = new System.Drawing.Point(113, 41);
            this.minThreads.Name = "minThreads";
            this.minThreads.Size = new System.Drawing.Size(43, 20);
            this.minThreads.TabIndex = 4;
            this.minThreads.Text = "1";
            this.minThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Concurrency limit";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Max threads";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Min threads";
            // 
            // chkShowLog
            // 
            this.chkShowLog.AutoSize = true;
            this.chkShowLog.Checked = true;
            this.chkShowLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLog.Location = new System.Drawing.Point(441, 122);
            this.chkShowLog.Name = "chkShowLog";
            this.chkShowLog.Size = new System.Drawing.Size(70, 17);
            this.chkShowLog.TabIndex = 15;
            this.chkShowLog.Text = "Show log";
            this.chkShowLog.UseVisualStyleBackColor = true;
            // 
            // produceWork
            // 
            this.produceWork.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.produceWork.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.produceWork.Location = new System.Drawing.Point(405, 20);
            this.produceWork.Name = "produceWork";
            this.produceWork.Size = new System.Drawing.Size(74, 23);
            this.produceWork.TabIndex = 3;
            this.produceWork.Text = "Start";
            this.produceWork.Click += new System.EventHandler(this.produceWork_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = -1;
            this.menuItem3.Text = "-";
            // 
            // btnStop
            // 
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStop.Location = new System.Drawing.Point(405, 49);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(74, 23);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(8, 145);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(503, 181);
            this.txtLog.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFailing);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtRunning);
            this.groupBox2.Controls.Add(this.txtCompleted);
            this.groupBox2.Controls.Add(this.txtQueued);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtScheduled);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(195, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 133);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Worker status";
            // 
            // txtFailing
            // 
            this.txtFailing.Location = new System.Drawing.Point(100, 110);
            this.txtFailing.Name = "txtFailing";
            this.txtFailing.Size = new System.Drawing.Size(56, 20);
            this.txtFailing.TabIndex = 12;
            this.txtFailing.Text = "0";
            this.txtFailing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(24, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 16);
            this.label9.TabIndex = 13;
            this.label9.Text = "Failing";
            // 
            // txtRunning
            // 
            this.txtRunning.Location = new System.Drawing.Point(100, 64);
            this.txtRunning.Name = "txtRunning";
            this.txtRunning.Size = new System.Drawing.Size(56, 20);
            this.txtRunning.TabIndex = 5;
            this.txtRunning.Text = "0";
            this.txtRunning.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCompleted
            // 
            this.txtCompleted.Location = new System.Drawing.Point(100, 87);
            this.txtCompleted.Name = "txtCompleted";
            this.txtCompleted.Size = new System.Drawing.Size(56, 20);
            this.txtCompleted.TabIndex = 1;
            this.txtCompleted.Text = "0";
            this.txtCompleted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQueued
            // 
            this.txtQueued.Location = new System.Drawing.Point(100, 19);
            this.txtQueued.Name = "txtQueued";
            this.txtQueued.Size = new System.Drawing.Size(56, 20);
            this.txtQueued.TabIndex = 2;
            this.txtQueued.Text = "0";
            this.txtQueued.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Queued";
            // 
            // txtScheduled
            // 
            this.txtScheduled.Location = new System.Drawing.Point(100, 41);
            this.txtScheduled.Name = "txtScheduled";
            this.txtScheduled.Size = new System.Drawing.Size(56, 20);
            this.txtScheduled.TabIndex = 4;
            this.txtScheduled.Text = "0";
            this.txtScheduled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(24, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Completed";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(24, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Running";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(24, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "Scheduled";
            // 
            // txtErrorLog
            // 
            this.txtErrorLog.Location = new System.Drawing.Point(8, 348);
            this.txtErrorLog.Multiline = true;
            this.txtErrorLog.Name = "txtErrorLog";
            this.txtErrorLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrorLog.Size = new System.Drawing.Size(329, 108);
            this.txtErrorLog.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(5, 331);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "Error log";
            // 
            // txtLoadDataLog
            // 
            this.txtLoadDataLog.Location = new System.Drawing.Point(343, 348);
            this.txtLoadDataLog.Multiline = true;
            this.txtLoadDataLog.Name = "txtLoadDataLog";
            this.txtLoadDataLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLoadDataLog.Size = new System.Drawing.Size(165, 108);
            this.txtLoadDataLog.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(341, 331);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 16);
            this.label12.TabIndex = 17;
            this.label12.Text = "Load data log";
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(386, 87);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(108, 23);
            this.btnSync.TabIndex = 18;
            this.btnSync.Text = "Sync file not done";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // chkScanDB
            // 
            this.chkScanDB.AutoSize = true;
            this.chkScanDB.Location = new System.Drawing.Point(365, 121);
            this.chkScanDB.Name = "chkScanDB";
            this.chkScanDB.Size = new System.Drawing.Size(69, 17);
            this.chkScanDB.TabIndex = 19;
            this.chkScanDB.Text = "Scan DB";
            this.chkScanDB.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(365, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.produceWork;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(517, 460);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkScanDB);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtLoadDataLog);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtErrorLog);
            this.Controls.Add(this.chkShowLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.produceWork);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Sync File LawData";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
         Application.EnableVisualStyles();
         Application.DoEvents();

         Application.Run(new MainForm());
		}

      /// <summary>
      ///   Generate some work.
      /// </summary>
      private void produceWork_Click(object sender, System.EventArgs e)
      {
        try
        {
            if (m_ThreadRunning > 0)
            {
                WriteLog_Error("Some thread is running, please wait...");
                return;
            }

            if (chkScanDB.Checked)
            {
                m_ThreadRunning = 0;
                isRunning = true;
                produceWork.Enabled = false;

                Thread myThread;
                myThread = new Thread(new ThreadStart(MonitorLoadData));
                myThread.IsBackground = true;
                myThread.Start();
            }
            else
            {
                LoadDocFileData(0);
            }

            m_SockServer.Capacity = 500;
            m_SockServer.LocalPort = 8688;
            m_SockServer.PackageDelimiter = Environment.NewLine;
            m_SockServer.StartServer();
            produceWork.Enabled = false;

            WriteLog_Error("Server Started");
        }
        catch(Exception ex)
        {
            WriteLog_Error(ex.Message.ToString());
        }
      }

      private void MonitorLoadData()
      {
          m_ThreadRunning++;
          //WriteLog_Error("Start thread load data!");
          while (isRunning)
          {
                LoadDocFileData(0);
              System.Threading.Thread.Sleep(m_SleepTime);
          }
          m_ThreadRunning--;
          //WriteLog_Error("End thread load data!");
      }

      private void LoadData(int MediaId)
      {
          try
          {
              int totalRow = 0;
              lock (work)
              {
                  if (work.Count <= 0 || MediaId > 0)
                  {
                      MediasDB m_Medias = new MediasDB();
                      DataSet m_Files = MediaId == 0 ? m_Medias.GetFilesNotSync() : m_Medias.GetFilesById(MediaId);
                      if (m_Files.Tables.Count > 0)
                      {
                          foreach (DataRow row in m_Files.Tables[0].Rows)
                          {
                              totalRow++;
                              AddQueueFiles(int.Parse(row["MediaId"].ToString()), int.Parse(row["MediaTypeId"].ToString()), row["FileName"].ToString());
                          }
                      }
                      WriteLog_LoadData("Load data: " + totalRow.ToString());
                  }
              }
          }
          catch (Exception ex)
          {
              WriteLog_Error(ex.ToString());
          }
      }
        private void LoadDocFileData(int DocFileId)
        {
            try
            {
                int totalRow = 0;
                lock (work)
                {
                    if (work.Count <= 0 || DocFileId > 0)
                    {
                        DocFilesDB m_DocFiles = new DocFilesDB();
                        DataSet m_Files = DocFileId == 0 ? m_DocFiles.GetFilesNotSync() : m_DocFiles.GetFilesById(DocFileId);
                        if (m_Files.Tables.Count > 0)
                        {
                            foreach (DataRow row in m_Files.Tables[0].Rows)
                            {
                                totalRow++;
                                AddQueueDocFiles(int.Parse(row["DocFileId"].ToString()), row["FilePath"].ToString());
                            }
                        }
                        WriteLog_LoadData("Load data: " + totalRow.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog_Error(ex.ToString());
            }
        }
        /// <summary>
        ///   All work is completed.  Update the GUI.
        /// </summary>
        private void work_AllWorkCompleted(object sender, EventArgs e)
      {
         if (this.InvokeRequired)
         {
            this.Invoke(new EventHandler(work_AllWorkCompleted), new object[] {sender, e});
         }
         else
         {
            RefreshCounts();

            //stats = new int[6];
            //if (isRunning) LoadData();
         }
      }


      private void txtAmount_Leave(object sender, System.EventArgs e)
      {
          m_MaxRow = Int16.Parse(txtAmount.Text);
      }

      private void txtWait_Leave(object sender, System.EventArgs e)
      {
          m_SleepTime = Int32.Parse(txtWait.Text);
      }

      /// <summary>
      ///   Change the number of threads to use.
      /// </summary>
      private void maxThreads_Leave(object sender, System.EventArgs e)
      {
         ((WorkThreadPool) work.WorkerPool).MaxThreads = Int32.Parse(maxThreads.Text);
      }

      /// <summary>
      ///   Change the number of concurrent work items that can ran.
      /// </summary>
      private void concurrentLimit_Leave(object sender, System.EventArgs e)
      {
         work.ConcurrentLimit = Int32.Parse(concurrentLimit.Text);
      }

      /// <summary>
      ///   Record where the state of the work item.
      /// </summary>
      private void work_ChangedWorkItemState(object sender, ChangedWorkItemStateEventArgs e)
      {
         lock (this)
         {
            stats[(int) e.PreviousState] -= 1;
            stats[(int) e.WorkItem.State] += 1;
         }
          
         if (!pause.Enabled || DateTime.Now > nextRefreshTime)
         {
            RefreshCounts();
            nextRefreshTime = DateTime.Now + refreshInterval;
            
         }
      }

      /// <summary>
      ///   Update the work item state counts.
      /// </summary>
      private void RefreshCounts()
      {
         if (this.InvokeRequired)
         {
            MethodInvoker mi = new MethodInvoker(RefreshCounts);
            this.BeginInvoke(mi);
         }
         else
         {
            lock (this)
            {
               txtCompleted.Text = stats[(int) WorkItemState.Completed].ToString("N0");
               txtFailing.Text = stats[(int) WorkItemState.Failing].ToString("N0");
               txtQueued.Text = stats[(int) WorkItemState.Queued].ToString("N0");
               txtRunning.Text = stats[(int) WorkItemState.Running].ToString("N0");
               txtScheduled.Text = stats[(int) WorkItemState.Scheduled].ToString("N0");
            }
         }
      }

      /// <summary>
      ///   Clear the work item state counts.
      /// </summary>
      private void reset_Click(object sender, System.EventArgs e)
      {
         lock (stats)
         {
            for (int i = 0; i < stats.Length; ++i)
               stats[i] = 0;
         }
         RefreshCounts();
      }

      /// <summary>
      ///   Pause the work queue.
      /// </summary>
      private void pause_Click(object sender, System.EventArgs e)
      {
         work.Pause();
         pause.Enabled = false;
         resume.Enabled = true;
         Refresh();
      }

      /// <summary>
      ///   Resume the work queue.
      /// </summary>
      private void resume_Click(object sender, System.EventArgs e)
      {
         work.Resume();
         pause.Enabled = true;
         resume.Enabled = false;
         Refresh();
      }

      private void work_WorkerException(object sender, ResourceExceptionEventArgs e)
      {
         Application.OnThreadException(e.Exception);
      }

      private void btnStop_Click(object sender, EventArgs e)
      {
          isRunning = false;
          produceWork.Enabled = true;

          m_SockServer.StopServer();
          WriteLog_Error("Server Stoped");
      }

      public void WriteLog(string s)
      {
          lock (txtLog)
          {
              if (chkShowLog.Checked)
              {
                  m_CountLog++;
                  if (m_CountLog > m_MaxCountLog)
                  {
                      m_CountLog = 0;
                      txtLog.Clear();
                  }
                  txtLog.AppendText(DateTime.Now.ToString() + ": " + s + "\r\n");
              }
          }
      }

      public void WriteLog_LoadData(string s)
      {
          lock (txtLog)
          {
              m_CountLog_LoadData++;
              if (m_CountLog_LoadData > m_MaxCountLog)
              {
                  m_CountLog_LoadData = 0;
                  txtLoadDataLog.Clear();
              }
              txtLoadDataLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + ": " + s + "\r\n");
          }
      }

      public void WriteLog_Error(string s)
      {
          lock (txtLog)
          {
              m_CountLog_Error++;
              if (m_CountLog_Error > m_MaxCountLog)
              {
                  m_CountLog_Error = 0;
                  txtErrorLog.Clear();
              }
              txtErrorLog.AppendText(DateTime.Now.ToString() + ": " + s + "\r\n");
          }
      }

      private void MainForm_Load(object sender, EventArgs e)
      {
          m_SockServer.evOpenConnection += new Devshock.Net.SockServer.OpenConnectionEventHandler(m_SockServer_evOpenConnection);
          m_SockServer.evCloseConnection += new Devshock.Net.SockServer.CloseConnectionEventHandler(m_SockServer_evCloseConnection);
          m_SockServer.evDataReceived += new Devshock.Net.SockServer.DataReceivedEventHandler(m_SockServer_evDataReceived);
          m_SockServer.evClassInformation += new Devshock.Net.SockServer.ClassInformationEventHandler(m_SockServer_evClassInformation);
          m_SockServer.evClassException += new Devshock.Net.SockServer.ClassExceptionEventHandler(m_SockServer_evClassException);
          this.Closing += new CancelEventHandler(MainForm_Closing);
      }

      private void m_SockServer_evOpenConnection(object sender, Devshock.Net.SockServer.OpenConnectionEventArgs e)
      {
          WriteLog("Open Connection");
      }

      private void m_SockServer_evCloseConnection(object sender, Devshock.Net.SockServer.CloseConnectionEventArgs e)
      {
          WriteLog("Close Connection");
      }

      private void m_SockServer_evDataReceived(object sender, Devshock.Net.SockServer.DataReceivedEventArgs e)
      {
          try
          {
              string[] Params = e.DataReceived.Split(Environment.NewLine.ToCharArray());
              if (Params.Length > 0)
              {
                  string data = Params[0].Trim();
                  if (data.StartsWith("SYNCFILE"))
                  {
                      int MediaId = 0;
                      int MediaTypeId = 0;
                      string FilePath = data.Replace("SYNCFILE ", "").Trim();
                      string FileExtension = FilePath.Substring(FilePath.LastIndexOf(".") + 1).ToLower();
                      string ImageExtension = "jpg,jpeg,gif,bmp,png";
                      if (ImageExtension.Contains(FileExtension)) MediaTypeId = 1;

                      AddQueueFiles(MediaId, MediaTypeId, FilePath);
                  }
                    else if (data.StartsWith("SYNCDOCFILE"))
                    {
                        int MediaId = 0;
                        string FilePath = data.Replace("SYNCDOCFILE ", "").Trim();
                        AddQueueDocFiles(MediaId, FilePath);
                    }
                    else if (data.StartsWith("SYNCDOC"))
                    {
                        WriteLog_Error("From: " + m_SockServer[e.ConnectionGuid].RemoteHost.ToString() + ": " + data); //valid command: SYNC MediaId:FileType:RelativeFilePath; Exp: SYNC 1234:Image:Standard/abc.gif
                        string[] dataArr = data.Replace("SYNCDOC ", "").Split(':');
                        if (dataArr.Length == 3)
                        {
                            AddQueueDocFiles(int.Parse(dataArr[0]), dataArr[2]);
                        }
                        else if (dataArr.Length == 1)
                        {
                            LoadDocFileData(int.Parse(dataArr[0]));
                        }
                        else
                        {
                            LoadDocFileData(0);
                        }
                    }
                    else if (data.StartsWith("SYNC"))
                  {
                      WriteLog_Error("From: " + m_SockServer[e.ConnectionGuid].RemoteHost.ToString() + ": " + data); //valid command: SYNC MediaId:FileType:RelativeFilePath; Exp: SYNC 1234:Image:Standard/abc.gif
                      string[] dataArr = data.Replace("SYNC ","").Split(':');
                      if (dataArr.Length == 3)
                      {
                          AddQueueFiles(int.Parse(dataArr[0]), int.Parse(dataArr[1]), dataArr[2]);
                      }
                      else if (dataArr.Length == 1)
                      {
                          LoadData(int.Parse(dataArr[0]));
                      }
                      else
                      {
                          LoadData(0);
                      }
                  }
                  
                }
              m_SockServer.Send(e.ConnectionGuid, "OK");
          }
          catch (Exception ex)
          {
              WriteLog_Error(ex.ToString());
          }
      }

      private void m_SockServer_evClassInformation(object sender, Devshock.Net.SockServer.ClassInformationEventArgs e)
      {
          WriteLog("Server: " + e.Description);
      }

      private void m_SockServer_evClassException(object sender, Devshock.Net.SockServer.ClassExceptionEventArgs e)
      {
          WriteLog("Exception Server: " + e.ClassException.ToString());
      }

      private void MainForm_Closing(object sender, CancelEventArgs e)
      {
          if (btnStop.Enabled) btnStop_Click(btnStop, new System.EventArgs());
      }

      private void btnSync_Click(object sender, EventArgs e)
      {
          LoadDocFileData(0);
      }

      private void AddQueueFiles(int MediaId, int MediaTypeId, string FilePathRelative)
      {
          if (MediaId > 0)
          {
              //Update status DB
              MediasDB m_Medias = new MediasDB();
              m_Medias.ResetSyncServerCount(MediaId);
              m_Medias.UpdateSyncStatus(MediaId, 3);
          }
            string l_SynServer = ConfigurationManager.AppSettings["ServiceAddress"].ToString();
            if(MediaTypeId != 1)
            {
                l_SynServer = ConfigurationManager.AppSettings["ServiceAddressOther"].ToString();
            }
          foreach (string ServiceAddress in l_SynServer.Split(';'))
          {
              SyncFileInfo m_SyncFileInfo = new SyncFileInfo();
              m_SyncFileInfo.ServiceAddress = ServiceAddress;
              m_SyncFileInfo.Username = ConfigurationManager.AppSettings["Username"].ToString();
              m_SyncFileInfo.Password = ConfigurationManager.AppSettings["Password"].ToString();
              m_SyncFileInfo.LocalRootDir = ConfigurationManager.AppSettings["LocalRootDir"].ToString();
              m_SyncFileInfo.MediaId = MediaId;
              m_SyncFileInfo.FileType = MediaTypeId == 1 ? "Image" : "Other";
              m_SyncFileInfo.LocalRelativeFilePath = FilePathRelative.Replace("/", "\\");
              if (m_SyncFileInfo.LocalRelativeFilePath.StartsWith("\\"))
              {
                  m_SyncFileInfo.LocalRelativeFilePath = m_SyncFileInfo.LocalRelativeFilePath.Substring(1);
              }

              string filename = m_SyncFileInfo.LocalRelativeFilePath.ToLower();
              if (filename.EndsWith("flv"))
              {
                  m_SyncFileInfo.LocalRelativeFilePath = "FLV\\" + m_SyncFileInfo.LocalRelativeFilePath;
              }
              else if (filename.EndsWith("3gp"))
              {
                  m_SyncFileInfo.LocalRelativeFilePath = "3GP\\" + m_SyncFileInfo.LocalRelativeFilePath;
              }
              else if (filename.EndsWith("mp4"))
              {
                  m_SyncFileInfo.LocalRelativeFilePath = "MP4\\" + m_SyncFileInfo.LocalRelativeFilePath;
              }
              else
              {
                  m_SyncFileInfo.LocalRelativeFilePath = "original\\" + m_SyncFileInfo.LocalRelativeFilePath;
              }

              if (m_SyncFileInfo.LocalRelativeFilePath.StartsWith("\\"))
              {
                  m_SyncFileInfo.LocalRelativeFilePath = m_SyncFileInfo.LocalRelativeFilePath.Substring(1);
              }
              work.Add(new SyncFileWorkItem(m_SyncFileInfo, this));
          }
      }
        private void AddQueueDocFiles(int DocFileId, string FilePathRelative)
        {
            if (DocFileId > 0)
            {
                //Update status DB
                DocFilesDB m_DocFiles = new DocFilesDB();
                m_DocFiles.ResetSyncServerCount(DocFileId);
                m_DocFiles.UpdateSyncStatus(DocFileId, 3);
            }
            string l_SynServer = ConfigurationManager.AppSettings["ServiceAddressOther"].ToString();
            
            foreach (string ServiceAddress in l_SynServer.Split(';'))
            {
                SyncFileInfo m_SyncFileInfo = new SyncFileInfo();
                m_SyncFileInfo.ServiceAddress = ServiceAddress;
                m_SyncFileInfo.Username = ConfigurationManager.AppSettings["Username"].ToString();
                m_SyncFileInfo.Password = ConfigurationManager.AppSettings["Password"].ToString();
                m_SyncFileInfo.LocalRootDir = ConfigurationManager.AppSettings["LocalRootDir"].ToString();
                m_SyncFileInfo.MediaId = DocFileId;
                m_SyncFileInfo.FileType =  "Other";
                m_SyncFileInfo.LocalRelativeFilePath = FilePathRelative.Replace("/", "\\");
                if (m_SyncFileInfo.LocalRelativeFilePath.StartsWith("\\"))
                {
                    m_SyncFileInfo.LocalRelativeFilePath = m_SyncFileInfo.LocalRelativeFilePath.Substring(1);
                }
                
                work.Add(new SyncDocFileWorkItem(m_SyncFileInfo, this));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtLog.Text = FilterDocContent(txtLog.Text);
        }
        public string FilterDocContent(string DocContent)
        {
            try
            {
                string RetVal = DocContent;
                int MaxLoop = 10;
                int CurentLoop = 0;
                //DocLink
                string RegexLinkRule = "<a([^>]+)href=([^>]+)/([^>]+)-([0-9]+)-(d[1-8]).html([^>]+)>";
                RegexOptions m_RegexOptions = RegexOptions.None;// RegexOptions.Multiline | RegexOptions.IgnoreCase;
                Match m_Match = Regex.Match(RetVal, RegexLinkRule, m_RegexOptions);
                while (m_Match.Success)
                {
                    int DocId = int.Parse(m_Match.Groups[4].Value);
                    string DocHexId = DocId.ToString("x");
                    string NewUrl = DocHexId + ".html";
                    RetVal = RetVal.Replace(m_Match.Groups[4].Value + "-" + m_Match.Groups[5].Value + ".html", NewUrl);
                    m_Match = m_Match.NextMatch();
                }

                //End DocLink
                //Old Doc Link
                RegexLinkRule = "<a([^>]+)href=([^>]+)/VL/([^>]+)>";
                m_Match = Regex.Match(RetVal, RegexLinkRule, m_RegexOptions);
                while (m_Match.Success)
                {

                    string NewUrl = "<a href=\"#\">";
                    RetVal = RetVal.Replace(m_Match.Groups[0].Value, NewUrl);
                    m_Match = m_Match.NextMatch();
                }
                //end Old Doc Link
                //finanly replace all
                RetVal = RetVal.Replace("luatvietnam.vn", "vanbanluat.vn");
                // Put your code here
                return RetVal;
            }
            catch(Exception ex)
            {
                txtErrorLog.Text += Environment.NewLine + ex.ToString();
                return DocContent;
            }

        }
    }
}
