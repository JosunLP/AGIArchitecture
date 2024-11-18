
using System;
using System.Collections.Generic;

namespace AGI.InferenceEngine
{
    public class InferenceEngine
    {
        private Dictionary<string, bool> KnowledgeBase;

        public InferenceEngine()
        {
            KnowledgeBase = new Dictionary<string, bool>();
        }

        public void AddFact(string fact, bool value)
        {
            KnowledgeBase[fact] = value;
        }

        // Infer method: Extended with additional logical operators (AND, OR, NOT)
        public bool Infer(string rule)
        {
            // For simplicity, assuming rule is in form: "A AND B", "A OR B", "NOT A"
            string[] parts = rule.Split(' ');
            if (parts.Length == 3)
            {
                bool operand1 = KnowledgeBase.ContainsKey(parts[0]) && KnowledgeBase[parts[0]];
                bool operand2 = KnowledgeBase.ContainsKey(parts[2]) && KnowledgeBase[parts[2]];
                string op = parts[1];

                switch (op)
                {
                    case "AND":
                        return operand1 && operand2;
                    case "OR":
                        return operand1 || operand2;
                    default:
                        throw new ArgumentException("Unknown operator");
                }
            }
            else if (parts.Length == 2 && parts[0] == "NOT")
            {
                return !KnowledgeBase.ContainsKey(parts[1]) || !KnowledgeBase[parts[1]];
            }
            else
            {
                throw new ArgumentException("Invalid rule format");
            }
        }
    }
}
