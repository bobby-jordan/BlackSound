using BlackSound.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace BlackSound.Repositories
{
    public abstract class BaseRepository<T> where T : IEntity, new()
    {
        protected readonly string filePath;
        public BaseRepository(string filePath)
        {
            this.filePath = filePath;
        }
        public abstract void ReadItem(StreamReader streamReader, T item);
        public abstract void WriteItem(StreamWriter streamWriter, T item);
        private int GetNextId()
        {
            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);

            int id = 1;

            try
            {
                while (!streamReader.EndOfStream)
                {
                    T item = new T();
                    ReadItem(streamReader, item);

                    if (id <= item.Id)
                    {
                        id = item.Id + 1;
                    }
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
            return id;
        }
        private void Insert(T item)
        {
            item.Id = GetNextId();

            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            try
            {
                long endPoint = fileStream.Length;
                fileStream.Seek(endPoint, SeekOrigin.Begin);
                WriteItem(streamWriter, item);
                streamWriter.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine("The exception is: " + e);
            }
            finally
            {
                streamWriter.Close();
                fileStream.Close();
            }
        }
        private void Update(T item)
        {
            string tempFilePath = filePath + ".temp";

            FileStream firstOpen = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(firstOpen);

            FileStream secondOpen = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(secondOpen);

            try
            {
                while (!streamReader.EndOfStream)
                {
                    T current = new T();
                    ReadItem(streamReader, current);

                    if (current.Id != item.Id)
                    {
                        WriteItem(streamWriter, current);
                    }
                    else
                    {
                        WriteItem(streamWriter, item);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The exception is: " + e);
            }
            finally
            {
                streamReader.Close();
                firstOpen.Close();
                streamWriter.Close();
                secondOpen.Close();
            }
            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }
        public T GetById(int id)
        {
            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);

            try
            {
                while (!streamReader.EndOfStream)
                {
                    T item = new T();
                    ReadItem(streamReader, item);
                    if (item.Id == id)
                    {
                        return item;
                    }
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
            return default(T);
        }
        public List<T> GetAll()
        {
            List<T> result = new List<T>();
            FileStream fileStream = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(fileStream);

            try
            {
                while (!streamReader.EndOfStream)
                {
                    T item = new T();
                    ReadItem(streamReader, item);
                    result.Add(item);
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
            return result;
        }
        public void Delete(T item)
        {
            string tempFilePath = filePath + ".temp";

            FileStream firstOpen = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader streamReader = new StreamReader(firstOpen);

            FileStream secondOpen = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter streamWriter = new StreamWriter(secondOpen);

            try
            {
                while (!streamReader.EndOfStream)
                {
                    T current = new T();
                    ReadItem(streamReader, current);

                    if (current.Id != item.Id)
                    {
                        WriteItem(streamWriter, current);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The exception is: " + e);
            }
            finally
            {
                streamWriter.Close();
                firstOpen.Close();
                streamReader.Close();
                secondOpen.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }
        public void Save(T item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }
    }
}
