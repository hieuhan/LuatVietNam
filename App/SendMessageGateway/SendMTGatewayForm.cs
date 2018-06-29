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
using ICSoft.LawDocsLib;
using System.Collections.Generic;

namespace SendMTGateway
{
	/// <summary>
	///  A WorkQueue sample.
	/// </summary>
	public class SendMTGatewayForm : System.Windows.Forms.Form
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
      public bool m_NewsletterRun = false;
      public bool m_MessageNotifyRun = false;
      public bool m_ProcessVideoFileRun = false;

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

      public SendMTGatewayForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            if (ConfigurationManager.AppSettings["MaxRow"] != null) txtAmount.Text = ConfigurationManager.AppSettings["MaxRow"].ToString();
            if (ConfigurationManager.AppSettings["SleepTime"] != null) txtWait.Text = ConfigurationManager.AppSettings["SleepTime"].ToString();
            string m_FormTitle = ConfigurationManager.AppSettings["FormTitle"] != null ? ConfigurationManager.AppSettings["FormTitle"].ToString() : "Send message";
            this.Text = m_FormTitle;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMTGatewayForm));
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
            this.txtWait.Text = "2000";
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
            this.maxThreads.Text = "3";
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
            this.btnStop.Location = new System.Drawing.Point(405, 66);
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
            // SendMTGatewayForm
            // 
            this.AcceptButton = this.produceWork;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(517, 460);
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
            this.Name = "SendMTGatewayForm";
            this.Text = "Send message to gate";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
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

         Application.Run(new SendMTGatewayForm());
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
            

          m_ThreadRunning = 0;
          isRunning = true;
          produceWork.Enabled = false;

          Thread myThread;
          if (ConfigurationManager.AppSettings["ENABLE_SEND_MESSAGE"].ToString() == "1")
          {
              myThread = new Thread(new ThreadStart(MonitorLoadData));
              myThread.IsBackground = true;
              myThread.Start();
          }
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
              LoadData();
              System.Threading.Thread.Sleep(m_SleepTime);
          }
          m_ThreadRunning--;
          //WriteLog_Error("End thread load data!");
      }

      private void LoadData()
      {
          try
          {
              //if (m_NewsletterRun == false) work.Add(new NewsletterWorkItem(this));
              //if (m_MessageNotifyRun == false) work.Add(new MessageNotifyWorkItem(this));

              int totalRow = 0;
              lock (work)
              {
                  if (work.Count <= 0)
                  {
                      try
                      {
                          MessageSends m_MessageSends = new MessageSends();
                          List<MessageSends> l_MessageSends = m_MessageSends.MessageSends_GetListToGateway(0, m_MaxRow);
                          totalRow = l_MessageSends.Count;
                          for (int i = 0; i < l_MessageSends.Count; i++)
                          {
                              work.Add(new MessageNotifyWorkItem(l_MessageSends[i], this));
                          }

                          //NewsletterSends m_NewsletterSends = new NewsletterSends();
                          //List<NewsletterSends> l_NewsletterSends = m_NewsletterSends.GetListToGateway(m_MaxRow);
                          //totalRow = totalRow + l_NewsletterSends.Count;
                          //for (int i = 0; i < l_NewsletterSends.Count; i++)
                          //{
                          //    work.Add(new NewsletterWorkItem(l_NewsletterSends[i], this));
                          //}
                      }
                      catch (Exception ex)
                      {
                          WriteLog_Error(ex.Message.ToString());
                      }
                  }
              }
              WriteLog_LoadData("Load data: " + totalRow.ToString());
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
                  txtLog.AppendText(DateTime.Now.ToString() + ": " + s + "\r\n\r\n");
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
              txtErrorLog.AppendText(DateTime.Now.ToString() + ": " + s + "\r\n\r\n");
          }
      }
   }
}
