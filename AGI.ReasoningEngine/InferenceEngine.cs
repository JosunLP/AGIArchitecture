using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGI.ReasoningEngine
{
    public class InferenceEngine
    {
        public static bool Infer(string fact1, string fact2)
        {
            // Beispielhafte Inferenzlogik
            if (fact1.Contains("ist ein") && fact2.Contains("ist ein"))
            {
                return fact1.Split(' ')[2] == fact2.Split(' ')[2];
            }
            return false;
        }
    }
}
