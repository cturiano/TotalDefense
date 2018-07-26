using System;
using System.Collections.Generic;
using TotalDefenseTestAppShared;

namespace TotalDefenseTestAppLucene
{
    public class LuceneRunner : IRunner
    {
        #region Public Methods

        public void Run(string pathToFile)
        {
            var indexManager = new IndexManager();
            indexManager.BuildIndex(pathToFile);
            OutputResults(indexManager.DumpAllResults());
        }

        #endregion

        #region Private Methods

        private static void OutputResults(IEnumerable<SearchResult> results)
        {            
            Console.WriteLine("Using Lucene method.");
            var i = 0;
            foreach (var result in results)
            {
                Console.WriteLine($"{LetterCounter.GetLetterCounter(i)}. " + result);
                i++;
            }
        }

        #endregion
    }
}