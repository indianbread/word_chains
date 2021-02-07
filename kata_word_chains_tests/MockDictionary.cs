using System;
using kata_word_chains;

namespace kata_word_chains_tests
{
    public class MockDictionary : IDictionary
    {
        public MockDictionary()
        {
        }

        public string GetValidWord(string startingWord, string endingWord, int indexOfLetterToReplace)
        {
            throw new NotImplementedException();
        }

        public bool WordIsValid(string word)
        {
            throw new NotImplementedException();
        }
    }
}
