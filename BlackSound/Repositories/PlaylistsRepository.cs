using System;
using System.Collections.Generic;
using System.IO;
using BlackSound.Entities;
using BlackSound.Service;

namespace BlackSound.Repositories
{
    public class PlaylistRepository : BaseRepository<Playlist>
    {
        public PlaylistRepository(string filePath)
            : base(filePath)
        {
        }
        public List<Playlist> GetAll()
        {
            List<Playlist> result = new List<Playlist>();

            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);

            try
            {
                while (!streamReader.EndOfStream)
                {
                    Playlist playlists = new Playlist();
                    ReadItem(streamReader, playlists);
                    result.Add(playlists);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The exception is: " + e);
            }
            finally
            {
                streamReader.Close();
                fileStream.Close();
            }

            if (AuthenticationService.LoggedUser.IsAdmin)
            {
                return result;
            }

            List<Playlist> playlistsList = new List<Playlist>();
            foreach (var a in result)
            {
                if (a.IsPublic == true || a.UserOwnerId == AuthenticationService.LoggedUser.Id)
                {
                    playlistsList.Add(a);
                }
            }
            return playlistsList;
        }
        public override void ReadItem(StreamReader streamReader, Playlist item)
        {
            string row = streamReader.ReadLine();
            string[] items = row.Split(';');

            item.Id = Convert.ToInt32(items[0]);
            item.UserOwnerId = Convert.ToInt32(items[1]);
            item.Name = items[2];
            item.Description = items[3];
            item.Songs = items[4];
            item.IsPublic = Convert.ToBoolean(items[5]);

            string[] songs = item.Songs.Split(',');
            item.SongsIts = new List<int>();
            foreach (var song in songs)
            {
                int addSong = Convert.ToInt32(song);
                item.SongsIts.Add(addSong);
            }
        }
        public override void WriteItem(StreamWriter streamWriter, Playlist item)
        {
            string row = $"{item.Id};{item.UserOwnerId};{item.Name};{item.Description};{item.Songs};{item.IsPublic}";
            streamWriter.WriteLine(row);
        }
    }
}

