using System;
using System.Net;
using System.Net.Sockets;

namespace WPFComunicator {
    
    public class Communicator {

        private string _ip = GetLocalIPAddress();
        private static string _PORT = "17000";
        
        public Communicator() {
            
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}