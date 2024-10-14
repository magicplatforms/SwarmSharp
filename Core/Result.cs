// Result.cs

using System.Collections.Generic;

namespace SwarmSharp.Core
{
    public class Result
    {
        public string Value { get; set; } = "";
        public Agent Agent { get; set; } = null;
        public Dictionary<string, string> ContextVariables { get; set; } = new Dictionary<string, string>();
    }
}
