using BlackSound.Entities;
using BlackSound.Repositories;
using BlackSound.Tools;
using System;
using System.Collections.Generic;

namespace BlackSound.Views
{
    public class SongsManagerView
    {
        private Playlist playlist = null;
        private SongsRepository _songsRepository = new SongsRepository(Properties.Settings.Default.FileSongs);
        public SongsManagerView(Playlist playlist)
        {
            this.playlist = playlist;
        }
        private void IsEmptyValidation(ref string stringInput)
        {
            while (true)
            {
                if (String.IsNullOrEmpty(stringInput))
                {
                    Console.Write("\t\tPlease, try again! : ");
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
                    Console.Write("\t\tPlease, try again! You should type only numbers : ");
                    intInput = Convert.ToInt32(Console.ReadLine());
                }
                else break;
            }
        }
        private SongsManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("---------> Please, first choose songs from <---------");
                Console.WriteLine("---------------> the Songs Manager <-----------------");
                Console.WriteLine("\n");
                Console.WriteLine("\t\t(G)et all Songs");
                Console.WriteLine("\t\t(A)dd Songs");
                Console.WriteLine("\t\t(D)elete Songs");
                Console.WriteLine("\t\tE(x)it from Song Manager");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return SongsManagementEnum.Select;
                        }
                    case "A":
                        {
                            return SongsManagementEnum.Insert;
                        }
                    case "D":
                        {
                            return SongsManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return SongsManagementEnum.Exit;
                        }
                    default:
                        {
                            Console.WriteLine("\tInvalid choice. Please, try again.");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }
        public void Show()
        {
            while (true)
            {
                SongsManagementEnum choice = RenderMenu();

                switch (choice)
                {
                    case SongsManagementEnum.Select:
                        {
                            GetAll();
                            break;
                        }
                    case SongsManagementEnum.Insert:
                        {
                            Add();
                            break;
                        }
                    case SongsManagementEnum.Delete:
                        {
                            Delete();
                            break;
                        }
                    case SongsManagementEnum.Exit:
                        {
                            return;
                        }
                }
            }
        }
        private void GetAll()
        {
            Console.Clear();

            List<Song> songs = _songsRepository.GetAll(this.playlist.Id);

            foreach (Song song in songs)
            {
                Console.WriteLine("\tSong ID : " + song.Id);
                Console.WriteLine("\tName : " + song.ArtistName);
                Console.WriteLine("\tTitle : " + song.Title);
                Console.WriteLine("\tYear : " + song.Year);
                Console.WriteLine("___________________________________");
            }
            Console.ReadKey(true);
        }
        private void Add()
        {
            Console.Clear();

            Song song = new Song();

            Console.WriteLine("\tAdd new Song : ");
            Console.Write("\tTitle : ");
            string title = Console.ReadLine();
            IsEmptyValidation(ref title);  
            song.Title = title;

            Console.Write("\tArtist Name : ");
            string artistName = Console.ReadLine();
            IsEmptyValidation(ref artistName);
            song.ArtistName = artistName;

            Console.Write("\tYear : ");
            int year = Convert.ToInt32(Console.ReadLine());
            IsEmptyValidation(ref year);
            song.Year = year;

             _songsRepository.Save(song);

            Console.WriteLine("\tSong saved successfully.");
            Console.ReadKey(true);
        }
        private void Delete()
        {
            Console.Clear();

            Console.WriteLine("\tDelete Song : ");
            Console.Write("\tSong Id : ");
            int songId = Convert.ToInt32(Console.ReadLine());
            IsEmptyValidation(ref songId);

            Song song = _songsRepository.GetById(songId);
            if (song == null)
            {
                Console.WriteLine("\tSong not found!");
            }
            else
            {
                _songsRepository.Delete(song);
                Console.WriteLine("\n");
                Console.WriteLine("\tSong deleted successfully.");
            }
            Console.ReadKey(true);
        }
    }
}
