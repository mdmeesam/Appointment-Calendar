
namespace Calendar
{
    partial class CreateEvent
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();
            this.actionList = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.applicationListOpen = new Telerik.WinControls.UI.RadDropDownList();
            this.appListLabel = new System.Windows.Forms.Label();
            this.applicationListClose = new Telerik.WinControls.UI.RadDropDownList();
            this.Schedule = new System.Windows.Forms.Label();
            this.EventDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.EventTime = new Telerik.WinControls.UI.RadTimePicker();
            this.SaveEventBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.actionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationListOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationListClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventTime)).BeginInit();
            this.SuspendLayout();
            // 
            // actionList
            // 
            radListDataItem1.Text = "Open an Application";
            radListDataItem2.Text = "Turn off PC";
            radListDataItem3.Text = "Turn off Application";
            this.actionList.Items.Add(radListDataItem1);
            this.actionList.Items.Add(radListDataItem2);
            this.actionList.Items.Add(radListDataItem3);
            this.actionList.Location = new System.Drawing.Point(156, 77);
            this.actionList.Name = "actionList";
            this.actionList.Size = new System.Drawing.Size(209, 20);
            this.actionList.TabIndex = 0;
            this.actionList.Text = "Select an Action";
            this.actionList.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownList1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "What would you like to do?";
            // 
            // applicationListOpen
            // 
            radListDataItem4.Text = "Notepad";
            this.applicationListOpen.Items.Add(radListDataItem4);
            this.applicationListOpen.Location = new System.Drawing.Point(156, 116);
            this.applicationListOpen.Name = "applicationListOpen";
            this.applicationListOpen.Size = new System.Drawing.Size(125, 20);
            this.applicationListOpen.TabIndex = 2;
            this.applicationListOpen.Visible = false;
            this.applicationListOpen.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.applicationList_SelectedIndexChanged);
            // 
            // appListLabel
            // 
            this.appListLabel.AutoSize = true;
            this.appListLabel.Location = new System.Drawing.Point(52, 117);
            this.appListLabel.Name = "appListLabel";
            this.appListLabel.Size = new System.Drawing.Size(98, 13);
            this.appListLabel.TabIndex = 3;
            this.appListLabel.Text = "Choose Application";
            this.appListLabel.Visible = false;
            // 
            // applicationListClose
            // 
            radListDataItem5.Text = "Notepad";
            this.applicationListClose.Items.Add(radListDataItem5);
            this.applicationListClose.Location = new System.Drawing.Point(287, 116);
            this.applicationListClose.Name = "applicationListClose";
            this.applicationListClose.Size = new System.Drawing.Size(125, 20);
            this.applicationListClose.TabIndex = 4;
            this.applicationListClose.Visible = false;
            this.applicationListClose.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.applicationListClose_SelectedIndexChanged);
            // 
            // Schedule
            // 
            this.Schedule.AutoSize = true;
            this.Schedule.Location = new System.Drawing.Point(53, 161);
            this.Schedule.Name = "Schedule";
            this.Schedule.Size = new System.Drawing.Size(104, 13);
            this.Schedule.TabIndex = 5;
            this.Schedule.Text = "Schedule Date Time";
            // 
            // EventDate
            // 
            this.EventDate.Location = new System.Drawing.Point(165, 158);
            this.EventDate.Name = "EventDate";
            this.EventDate.Size = new System.Drawing.Size(148, 20);
            this.EventDate.TabIndex = 7;
            this.EventDate.TabStop = false;
            this.EventDate.Text = "Monday, May 17, 2021";
            this.EventDate.Value = new System.DateTime(2021, 5, 17, 11, 22, 49, 818);
            // 
            // EventTime
            // 
            this.EventTime.Location = new System.Drawing.Point(165, 186);
            this.EventTime.MaxValue = new System.DateTime(9999, 12, 31, 23, 59, 59, 0);
            this.EventTime.MinValue = new System.DateTime(((long)(0)));
            this.EventTime.Name = "EventTime";
            this.EventTime.Size = new System.Drawing.Size(148, 20);
            this.EventTime.TabIndex = 8;
            this.EventTime.TabStop = false;
            this.EventTime.Value = new System.DateTime(2021, 5, 17, 11, 23, 5, 582);
            // 
            // SaveEventBtn
            // 
            this.SaveEventBtn.Enabled = false;
            this.SaveEventBtn.Location = new System.Drawing.Point(156, 228);
            this.SaveEventBtn.Name = "SaveEventBtn";
            this.SaveEventBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveEventBtn.TabIndex = 9;
            this.SaveEventBtn.Text = "Save";
            this.SaveEventBtn.UseVisualStyleBackColor = true;
            this.SaveEventBtn.Click += new System.EventHandler(this.SaveEventBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Location = new System.Drawing.Point(276, 228);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(75, 23);
            this.CloseBtn.TabIndex = 10;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Location = new System.Drawing.Point(43, 228);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(75, 23);
            this.DeleteBtn.TabIndex = 11;
            this.DeleteBtn.Text = "Delete";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Visible = false;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // CreateEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 279);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.SaveEventBtn);
            this.Controls.Add(this.EventTime);
            this.Controls.Add(this.EventDate);
            this.Controls.Add(this.Schedule);
            this.Controls.Add(this.applicationListClose);
            this.Controls.Add(this.appListLabel);
            this.Controls.Add(this.applicationListOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.actionList);
            this.Name = "CreateEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateEvent";
            this.Load += new System.EventHandler(this.CreateEvent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.actionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationListOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationListClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EventTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Telerik.WinControls.UI.RadDropDownList actionList;
        private System.Windows.Forms.Label label1;
        public Telerik.WinControls.UI.RadDropDownList applicationListOpen;
        private System.Windows.Forms.Label appListLabel;
        public Telerik.WinControls.UI.RadDropDownList applicationListClose;
        private System.Windows.Forms.Label Schedule;
        public Telerik.WinControls.UI.RadDateTimePicker EventDate;
        public Telerik.WinControls.UI.RadTimePicker EventTime;
        private System.Windows.Forms.Button SaveEventBtn;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Button DeleteBtn;
    }
}