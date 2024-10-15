using SwarmSharp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmSharp.BasicSamples
{
    internal class Example03_Context_Variables
    {
        private static Swarm client = new Swarm();
        internal static async Task RunAsync()
        {
            // Define the Instructions function
            string Instructions(Dictionary<string, string> contextVariables)
            {
                string name = contextVariables.ContainsKey("name") ? contextVariables["name"] : "User";
                return $"You are a helpful agent. Greet the user by name ({name}).";
            }

            // Define the PrintAccountDetails function
            Result PrintAccountDetails(Dictionary<string, string> contextVariables)
            {
                string userId = contextVariables.ContainsKey("user_id") ? contextVariables["user_id"] : null;
                string name = contextVariables.ContainsKey("name") ? contextVariables["name"] : null;
                Console.WriteLine($"Account Details: {name} {userId}");
                return new Result();
            }

  

            // Create the agent with instructions and functions
            var agent = new Agent
            {
                Name = "Agent",
                InstructionsDelegate = Instructions,
                Functions = new List<Func<Dictionary<string, string>, Result>>
                    {
                        PrintAccountDetails
                    }
            };

            // Define the context variables
            var contextVariables = new Dictionary<string, string>
                {
                    { "name", "James" },
                    { "user_id", "123" }
                };

            var messages_item = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "role", "user" },
                        { "content", "Hi" }
                    }
                };
            // First run with "Hi!"
            var response = await client.Run(
                agent: agent,
                messages: messages_item,
                contextVariables: contextVariables
            );
            Console.WriteLine(response.Messages[^1].Content);

            // Second run with "Print my account details!"
            /*
            response = await client.Run(
                agent: agent,
                messages: new List<Message>
                {
                        new Message ( Role = "user", Content = "Print my account details!" }
                },
                contextVariables: contextVariables
            );
            Console.WriteLine(response.Messages[^1].Content);
            */
        }
    }

    public class FunctionResult
    {
        /// <summary>
        /// An optional new Agent instance to switch to after the function execution.
        /// If set, the Swarm will transfer control to this new agent.
        /// </summary>
        public Agent Agent { get; set; }

        /// <summary>
        /// Indicates whether the function execution was successful.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Optional message or data returned by the function.
        /// </summary>
        public string Message { get; set; }
    }

}