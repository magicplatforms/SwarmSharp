// Swarm.cs
using OpenAI;
using OpenAI.Chat;



namespace SwarmSharp.Core
{
    public class Swarm
    {
        private ChatClient _client;

        public Swarm(ChatClient client = null)
        {
            _client = client ?? new ChatClient(model: "gpt-4", apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        }

        public async Task<Response> Run(
          Agent agent,
          List<Dictionary<string, string>> messages,
          Dictionary<string, string> contextVariables = null,
          string modelOverride = null,
          bool stream = false,
          bool debug = false,
          int maxTurns = int.MaxValue,
          bool executeTools = true)
        {
            var contextVars = contextVariables ?? new Dictionary<string, string>();
            var history = new List<ChatMessage>();

            // Add system message with instructions
            string instructions = agent.InstructionsDelegate != null
                ? agent.InstructionsDelegate(contextVars)
                : agent.Instructions;

            history.Add(new SystemChatMessage(instructions));

            // Add previous messages
            foreach (var msgDict in messages)
            {
                var role = msgDict["role"];
                var content = msgDict["content"];
                if (role == "user")
                    history.Add(new UserChatMessage(content));
                else
                    history.Add(new AssistantChatMessage(content));
            }

            // Check and execute agent functions
            foreach (var function in agent.Functions2)
            {
                var result = function();
                if (result.Agent != null)
                {
                    // Transfer to the new agent
                    return await Run(result.Agent, messages, contextVariables, modelOverride, stream, debug, maxTurns, executeTools);
                }
            }

            foreach (var function in agent.Functions)
            {
                var result = function(contextVariables);
                if (result.Agent != null)
                {
                    // Transfer to the new agent
                    return await Run(result.Agent, messages, contextVariables, modelOverride, stream, debug, maxTurns, executeTools);
                }
            }

            // Update the client model if a model override is provided
            if (modelOverride != null)
            {
                _client = new ChatClient(model: modelOverride, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
            }

            // Create the chat completion
            ChatCompletion completion;
            if (stream)
            {
                // Handle streaming response
                completion = await _client.CompleteChatAsync(history); // Modify as per streaming requirements
            }
            else
            {
                completion = await _client.CompleteChatAsync(history);
            }

            var responseMessage = completion.Content[0].Text;

            var response = new Response
            {
                Messages = new List<Message>
        {
            new Message
            {
                Role = "assistant",
                Content = responseMessage
            }
        },
                Agent = agent,
                ContextVariables = contextVars
            };

            return response;
        }

        public async Task Run(Agent agent, object value, List<Dictionary<string, string>> messages, Dictionary<string, string> contextVariables)
        {
            throw new NotImplementedException();
        }
    }
}
