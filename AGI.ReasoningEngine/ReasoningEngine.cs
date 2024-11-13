
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
    }
}
