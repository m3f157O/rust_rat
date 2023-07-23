using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WinFormMVC.Controller;
using WinFormMVC.Model;

namespace WinFormMVC.View
{
    public partial class UsersView : Form, IUsersView
    {
        public UsersView()
        {
            InitializeComponent();
        }

        UsersController _controller;

        #region Events raised  back to controller

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this._controller.AddNewUser();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this._controller.RemoveUser();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this._controller.Save();
        }

        private void grdUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.grdUsers.SelectedItems.Count > 0)
                this._controller.SelectedUserChanged(this.grdUsers.SelectedItems[0].Text);
        }


        #endregion

        #region ICatalogView implementation

        public void SetController(UsersController controller)
        {
            _controller = controller;
        }

        public void ClearGrid()
        {
            // Define columns in grid
            this.grdUsers.Columns.Clear();

            this.grdUsers.Columns.Add("Id",          150,  HorizontalAlignment.Left);
            this.grdUsers.Columns.Add("Username",  150,  HorizontalAlignment.Left);
            this.grdUsers.Columns.Add("Caller IP", 150,  HorizontalAlignment.Left);
            this.grdUsers.Columns.Add("AV",  150,  HorizontalAlignment.Left);
            this.grdUsers.Columns.Add("Status", 100, HorizontalAlignment.Left);
            this.grdUsers.Columns.Add("SEDebug", 80, HorizontalAlignment.Left);
            this.grdUsers.Columns.Add("Path", 450, HorizontalAlignment.Left);

            // Add rows to grid
            this.grdUsers.Items.Clear();
        }

        public void AddUserToGrid(User usr)
        {
            ListViewItem parent;
            parent = this.grdUsers.Items.Add(usr.ID);
            parent.SubItems.Add(usr.Username);
            parent.SubItems.Add(usr.LastName);
            parent.SubItems.Add(usr.Department);
            parent.SubItems.Add(Enum.GetName(typeof(User.Status), usr.status));
            parent.SubItems.Add(usr.seDBG);
            parent.SubItems.Add(usr.path);
        }

        public void UpdateGridWithChangedUser(User usr)
        {
            ListViewItem rowToUpdate = null;

            foreach (ListViewItem row in this.grdUsers.Items)
            {
                if (row.Text == usr.ID)
                {
                    rowToUpdate = row;
                }
            }

            if (rowToUpdate != null)
            {
                rowToUpdate.Text = usr.ID;
                rowToUpdate.SubItems[1].Text = usr.Username;
                rowToUpdate.SubItems[2].Text = usr.LastName;
                rowToUpdate.SubItems[3].Text = usr.Department;
                rowToUpdate.SubItems[4].Text = Enum.GetName(typeof(User.Status), usr.status);
                rowToUpdate.SubItems[5].Text = usr.seDBG;
                rowToUpdate.SubItems[6].Text = usr.path;
            }
        }

        public void RemoveUserFromGrid(User usr)
        {

            ListViewItem rowToRemove = null;

            foreach (ListViewItem row in this.grdUsers.Items)
            {
                if (row.Text == usr.ID)
                {
                    rowToRemove = row;
                }
            }

            if (rowToRemove != null)
            {
                this.grdUsers.Items.Remove(rowToRemove);
                this.grdUsers.Focus();
            }
        }

        public string GetIdOfSelectedUserInGrid()
        {
            if (this.grdUsers.SelectedItems.Count > 0)
                return this.grdUsers.SelectedItems[0].Text;
            else
                return "";
        }

        public void SetSelectedUserInGrid(User usr)
        {
            foreach (ListViewItem row in this.grdUsers.Items)
            {
                if (row.Text == usr.ID)
                {
                    row.Selected = true;
                }
            }
        }

        public string Username
        {
            get { return this.txtFirstName.Text; }
            set { this.txtFirstName.Text = value; }
        }

        public string lsPid
        {
            get { return this.txtLsText.Text; }
            set { this.txtLsText.Text = value; }
        }

        public string seDBG
        {
            get { return this.txtSeDBGbox.Text; }
            set { this.txtSeDBGbox.Text = value; }
        }

        public string LastName 
        {
            get { return this.txtLastName.Text; }
            set { this.txtLastName.Text = value; }
        }

        public string ID
        {
            get { return this.txtID.Text; }
            set { this.txtID.Text = value; }
        }


        public string Defender 
        {
            get { return this.txtDepartment.Text; }
            set { this.txtDepartment.Text = value; }
        }

        public bool status
        {
            
            get
            {
                if (this.rdSlow.Checked == true)
                {
                    return true;
                }
                else
                    return false;
            }
            set
            {
                if (value == true)
                    this.rdSlow.Checked = true;
                else
                    this.rdNormal.Checked = true;
            }
        }

        public bool CanModifyID
        {
            set {
                this.txtID.Enabled = value;
                this.txtFirstName.Enabled = false;
                this.txtLastName.Enabled = false;
                this.txtDepartment.Enabled = false;
                this.txtExePath.Enabled = false;
                this.txtLsText.Enabled = false;
                this.txtSeDBGbox.Enabled = false;
            }
        }

        public string Output {
            get { return this.txtOutPut.Text; }
            set { this.txtOutPut.Text = value; }
        }

        public string path
        {
            get { return this.txtExePath.Text; }
            set { this.txtExePath.Text = value; }
        }
  

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this._controller.Dir(this.txtPath.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this._controller.getUsername();
        }

        private void rdMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UsersView_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void catButton_Click(object sender, EventArgs e)
        {
            if (this._controller.cat(this.txtPath.Text))
                MessageBox.Show("INSERT A FILE NAME", "WRONG ARGUMENT");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this._controller.getTasks();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (this.txtProcName.Text.Length == 0)
                MessageBox.Show("INSERT A PROCESS NAME", "WRONG ARGUMENT");
            else
                this._controller.searchName(this.txtProcName.Text);

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            if (this.txtProcName.Text.Length == 0)
                MessageBox.Show("INSERT A PROCESS NAME", "WRONG ARGUMENT");
            else
                this._controller.killName(this.txtProcName.Text);

        }

        private void localIp_Click(object sender, EventArgs e)
        {
            this._controller.getLocalIp();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.txtProcName.Text.Length != 0 || !this.txtPath.Text.StartsWith("http"))
                MessageBox.Show("INSERT A VALID URL", "WRONG ARGUMENT");
            else
                this._controller.download(this.txtPath.Text);

        }

        private void WinDefButton_Click(object sender, EventArgs e)
        {
            this._controller.searchWD();

        }

        private void lblDepartment_Click(object sender, EventArgs e)
        {

        }

        private void grpDetails_Enter(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this._controller.getPath();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this._controller.sedbg();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(this.txtLsText.Text.Equals("unknown"))
                MessageBox.Show("RUN TASKLIST FIRST", "UNKNOWN PID");
            else
            this._controller.grablsa();

        }
    }
}
