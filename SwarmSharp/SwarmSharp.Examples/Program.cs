// Copyright (c) Microsoft. All rights reserved.

//await Example07_Dynamic_GroupChat_Calculate_Fibonacci.RunAsync();

//using AutoGen.BasicSample;

//Define allSamples collection for all examples
using SwarmSharp.BasicSamples;

List<Tuple<string, Func<Task>>> allSamples = new List<Tuple<string, Func<Task>>>();

// When a new sample is created please add them to the allSamples collection
allSamples.Add(new Tuple<string, Func<Task>>("Bare Minimum", async () => { await Example01_Bare_Minimum.RunAsync(); }));
allSamples.Add(new Tuple<string, Func<Task>>("Agent Handoff", async () => { await Example02_Agent_Handoff.RunAsync(); }));
allSamples.Add(new Tuple<string, Func<Task>>("Context Variable", async () => { await Example03_Context_Variables.RunAsync(); }));

int idx = 1;
Dictionary<int, Tuple<string, Func<Task>>> map = new Dictionary<int, Tuple<string, Func<Task>>>();
Console.WriteLine("Available Examples:\n\n");
foreach (Tuple<string, Func<Task>> sample in allSamples)
{
    map.Add(idx, sample);
    Console.WriteLine("{0}. {1}", idx++, sample.Item1);
}

Console.WriteLine("\n\nEnter your selection:");

while (true)
{
    var input = Console.ReadLine();
    if (input == "exit")
    {
        break;
    }
    int val = Convert.ToInt32(input);
    if (!map.ContainsKey(val))
    {
        Console.WriteLine("Invalid choice");
    }
    else
    {
        Console.WriteLine("\nRunning {0}", map[val].Item1);
        await map[val].Item2.Invoke();
    }
}