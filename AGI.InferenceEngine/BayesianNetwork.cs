
using System;
using System.Collections.Generic;

namespace AGI.InferenceEngine
{
    public class BayesianNetwork
    {
        private Dictionary<string, double> Probabilities;

        public BayesianNetwork()
        {
            Probabilities = new Dictionary<string, double>();
        }

        public void AddProbability(string eventName, double probability)
        {
            if (probability < 0 || probability > 1)
                throw new ArgumentException("Probability must be between 0 and 1.");

            Probabilities[eventName] = probability;
        }

        public double Infer(string eventName)
        {
            if (Probabilities.ContainsKey(eventName))
                return Probabilities[eventName];

            throw new ArgumentException("Event not found in Bayesian Network.");
        }

        // Method to calculate conditional probability (for simplicity)
        public double InferConditional(string givenEvent, string targetEvent)
        {
            if (Probabilities.ContainsKey(givenEvent) && Probabilities.ContainsKey(targetEvent))
            {
                // Simplified calculation, for demonstration purposes.
                return Probabilities[givenEvent] * Probabilities[targetEvent];
            }

            throw new ArgumentException("Events not found in Bayesian Network.");
        }
    }
}
