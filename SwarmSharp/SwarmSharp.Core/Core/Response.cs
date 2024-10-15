// Response.cs

using System.Collections.Generic;

namespace SwarmSharp.Core
{
    public class Response
    {
        public List<Message> Messages { get; set; } = new List<Message>();
        public Agent Agent { get; set; } = null;
        public Dictionary<string, string> ContextVariables { get; set; } = new Dictionary<string, string>();
    }
}
