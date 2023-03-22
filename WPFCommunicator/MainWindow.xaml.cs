using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Interop;
using System.Windows.Media;

namespace WPFCommunicator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        private Communicator _communicator;
        private Dictionary<string, MessageStorage> _messages = new Dictionary<string, MessageStorage>();
        private int? _lastSelectedItemIndex = null;

        public MainWindow() {
            InitializeComponent();
            _communicator = new Communicator();
        }

        public void AddMessage(string ip) {
            ipList_ui.Items.Add(ip);
            _messages.Add(ip, new MessageStorage(ip));
        }

        public void AddMessage(string ip, string content, bool isSent) {
            if (IpIsExisting(ip)) {
                AddMessagesToExisting(ip, content, isSent);
            }
            else {
                AddMessage(ip);
                AddMessagesToExisting(ip, content, isSent);
                ipList_ui.SelectedItem = ipList_ui.Items[ipList_ui.Items.Count - 1];
            }
            LoadMessages(_messages[ipList_ui.SelectedItem as String]);
        }

        private void AddMessagesToExisting(string ip, string content, bool isSent) {
            var msg = new Message();
            msg.content = content;
            if (isSent) {
                msg.type = MessageType.sent;
            }
            else {
                msg.type = MessageType.received;
            }
            
            _messages[ip].messages.Add(msg);
        }

        public bool IpIsExisting(string ip) {
            return _messages.Keys.FirstOrDefault(key => ip == key) != null;
        }

        private void Refresh_ui_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void LoadMessages(MessageStorage msg) {
            messages_ui.Items.Clear();
            foreach (var message in msg.messages) {
                var item = new ListViewItem();
                if (message.type == MessageType.sent) {
                    item.HorizontalContentAlignment = HorizontalAlignment.Right;
                    item.Background = Brushes.Lavender;
                }

                item.Content = message.content;
                messages_ui.Items.Add(item);
            }
        }

        private void IpList_ui_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            var item = ipList_ui.SelectedItem;
            if (item == null) {
                messages_ui.Items.Clear();
                return;
            }
            MessageStorage msg = _messages[item as String];
            LoadMessages(msg);
            if (messages_ui.Items.Count > 0) {
                messages_ui.ScrollIntoView(
                    messages_ui.Items[messages_ui.Items.Count - 1]
                );
            }
        }

        private void Add_ui_OnClick(object sender, RoutedEventArgs e) {
            var window = new ComputerAddWindow();
            window.Show();
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e) {
            if (_lastSelectedItemIndex != null) {
                var index = _lastSelectedItemIndex ?? -1;
                var ip = ipList_ui.Items[index].ToString();
                _messages.Remove(ip);
                ipList_ui.Items.RemoveAt( (int) _lastSelectedItemIndex);
                _lastSelectedItemIndex = null;
            }
        }

        private void IpList_ui_OnContextMenuClosing(object sender, ContextMenuEventArgs e) {
            _lastSelectedItemIndex = null;
        }

        private void IpList_ui_OnContextMenuOpening(object sender, ContextMenuEventArgs e) {
            var temp = ipList_ui.SelectedIndex;
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
                
            }

            messageToSend_ui.Text = "";
        }
    }
}