using System;
using System.Collections;
using System.Net.Sockets;

namespace WinFormMVC.Model
{
    public class User
    {
        public enum Status
        {
            Normal =0,
            Slow   = 1,
            Online = 2
        }

        private string _FirstName;
        public string Username 
        {
            get { return _FirstName; } 
            set 
            { 
                 if (value.Length > 50)
                     Console.WriteLine("Error! FirstName must be less than 51 characters!"); 
                 else
                     _FirstName = value; 
            } 
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (value.Length > 50)
                    Console.WriteLine("Error! LastName must be less than 51 characters!");
                else
                    _LastName = value;
            }
        }

        private string _ID;
        public string ID
        {
            get { return _ID; }
            set
            {
                if (value.Length > 9)
                    Console.WriteLine("Error! ID must be less than 10 characters!");
                else
                    _ID = value;
            }
        }

        private string _Department;
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        private bool _status;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Socket Sock { get; private set; }
        public IList Fsinfo { get; private set; }
        private string _ppath;

        public string path { get
            {
                return _ppath;
            }
             
            set { _ppath = value; }
        }
        public string seDBG { get; set; }
        public string lsPid { get; set; }

        public User(string firstname, string lastname, string id, string department, bool sex, Socket sock,IList fsinfo, string newPath, string SEDBGstatus,string lsPidNew)
        {
            Username   = firstname;
            LastName    = lastname;
            ID          = id;
            Department  = department;
            status = sex;
            Sock = sock;
            Fsinfo = fsinfo;
            path = newPath;
            seDBG = SEDBGstatus;
            lsPid = lsPidNew;
        }
    } 

}
