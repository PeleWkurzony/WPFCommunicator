using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Interop;

namespace WPFCommunicator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        private Communicator _communicator;
        private Dictionary<string, Message> _messages = new Dictionary<string, Message>();
        private int? _lastSelectedItemIndex = null;

        public MainWindow() {
            InitializeComponent();
            _communicator = new Communicator();
        }

        public void AddMessage(string ip) {
            ipList_ui.Items.Add(ip);
            _messages.Add(ip, new Message(ip));
        }

        public void AddMessage(string ip, string content) {
            if (IpIsExisting(ip)) {
                AddMessagesToExisting(ip, content);
            }
            else {
                AddMessage(ip);
                AddMessagesToExisting(ip, content);
            }
            LoadMessages(_messages[ipList_ui.SelectedItem as String]);
        }

        private void AddMessagesToExisting(string ip, string content) {
            _messages[ip].messages.Add(content);
        }

        public bool IpIsExisting(string ip) {
            return _messages.Keys.FirstOrDefault(key => ip == key) != null;
        }

        private void Refresh_ui_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void LoadMessages(Message msg) {
            messages_ui.Items.Clear();
            foreach (var message in msg.messages) {
                messages_ui.Items.Add(message);
            }
        }

        private void IpList_ui_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            Message msg = _messages[ipList_ui.SelectedItem as String];
            LoadMessages(msg);
        }

        private void Add_ui_OnClick(object sender, RoutedEventArgs e) {
            var window = new ComputerAddWindow();
            window.Show();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e) {
            if (_lastSelectedItemIndex != null) {
                ipList_ui.Items.RemoveAt( (int) _lastSelectedItemIndex);
                _lastSelectedItemIndex = null;
            }
        }

        private void IpList_ui_OnContextMenuClosing(object sender, ContextMenuEventArgs e) {
            _lastSelectedItemIndex = null;
        }

        private void IpList_ui_OnContextMenuOpening(object sender, ContextMenuEventArgs e) {
            int temp = ipList_ui.SelectedIndex;
            if (temp == -1) {
                _lastSelectedItemIndex = null;
            }
            else {
                _lastSelectedItemIndex = temp;
            }
        }

        private void Send_ui_OnClick(object sender, RoutedEventArgs e) {
            var msg = messageToSend_ui.Text;
            var ip = ipList_ui.SelectedItem as String;
            if (ip != null && msg != "") {
                _communicator.SendMessage(ipList_ui.SelectedItem.ToString(), msg);
                AddMessage(ip, msg);
            }
        }
    }
}