using System;
using System.IO;
using System.Linq;

namespace kata_word_chains
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your starting word");
            var startingWord = Console.ReadLine();
            Console.WriteLine("Enter your ending word");
            var endingWord = Console.ReadLine();
            Console.WriteLine("Enter the number of attempts");
            var attempts = Int32.Parse(Console.ReadLine());
            var dictionary = new Dictionary($"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/word chains kata/kata_word_chains/kata_word_chains/data/words_alpha.txt");
            var wordchain = new WordChain(dictionary, startingWord, endingWord);
            try
            {
                var chain = wordchain.Create(attempts);
                Console.WriteLine(wordchain.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}
