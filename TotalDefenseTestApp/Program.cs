using System;
using System.IO;
using System.Reflection;
using CommandLine;

namespace TotalDefenseTestApp
{
    internal class Program
    {
        #region Private Methods

        private static string FixPath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }

            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            return Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path)), path);
        }

        private static string GetLetterCounter(int i)
        {
            return new string((char)(i % 26 + 97), i / 26 + 1);
        }

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Run);
            Console.Read();
        }

        private static void Run(Options opts)
        {
            var path = FixPath(opts.InputFile);
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return;
            }

            var runner = RunnerFactory.GetRunner(opts.ProcessingType);
            runner.Run(opts.InputFile);
        }

        #endregion
    }
}