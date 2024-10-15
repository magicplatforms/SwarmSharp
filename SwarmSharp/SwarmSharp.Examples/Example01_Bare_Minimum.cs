using SwarmSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmSharp.BasicSamples
{
    internal class Example01_Bare_Minimum
    {
        private static Swarm client = new Swarm();
        internal static async Task RunAsync()
        {
            var agent = new Agent
            {
                Name = "Agent",
                Instructions = "You are a helpful agent.",
                Model = "gpt-3.5-turbo" // Replace with "gpt-4" if you have access
            };

            // Prepare the conversation with a user message
            var messages = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string> { { "role", "user" }, { "content", "Hi!" } }
                };

            // Run the conversation and get the response
            var response = await client.Run(agent, messages);

            // Print the assistant's reply
            Console.WriteLine(response.Messages[^1].Content);
        }
    }
}


