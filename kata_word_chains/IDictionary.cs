using System;
namespace kata_word_chains
{
    public interface IDictionary
    {

        bool WordIsValid(string word);

        string GetValidWord(string startingWord, string endingWord, int indexOfLetterToReplace);

    }
}
