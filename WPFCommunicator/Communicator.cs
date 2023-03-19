using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace WPFCommunicator {
    public class Communicator {
       

        private string _ip = GetLocalIPAddress();
        private static int _PORT = 17000;
        private UdpClient _client = new UdpClient(_PORT);
        
        public Communicator() {
            // TODO: Do 
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