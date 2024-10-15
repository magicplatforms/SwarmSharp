
// Message.cs
namespace SwarmSharp.Core
{
    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public FunctionCall FunctionCall { get; set; }
    }

    public class FunctionCall
    {
        public string Name { get; set; }
        public string Arguments { get; set; }
    }
}
