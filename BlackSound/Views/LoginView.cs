using BlackSound.Service;
using BlackSound.Entities;
using System;
using BlackSound.Repositories;

namespace BlackSound.Views
{
    public class LoginView
    {
        public void IsEmptyValidation(ref string stringInput)
         {
             while (true)
             {
                 if (String.IsNullOrEmpty(stringInput))
                 {
                     Console.Write("\t\tTry again! : ");
                     stringInput = Console.ReadLine();
                 }
                 else break;
             }
         }
        public void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("------------> Do you have acount? <-------------");
                Console.WriteLine("------------------> (Yes/No) <------------------");
                Console.WriteLine("\n");

                string input = Console.ReadLine().ToLower();

                if (input == "yes")
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("--------------> Please, Login <--------------");

                    Console.WriteLine("\n");
                    Console.Write("\t\tUsername: ");
                    string username = Console.ReadLine();

                    Console.Write("\t\tPassword: ");
                    string password = Console.ReadLine();

                    AuthenticationService.AuthenticateUser(username, password);

                    if (AuthenticationService.LoggedUser != null)
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine("\t  Welcome to BlackSound, " + AuthenticationService.LoggedUser.Username + "!");
                        Console.WriteLine("\n");
                        Console.WriteLine("\t ~~ Type enter to continue ~~");
                        Console.ReadKey(true);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine("\t Invalid username or password");
                        Console.WriteLine("\n");
                        Console.WriteLine("\t   ~~ Type enter to continue ~~");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    User user = new User();
                    Console.WriteLine("\n");
                    Console.WriteLine("--------------> Please, Sign up <--------------");
                    Console.WriteLine("\n");

                    Console.Write("\t\tUsername : ");
                    string nameInput = Console.ReadLine();
                    IsEmptyValidation(ref nameInput);
                    user.Username = nameInput;

                    Console.Write("\t\tPassword : ");
                    string password = Console.ReadLine();
                    IsEmptyValidation(ref password);
                    user.Password = password;

                    Console.Write("\t\tEmail : ");
                    string email = Console.ReadLine();
                    IsEmptyValidation(ref email);
                    user.Email = email;

                    string isAdmin = Console.ReadLine();
                    user.IsAdmin = false;

                    UsersRepository usersRepository = new UsersRepository(Properties.Settings.Default.FileUsers);
                    usersRepository.Save(user);

                    Console.WriteLine("\n");
                    Console.WriteLine("\t     User saved successfully.");
                    Console.WriteLine("\n");
                    Console.WriteLine("\t   ~~ Type enter to continue ~~");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
