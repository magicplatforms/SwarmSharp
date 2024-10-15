// Program.cs
using SwarmSharp.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwarmApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new Swarm();


            var agent1 = new Agent
            {
                Name = "Agent",
                Instructions = "You are a helpful agent.",
                Model = "gpt-3.5-turbo" // Replace with "gpt-4" if you have access
            };

            Agent spanish_agent = new Agent
            {
                Name = "Spanish Agent",
                Instructions = "You only speak Spanish.",
                Model = "gpt-3.5-turbo" // Replace with "gpt-4" if you have access
            };

            Agent english_agent = new Agent
            {
                Name = "English Agent",
                Instructions = "You only speak English.",
                Model = "gpt-3.5-turbo" // Replace with "gpt-4" if you have access
            };

            var messages = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        { "role", "user" },
                        { "content", "Hola. ¿Como estás?" }
                    }
                };

            Func<Result> transferToSpanishAgent = () =>
            {
                // Transfer Spanish speaking users immediately.
                return new Result { Agent = spanish_agent };
            };

            english_agent.Functions2.Add(transferToSpanishAgent);

            var response = await client.Run(english_agent, messages);

            Console.WriteLine(response.Messages[^1].Content);
        }
    }
}