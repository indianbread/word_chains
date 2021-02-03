using System;
using System.IO;
using kata_word_chains;
using Xunit;

namespace kata_word_chains_tests
{
    public class WordChainTests
    {
        public WordChainTests()
        {
            _dictionary = new Dictionary($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/data/dictionary.txt");
        }

        private readonly Dictionary _dictionary;

        [Theory]
        [InlineData("cat", "cot", "dog")]
        [InlineData("ruby", "rubs", "code")]
        public void EachWordShouldOnlyDifferByOneLetter_GivenStartAndEndWord(string startingWord, string expectedNextWord, string endingWord)
        {
            var actualNextWord = _dictionary.GetNextWord(startingWord, endingWord, 1);

            Assert.Equal(expectedNextWord, actualNextWord);
        }


        [Theory]
        [InlineData("cat", "dog")]
        public void Form_Word_Chain_Given_Start_And_End_Word(string startWord, string endWord)
        {
  
        }
    }
}

//mock the dictionary to make it contain only certain words