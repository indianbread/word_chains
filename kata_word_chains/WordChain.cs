using System;
using System.Collections.Generic;
using System.Linq;

namespace kata_word_chains
{
    public class WordChain
    {

        public WordChain(IDictionary dictionary, string startingWord, string endingWord)
        {
            _dictionary = dictionary;
            _startingWord = startingWord;
            _endingWord = endingWord;
        }

        public IEnumerable<string> Create(int numberOfAttempts, int attemptNumber = 1)
        {
            if (numberOfAttempts == 0)
                throw new ArgumentException("Unable to generate a word chain");
            IList<string> wordChain = new List<string>() { _startingWord };
            if (attemptNumber > 1)
            {
                var random = new Random();
                var randomIndexOfLetterToReplace = random.Next(0, _startingWord.Length - 1);
                var randomFillerWord = _dictionary.GetPossibleNextWords(_startingWord, _endingWord, randomIndexOfLetterToReplace).First();
                wordChain.Add(randomFillerWord);

            }

            IList<int> letterIndexesNotChanged = Enumerable.Range(0, _startingWord.Length).ToList();
            ReplaceLettersWherePossible(wordChain, letterIndexesNotChanged);
            //replace remaining letters if possible
            if (!letterIndexesNotChanged.Any()) return _words = wordChain;

            ReplaceRemainingLettersWherePossible(wordChain, letterIndexesNotChanged);

            //find best possible matches and replace with filler word before attempting to replace letters
            if (!letterIndexesNotChanged.Any()) return _words = wordChain;
            FindNextBestWordAndAttemptToReplaceLetters(wordChain, letterIndexesNotChanged);

            if (!letterIndexesNotChanged.Any()) return _words = wordChain;
            if (letterIndexesNotChanged.Count() == 1)
            {
                var currentLastWord = wordChain.Last();
                var nextWord = currentLastWord.Replace(currentLastWord[letterIndexesNotChanged.First()], _endingWord[letterIndexesNotChanged.First()]);
                if (nextWord == _endingWord)
                {
                    wordChain.Add(_endingWord);
                    return _words = wordChain;
                }
            }
            if (wordChain.Last() == _endingWord) return _words = wordChain;
            Console.WriteLine(String.Join(", ", wordChain));
            numberOfAttempts--;
            attemptNumber++;
            return Create(numberOfAttempts, attemptNumber);
        }

        private void FindNextBestWordAndAttemptToReplaceLetters(IList<string> wordChain, IList<int> letterIndexesNotChanged)
        {
            for (int i = 0; i < letterIndexesNotChanged.Count(); i++)
            {
                if(wordChain.Last() == _endingWord) return;
                var possibleNextWords = _dictionary.GetPossibleNextWords(wordChain.Last(), _endingWord, letterIndexesNotChanged[i]);
                if (!possibleNextWords.Any())
                    throw new ArgumentException("Unable to generate word chain");
                if (possibleNextWords.Count() == 1 && !wordChain.Contains(possibleNextWords.First()))
                {
                    wordChain.Add(possibleNextWords.First());
                }
                else if (possibleNextWords.Count() > 1)
                {
                    foreach (var word in possibleNextWords)
                    {
                        if(wordChain.Last() == _endingWord) return;
                        if (wordChain.Contains(word)) continue;
                        wordChain.Add(word);
                        var nextNextWord = word.Replace(word[letterIndexesNotChanged[i]], _endingWord[i]);
                        if (!_dictionary.WordIsValid(nextNextWord)) continue;
                        if (wordChain.Contains(nextNextWord)) continue;
                        wordChain.Add(nextNextWord);
                    }

                }

            }
        }

        private void ReplaceRemainingLettersWherePossible(IList<string> wordChain, IList<int> letterIndexesNotChanged)
        {
            var indexesNotChanged = letterIndexesNotChanged.ToList();
            foreach (int index in indexesNotChanged)
            {
                if (wordChain.Last() == _endingWord) return;
                var currentLastWordInChain = wordChain.Last();
                var nextWord = currentLastWordInChain.Replace(currentLastWordInChain[index], _endingWord[index]);
                if (!_dictionary.WordIsValid(nextWord)) continue;
                if (wordChain.Contains(nextWord)) continue;
                wordChain.Add(nextWord);
                letterIndexesNotChanged.Remove(index);
            }
        }

        private void ReplaceLettersWherePossible(IList<string> wordChain, IList<int> letterIndexesNotChanged)
        {
            var letterIndexes = letterIndexesNotChanged.ToList();
            var currentLastWordInChain = wordChain.Last();
            foreach (int index in letterIndexes)
            {
                if (wordChain.Last() == _endingWord) return;
                var nextWord = currentLastWordInChain.Replace(currentLastWordInChain[index], _endingWord[index]);
                if (!_dictionary.WordIsValid(nextWord)) continue;
                if (wordChain.Contains(nextWord)) continue;
                wordChain.Add(nextWord);
                letterIndexesNotChanged.Remove(index);
            }
        }

        public override string ToString()
        {
            return String.Join(", ", _words);
        }

        private IDictionary _dictionary;
        private readonly string _startingWord;
        private readonly string _endingWord;
        private IList<string> _words;
    }
}
