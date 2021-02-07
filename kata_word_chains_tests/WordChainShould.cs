using System;
using System.IO;
using System.Linq;
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
        [InlineData("ruby", "code", new string[] { "ruby", "rubs", "robs", "rods", "rode", "code" })]
        public void ShouldGenerateAWordChain_GivenStartingAndEndingWord(string startingWord, string endingWord, string[] expectedWordChain)
        {
            var wordChain = new WordChain(_dictionary, startingWord, endingWord);
            var actualWordChain = wordChain.Create(1);

            Assert.Equal(expectedWordChain.ToList().ToString(), actualWordChain.ToString());

        }

    }
}
