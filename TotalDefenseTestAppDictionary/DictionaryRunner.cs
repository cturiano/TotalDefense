using System;
using System.Collections.Generic;
using TotalDefenseTestAppShared;

namespace TotalDefenseTestAppDictionary
{
    public class DictionaryRunner : IRunner
    {
        #region Public Methods

        public void Run(string path)
        {
            OutputResults(SentenceReader.ReadAllRows(path));
        }

        #endregion

        #region Private Methods

        private static void OutputResults(SortedDictionary<string, List<int>> results)
        {
            Console.WriteLine("Using dictionary method.");
            var i = 0;
            foreach (var result in results)
            {
                Console.WriteLine($"{LetterCounter.GetLetterCounter(i)}. {result.Key} {{{result.Value.Count}:{string.Join(",", result.Value)}}}");
                i++;
            }
        }

        #endregion
    }
}