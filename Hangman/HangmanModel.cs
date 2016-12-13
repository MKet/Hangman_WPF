using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_MVVM
{
    class HangmanModel
    {
        public char? Guess { get; set; }
        public Word wordToGuess { get; set; }
        public string CharactersGuessed { get; set; }
        public int GuessWrongAmount { get; set; }
        public int GuessRightAmount { get; set; }
    }
}
