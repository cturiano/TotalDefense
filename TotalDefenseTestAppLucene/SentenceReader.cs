using System;
using System.Collections.Generic;
using System.IO;

namespace TotalDefenseTestAppLucene
{
    internal class SentenceReader
    {
        #region Public Methods

        public static IEnumerable<Sentence> ReadAllRows(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(nameof(path));
            }

            var sentences = File.ReadAllText(path).Split(new[] {".  ", "\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < sentences.Length; i++)
            {
                yield return new Sentence
                             {
                                 SentenceNumber = i + 1,
                                 Text = sentences[i]
                             };
            }
        }

        #endregion
    }
}