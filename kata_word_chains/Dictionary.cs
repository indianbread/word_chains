using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace kata_word_chains
{
    public class Dictionary : IDictionary
    {
        public Dictionary(string filePath) 
        {
            _filePath = filePath;
            LoadWords();
        }

        private string[] _validWords;
        private readonly string _filePath;

        public bool WordIsValid(string word)
        {
            return _validWords.Contains(word);
        }

        public IEnumerable<string> GetPossibleNextWords(string startingWord, string endingWord, int indexOfLetterToReplace)
        {
            var wordLength = startingWord.Length;
            SplitWord(startingWord, indexOfLetterToReplace, wordLength, out string firstChunk, out string secondChunk);
            return GetBestNextWord(startingWord, indexOfLetterToReplace, wordLength, firstChunk, secondChunk);

        }

        private IEnumerable<string> GetBestNextWord(string startingWord, int indexOfLetterToReplace, int wordLength, string firstChunk, string secondChunk)
        {
            var possibleWords = _validWords.Where(word => word.Length == startingWord.Length && word != startingWord);
            var firstChunkMatches = possibleWords.Where(word => word.Substring(0, firstChunk.Length) == firstChunk && word != startingWord).ToArray();
            var firstAndSecondChunkMatches = firstChunkMatches.Where(word => word.Substring(indexOfLetterToReplace + 1, (wordLength - firstChunk.Length - 1)) == secondChunk && word != startingWord);
            if (possibleWords.Any())
            {
                if (firstAndSecondChunkMatches.Any())
                {
                    return firstAndSecondChunkMatches;
                }
                if (firstChunkMatches.Any())
                {
                    return firstChunkMatches;
                }

            }

            return possibleWords;
        }

        private static void SplitWord(string startingWord, int indexOfLetterToReplace, int wordLength, out string firstChunk, out string secondChunk)
        {
            firstChunk = startingWord.Substring(0, indexOfLetterToReplace);
            secondChunk = startingWord.Substring(indexOfLetterToReplace + 1, (wordLength - firstChunk.Length - 1));
        }

        private void LoadWords()
        {
            try
            {
                _validWords = File.ReadAllLines(_filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file" + ex.Message);
            }

        }

    }
}
