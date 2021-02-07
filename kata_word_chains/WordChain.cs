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

        public IEnumerable<string> Create(int numberOfAttempts)
        {
            if (numberOfAttempts == 0)
                throw new ArgumentException("Unable to generate a word chain");
         
            IList<int> letterIndexesNotChanged = Enumerable.Range(0, _startingWord.Length).ToList();
            letterIndexesNotChanged.Shuffle();
            IList<string> wordChain = new List<string>() { _startingWord };
            CreatePossibleWordChain(letterIndexesNotChanged, wordChain);
            if (wordChain.Last() == _endingWord) return wordChain;
            numberOfAttempts--;
            return Create(numberOfAttempts);
        }

        public override string ToString()
        {
            return String.Join(", ", _words);
        }

        private void CreatePossibleWordChain(IList<int> letterIndexesNotChanged, IList<string> wordChain)
        {
            GenerateWordChain(letterIndexesNotChanged, wordChain, _startingWord, _endingWord);
            if (letterIndexesNotChanged.Count() == 1)
            {
                var currentLastWordInChain = wordChain.Last();
                var letterToReplace = _endingWord.Where(letter => !currentLastWordInChain.Contains(letter)).First();
                var nextWord = currentLastWordInChain.Replace(currentLastWordInChain[letterIndexesNotChanged.First()], letterToReplace);
            }
            if (letterIndexesNotChanged.Count() > 1)
            {
                GenerateWordChain(letterIndexesNotChanged, wordChain, wordChain.Last(), _endingWord);
            }
        }

        private IEnumerable<string> GenerateWordChain(IList<int> letterIndexesNotChanged, IList<string> wordChain, string startingWord, string endingWord)
        {
            var letterIndexesReplaced = letterIndexesNotChanged.ToList();
            foreach (int i in letterIndexesNotChanged)
            {
                var possibleNextWords = _dictionary.GetPossibleNextWords(startingWord, endingWord, letterIndexesNotChanged[i]).ToArray();
                if (!possibleNextWords.Any()) continue;
                var random = new Random();
                var randomIndex = random.Next(0, possibleNextWords.Length - 1);
                var nextWord = possibleNextWords[randomIndex];
                letterIndexesReplaced.Add(i);
                wordChain.Add(nextWord);
            }
            letterIndexesReplaced.ForEach(index => letterIndexesNotChanged.Remove(index));
            return wordChain;
        }



        private IDictionary _dictionary;
        private readonly string _startingWord;
        private readonly string _endingWord;
        private string[] _words;
    }
}
