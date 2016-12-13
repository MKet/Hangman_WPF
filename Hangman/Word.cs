using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_MVVM
{
    class Word
    {
        public Word()
        {
            Content = string.Empty;
        }

        public Word(string content)
        {
            Content = content;
        }

        public string Content { get; set; }

        public override string ToString() => Content;
    }
}
