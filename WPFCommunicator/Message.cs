using System;
using System.Collections.Generic;

namespace WPFCommunicator {
    internal class Message {

        public string ip;
        public List<String> messages = new List<string>();
        
        public Message(string ip) {
            this.ip = ip;
        }
    }
}
