using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace kata_word_chains
{
    public class Dictionary
    {
        public Dictionary(string filePath)
        {
            _filePath = filePath;
            LoadWords();
        }

        private string[] _validWords;
        private readonly string _filePath;

        public void LoadWords()
        {
            //var filePath = "./data/words_alpha.txt";
            try
            {
                _validWords = File.ReadAllLines(_filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file" + ex.Message);
            }

        }

        public bool WordIsValid(string word)
        {
            return _validWords.Contains(word);
        }

        public string GetNextWord(string startingWord, string endingWord, int indexOfLetterToReplace)
        {
            var wordLength = startingWord.Length;
            var letters = startingWord.Split("");
            var firstChunk = startingWord.Substring(0, indexOfLetterToReplace);
            var secondChunk = startingWord.Substring(indexOfLetterToReplace + 1, (wordLength - firstChunk.Length - 1));
            var possibleWords = _validWords.Where(word => word.Length == startingWord.Length);
            var firstChunkMatches = possibleWords.Where(word => word.Substring(0, firstChunk.Length) == firstChunk);
            var secondChunkMatches = firstChunkMatches.Where(word => word.Substring(indexOfLetterToReplace + 1, (wordLength - firstChunk.Length - 1)) == secondChunk).ToArray();
            string randomWord = null;
            for (int i = 0; i < secondChunkMatches.Count(); i++)
            {
                var word = secondChunkMatches[i];
                var lettersInWord = word.ToCharArray();
                var letterReplacement = lettersInWord[indexOfLetterToReplace];
                if (endingWord.Contains(letterReplacement))
                {
                    randomWord = word;
                    return randomWord;
                }

            }

            return randomWord;
        }

    }
}
