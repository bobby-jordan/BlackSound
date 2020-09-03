using BlackSound.Repositories;
using BlackSound.Tools;
using BlackSound.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using BlackSound.Service;

namespace BlackSound.Views
{
    public class PlaylistManagerView
    {
        private PlaylistRepository _playlistRepository = new PlaylistRepository(Properties.Settings.Default.FilePlaylists);
        public void Show()
        {
            while (true)
            {
                PlaylistManagementEnum choice = RenderMenu();

                try
                {
                    switch (choice)
                    {
                        case PlaylistManagementEnum.Select:
                            {
                                GetAll();
                                break;
                            }
                        case PlaylistManagementEnum.View:
                            {
                                View();
                                break;
                            }
                        case PlaylistManagementEnum.Insert:
                            {
                                Add();
                                break;
                            }
                        case PlaylistManagementEnum.Update:
                            {
                                Update();
                                break;
                            }
                        case PlaylistManagementEnum.Delete:
                            {
                                Delete();
                                break;
                            }
                        case PlaylistManagementEnum.Exit:
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
        private PlaylistManagementEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n");
                Console.WriteLine("---------> Playlist management <-------");
                Console.WriteLine("\n");
                Console.WriteLine("\t(G)et all Playlist");
                Console.WriteLine("\t(V)iew Playlist");
                Console.WriteLine("\t(A)dd Playlist");
                Console.WriteLine("\t(E)dit Playlist");
                Console.WriteLine("\t(D)elete Playlist");
                Console.WriteLine("\tE(x)it");

                string choice = Console.ReadLine();
                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return PlaylistManagementEnum.Select;
                        }
                    case "V":
                        {
                            return PlaylistManagementEnum.View;
                        }
                    case "A":
                        {
                            return PlaylistManagementEnum.Insert;
                        }
                    case "E":
                        {
                            return PlaylistManagementEnum.Update;
                        }
                    case "D":
                        {
                            return PlaylistManagementEnum.Delete;
                        }
                    case "X":
                        {
                            return PlaylistManagementEnum.Exit;
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
        private void GetAll()
        {
            Console.Clear();

            SongsRepository songsRepository = new SongsRepository(Properties.Settings.Default.FileSongs);
            UsersRepository usersRepository = new UsersRepository(Properties.Settings.Default.FileUsers);
            List<Playlist> playlist = _playlistRepository.GetAll();
            List<Song> songs = songsRepository.GetAll();
            List<User> users = usersRepository.GetAll();

            foreach (Playlist playlistitem in playlist)
            {
                var owners = users.Where(x => playlistitem.UserOwnerId.Equals(x.Id)).ToList();

                foreach (User ownerUser in owners)
                {
                    Console.WriteLine("_______________________________________");
                    Console.WriteLine("\tOwner Username: " + ownerUser.Username);
                }
                Console.WriteLine("\tPlaylist ID : " + playlistitem.Id);
                Console.WriteLine("\tPlaylist Name : " + playlistitem.Name);
                Console.WriteLine("\tPlaylist Public : " + playlistitem.IsPublic);
               

                Console.WriteLine("_______________________________________");

                var songsForThisPlayList = songs.Where(x => playlistitem.SongsIts.Contains(x.Id)).ToList();

                foreach (Song songitem in songsForThisPlayList)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("\tSong ID : " + songitem.Id);
                    Console.WriteLine("\tSong Name : " + songitem.ArtistName);
                    Console.WriteLine("\tSong Title : " + songitem.Title);
                    Console.WriteLine("\tSong Year : " + songitem.Year);
                }
                Console.WriteLine("---------------------------------------"); 
                Console.WriteLine("\n");
            }

            Console.ReadKey(true);
        }
        private void View()
        {
            Console.Clear();

            Console.Write("\tPlaylist ID: ");
            int playlistId = Convert.ToInt32(Console.ReadLine());
            IsEmptyValidation(ref playlistId);

            SongsRepository phonesRepository = new SongsRepository(Properties.Settings.Default.FileSongs);
            Playlist playlist = _playlistRepository.GetById(playlistId);

            if (playlist == null || playlist.IsPublic == false)
            {
                Console.Clear();
                Console.WriteLine("\tPlaylist not found.");
                Console.ReadKey(true);
            }
            else
            {
                Console.Clear();
                SongsManagerView songManagerView = new SongsManagerView(playlist);
                songManagerView.Show();
            }
        }
        private void Add()
        {
            Console.Clear();
            Playlist playlist = new Playlist();

            SongsManagerView songManagerView = new SongsManagerView(playlist);
            songManagerView.Show();
 
            Console.WriteLine("\tAdd new Playlist : ");
            Console.Write("\n");
            Console.Write("\tName : ");
            string name = Console.ReadLine();
            IsEmptyValidation(ref name);
            playlist.Name = name;

            var sessionId = AuthenticationService.LoggedUser.Id;
            playlist.UserOwnerId = sessionId;

            Console.Write("\tDescription : ");
            string description = Console.ReadLine();
            IsEmptyValidation(ref description);
            playlist.Description = description;

            Console.Write("\tSongs ids : ");
            playlist.Songs = Console.ReadLine();
       
            Console.Write("\tIs public?(Yes/No?) : ");
            string isPublic = Console.ReadLine().ToLower();
            playlist.IsPublic = (isPublic == "yes" || isPublic == "y" || isPublic == "true" || isPublic == "1") ? true : false;

            _playlistRepository.Save(playlist);

            Console.WriteLine("\tPlaylist saved successfully.");
            Console.ReadKey(true);
        }
        private void Update()
        {
            Console.Clear();

            Console.Write("\tPlaylist ID : ");
            int playlistId = Convert.ToInt32(Console.ReadLine());

            Playlist playlist = _playlistRepository.GetById(playlistId);

            if (playlist == null)
            {
                Console.Clear();
                Console.WriteLine("\tPlaylist not found.");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("\tEditing Playlist (" + playlist.Name + ")");
            Console.WriteLine("\tPlaylist ID : " + playlist.Id);

            Console.WriteLine("\tName : " + playlist.Name);
            Console.Write("\tNew Name : ");
            string name = Console.ReadLine();
            Console.WriteLine("\tDescription : " + playlist.Description);
            Console.Write("\tNew Description : ");
            string description = Console.ReadLine();

            if (!string.IsNullOrEmpty(name))
                playlist.Name = name;
            if (!string.IsNullOrEmpty(description))
                playlist.Description = description;

            _playlistRepository.Save(playlist);

            Console.WriteLine("\tPlaylist saved successfully.");
            Console.ReadKey(true);

            SongsManagerView songManagerView = new SongsManagerView(playlist);
            songManagerView.Show();
        }
        private void Delete()
        {
            Console.Clear();

            Console.WriteLine("\tDelete Playlist : ");
            Console.Write("\tPlaylist Id : ");
            int playlistId = Convert.ToInt32(Console.ReadLine());

            Playlist playlist = _playlistRepository.GetById(playlistId);
            if (playlist == null)
            {
                Console.WriteLine("\tPlaylist not found!");
            }
            else
            {
                _playlistRepository.Delete(playlist);
                Console.WriteLine("\n");
                Console.WriteLine("\tPlaylist deleted successfully.");
            }
            Console.ReadKey(true);
        }
    }
}
