using BlackSound.Entities;
using BlackSound.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Service
{
    class AuthenticationService
    {
        public static User LoggedUser { get; private set; }
        public static void AuthenticateUser(string username, string password)
        {
            UsersRepository userRepository = new UsersRepository(Properties.Settings.Default.FileUsers);
            AuthenticationService.LoggedUser = userRepository.GetByUsernameAndPassword(username, password);
        }
    }
}
