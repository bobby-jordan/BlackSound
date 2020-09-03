using System;
using System.Collections.Generic;
using BlackSound.Entities;
using System.IO;

namespace BlackSound.Repositories
{
    public class SongsRepository : BaseRepository<Song>
    {
        public SongsRepository(string filePath)
            : base(filePath)
        {

        }
        public List<Song> GetAll(int parentUserId)
        {
            List<Song> result = new List<Song>();

            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);

            try
            {
                while (!streamReader.EndOfStream)
                {
                    Playlist playlist = new Playlist();
                    Song song = new Song();
                    User user = new User();
                    if (user.IsAdmin)
                    {
                        ReadItem(streamReader, song);
                        result.Add(song);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("The exception is: " + e);
            }
            finally
            {
                streamReader.Close();
                fileStream.Close();
            }

            return result;
        }
        public override void ReadItem(StreamReader streamReader, Song item)
        {
            var row = streamReader.ReadLine();
            var items = row.Split(';');
            
            item.Id = Convert.ToInt32(items[0]);
            item.Title = items[1];
            item.ArtistName = items[2];
            item.Year = Convert.ToInt32(items[3]);
        }
        public override void WriteItem(StreamWriter streamWriter, Song item)
        {
            string row = $"{item.Id};{item.Title};{item.ArtistName};{item.Year}";
            streamWriter.WriteLine(row);
        }
    }
}
