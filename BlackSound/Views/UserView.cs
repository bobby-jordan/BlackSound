using System;

namespace BlackSound.Views
{
    public class UserView
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
                    Console.WriteLine("---------------------> User View <----------------------");
                    Console.WriteLine("\n");
                    Console.WriteLine("    Please, choose from the menu by typing a letter");
                    Console.WriteLine("\n");
                    Console.WriteLine("\t\t (P)laylist Management");
                    Console.WriteLine("\t\t E(x)it");

                    string choice = Console.ReadLine();
                    switch (choice.ToUpper())
                    {
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
