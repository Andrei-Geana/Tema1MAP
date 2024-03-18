using Dictionary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Dictionary.ViewModel
{
    public class UserMenuViewModel : ViewModelBase
    {
        private static string _defaultCategory = "all";
        public UserMenuViewModel(DatabaseEmulator databaseEmulator)
        {
            emulator = databaseEmulator;
            words = emulator.GetWordsFromFile();

            ObservableCollection<string> categories = emulator.GetCategories();
            if(!categories.Contains(_defaultCategory))
                categories.Add(_defaultCategory);
            Categories = categories;

            _defaultWord = new Word("-", _defaultCategory, "-");
        }

        public UserMenuViewModel()
        {
            /*EMPTY*/
        }

        private DatabaseEmulator emulator;
        private IEnumerable<Word> words;

        public IEnumerable<Word> FilteredWords 
        { 
            get
            {
                if(SearchText is null && Category == _defaultCategory)
                    return words.ToList();

                if(SearchText is null && Category != _defaultCategory)
                    return words.Where(word => word.Category == Category).ToList();


                if (Category ==_defaultCategory)
                    return words.Where(word => word.WordValue.StartsWith(SearchText)).ToList();

                if (Category != _defaultCategory)
                    return words.Where(word => word.WordValue.StartsWith(SearchText) && word.Category==Category).ToList();

                return words.Where(word => word.WordValue.StartsWith(SearchText)).ToList();
            }
            set => words = value; 
        }

        private string _searchText;
        public string SearchText 
        { 
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        private readonly Word _defaultWord;

        public Word CurrentWord
        {
            get
            {
                if (SearchText != null && SearchText!= "" )
                    return FilteredWords.FirstOrDefault(w => w.WordValue == SearchText);
                return _defaultWord;
            }
            set
            {
                OnPropertyChanged(nameof(CurrentWord));
            }
        }

        public ObservableCollection<string> Categories { get => _categories; set => _categories = value; }
        public string Category 
        { 
            get =>  _category;
            set
            {
                if (value==_defaultCategory)
                {
                    SearchText = "";
                    OnPropertyChanged(nameof(CurrentWord));
                }
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private ObservableCollection<string> _categories;

        private string _category = _defaultCategory;

    }
}
