using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Hangman_MVVM
{
    class FileDataManager : IDataManager
    {
        private readonly string FileName = "Dutch_Dictionary.txt";

        private IMessengerService messenger;

        public FileDataManager(IMessengerService messenger)
        {
            this.messenger = messenger;
        }

        public async Task addWord(Word word)
        {
            List<string> wordList = new List<string>();

            try
            {
                using (StreamReader reader = File.OpenText(FileName))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        // no use in adding a word that already exists
                        if (line == word.Content)
                            return;
                        wordList.Add(line);
                    }
                }

                wordList.Add(word.Content);

                File.WriteAllLines(FileName, wordList.OrderBy(x => x).ToArray());
            }
            catch (FileNotFoundException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Het bestand is niet gevonden.");
            }
            catch (IOException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Er is is misgegaan bij het lezen van het woord bestand.");
            }
        }

        public async Task deleteWord(Word word)
        {
            try
            {
                List<string> wordList = new List<string>();
                using (StreamReader reader = File.OpenText(FileName))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (line == word.Content)
                            continue;
                        wordList.Add(line);
                    }
                }

                File.WriteAllLines(FileName, wordList.ToArray());
            }
            catch (FileNotFoundException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Het bestand is niet gevonden.");
            }
            catch (IOException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Er is is misgegaan bij het lezen van het woord bestand.");
            }
        }

        public async Task<T> getWords<T>() where T : ICollection<Word>, new()
        {

            T wordList = new T();
            try
            {
                using (StreamReader reader = File.OpenText(FileName))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        Word w = new Word();
                        w.Content = line;
                        wordList.Add(w);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Het bestand is niet gevonden.");
            }
            catch (IOException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Er is is misgegaan bij het lezen van het woord bestand.");
            }

            return wordList;
        }

        public async Task<T> getWords<T>(Regex name) where T : ICollection<Word>, new()
        {
            T wordList = new T();
            try
            {
                using (StreamReader reader = File.OpenText(FileName))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (!name.IsMatch(line))
                            continue;
                        Word w = new Word();
                        w.Content = line;
                        wordList.Add(w);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Het bestand is niet gevonden.");
            }
            catch (IOException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Er is is misgegaan bij het lezen van het woord bestand.");
            }
            return wordList;
        }

        public async Task<T> getWords<T>(Regex name, int count) where T : ICollection<Word>, new()
        {
            T wordList = new T();
            try
            {
                using (StreamReader reader = File.OpenText(FileName))
                {
                    string line;
                    int I = 0;
                    while ((line = await reader.ReadLineAsync()) != null && I < count)
                    {
                        if (!name.IsMatch(line))
                            continue;
                        Word w = new Word();
                        w.Content = line;
                        wordList.Add(w);
                        I++;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Het bestand is niet gevonden.");
            }
            catch (IOException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Er is is misgegaan bij het lezen van het woord bestand.");
            }
            return wordList;
        }

        public async Task<T> getWords<T>(string name) where T : ICollection<Word>, new()
        {
            T wordList = new T();

            try
            {
                using (StreamReader reader = File.OpenText(FileName))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (!line.Contains(name))
                            continue;
                        Word w = new Word();
                        w.Content = line;
                        wordList.Add(w);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Het bestand is niet gevonden.");
            }
            catch (IOException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Er is is misgegaan bij het lezen van het woord bestand.");
            }
            return wordList;
        }

        public async Task<T> getWords<T>(string name, int count) where T : ICollection<Word>, new()
        {
            T wordList = new T();
            try
            {
                using (StreamReader reader = File.OpenText(FileName))
                {
                    string line;
                    int I = 0;
                    while ((line = await reader.ReadLineAsync()) != null && I < count)
                    {
                        if (!line.Contains(name))
                            continue;
                        Word w = new Word();
                        w.Content = line;
                        wordList.Add(w);
                        I++;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Het bestand is niet gevonden.");
            }
            catch (IOException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Er is is misgegaan bij het lezen van het woord bestand.");
            }
            return wordList;
        }

        public async Task<Word> getRandomWord()
        {
            Word w = new Word();
            try
            {
                using (StreamReader reader = File.OpenText(FileName))
                {
                    int numberSeen = 0;
                    Random r = new Random();
                    var line = string.Empty;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        numberSeen++;
                        if (r.Next(numberSeen) == 0)
                        {
                            w.Content = line;
                        }
                    }
                }

            }
            catch (FileNotFoundException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Het bestand is niet gevonden.");
            }
            catch (IOException e)
            {
                messenger.Debug(e.Message);
                messenger.Error("Er is is misgegaan bij het lezen van het woord bestand.");
            }
            w.Content = w.Content.Normalize(NormalizationForm.FormD);
            return w;
        }
    }
}
