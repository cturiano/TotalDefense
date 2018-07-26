using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;

namespace TotalDefenseTestAppLucene
{
    internal class IndexManager : IDisposable
    {
        #region Fields

        private Analyzer _analyzer;
        private bool _disposed;
        private string _indexPath;
        private Directory _luceneIndexDirectory;
        private IndexWriter _writer;

        #endregion

        #region Constructors

        public IndexManager()
        {
            InitializeLucene();
        }

        ~IndexManager()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public void BuildIndex(string path)
        {
            var enumerable = SentenceReader.ReadAllRows(path);

            var sentences = enumerable?.ToList();
            if (sentences != null)
            {
                foreach (var sentence in sentences)
                {
                    var words = new List<string>(sentence.Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)).ConvertAll(s => s.ToLower()).ConvertAll(s => s.TrimEnd('.', ','));
                    foreach (var word in words)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            var doc = new Document();
                            doc.Add(new Field("number", sentence.SentenceNumber.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                            doc.Add(new Field("word", word.ToLower(), Field.Store.YES, Field.Index.ANALYZED));
                            _writer.AddDocument(doc);
                        }
                    }
                }

                _writer.Optimize();
                _writer.Flush(true, true, true);
                _writer.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<SearchResult> DumpAllResults()
        {
            var searcher = new IndexSearcher(_luceneIndexDirectory, true);
            var results = new HashSet<SearchResult>();

            var topDocs = searcher.Search(new MatchAllDocsQuery(), int.MaxValue);

            for (var i = 0; i < topDocs.ScoreDocs.Length; i++)
            {
                var doc = searcher.Doc(i);
                var word = doc.Get("word");

                if (results.Any(sr => sr.Word == word))
                {
                    results.First(sr => sr.Word == word).SentenceNumbers.Add(int.Parse(doc.Get("number")));
                }
                else
                {
                    var result = new SearchResult
                                 {
                                     Word = word
                                 };

                    result.SentenceNumbers.Add(int.Parse(doc.Get("number")));
                    results.Add(result);
                }
            }

            return results.OrderBy(x => x.Word).ToList();
        }

        #endregion

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _analyzer?.Dispose();
                _luceneIndexDirectory?.Dispose();
                _writer?.Dispose();
            }

            if (System.IO.Directory.Exists(_indexPath))
            {
                System.IO.Directory.Delete(_indexPath, true);
            }

            _disposed = true;
        }

        #endregion

        #region Private Methods

        private void InitializeLucene()
        {
            if (System.IO.Directory.Exists(_indexPath))
            {
                System.IO.Directory.Delete(_indexPath, true);
            }

            _analyzer = new StandardAnalyzer(Version.LUCENE_30);
            _indexPath = Path.Combine(Path.GetTempPath(), "TotalDefenseLuceneIndex");
            _luceneIndexDirectory = FSDirectory.Open(_indexPath);
            _writer = new IndexWriter(_luceneIndexDirectory, _analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        #endregion
    }
}