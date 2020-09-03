using BlackSound.Views;
using BlackSound.Entities;
using BlackSound.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginView loginView = new LoginView();
            loginView.Show();

            if (AuthenticationService.LoggedUser.IsAdmin)
            {
                AdminView adminView = new AdminView();
                adminView.Show();
            }
            else
            {
                UserView userView = new UserView();
                userView.Show();
            }
        }
    }
}
