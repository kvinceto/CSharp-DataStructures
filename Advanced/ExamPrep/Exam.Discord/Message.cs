using System.Collections.Generic;

namespace Exam.Discord
{
    using System;

    public class Message
    {
        public Message(string id, string content, int timestamp, string channel)
        {
            this.Id = id;
            this.Content = content;
            this.Timestamp = timestamp;
            this.Channel = channel;
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public int Timestamp { get; set; }

        public string Channel { get; set; }

        public List<string> Reactions { get; set; } = new List<string>();

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj == null) return false;

            return this.Id == ((Message)obj).Id;
        }
    }
}
