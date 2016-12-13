using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

namespace Hangman_MVVM
{
    class HangmanViewModel : ViewModel
    {
        const int LosingThreshHold = 10;
        private HangmanModel model;
        public IMessengerService messenger { get; set; }
        public IDataManager dataManager { get; set; }

        public List<char> alphabet { get; set; }

        
        public char? Guess
        {
            get
            {
                return model.Guess;
            }
            set
            {
                if (model.Guess != value)
                {
                    model.Guess = value;

                    InvokePropertyChanged();
                    command.ExecuteChangedInvoke();
                }
            }
        }

        public Word wordToGuess
        {
            get
            {
                return model.wordToGuess;
            }
            set
            {
                if (model.wordToGuess != value)
                {
                    model.wordToGuess = value;
                    InvokePropertyChanged();
                }
            }
        }

        public string CharactersGuessed
        {
            get
            {
                return model.CharactersGuessed;
            }
            set
            {
                if (model.CharactersGuessed != value)
                {
                    model.CharactersGuessed = value;
                    InvokePropertyChanged();
                }
            }
        }

        public int GuessWrongAmount
        {
            get
            {
                return model.GuessWrongAmount;
            }
            set
            {
                if (model.GuessWrongAmount != value)
                {
                    model.GuessWrongAmount = value;
                    InvokePropertyChanged();
                }
            }
        }
        
        public int GuessRightAmount
        {
            get
            {
                return model.GuessRightAmount;
            }
            set
            {
                if (model.GuessRightAmount != value)
                {
                    model.GuessRightAmount = value;
                    InvokePropertyChanged();
                }
            }
        }

        private RelayCommand command;
        public ICommand buttonCommand => command;

        public HangmanViewModel(IDataManager dataManager, IMessengerService messenger)
        { 
            this.dataManager = dataManager;
            this.messenger = messenger;
            
            command = new RelayCommand(ButtonCommandExecutes, validate);
            model = new HangmanModel();
            alphabet = new List<char>(26);
            foreach (char I in Enumerable.Range('A', 26))
                alphabet.Add(I);

            ChooseRandomWord();
            Ready = true;
        }

        private bool validate() => Guess != null && Ready;

        private async void ButtonCommandExecutes()
        {
            if (Guess == null)
            {
                messenger.Info("Vul een letter in");
                return;
            }
            
            Ready = false;
            CheckGuess();
            await CheckWinner();
            Ready = true;
            Guess = null;
        }

        private void CheckGuess()
        {
            if (CharactersGuessed.Contains(Guess.ToString()))
            {
                messenger.Info("De letter is al geraden");
                return;
            }
            var GuessedCorrectly = false;
            char[] characters = CharactersGuessed.ToCharArray();

            for (int I = 0; I < characters.Length; I++)
            {
                if (char.ToLower(wordToGuess.Content[I]) == char.ToLower((char)Guess))
                {
                    characters[I] = wordToGuess.Content[I];
                    GuessedCorrectly = true;
                }
            }
            if (GuessedCorrectly)
            {
                CharactersGuessed = new string(characters);
                GuessRightAmount++;
            }
            else
            {
                GuessWrongAmount++;
            }
        }

        private async Task CheckWinner()
        {
            if (GuessWrongAmount == LosingThreshHold)
            {
                messenger.Info($"Helaas! Je hebt verloren. Het woord was {wordToGuess.Content}.");
                await Reset();
            }
            else if (string.Compare(wordToGuess.Content, CharactersGuessed, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0)
            {
                messenger.Info("Je hebt het woord geraden!");
                await Reset();
            }
        }

        private async Task ChooseRandomWordAsync()
        {
            wordToGuess = await dataManager.getRandomWord();

            var sb = new StringBuilder(wordToGuess.Content.Length);
            foreach (char c in wordToGuess.Content)
            {
                var lowerChar = char.ToLower(c);

                if (char.IsPunctuation(lowerChar) || char.IsWhiteSpace(lowerChar))
                    sb.Append(c);
                else if (char.GetUnicodeCategory(lowerChar) != UnicodeCategory.NonSpacingMark)
                    sb.Append('-');
            }

            CharactersGuessed = sb.ToString();
        }

        private async void ChooseRandomWord()
        {
            Ready = false;
            await ChooseRandomWordAsync();
            Ready = true;
        }

        private async Task Reset()
        {
            await ChooseRandomWordAsync();
            GuessRightAmount = 0;
            GuessWrongAmount = 0;
        }
    }
}
