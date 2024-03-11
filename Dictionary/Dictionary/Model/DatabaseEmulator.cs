using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Dictionary.Model
{
    internal class DatabaseEmulator
    {
        private static readonly string _pathToWords = "D:\\facultate\\an2\\sem2\\MAP\\Tema1MAP\\Dictionary\\Dictionary\\Resource\\worddata.json";
        private static readonly string _pathToUsers = "D:\\facultate\\an2\\sem2\\MAP\\Tema1MAP\\Dictionary\\Dictionary\\Resource\\userdata.json";

        public DatabaseEmulator() {/*EMPTY*/; }

        public IEnumerable<Word> GetWordsFromFile()
        {
            try
            {
                using (FileStream json = File.OpenRead(_pathToWords))
                using (StreamReader reader = new StreamReader(json))
                {
                    string jsonString = reader.ReadToEnd();
                    List<Word> words = JsonConvert.DeserializeObject<List<Word>>(jsonString);
                    return words;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public void WriteWordsToFile(List<Word> words)
        {
            try
            {
                using (StreamWriter file = File.CreateText(_pathToWords))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, words);
                }
            }
            catch (Exception) { }
        }

        public IEnumerable<User> GetUsersFromFile()
        {
            try
            {
                using (FileStream json = File.OpenRead(_pathToUsers))
                using (StreamReader reader = new StreamReader(json))
                {
                    string jsonString = reader.ReadToEnd();
                    IEnumerable<User> users = JsonConvert.DeserializeObject<IEnumerable<User>>(jsonString);
                    return users;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void WriteUsersToFile(List<User> words)
        {
            try
            {
                using (StreamWriter file = File.CreateText(_pathToUsers))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, words);
                }
            }
            catch (Exception) { }
        }
    }
}
