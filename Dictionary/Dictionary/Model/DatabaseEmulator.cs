﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Dictionary.Model
{
    public class DatabaseEmulator
    {
        public ObservableCollection<Word> words;
        public ObservableCollection<User> users;

        private static readonly string _pathToWords = AppDomain.CurrentDomain.BaseDirectory + "Resource/worddata.json";
        private static readonly string _pathToUsers = AppDomain.CurrentDomain.BaseDirectory + "Resource/userdata.json";

        public DatabaseEmulator() 
        {
            words = GetWordsFromFile() ?? new ObservableCollection<Word>();
            users = GetUsersFromFile() ?? new ObservableCollection<User>();
        }

        public ObservableCollection<Word> GetWordsFromFile()
        {
            try
            {
                using (FileStream json = File.OpenRead(_pathToWords))
                using (StreamReader reader = new StreamReader(json))
                {
                    string jsonString = reader.ReadToEnd();
                    ObservableCollection<Word> words = JsonConvert.DeserializeObject<ObservableCollection<Word>>(jsonString);
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

        public ObservableCollection<User> GetUsersFromFile()
        {
            try
            {
                using (FileStream json = File.OpenRead(_pathToUsers))
                using (StreamReader reader = new StreamReader(json))
                {
                    string jsonString = reader.ReadToEnd();
                    ObservableCollection<User> users = JsonConvert.DeserializeObject<ObservableCollection<User>>(jsonString);
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

        public ObservableCollection<string> GetCategories()
        {
            if (words.Count == 0)
                return new ObservableCollection<string>();
            return new ObservableCollection<string>(
                 words.DefaultIfEmpty().Select(w => w.Category).Distinct().ToList());
        }

        private void WriteWordsInDatabaseToFile()
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

        public void AddWord(Word newWord)
        {
            if (string.IsNullOrEmpty(newWord.WordValue))
                throw new NotImplementedException();
            if (words.Where(word => word.WordValue==newWord.WordValue).ToList().Count>0)
                throw new Exception();
            words.Add(newWord);
            WriteWordsInDatabaseToFile();
        }

        public void DeleteWord(int index)
        {
            if (index < 0 || index > words.Count) throw new Exception();
            words.RemoveAt(index);
            WriteWordsInDatabaseToFile();
        }

        public void ModifyWord(Word word, int index)
        {
            if (index < 0 || index > words.Count)
            {
                return;
            }
            words[index] = word;
            WriteWordsInDatabaseToFile();
        }
    }
}
