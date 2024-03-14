using Dictionary.Command;
using Dictionary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dictionary.ViewModel
{
    public class AdminMenuViewModel : ViewModelBase
    {
        private DatabaseEmulator emulator;
        private ObservableCollection<Word> _words;
        private Word _currentWord;
        private ObservableCollection<string> _categories;

        public AdminMenuViewModel()
        {
            /*EMPTY*/
        }

        public AdminMenuViewModel(DatabaseEmulator databaseEmulator)
        {
            emulator = databaseEmulator;
            Words = emulator.GetWordsFromFile();
            CurrentWord = new Word();
            Categories = emulator.GetCategories();
            NewWordCommand = new NewWordCommand(this);
            AddWordCommand = new AddWordCommand(this);
            ModifyWordCommand = new ModifyWordCommand(this);
            DeleteWordCommand = new DeleteWordCommand(this);
        }

        public ObservableCollection<Word> Words 
        { 
            get => _words;
            set
            {
                _words = value;
                OnPropertyChanged(nameof(Words));
            }
        }
        public Word CurrentWord
        {
            get => _currentWord;
            set
            {
                _currentWord = value;
                OnPropertyChanged(nameof(CurrentWord));
            }
        }
        public ObservableCollection<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public ICommand NewWordCommand { get; }
        public ICommand AddWordCommand { get; }
        public ICommand ModifyWordCommand { get; }
        public ICommand DeleteWordCommand { get; }

        public void AddWord()
        {
            if(_words.Contains(CurrentWord))
            {
                MessageBox.Show("Cuvantul nu poate fi readaugat!");
                return;
            }
            try
            {
                emulator.AddWord(CurrentWord);
                Words = emulator.GetWordsFromFile(); 
                NewWord();
                Categories = emulator.GetCategories();
            }
            catch (NotImplementedException)
            {
                MessageBox.Show("Campul denumirii nu poate fi gol!");
            }
            catch (Exception)
            {
                MessageBox.Show("Cuvantul deja exista!");
            }
        }

        public void NewWord()
        {
            CurrentWord = new Word();
        }

        public bool CanAddWord => !string.IsNullOrEmpty(CurrentWord.WordValue);

        public void ModifyWord()
        {
            if(CurrentWord is null)
            {
                MessageBox.Show("Nu ai selectat un cuvant!");
                return;
            }
            if(string.IsNullOrEmpty(CurrentWord.WordValue))
            {
                MessageBox.Show("Cuvantul trebuie sa aiba o denumire!");
                return;
            }
            emulator.ModifyWord(CurrentWord, Words.IndexOf(CurrentWord));
            Words = emulator.GetWordsFromFile();
            NewWord();
            Categories = emulator.GetCategories();
        }

        public void DeleteWord()
        {
            if (CurrentWord is null || string.IsNullOrEmpty(CurrentWord.WordValue))
            {
                MessageBox.Show("Nu ai selectat un cuvant!");
                return;
            }
            emulator.DeleteWord(Words.IndexOf(CurrentWord));
            Words = emulator.GetWordsFromFile();
            NewWord();
            Categories = emulator.GetCategories();
        }
    }
}
