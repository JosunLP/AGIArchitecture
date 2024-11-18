
using System;
using System.Collections.Generic;
using System.Linq;

namespace AGI.ReasoningEngine
{
    public class ReasoningEngine
    {
        private readonly Dictionary<string, List<string>> _knowledgeBase;

        public ReasoningEngine()
        {
            _knowledgeBase = [];
        }

        // Add a relation to the knowledge base
        public void AddRelation(string subject, string relation, string obj)
        {
            var key = $"{subject}-{relation}";
            if (!_knowledgeBase.ContainsKey(key))
            {
                _knowledgeBase[key] = [];
            }
            _knowledgeBase[key].Add(obj);
        }

        // Query the knowledge base for relations
        public List<string> QueryRelation(string subject, string relation)
        {
            var key = $"{subject}-{relation}";
            return _knowledgeBase.ContainsKey(key) ? _knowledgeBase[key] : new List<string>();
        }

        // Infer simple relationships
        public bool InferRelation(string subject, string relation, string obj)
        {
            var key = $"{subject}-{relation}";
            return _knowledgeBase.ContainsKey(key) && _knowledgeBase[key].Contains(obj);
        }


        public int AnalyzeGoalOutcome(string goalDescription)
        {
            // Simple heuristic: if the goal description contains certain keywords, return a higher probability
            var positiveKeywords = new List<string> { "achieve", "success", "complete" };
            var negativeKeywords = new List<string> { "fail", "impossible", "difficult" };

            float score = 0.5f; // Start with a neutral score

            foreach (var keyword in positiveKeywords)
            {
                if (goalDescription.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    score += 0.1f;
                }
            }

            foreach (var keyword in negativeKeywords)
            {
                if (goalDescription.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    score -= 0.1f;
                }
            }

            // Ensure the score is within the range [0, 1]
            return (int)Math.Clamp(score, 0.0f, 1.0f);
        }

        // Remove a relation from the knowledge base
        public void RemoveRelation(string subject, string relation, string obj)
        {
            var key = $"{subject}-{relation}";
            if (_knowledgeBase.ContainsKey(key))
            {
                _knowledgeBase[key].Remove(obj);
                if (_knowledgeBase[key].Count == 0)
                {
                    _knowledgeBase.Remove(key);
                }
            }
        }
    }
}
