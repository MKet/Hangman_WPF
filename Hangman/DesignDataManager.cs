using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hangman_MVVM
{
    class DesignDataManager : IDataManager
    {
        private T initializeList<T>() where T : ICollection<Word>, new()
        {
            return new T()
            {
                new Word("Aardbei"),
                new Word("Aardappel"),
                new Word("Gehaktbal"),
                new Word("Wortel"),
            };

        }

        public async Task addWord(Word word)
        {
            await Task.Factory.StartNew(() => { });
        }

        public async Task deleteWord(Word word)
        {
            await Task.Factory.StartNew(() => { });
        }

        public async Task<T> getWords<T>() where T : ICollection<Word>, new()
        {
            return await Task<T>.Factory.StartNew(() => (T)initializeList<T>());
        }

        public async Task<Word> getRandomWord()
        {
            return await Task<Word>.Factory.StartNew(() => new Word("Aardappel"));
        }


        public async Task<T> getWords<T>(Regex name) where T : ICollection<Word>, new()
        {
            return await Task.Run(() => initializeList<T>());
        }

        public async Task<T> getWords<T>(string name) where T : ICollection<Word>, new()
        {
            return await Task.Run(() => initializeList<T>());
        }

        public async Task<T> getWords<T>(string name, int I) where T : ICollection<Word>, new()
        {
            return await Task.Run(() => initializeList<T>());
        }
    }
}
