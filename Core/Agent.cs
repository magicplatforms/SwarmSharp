// Agent.cs
using System;
using System.Collections.Generic;

namespace SwarmSharp.Core
{
    public delegate Result AgentFunction(List<Dictionary<string, string>> messages, Dictionary<string, string> contextVariables);


    public class Agent
    {
        public string Name { get; set; } = "Agent";
        public string Model { get; set; } = "gpt-4";
        public string Instructions { get; set; } = "You are a helpful agent.";
        public Func<Dictionary<string, string>, string> InstructionsDelegate { get; set; } = null;
        public List<Func<Result>> Functions { get; set; } = new List<Func<Result>>();
        public string ToolChoice { get; set; } = null;
        public bool ParallelToolCalls { get; set; } = true;

        public Agent(string name = "Agent", string instructions = "You are a helpful agent.")
        {
            Name = name;
            Instructions = instructions;
        }
    }
}
