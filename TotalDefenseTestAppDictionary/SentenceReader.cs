using System;
using System.Collections.Generic;
using System.IO;

namespace TotalDefenseTestAppDictionary
{
    internal class SentenceReader
    {
        #region Public Methods

        public static SortedDictionary<string, List<int>> ReadAllRows(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(nameof(path));
            }

            var dict = new SortedDictionary<string, List<int>>();

            var sentences = new List<string>(File.ReadAllText(path).Split(new[] {".  ", "\r", "\n"}, StringSplitOptions.RemoveEmptyEntries)).ConvertAll(s => s.ToLower());

            for (var i = 0; i < sentences.Count; i++)
            {
                var words = new List<string>(sentences[i].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)).ConvertAll(s => s.TrimEnd('.', ',', ':', ';'));
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        if (dict.ContainsKey(word))
                        {
                            dict[word].Add(i + 1);
                        }
                        else
                        {
                            dict[word] = new List<int>(new []{i + 1});
                        }
                    }                                             
                }
            }

            return dict;
        }

        #endregion
    }
}