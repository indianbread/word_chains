using System;
using System.Collections.Generic;

namespace kata_word_chains
{
    public interface IDictionary
    {

        bool WordIsValid(string word);

        IEnumerable<string> GetPossibleNextWords(string startingWord, string endingWord, int indexOfLetterToReplace);

    }
}
