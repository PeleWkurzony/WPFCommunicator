using System;
using System.Collections.Generic;

namespace WPFCommunicator {
    
    internal enum MessageType {
        sent, 
        received
    }

    internal class Message {
        public string content;
        public MessageType type;
            
        public Message() {}
    }
    
    internal class MessageStorage {

        public string ip;
        public List<Message> messages = new List<Message>();
        
        public MessageStorage(string ip) {
            this.ip = ip;
        }
    }
}
