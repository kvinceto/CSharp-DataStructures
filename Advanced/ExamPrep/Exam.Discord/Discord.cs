using System;
using System.Collections.Generic;

namespace Exam.Discord
{
    using System.Linq;

    public class Discord : IDiscord
    {
        private Dictionary<string, Message> messagesById;
        private Dictionary<string, List<Message>> messagesByChannal;

        public Discord()
        {
            messagesById = new Dictionary<string, Message>();
            messagesByChannal = new Dictionary<string, List<Message>>();
        }

        public int Count => messagesById.Count;

        public bool Contains(Message message)
        {
            if (message == null)
            {
                return false;
            }

            if (messagesById.ContainsKey(message.Id))
            {
                return true;
            }

            return false;
        }

        public void DeleteMessage(string messageId)
        {
            if (!messagesById.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }
            Message m = messagesById[messageId];
            messagesByChannal[m.Channel].Remove(m);
            messagesById.Remove(messageId);
        }

        public IEnumerable<Message> GetAllMessagesOrderedByCountOfReactionsThenByTimestampThenByLengthOfContent()
        {
            return messagesById.Values
                .OrderByDescending(m => m.Reactions.Count)
                .ThenBy(m => m.Timestamp)
                .ThenBy(m => m.Content.Length);
        }

        public IEnumerable<Message> GetChannelMessages(string channel)
        {
            var chanelMessages = messagesByChannal[channel];
            if (chanelMessages.Count == 0)
            {
                throw new ArgumentException();
            }

            return chanelMessages;
        }

        public Message GetMessage(string messageId)
        {
            if (!messagesById.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }

            return messagesById[messageId];
        }

        public IEnumerable<Message> GetMessageInTimeRange(int lowerBound, int upperBound)
        {
            return messagesById.Values
                .Where(m => m.Timestamp >= lowerBound && m.Timestamp <= upperBound)
                .OrderByDescending(m => messagesByChannal[m.Channel].Count);
        }

        public IEnumerable<Message> GetMessagesByReactions(List<string> reactions)
        { 
            return messagesById.Values
                .Where(m => reactions.All(r => reactions.Contains(r)))
                .OrderByDescending(m => m.Reactions.Count)
                .ThenBy(m => m.Timestamp);
        }

        public IEnumerable<Message> GetTop3MostReactedMessages()
        {
            return messagesById.Values
                .OrderByDescending(m => m.Reactions.Count)
                .Take(3);
        }

        public void ReactToMessage(string messageId, string reaction)
        {
            if (!messagesById.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }

            messagesById[messageId].Reactions.Add(reaction);
        }

        public void SendMessage(Message message)
        {
            if (!messagesById.ContainsKey(message.Id))
            {
                messagesById.Add(message.Id, message);
                if (!messagesByChannal.ContainsKey(message.Channel))
                {
                    messagesByChannal.Add(message.Channel, new List<Message>());
                }

                messagesByChannal[message.Channel].Add(message);
            }
        }
    }
}
