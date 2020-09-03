using System;


namespace BlackSound.Views
{
    public class AdminView
    {
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    Console.WriteLine("--------------> Administration View <----------------");
                    Console.WriteLine("\n    Please, choose from the menu by typing a letter");
                    Console.WriteLine("\n\t\t(U)ser Management");
                    Console.WriteLine("\t\t(P)laylist Management");
 
                    Console.WriteLine("\t\tE(x)it");


                    string choice = Console.ReadLine();
                    switch (choice.ToUpper())
                    {
                        case "U":
                            {
                                UserManagementView view = new UserManagementView();
                                view.Show();

                                break;
                            }
                        case "P":
                            {
                                PlaylistManagerView view = new PlaylistManagerView();
                                view.Show();

                                break;
                            }
                        case "X":
                            {
                                return;
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
        }
    }
}

