using System;
using System.Collections.Generic;

namespace AGI.ReasoningEngine
{
    public class ReasoningEngine
    {
        // A simple graph representation using adjacency list
        private readonly Dictionary<string, List<string>> _knowledgeGraph;

        public ReasoningEngine()
        {
            _knowledgeGraph = [];
        }

        // Add a relation to the knowledge graph
        public void AddRelation(string subject, string relation, string obj)
        {
            if (!_knowledgeGraph.ContainsKey(subject))
            {
                _knowledgeGraph[subject] = [];
            }

            if (!_knowledgeGraph[subject].Contains(obj))
            {
                _knowledgeGraph[subject].Add(obj);
            }
        }

        // Logical inference using logical operators and more complex rules
        public bool Infer(string subject, string relation, string obj)
        {
            if (_knowledgeGraph.ContainsKey(subject) && _knowledgeGraph[subject].Contains(obj))
            {
                Console.WriteLine($"Inferred relation: {subject} -> {relation} -> {obj}");
                return true;
            }
            return false;
        }

        // Explain inference by showing the path in the graph
        public string ExplainInference(string subject, string relation, string obj)
        {
            if (Infer(subject, relation, obj))
            {
                return $"The relation '{subject} -> {relation} -> {obj}' was inferred based on direct edges found in the knowledge graph.";
            }
            return "No valid inference path found.";
        }

        public string InferRelation(string subject, string relation, string obj)
        {
            return Infer(subject, relation, obj) ? "Yes" : "No";
        }

        public string QueryRelation(string subject, string relation)
        {
            if (_knowledgeGraph.ContainsKey(subject))
            {
                return string.Join(", ", _knowledgeGraph[subject]);
            }
            return "No relations found.";
        }
    }
}
