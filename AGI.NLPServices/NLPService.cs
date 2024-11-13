
using System;
using System.Collections.Generic;
using System.Linq;

namespace AGI.NLPServices
{
    public class NLPService
    {
        // Tokenize a given text into individual words
        public List<string> Tokenize(string text)
        {
            // Basic tokenization by splitting on whitespace
            return text.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        // Count word frequencies in a given text
        public Dictionary<string, int> GetWordFrequencies(string text)
        {
            var tokens = Tokenize(text);
            return tokens.GroupBy(word => word.ToLower())
                         .ToDictionary(group => group.Key, group => group.Count());
        }
    }
}
