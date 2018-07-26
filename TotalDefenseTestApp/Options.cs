using System.ComponentModel;
using CommandLine;

namespace TotalDefenseTestApp
{
    internal class Options
    {
        #region Properties

        [Option('f', "file", Required = true, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }
                                              

        [Option('t', "type", Required = false, HelpText = "Type of processing to use. 1 = Lucene, 0 = Dictionary (default)")]
        [DefaultValue(0)]
        public int ProcessingType { get; set; }

        #endregion
    }
}