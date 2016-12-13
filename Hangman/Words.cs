using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hangman_MVVM
{
    class Words
    {
        private IDataManager manager;
        public ObservableCollection<Word> WordList { get; set;}

        public Words(IDataManager datamanager)
        {
            manager = datamanager;
            WordList = new ObservableCollection<Word>();
        }

        public async Task Fill()
        {
            WordList.Clear();
            foreach (Word word in await manager.getWords<List<Word>>())
            {
                WordList.Add(word);
            }
        }

        public async Task Fill(string name)
        {
            WordList.Clear();
            foreach (Word word in await manager.getWords<List<Word>>())
            {
                WordList.Add(word);
            }
        }

        public async Task Fill(string name, int amount)
        {
            WordList.Clear();
            foreach (Word word in await manager.getWords<List<Word>>(name, amount))
            {
                WordList.Add(word);
            }
        }

        public async Task Fill(Regex name)
        {
            WordList.Clear();
            foreach (Word word in await manager.getWords<List<Word>>(name))
            {
                WordList.Add(word);
            }
        }

        public async Task RemoveWord(Word word)
        {
            await manager.deleteWord(word);
        }

        public async Task AddWord(Word word)
        {
            await manager.addWord(word);
        }
        
    }
}
