using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Interop;

namespace WPFCommunicator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        private Communicator _communicator = new Communicator();
        private Dictionary<string, Message> _messages = new Dictionary<string, Message>();
        private int? _lastSelectedItemIndex = null;

        public MainWindow() {
            InitializeComponent();
        }

        public void AddMessages(string ip) {
            _messages.Add(ip, new Message(ip));
        }

        private void Refresh_ui_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void LoadMessages(ref Message msg) {
            messages_ui.Items.Clear();
            foreach (var message in msg.messages) {
                messages_ui.Items.Add(message);
            }
        }

        private void IpList_ui_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            Message msg = _messages[ipList_ui.SelectedItem as String];
            LoadMessages(ref msg);
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
    }
}