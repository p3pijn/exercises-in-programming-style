using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TermFrequency
{
    class Program
    {
        static void Main(string[] args)
        {
            calculateTermFrequencies(args[0], "../stop_words.txt", 25);
        }

        static void calculateTermFrequencies(String SourcePath, String StopwordsPath, int Number)
        {
            Console.Write(
                String.Join(Environment.NewLine,
                (
                    from _line in File.ReadLines(SourcePath)
                    from _match in new Regex("[\\w]{2,}", RegexOptions.Compiled).Matches(_line).Cast<Match>()
                    let _stopwords = File.ReadAllText(StopwordsPath).Split(',')
                    let _word = _match.Value.ToLower()
                    where !_stopwords.Contains(_word)
                    group _word by _word into _words
                    let _count = _words.Count()
                    orderby _count descending
                    select String.Format("{0} - {1}", _words.Key, _count)
                ).Take(Number).ToArray())
            );
        }
    }
}
