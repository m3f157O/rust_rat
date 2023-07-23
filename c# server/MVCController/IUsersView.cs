using System;
using WinFormMVC.Model;

namespace WinFormMVC.Controller
{
    public interface IUsersView
    {
        void SetController(UsersController controller);
        void ClearGrid();
        void AddUserToGrid(User user);
        void UpdateGridWithChangedUser(User user);
        void RemoveUserFromGrid(User user);
        string GetIdOfSelectedUserInGrid();
        void SetSelectedUserInGrid(User user);

        string Username { get; set; }
        string lsPid { get; set; }
        string seDBG { get; set; }
        string Output { get; set; }
        string LastName      { get; set; }
        string ID            { get; set; }
        string Defender { get; set; }
        string path { get; set; }
        bool status { get; set; }
        bool CanModifyID     {      set; }
    }
}
