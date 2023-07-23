namespace WinFormMVC.View
{
    partial class UsersView 
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
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.catButton = new System.Windows.Forms.Button();
            this.dirButton = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.grbSex = new System.Windows.Forms.GroupBox();
            this.rdNormal = new System.Windows.Forms.RadioButton();
            this.rdSlow = new System.Windows.Forms.RadioButton();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.grdUsers = new System.Windows.Forms.ListView();
            this.button3 = new System.Windows.Forms.Button();
            this.txtOutPut = new System.Windows.Forms.TextBox();
            this.Tasklist = new System.Windows.Forms.Button();
            this.searchProc = new System.Windows.Forms.Button();
            this.localIp = new System.Windows.Forms.Button();
            this.WinDefButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtProcName = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExePath = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.txtSeDBGbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLsText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpDetails.SuspendLayout();
            this.grbSex.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(78, 89);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(127, 20);
            this.txtID.TabIndex = 5;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(78, 54);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(127, 20);
            this.txtLastName.TabIndex = 4;
            // 
            // lblID
            // 
            this.lblID.Location = new System.Drawing.Point(18, 89);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(80, 23);
            this.lblID.TabIndex = 25;
            this.lblID.Text = "ID:";
            // 
            // lblLastName
            // 
            this.lblLastName.Location = new System.Drawing.Point(18, 57);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(63, 23);
            this.lblLastName.TabIndex = 23;
            this.lblLastName.Text = "IP";
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.button10);
            this.grpDetails.Controls.Add(this.txtExePath);
            this.grpDetails.Controls.Add(this.label1);
            this.grpDetails.Controls.Add(this.button9);
            this.grpDetails.Controls.Add(this.button8);
            this.grpDetails.Controls.Add(this.button7);
            this.grpDetails.Controls.Add(this.button6);
            this.grpDetails.Controls.Add(this.txtDepartment);
            this.grpDetails.Controls.Add(this.lblDepartment);
            this.grpDetails.Controls.Add(this.button4);
            this.grpDetails.Controls.Add(this.txtID);
            this.grpDetails.Controls.Add(this.lblID);
            this.grpDetails.Controls.Add(this.txtLastName);
            this.grpDetails.Controls.Add(this.lblLastName);
            this.grpDetails.Controls.Add(this.txtFirstName);
            this.grpDetails.Controls.Add(this.lblFirstName);
            this.grpDetails.Controls.Add(this.WinDefButton);
            this.grpDetails.Controls.Add(this.button3);
            this.grpDetails.Controls.Add(this.localIp);
            this.grpDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grpDetails.Location = new System.Drawing.Point(12, 12);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(600, 179);
            this.grpDetails.TabIndex = 34;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Register new user :";
            this.grpDetails.Enter += new System.EventHandler(this.grpDetails_Enter);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(35, 255);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 38;
            this.button2.Text = "HTTP GET";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // catButton
            // 
            this.catButton.Location = new System.Drawing.Point(116, 197);
            this.catButton.Name = "catButton";
            this.catButton.Size = new System.Drawing.Size(75, 23);
            this.catButton.TabIndex = 37;
            this.catButton.Text = "Cat";
            this.catButton.UseVisualStyleBackColor = true;
            this.catButton.Click += new System.EventHandler(this.catButton_Click);
            // 
            // dirButton
            // 
            this.dirButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.dirButton.Location = new System.Drawing.Point(35, 197);
            this.dirButton.Name = "dirButton";
            this.dirButton.Size = new System.Drawing.Size(75, 23);
            this.dirButton.TabIndex = 36;
            this.dirButton.Text = "Dir";
            this.dirButton.UseVisualStyleBackColor = true;
            this.dirButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(35, 226);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(187, 20);
            this.txtPath.TabIndex = 32;
            // 
            // grbSex
            // 
            this.grbSex.Controls.Add(this.rdNormal);
            this.grbSex.Controls.Add(this.rdSlow);
            this.grbSex.ForeColor = System.Drawing.SystemColors.MenuText;
            this.grbSex.Location = new System.Drawing.Point(332, 201);
            this.grbSex.Name = "grbSex";
            this.grbSex.Size = new System.Drawing.Size(184, 54);
            this.grbSex.TabIndex = 29;
            this.grbSex.TabStop = false;
            this.grbSex.Text = "Speed (todo)";
            // 
            // rdNormal
            // 
            this.rdNormal.Location = new System.Drawing.Point(87, 24);
            this.rdNormal.Name = "rdNormal";
            this.rdNormal.Size = new System.Drawing.Size(72, 24);
            this.rdNormal.TabIndex = 5;
            this.rdNormal.Text = "Normal";
            // 
            // rdSlow
            // 
            this.rdSlow.Location = new System.Drawing.Point(6, 21);
            this.rdSlow.Name = "rdSlow";
            this.rdSlow.Size = new System.Drawing.Size(75, 24);
            this.rdSlow.TabIndex = 4;
            this.rdSlow.Text = "Slow";
            this.rdSlow.CheckedChanged += new System.EventHandler(this.rdMale_CheckedChanged);
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(78, 118);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(127, 20);
            this.txtDepartment.TabIndex = 27;
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(18, 118);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(80, 23);
            this.lblDepartment.TabIndex = 28;
            this.lblDepartment.Text = "Defender";
            this.lblDepartment.Click += new System.EventHandler(this.lblDepartment_Click);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(78, 28);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(127, 20);
            this.txtFirstName.TabIndex = 1;
            // 
            // lblFirstName
            // 
            this.lblFirstName.Location = new System.Drawing.Point(18, 31);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(63, 23);
            this.lblFirstName.TabIndex = 19;
            this.lblFirstName.Text = "User";
            // 
            // grdUsers
            // 
            this.grdUsers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdUsers.FullRowSelect = true;
            this.grdUsers.GridLines = true;
            this.grdUsers.HideSelection = false;
            this.grdUsers.Location = new System.Drawing.Point(0, 327);
            this.grdUsers.Name = "grdUsers";
            this.grdUsers.Size = new System.Drawing.Size(1283, 147);
            this.grdUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.grdUsers.TabIndex = 35;
            this.grdUsers.UseCompatibleStateImageBehavior = false;
            this.grdUsers.View = System.Windows.Forms.View.Details;
            this.grdUsers.SelectedIndexChanged += new System.EventHandler(this.grdUsers_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(211, 26);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 23);
            this.button3.TabIndex = 31;
            this.button3.Text = "Get Username";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtOutPut
            // 
            this.txtOutPut.Location = new System.Drawing.Point(744, 25);
            this.txtOutPut.Multiline = true;
            this.txtOutPut.Name = "txtOutPut";
            this.txtOutPut.Size = new System.Drawing.Size(527, 296);
            this.txtOutPut.TabIndex = 36;
            this.txtOutPut.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Tasklist
            // 
            this.Tasklist.Location = new System.Drawing.Point(663, 23);
            this.Tasklist.Name = "Tasklist";
            this.Tasklist.Size = new System.Drawing.Size(75, 23);
            this.Tasklist.TabIndex = 37;
            this.Tasklist.Text = "Tasklist";
            this.Tasklist.UseVisualStyleBackColor = true;
            this.Tasklist.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // searchProc
            // 
            this.searchProc.Location = new System.Drawing.Point(618, 52);
            this.searchProc.Name = "searchProc";
            this.searchProc.Size = new System.Drawing.Size(120, 23);
            this.searchProc.TabIndex = 38;
            this.searchProc.Text = "Search proc name";
            this.searchProc.UseVisualStyleBackColor = true;
            this.searchProc.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // localIp
            // 
            this.localIp.Location = new System.Drawing.Point(211, 54);
            this.localIp.Name = "localIp";
            this.localIp.Size = new System.Drawing.Size(75, 23);
            this.localIp.TabIndex = 39;
            this.localIp.Text = "Local IP";
            this.localIp.UseVisualStyleBackColor = true;
            this.localIp.Click += new System.EventHandler(this.localIp_Click);
            // 
            // WinDefButton
            // 
            this.WinDefButton.Location = new System.Drawing.Point(211, 115);
            this.WinDefButton.Name = "WinDefButton";
            this.WinDefButton.Size = new System.Drawing.Size(116, 23);
            this.WinDefButton.TabIndex = 40;
            this.WinDefButton.Text = "Check Defender";
            this.WinDefButton.UseVisualStyleBackColor = true;
            this.WinDefButton.Click += new System.EventHandler(this.WinDefButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(618, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 41;
            this.button1.Text = "Kill proc name";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_3);
            // 
            // txtProcName
            // 
            this.txtProcName.Location = new System.Drawing.Point(618, 110);
            this.txtProcName.Name = "txtProcName";
            this.txtProcName.Size = new System.Drawing.Size(120, 20);
            this.txtProcName.TabIndex = 42;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(320, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 23);
            this.button4.TabIndex = 43;
            this.button4.Text = "X Processors";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(116, 255);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 23);
            this.button5.TabIndex = 44;
            this.button5.Text = "X HTTP POST";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(509, 26);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(91, 23);
            this.button6.TabIndex = 44;
            this.button6.Text = "X Disk";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(417, 25);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(91, 23);
            this.button7.TabIndex = 45;
            this.button7.Text = "X RAM";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(211, 89);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 46;
            this.button8.Text = "X Hide file";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(292, 89);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 47;
            this.button9.Text = "X Destroy";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 48;
            this.label1.Text = "Path";
            // 
            // txtExePath
            // 
            this.txtExePath.Location = new System.Drawing.Point(78, 153);
            this.txtExePath.Name = "txtExePath";
            this.txtExePath.Size = new System.Drawing.Size(435, 20);
            this.txtExePath.TabIndex = 49;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(519, 151);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 45;
            this.button10.Text = "Get Path";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(618, 136);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(120, 23);
            this.button11.TabIndex = 45;
            this.button11.Text = "Enable SEDEBUG";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(618, 168);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(120, 23);
            this.button12.TabIndex = 46;
            this.button12.Text = "Dump LSASS";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // txtSeDBGbox
            // 
            this.txtSeDBGbox.Location = new System.Drawing.Point(618, 197);
            this.txtSeDBGbox.Name = "txtSeDBGbox";
            this.txtSeDBGbox.Size = new System.Drawing.Size(120, 20);
            this.txtSeDBGbox.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(528, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 23);
            this.label2.TabIndex = 50;
            this.label2.Text = "SeDebug:";
            // 
            // txtLsText
            // 
            this.txtLsText.Location = new System.Drawing.Point(618, 220);
            this.txtLsText.Name = "txtLsText";
            this.txtLsText.Size = new System.Drawing.Size(120, 20);
            this.txtLsText.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(528, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 23);
            this.label3.TabIndex = 52;
            this.label3.Text = "Lsass Pid:";
            // 
            // UsersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 474);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLsText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSeDBGbox);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.catButton);
            this.Controls.Add(this.txtProcName);
            this.Controls.Add(this.dirButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.grbSex);
            this.Controls.Add(this.searchProc);
            this.Controls.Add(this.Tasklist);
            this.Controls.Add(this.txtOutPut);
            this.Controls.Add(this.grdUsers);
            this.Controls.Add(this.grpDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UsersView";
            this.Text = "User List - Active Users";
            this.Load += new System.EventHandler(this.UsersView_Load);
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.grbSex.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtID;
        internal System.Windows.Forms.TextBox txtLastName;
        internal System.Windows.Forms.Label lblID;
        internal System.Windows.Forms.Label lblLastName;
        internal System.Windows.Forms.GroupBox grpDetails;
        internal System.Windows.Forms.TextBox txtFirstName;
        internal System.Windows.Forms.Label lblFirstName;
        internal System.Windows.Forms.ListView grdUsers;
        internal System.Windows.Forms.TextBox txtDepartment;
        internal System.Windows.Forms.Label lblDepartment;
        private  System.Windows.Forms.GroupBox grbSex;
        internal System.Windows.Forms.RadioButton rdNormal;
        internal System.Windows.Forms.RadioButton rdSlow;
        private System.Windows.Forms.Button dirButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtOutPut;
        private System.Windows.Forms.Button catButton;
        private System.Windows.Forms.Button Tasklist;
        private System.Windows.Forms.Button searchProc;
        private System.Windows.Forms.Button localIp;
        private System.Windows.Forms.Button WinDefButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtProcName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox txtExePath;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TextBox txtSeDBGbox;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLsText;
        internal System.Windows.Forms.Label label3;
    }
}

