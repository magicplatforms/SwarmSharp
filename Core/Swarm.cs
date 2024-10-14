// Swarm.cs
using OpenAI_API;
using OpenAI_API.Chat;

namespace SwarmSharp.Core
{
    public class Swarm
    {
        private OpenAIAPI _client;

        public Swarm(OpenAIAPI client = null)
        {
            // Replace "your-api-key" with your actual OpenAI API key
            _client = client ?? new OpenAIAPI(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
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
            string instructions;
            if (agent.InstructionsDelegate != null)
            {
                instructions = agent.InstructionsDelegate(contextVars);
            }
            else
            {
                instructions = agent.Instructions;
            }

            history.Add(new ChatMessage(ChatMessageRole.System, instructions));

            // Add previous messages
            foreach (var msgDict in messages)
            {
                var role = msgDict["role"] == "user" ? ChatMessageRole.User : ChatMessageRole.Assistant;
                var content = msgDict["content"];
                history.Add(new ChatMessage(role, content));
            }

            // Check and execute agent functions
            foreach (var function in agent.Functions)
            {
                var result = function();
                if (result.Agent != null)
                {
                    // Transfer to the new agent
                    return await Run(result.Agent, messages, contextVariables, modelOverride, stream, debug, maxTurns, executeTools);
                }
            }

            var chatRequest = new ChatRequest
            {
                Messages = history,
                Model = modelOverride ?? agent.Model
            };

            var chatResult = await _client.Chat.CreateChatCompletionAsync(chatRequest);

            var responseMessage = chatResult.Choices[0].Message;

            var response = new Response
            {
                Messages = new List<Message>
            {
                new Message
                {
                    Role = responseMessage.Role.ToString().ToLower(),
                    Content = responseMessage.Content
                }
            },
                Agent = agent,
                ContextVariables = contextVars
            };

            return response;
        }

    }
}

