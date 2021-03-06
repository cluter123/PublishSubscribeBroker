﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Broker
{
    public class Topic
    {
        public string Name { get; }
        public int ID { get; }
        private readonly List<Subscriber> _subscriberList;
        
        public Topic(int ownerID, string topicName)
        {
            Name = topicName;
            ID = ownerID;
            _subscriberList = new List<Subscriber>();
        }

        public void AddSubscriber(Subscriber sub)
        {
            _subscriberList.Add(sub);
        }

        public void RemoveSubscriber(Subscriber sub)
        {
            _subscriberList.Remove(sub);
        }

        public bool ContainsSubscriber(Subscriber sub)
        {
            return _subscriberList.IndexOf(sub) > -1;
        }

        public bool SendMessageToSubscribers(string messageContent)
        {
            try
            {
                foreach (var subscriber in _subscriberList)
                    subscriber.SendMessage($"[{Name}]: {messageContent}");

                return true;
            }
            catch (SocketException exception)
            {
                Console.WriteLine($"[ERR] {exception.Message}");
                return false;
            }
        }
    }
}
