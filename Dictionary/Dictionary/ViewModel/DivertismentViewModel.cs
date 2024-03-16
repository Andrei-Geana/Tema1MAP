using Dictionary.Command;
using Dictionary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Dictionary.ViewModel
{
    public class DivertismentViewModel : ViewModelBase
    {
        private DatabaseEmulator emulator;
        private List<Word> words;
        private int _currentRoundIndex = 0;
        public static int _numberOfRounds = 5;

        public DivertismentViewModel()
        {
            /*EMPTY*/
        }

        public DivertismentViewModel(DatabaseEmulator databaseEmulator)
        {
            emulator = databaseEmulator;
            words = emulator.GetWordsFromFile().ToList();
            StartAnotherGame();
            NextWordCommand = new QuizNextWorkCommand(this);
            PreviousWordCommand = new QuizPreviousWordCommand(this);

        }

        public ICommand NextWordCommand { get; set; }

        public ICommand PreviousWordCommand { get; set; }

        private void ShuffleWords()
        {
            var random = new Random();
            int n = words.Count();
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (words[n], words[k]) = (words[k], words[n]);
            }
        }

        private List<string> _input;

        public string Input
        {
            get => _input[_currentRoundIndex];
            set
            {
                _input[_currentRoundIndex] = value;
                OnPropertyChanged(nameof(Input));
            }
        }

        public int CurrentRoundIndex
        {
            get => _currentRoundIndex + 1;
            set
            {
                _currentRoundIndex = value;
                OnPropertyChanged(nameof(CurrentRoundIndex));
            }
        }

        public int NumberOfRounds
        {
            get => _numberOfRounds;
            set
            {
                _numberOfRounds = value;
                OnPropertyChanged(nameof(NumberOfRounds));
            }
        }

        public Word CurrentWord
        {
            get => words[_currentRoundIndex];
        }

        private List<bool> _imageIsShown;
        public bool ImageIsShown
        {
            get => _imageIsShown[_currentRoundIndex];
            set
            {
                _imageIsShown[_currentRoundIndex] = value;
                OnPropertyChanged(nameof(ImageIsShown));
            }
        }

        private void DecideIfImagineIsShownForEachWord()
        {
            //Returns true if an image is to be shown, or false otherwise
            var random = new Random();
            for(int index=0; index<NumberOfRounds;index++)
            {
                if (words[index].PathToImage == Word.DefaultPath)
                {
                    _imageIsShown[index] = false;
                    continue;
                }
                int k = random.Next(int.MaxValue);
                if (k % 2 == 1)
                    _imageIsShown[index] = true;
                else
                    _imageIsShown[index] = false;
            }
        }

        public void ExecuteNextWordCommand()
        {
            if (_currentRoundIndex < _numberOfRounds - 1)
            {
                _currentRoundIndex++; 
                NotifyAll();
            }
            else
            {
                ShowScore();
                StartAnotherGame();
            }

        }

        public void ExecutePreviousWordCommand()
        {
            _currentRoundIndex--; 
            NotifyAll();

        }

        private void StartAnotherGame()
        {
            _currentRoundIndex = 0;
            _input = Enumerable.Repeat(string.Empty, _numberOfRounds).ToList();
            _imageIsShown = Enumerable.Repeat(false, _numberOfRounds).ToList();
            ShuffleWords();
            DecideIfImagineIsShownForEachWord();
            NotifyAll();

        }

        private void ShowScore()
        {
            int score = CalculateScore();
            MessageBox.Show($"Your score is {score} out of {_numberOfRounds}");
        }

        private int CalculateScore()
        {
            int score = 0;
            for (int i = 0; i < _numberOfRounds; i++)
            {
                if (words[i].WordValue == _input[i]) score++;
            }
            return score;
        }

        public string RightButtonContent
        {
            get => (CurrentRoundIndex == NumberOfRounds) ? "FINISH" : "NEXT";
        }

        public bool LeftButtonIsEnabled
        {
            get => (CurrentRoundIndex != 1);
        }

        private void NotifyAll()
        {

            OnPropertyChanged(nameof(CurrentRoundIndex));
            OnPropertyChanged(nameof(CurrentWord));
            OnPropertyChanged(nameof(Input));
            OnPropertyChanged(nameof(RightButtonContent));
            OnPropertyChanged(nameof(LeftButtonIsEnabled));
            OnPropertyChanged(nameof(ImageIsShown));
        }

    }
}
