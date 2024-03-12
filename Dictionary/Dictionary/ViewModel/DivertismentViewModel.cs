using Dictionary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Dictionary.ViewModel
{
    public class DivertismentViewModel : ViewModelBase
    {
        private DatabaseEmulator emulator;
        private List<Word> words;
        private int currentRoundIndex = 0;
        private int numberOfRounds = 5;
        public DivertismentViewModel()
        {
            emulator = new DatabaseEmulator();
            words = emulator.GetWordsFromFile().ToList();
            ShuffleWords();
            ImageIsShown = ImageIsToBeShown();
        }

        private void ShuffleWords()
        {
            var random = new Random();
            int n = words.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (words[n], words[k]) = (words[k], words[n]);
            }
        }

        private string _input;

        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
        }

        public int CurrentRoundIndex
        {
            get => currentRoundIndex+1;
            set
            {
                currentRoundIndex = value;
                OnPropertyChanged(nameof(CurrentRoundIndex));
            }
        }

        public int NumberOfRounds
        {
            get => numberOfRounds;
            set
            {
                numberOfRounds = value;
                OnPropertyChanged(nameof(NumberOfRounds));
            }
        }

        public Word CurrentWord
        {
            get => words[currentRoundIndex];
        }
        public bool ImageIsShown
        { 
            get => _imageIsShown;
            set 
            {
                _imageIsShown = value;
                OnPropertyChanged(nameof(ImageIsShown));
            }
        }

        private bool ImageIsToBeShown()
        {
            //Returns true if an image is to be shown, or false otherwise
            var random = new Random();
            if (CurrentWord.PathToImage != Word.DefaultPath)
            {
                int k = random.Next(2);
                if (k == 1)
                    return true;
            }
            return false;
        }

        private bool _imageIsShown;

    }
}
