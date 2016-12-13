using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hangman_MVVM
{
    interface IDataManager
    {
        Task<Word> getRandomWord();
        Task<T> getWords<T>() where T : ICollection<Word>, new();
        Task<T> getWords<T>(string name, int I) where T : ICollection<Word>, new();
        Task<T> getWords<T>(string name) where T : ICollection<Word>, new();
        Task<T> getWords<T>(Regex name) where T : ICollection<Word>, new();

        Task addWord(Word word);
        Task deleteWord(Word word);
    }
}
