
using System;
using System.Collections.Generic;

namespace AGI.InferenceEngine
{
    public class CachedInferenceEngine
    {
        private Dictionary<string, bool> KnowledgeBase;
        private Dictionary<string, bool> Cache;

        public CachedInferenceEngine()
        {
            KnowledgeBase = new Dictionary<string, bool>();
            Cache = new Dictionary<string, bool>();
        }

        public void AddFact(string fact, bool value)
        {
            KnowledgeBase[fact] = value;
        }

        public bool Infer(string rule)
        {
            if (Cache.ContainsKey(rule))
            {
                Console.WriteLine("Cache hit for rule: " + rule);
                return Cache[rule];
            }

            // Basic inference logic as previously implemented
            string[] parts = rule.Split(' ');
            bool result;

            if (parts.Length == 3)
            {
                bool operand1 = KnowledgeBase.ContainsKey(parts[0]) && KnowledgeBase[parts[0]];
                bool operand2 = KnowledgeBase.ContainsKey(parts[2]) && KnowledgeBase[parts[2]];
                string op = parts[1];

                result = op switch
                {
                    "AND" => operand1 && operand2,
                    "OR" => operand1 || operand2,
                    _ => throw new ArgumentException("Unknown operator"),
                };
            }
            else if (parts.Length == 2 && parts[0] == "NOT")
            {
                result = !KnowledgeBase.ContainsKey(parts[1]) || !KnowledgeBase[parts[1]];
            }
            else
            {
                throw new ArgumentException("Invalid rule format");
            }

            // Store result in cache
            Cache[rule] = result;

            return result;
        }
    }
}
