# SwarmSharp: A C# Implementation of OpenAI's Swarm

SwarmSharp is a basic toolkit for .NET developers diving into the world of multi-agent systems. By leveraging lightweight, scalable, and highly customizable patterns, SwarmSharp empowers developers to orchestrate complex, distributed AI behaviors with ease and efficiency.

- C# Implementation: Leverages the power and flexibility of C# and the .NET ecosystem.
- Educational Focus: Designed as a learning tool for developers interested in multi-agent systems within the .NET framework.
- Customizable Architecture: Allows for easy modification and extension to suit specific project needs.

Based off of the OpenAI Swarm Project located here: https://github.com/openai/swarm

# Usage

```CSharp

    Swarm client = new Swarm();

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
        return new Result { Agent = spanish_agent };
    };

    english_agent.Functions2.Add(transferToSpanishAgent);

    var response = await client.Run(english_agent, messages);

    Console.WriteLine(response.Messages[^1].Content);
```

# Examples

This SwarmSharp.Example Project contains basic examples demonstrating core SwarmSharp capabilities. These examples show the simplest implementations of SwarmSharp, with one input message, and a corresponding output. 

| Sample | Description |
|----------|----------|
| Bare Minimum   | Demonstrates how to transfer a conversation from one agent to another. Usage: Transfers Spanish-speaking users from an English agent to a Spanish agent. |
| Agent Handoff   | A bare minimum example showing the basic setup of an agent. Usage: Sets up an agent that responds to a simple user message.   |
| Context Variable    | Shows how to use context variables within an agent. Usage: Uses context variables to greet a user by name and print account details.  |



