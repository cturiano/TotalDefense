using System.Collections.Generic;
using System.Linq;

namespace TotalDefenseTestAppLucene
{
    internal class SearchResult
    {
        #region Constructors

        public SearchResult()
        {
            SentenceNumbers = new List<int>();
        }

        #endregion

        #region Properties

        public int Count => SentenceNumbers.Count;

        public List<int> SentenceNumbers { get; set; }

        public string Word { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            var s = $"{Word} {{{Count}:";
            if (SentenceNumbers != null)
            {
                s = SentenceNumbers.Aggregate(s, (current, num) => current + $"{num},");
            }

            return s.TrimEnd(',') + '}';
        }

        #endregion
    }
}