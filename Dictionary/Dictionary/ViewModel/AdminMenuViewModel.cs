using Dictionary.Command;
using Dictionary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace Dictionary.ViewModel
{
    public class AdminMenuViewModel : ViewModelBase
    {
        private DatabaseEmulator emulator;
        private ObservableCollection<Word> _words;
        private Word _currentWord;
        private ObservableCollection<string> _categories;
        private static string _defaultCategory = "all";
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
            ChangeImageCommand = new ChangeImageCommand(this);

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
        public ICommand ChangeImageCommand { get; }

        public void AddWord()
        {
            if(_words.Contains(CurrentWord))
            {
                MessageBox.Show("Word already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(CurrentWord.Category)) CurrentWord.Category = _defaultCategory;
                if (!CanAddWord) throw new ArgumentException();
                emulator.AddWord(CurrentWord);
                Words = emulator.GetWordsFromFile(); 
                NewWord();
                Categories = emulator.GetCategories();
            }
            catch (NotImplementedException)
            {
                MessageBox.Show("Empty fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Invalid data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Word already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void NewWord()
        {
            CurrentWord = new Word(null,_defaultCategory,null);
        }

        public bool CanAddWord => _canAddWord();

        private bool _canAddWord()
        {

            if (CurrentWord == null) return false;

            if (string.IsNullOrEmpty(CurrentWord.WordValue)) return false;

            char[] specialChars = { '_', '\'', '"', '#', '$', '%', '^', '*', '(', ')', ' ' };

            if (CurrentWord.WordValue.Any(c => specialChars.Contains(c))) return false;

            if (string.IsNullOrEmpty(CurrentWord.Description)) return false;

            if (string.IsNullOrEmpty(CurrentWord.Category)) return false;

            if (CurrentWord.Category.Any(c => specialChars.Contains(c))) return false;

            return true;
        }


        public void ModifyWord()
        {
            if(CurrentWord is null)
            {
                MessageBox.Show("You haven't selected a word.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(!CanAddWord)
            {

                MessageBox.Show("Input values are not valid.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            emulator.ModifyWord(CurrentWord, Words.IndexOf(CurrentWord));
            Words = emulator.GetWordsFromFile();
            //NewWord();
            Categories = emulator.GetCategories();
        }

        public void DeleteWord()
        {
            if (CurrentWord is null || string.IsNullOrEmpty(CurrentWord.WordValue))
            {
                MessageBox.Show("You haven't selected a word.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            emulator.DeleteWord(Words.IndexOf(CurrentWord));
            Words = emulator.GetWordsFromFile();
            NewWord();
            Categories = emulator.GetCategories();
        }

        public void ChangeImage()
        {
            if (CurrentWord is null)
            {
                MessageBox.Show("You haven't selected a word.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp|All files|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                string sourceFilePath = openFileDialog.FileName;

                string resourcesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource/Image/");

                string fileName = Path.GetFileName(sourceFilePath);
                string destinationFilePath = Path.Combine(resourcesDirectory, fileName);

                File.Copy(sourceFilePath, destinationFilePath, true);


                string relativeUriPath = $"./Resource/Image/{fileName}";
                CurrentWord.PathToImage = relativeUriPath;
                ModifyWord();
                OnPropertyChanged(nameof(CurrentWord));
            }
        }
    }
}
