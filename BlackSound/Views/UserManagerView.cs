using BlackSound.Entities;
using BlackSound.Repositories;
using BlackSound.Tools;
using System;
using System.Collections.Generic;


namespace BlackSound.Views
{
    public class UserManagementView
    {
        private UsersRepository _usersRepository = new UsersRepository(Properties.Settings.Default.FileUsers);

        private void IsEmptyValidation(ref string stringInput)
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
        private void IsEmptyValidation(ref int intInput)
        {
            while (true)
            {
                if (intInput <= 0)
                {
                    Console.Write("\t\tTry again! : ");
                    intInput = Convert.ToInt32(Console.ReadLine());
                }
                else break;
            }
        }
        public void Show()
        {
            while (true)
            {
                UserManagementEnum choice = RenderMenu();
                try
                {
                    switch (choice)
                    {
                        case UserManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case UserManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case UserManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case UserManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case UserManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case UserManagementEnum.Exit:
                            {
                                return;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
        }
        private UserManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("------> Users management <------");
                Console.WriteLine("\n");
                Console.WriteLine("\t(G)et all Users");
                Console.WriteLine("\t(V)iew User");
                Console.WriteLine("\t(A)dd User");
                Console.WriteLine("\t(E)dit User");
                Console.WriteLine("\t(D)elete User");
                Console.WriteLine("\tE(x)it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return UserManagementEnum.Select;
                        }
                    case "V":
                        {
                            return UserManagementEnum.View;
                        }
                    case "A":
                        {
                            return UserManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return UserManagementEnum.Update;
                        }
                    case "D":
                        {
                            return UserManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return UserManagementEnum.Exit;
                        }
                    default:
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("\tInvalid choice. Please, try again.");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }
        private void GetAll()
        {
            Console.Clear();

            List<User> users = _usersRepository.GetAll();

            foreach (User user in users)
            {
                Console.WriteLine("\tID : " + user.Id);
                Console.WriteLine("\tUsername : " + user.Username);
                Console.WriteLine("\tPassword : " + user.Password);
                Console.WriteLine("\tEmail : " + user.Email);
                Console.WriteLine("\tIs Admin : " + user.IsAdmin);

                Console.WriteLine("_____________________________________");
            }

            Console.ReadKey(true);
        }
        private void View()
        {
            Console.Clear();

            Console.Write("\tUser ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            IsEmptyValidation(ref userId);
            Console.WriteLine("\n");

            User user = _usersRepository.GetById(userId);
            if (user == null)
            {
                Console.Clear();
                Console.WriteLine("\tUser not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("\tID : " + user.Id);
            Console.WriteLine("\tUsername : " + user.Username);
            Console.WriteLine("\tPassword : " + user.Password);
            Console.WriteLine("\tEmail : " + user.Email);

            Console.ReadKey(true);
        }
        private void Add()
        {
            Console.Clear();

            User user = new User();
            Console.WriteLine("---------> Add new User <---------");
            Console.WriteLine("\n");

            Console.Write("\tUsername : ");
            string usernameInput = Console.ReadLine();
            IsEmptyValidation(ref usernameInput);
            user.Username = usernameInput;

            Console.Write("\tPassword : ");
            string passwordInput = Console.ReadLine();
            IsEmptyValidation(ref passwordInput);
            user.Password = passwordInput;

            Console.Write("\tEmail : ");
            string emailInput = Console.ReadLine();
            IsEmptyValidation(ref emailInput);
            user.Email = emailInput;

            Console.Write("\tIs Admin (yes/no): ");
            string isAdmin = Console.ReadLine().ToLower();
            user.IsAdmin = (isAdmin == "yes" || isAdmin == "y" || isAdmin == "true" || isAdmin == "1") ? true : false;

            _usersRepository.Save(user);

            Console.WriteLine("\n");
            Console.WriteLine("\tUser saved successfully.");
            Console.ReadKey(true);
        }
        private void Update()
        {
            Console.Clear();

            Console.Write("\tUser ID: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            IsEmptyValidation(ref userId);

            User user = _usersRepository.GetById(userId);

            if (user == null)
            {
                Console.Clear();
                Console.WriteLine("\tUser not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("\n");

            Console.WriteLine("\tName : {" + user.Username + "}");
            Console.Write("\tNew Name: ");
            string name = Console.ReadLine();
            IsEmptyValidation(ref name);

            Console.WriteLine("\tPassword : {" + user.Password + "}");
            Console.Write("\tNew Password: ");
            string password = Console.ReadLine();
            IsEmptyValidation(ref password);

            Console.WriteLine("\tEmail : {" + user.Email + "}");
            Console.Write("\tNew Email : ");
            string email = Console.ReadLine();
            IsEmptyValidation(ref email);

            string isAdmin = Console.ReadLine();
            user.IsAdmin = false;

            _usersRepository.Save(user);
            Console.WriteLine("\tUser edited successfully.");
            Console.ReadKey(true);
        }
        private void Delete()
        {
            Console.Clear();

            Console.WriteLine("---------> Delete User <---------");
            Console.WriteLine("\n");
            Console.Write("\tUser Id: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            IsEmptyValidation(ref userId);

            User user = _usersRepository.GetById(userId);

            if (user == null)
            {
                Console.WriteLine("\tUser not found!");
            }
            else
            {
                _usersRepository.Delete(user);
                Console.WriteLine("\n");
                Console.WriteLine("\tUser deleted successfully.");

                //int playlistId = Convert.ToInt32(Console.ReadLine());
                //IsEmptyValidation(ref playlistId);
                //PlaylistRepository playlistRepository = new PlaylistRepository(Properties.Settings.Default.FilePlaylists);
                //Playlist playlist = playlistRepository.GetById(playlistId);

                //Console.WriteLine("Your playlist ID is : " + playlist.Id);

                //foreach (Playlist userOwnerId in playlist.Id)
                // {
                //      Console.WriteLine("Playlist ID : "); 
                // }
                // TO DO:  make references userOwnerId = userID
                //int b  = Convert.ToInt32(Console.ReadLine());

                //if (b == playlist.UserOwnerId) 
                //{
                //    playlistRepository.Delete(playlist);
                //   Console.WriteLine("\n");
                //   Console.WriteLine("\tPlaylist deleted successfully.");
                //}
                //Console.ReadKey(true);

      
            }
            Console.ReadKey(true);
        } //here
    }  
}
