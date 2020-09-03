using BlackSound.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Repositories
{
    public class UsersRepository : BaseRepository<User>
    {
        private List<User> _users = new List<User>();
        public UsersRepository(string filePath) 
            : base(filePath)
        {
        }
        public User GetByUsernameAndPassword(string username, string password)
        {
            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);

            try
            {
                while (!streamReader.EndOfStream)
                {
                    User user = new User();
                    ReadItem(streamReader, user);

                    _users.Add(user);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                streamReader.Close();
                fileStream.Close();
            }

            return _users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
        }
        public override void ReadItem(StreamReader streamReader, User item)
        {
            var row = streamReader.ReadLine();
            var items = row.Split(';');

            item.Id = Convert.ToInt32(items[0]);
            item.Username = items[1];
            item.Password = items[2];
            item.Email = items[3];
            item.IsAdmin = Convert.ToBoolean(items[4]);
        }
        public override void WriteItem(StreamWriter streamWriter, User item)
        {
            string row = $"{item.Id};{item.Username};{item.Password};{item.Email};{item.IsAdmin}";
            streamWriter.WriteLine(row);
        }
    }
}
