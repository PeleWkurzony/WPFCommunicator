using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFCommunicator {
    public class Communicator {
       

        private string _ip = GetLocalIPAddress();
        private static int _PORT = 17000;
        private MainWindow _mainWindow;
        
        public Communicator() {
            _mainWindow = Application.Current.MainWindow as MainWindow;
            StartReceivingMessages();
        }

        public void SendMessage(string ip, string content) {
            try {
                var client = new UdpClient(ip, _PORT);
                var bytesToSend = Encoding.ASCII.GetBytes(content);
                client.Send(bytesToSend, bytesToSend.Length);
            }
            catch (SocketException e) {
                MessageBox.Show(_mainWindow, "Taki address ip jest nieosiągalny. Sprawdź poprawność addressu!", "Błąd!");
            }
        }

        private async Task StartReceivingMessages() {
            var client = new UdpClient(_PORT);
            var buffer = await client.ReceiveAsync();
            var receiveIp = buffer.RemoteEndPoint.Address.ToString();
            var message = Encoding.ASCII.GetString(buffer.Buffer);
            
            _mainWindow.AddMessage(receiveIp, message);
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

// TODO: Make sure everything works good, make better ui solve some ui problem