using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
namespace Hangman_MVVM
{
    class DatamanagerViewModel : ViewModel
    {
        private Words wordModel;

        public IEnumerable<Word> WordList
        {
            get
            {
                return wordModel.WordList;
            }
            set
            {
                if (wordModel.WordList != value)
                {
                    wordModel.WordList = (ObservableCollection<Word>)value;
                    InvokePropertyChanged();
                }
            }
        }

        public string filter = string.Empty;
        public string Filter
        {
            get
            {
                return filter;
            }
            set
            {
                if (filter != value)
                {
                    filter = value;

                    InvokePropertyChanged();
                    InvokePropertyChanged(this, nameof(WordList));

                }
            }
        }

        public Word selectedWord;
        public Word SelectedWord
        {
            get
            {
                return selectedWord;
            }
            set
            {
                if (selectedWord != value)
                {
                    selectedWord = value;
                    InvokePropertyChanged();
                }
            }
        }

        public Word newWord;
        public Word NewWord
        {
            get
            {
                return newWord;
            }
            set
            {
                if (newWord != value)
                {
                    newWord = value;
                    InvokePropertyChanged();
                }
            }
        }

        private int resultAmount;
        public int ResultAmount
        {
            get
            {
                return resultAmount;
            }
            set
            {
                if (resultAmount != value)
                {
                    resultAmount = value;
                    InvokePropertyChanged();
                }
            }
        }

        private RelayCommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand;

        private RelayCommand addCommand;
        public ICommand AddCommand => addCommand;

        private RelayCommand fillCommand;
        public ICommand FillCommand => fillCommand;

        public DatamanagerViewModel(IDataManager dataManager)
        {
            wordModel = new Words(dataManager);
            NewWord = new Word();

            addCommand = new RelayCommand(AddWord, () => true);
            deleteCommand = new RelayCommand(RemoveWord, () => true);
            fillCommand = new RelayCommand(FillList, () => true);
            
            wordModel.WordList.CollectionChanged += (sender, e) =>
                    InvokePropertyChanged(this, nameof(WordList));
            Ready = true;
        }


        public async void FillList()
        {
            Ready = false;
            await wordModel.Fill(Filter, ResultAmount);
            Ready = true;
        }

        public async void RemoveWord()
        {
            Ready = false;
            await wordModel.RemoveWord(selectedWord);
            await wordModel.Fill(Filter, ResultAmount);
            Ready = true;
        }

        public async void AddWord()
        {
            Ready = false;
            await wordModel.AddWord(NewWord);
            NewWord = new Word();
            await wordModel.Fill(Filter, ResultAmount);
            Ready = true;
        }
    }
}
