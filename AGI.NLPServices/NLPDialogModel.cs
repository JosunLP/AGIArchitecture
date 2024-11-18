
using System;
using System.Collections.Generic;

namespace AGI.NLPServices
{
    public class NLPDialogModel
    {
        private Dictionary<string, string> DialogDatabase;

        public NLPDialogModel()
        {
            DialogDatabase = new Dictionary<string, string>
            {
                { "Hello", "Hi! How can I assist you today?" },
                { "What is your name?", "I am AGI, your assistant." },
                { "How are you?", "I am just a program, but I'm ready to assist you!" },
                { "Tell me a joke", "Why did the computer go to the doctor? Because it had a virus!" }
            };
        }

        // Basic method to respond to a user input
        public string GetResponse(string userInput)
        {
            if (DialogDatabase.ContainsKey(userInput))
            {
                return DialogDatabase[userInput];
            }
            else
            {
                return "I'm not sure how to respond to that. Could you please provide more details?";
            }
        }

        // Method to extend the dialog knowledge base
        public void AddDialogEntry(string userInput, string response)
        {
            if (!DialogDatabase.ContainsKey(userInput))
            {
                DialogDatabase[userInput] = response;
                Console.WriteLine($"Added new dialog entry: \"{userInput}\" -> \"{response}\"");
            }
            else
            {
                Console.WriteLine($"Entry for \"{userInput}\" already exists.");
            }
        }
    }
}
