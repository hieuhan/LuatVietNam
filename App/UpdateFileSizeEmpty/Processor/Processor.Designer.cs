namespace Processor
{
    partial class Processor
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
            this.tabControl_Processor = new System.Windows.Forms.TabControl();
            this.tab_SendMessage = new System.Windows.Forms.TabPage();
            this.lbFileProcess = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btBrowseOutput = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.lbOutputFolder = new System.Windows.Forms.Label();
            this.btBrowseRoot = new System.Windows.Forms.Button();
            this.txtRootFolder = new System.Windows.Forms.TextBox();
            this.lbRootFolder = new System.Windows.Forms.Label();
            this.chkShowLog = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tab_SendAlert = new System.Windows.Forms.TabPage();
            this.chkShowLog_SendAlert = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStatus_Alert = new System.Windows.Forms.TextBox();
            this.btnStop_SendAlert = new System.Windows.Forms.Button();
            this.btnStart_SendAlert = new System.Windows.Forms.Button();
            this.txtLog_Alert = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl_Processor.SuspendLayout();
            this.tab_SendMessage.SuspendLayout();
            this.tab_SendAlert.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_Processor
            // 
            this.tabControl_Processor.Controls.Add(this.tab_SendMessage);
            this.tabControl_Processor.Controls.Add(this.tab_SendAlert);
            this.tabControl_Processor.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Processor.Name = "tabControl_Processor";
            this.tabControl_Processor.SelectedIndex = 0;
            this.tabControl_Processor.Size = new System.Drawing.Size(628, 493);
            this.tabControl_Processor.TabIndex = 0;
            // 
            // tab_SendMessage
            // 
            this.tab_SendMessage.Controls.Add(this.lbFileProcess);
            this.tab_SendMessage.Controls.Add(this.lbStatus);
            this.tab_SendMessage.Controls.Add(this.label5);
            this.tab_SendMessage.Controls.Add(this.btBrowseOutput);
            this.tab_SendMessage.Controls.Add(this.txtOutputFolder);
            this.tab_SendMessage.Controls.Add(this.lbOutputFolder);
            this.tab_SendMessage.Controls.Add(this.btBrowseRoot);
            this.tab_SendMessage.Controls.Add(this.txtRootFolder);
            this.tab_SendMessage.Controls.Add(this.lbRootFolder);
            this.tab_SendMessage.Controls.Add(this.chkShowLog);
            this.tab_SendMessage.Controls.Add(this.label2);
            this.tab_SendMessage.Controls.Add(this.label1);
            this.tab_SendMessage.Controls.Add(this.txtStatus);
            this.tab_SendMessage.Controls.Add(this.btnStop);
            this.tab_SendMessage.Controls.Add(this.btnStart);
            this.tab_SendMessage.Controls.Add(this.txtLog);
            this.tab_SendMessage.Location = new System.Drawing.Point(4, 22);
            this.tab_SendMessage.Name = "tab_SendMessage";
            this.tab_SendMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tab_SendMessage.Size = new System.Drawing.Size(620, 467);
            this.tab_SendMessage.TabIndex = 0;
            this.tab_SendMessage.Text = "Extract word file";
            this.tab_SendMessage.UseVisualStyleBackColor = true;
            // 
            // lbFileProcess
            // 
            this.lbFileProcess.AutoSize = true;
            this.lbFileProcess.Location = new System.Drawing.Point(448, 81);
            this.lbFileProcess.Name = "lbFileProcess";
            this.lbFileProcess.Size = new System.Drawing.Size(0, 13);
            this.lbFileProcess.TabIndex = 16;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(54, 69);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(46, 13);
            this.lbStatus.TabIndex = 15;
            this.lbStatus.Text = "sleeping";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Status:";
            // 
            // btBrowseOutput
            // 
            this.btBrowseOutput.Location = new System.Drawing.Point(300, 43);
            this.btBrowseOutput.Name = "btBrowseOutput";
            this.btBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.btBrowseOutput.TabIndex = 13;
            this.btBrowseOutput.Text = "Browse";
            this.btBrowseOutput.UseVisualStyleBackColor = true;
            this.btBrowseOutput.Click += new System.EventHandler(this.btBrowseOutput_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(81, 43);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(213, 20);
            this.txtOutputFolder.TabIndex = 12;
            this.txtOutputFolder.Text = "E:\\Projects\\2017\\luatvietnamv2\\Code\\App\\CompleteData";
            // 
            // lbOutputFolder
            // 
            this.lbOutputFolder.AutoSize = true;
            this.lbOutputFolder.Location = new System.Drawing.Point(4, 46);
            this.lbOutputFolder.Name = "lbOutputFolder";
            this.lbOutputFolder.Size = new System.Drawing.Size(71, 13);
            this.lbOutputFolder.TabIndex = 11;
            this.lbOutputFolder.Text = "Output Folder";
            // 
            // btBrowseRoot
            // 
            this.btBrowseRoot.Location = new System.Drawing.Point(300, 17);
            this.btBrowseRoot.Name = "btBrowseRoot";
            this.btBrowseRoot.Size = new System.Drawing.Size(75, 23);
            this.btBrowseRoot.TabIndex = 10;
            this.btBrowseRoot.Text = "Browse";
            this.btBrowseRoot.UseVisualStyleBackColor = true;
            this.btBrowseRoot.Click += new System.EventHandler(this.btBrowseRoot_Click);
            // 
            // txtRootFolder
            // 
            this.txtRootFolder.Location = new System.Drawing.Point(81, 17);
            this.txtRootFolder.Name = "txtRootFolder";
            this.txtRootFolder.Size = new System.Drawing.Size(213, 20);
            this.txtRootFolder.TabIndex = 9;
            this.txtRootFolder.Text = "E:\\Projects\\2017\\luatvietnamv2\\Code\\App\\TempLawData";
            // 
            // lbRootFolder
            // 
            this.lbRootFolder.AutoSize = true;
            this.lbRootFolder.Location = new System.Drawing.Point(4, 17);
            this.lbRootFolder.Name = "lbRootFolder";
            this.lbRootFolder.Size = new System.Drawing.Size(62, 13);
            this.lbRootFolder.TabIndex = 8;
            this.lbRootFolder.Text = "Root Folder";
            // 
            // chkShowLog
            // 
            this.chkShowLog.AutoSize = true;
            this.chkShowLog.Checked = true;
            this.chkShowLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLog.Location = new System.Drawing.Point(300, 101);
            this.chkShowLog.Name = "chkShowLog";
            this.chkShowLog.Size = new System.Drawing.Size(70, 17);
            this.chkShowLog.TabIndex = 7;
            this.chkShowLog.Text = "Show log";
            this.chkShowLog.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(373, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(6, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Process status";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(376, 124);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(240, 336);
            this.txtStatus.TabIndex = 4;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(467, 22);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(448, 22);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(6, 124);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(364, 336);
            this.txtLog.TabIndex = 1;
            // 
            // tab_SendAlert
            // 
            this.tab_SendAlert.Controls.Add(this.chkShowLog_SendAlert);
            this.tab_SendAlert.Controls.Add(this.label3);
            this.tab_SendAlert.Controls.Add(this.label4);
            this.tab_SendAlert.Controls.Add(this.txtStatus_Alert);
            this.tab_SendAlert.Controls.Add(this.btnStop_SendAlert);
            this.tab_SendAlert.Controls.Add(this.btnStart_SendAlert);
            this.tab_SendAlert.Controls.Add(this.txtLog_Alert);
            this.tab_SendAlert.Location = new System.Drawing.Point(4, 22);
            this.tab_SendAlert.Name = "tab_SendAlert";
            this.tab_SendAlert.Size = new System.Drawing.Size(620, 467);
            this.tab_SendAlert.TabIndex = 1;
            this.tab_SendAlert.Text = "Send Alert";
            this.tab_SendAlert.UseVisualStyleBackColor = true;
            // 
            // chkShowLog_SendAlert
            // 
            this.chkShowLog_SendAlert.AutoSize = true;
            this.chkShowLog_SendAlert.Checked = true;
            this.chkShowLog_SendAlert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLog_SendAlert.Location = new System.Drawing.Point(290, 6);
            this.chkShowLog_SendAlert.Name = "chkShowLog_SendAlert";
            this.chkShowLog_SendAlert.Size = new System.Drawing.Size(70, 17);
            this.chkShowLog_SendAlert.TabIndex = 14;
            this.chkShowLog_SendAlert.Text = "Show log";
            this.chkShowLog_SendAlert.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(376, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Message";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(5, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Process status";
            // 
            // txtStatus_Alert
            // 
            this.txtStatus_Alert.Location = new System.Drawing.Point(376, 68);
            this.txtStatus_Alert.Multiline = true;
            this.txtStatus_Alert.Name = "txtStatus_Alert";
            this.txtStatus_Alert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus_Alert.Size = new System.Drawing.Size(240, 392);
            this.txtStatus_Alert.TabIndex = 11;
            // 
            // btnStop_SendAlert
            // 
            this.btnStop_SendAlert.Location = new System.Drawing.Point(467, 22);
            this.btnStop_SendAlert.Name = "btnStop_SendAlert";
            this.btnStop_SendAlert.Size = new System.Drawing.Size(75, 23);
            this.btnStop_SendAlert.TabIndex = 10;
            this.btnStop_SendAlert.Text = "Stop";
            this.btnStop_SendAlert.UseVisualStyleBackColor = true;
            this.btnStop_SendAlert.Visible = false;
            // 
            // btnStart_SendAlert
            // 
            this.btnStart_SendAlert.Location = new System.Drawing.Point(448, 22);
            this.btnStart_SendAlert.Name = "btnStart_SendAlert";
            this.btnStart_SendAlert.Size = new System.Drawing.Size(75, 23);
            this.btnStart_SendAlert.TabIndex = 9;
            this.btnStart_SendAlert.Text = "Start";
            this.btnStart_SendAlert.UseVisualStyleBackColor = true;
            // 
            // txtLog_Alert
            // 
            this.txtLog_Alert.Location = new System.Drawing.Point(6, 24);
            this.txtLog_Alert.Multiline = true;
            this.txtLog_Alert.Name = "txtLog_Alert";
            this.txtLog_Alert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog_Alert.Size = new System.Drawing.Size(364, 436);
            this.txtLog_Alert.TabIndex = 8;
            // 
            // Processor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 492);
            this.Controls.Add(this.tabControl_Processor);
            this.MaximizeBox = false;
            this.Name = "Processor";
            this.Text = "Form1";
            this.tabControl_Processor.ResumeLayout(false);
            this.tab_SendMessage.ResumeLayout(false);
            this.tab_SendMessage.PerformLayout();
            this.tab_SendAlert.ResumeLayout(false);
            this.tab_SendAlert.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_Processor;
        private System.Windows.Forms.TabPage tab_SendMessage;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowLog;
        private System.Windows.Forms.TabPage tab_SendAlert;
        private System.Windows.Forms.CheckBox chkShowLog_SendAlert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStatus_Alert;
        private System.Windows.Forms.Button btnStop_SendAlert;
        private System.Windows.Forms.Button btnStart_SendAlert;
        private System.Windows.Forms.TextBox txtLog_Alert;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btBrowseOutput;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label lbOutputFolder;
        private System.Windows.Forms.Button btBrowseRoot;
        private System.Windows.Forms.TextBox txtRootFolder;
        private System.Windows.Forms.Label lbRootFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbFileProcess;
    }
}

