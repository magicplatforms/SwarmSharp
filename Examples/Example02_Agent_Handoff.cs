using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwarmSharp.Core;


namespace SwarmSharp.BasicSamples
{

    /// <summary>
    /// This example shows the basic usage of <see cref="ConversableAgent"/> class.
    /// </summary>
    public static class Example02_Agent_Handoff
    {
        private static Swarm client = new Swarm();

        internal static async Task RunAsync()
        {

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


