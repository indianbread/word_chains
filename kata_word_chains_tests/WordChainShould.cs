using System;
using System.IO;
using kata_word_chains;
using Xunit;

namespace kata_word_chains_tests
{
    public class WordChainShould
    {
        public WordChainShould()
        {
            _dictionary = new Dictionary($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/data/dictionary.txt");            
        }

        private readonly Dictionary _dictionary;


        [Theory]
        [InlineData("cat", "dog", new string[] { "cat", "cot", "cog", "dog" })]
        public void ShouldGenerateAWordChain_GivenStartingAndEndingWord(string startingWord, string endingWord, string[] expectedWordChain)
        {
            var wordChain = new WordChain(_dictionary, startingWord, endingWord);
            var actualWordChain = wordChain.Create(5);

            Assert.Equal(expectedWordChain.ToString(), actualWordChain.ToString());

        }

    }
}
