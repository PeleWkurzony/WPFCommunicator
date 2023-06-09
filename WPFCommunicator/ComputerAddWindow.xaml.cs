﻿using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFCommunicator {
    public partial class ComputerAddWindow : Window {

        private bool _isAddedInformation = false;
        public ComputerAddWindow() {
            InitializeComponent();
        }

        private bool ValidateIp(string ip) {
            if (ip == "") return false;
            if (!IPAddress.TryParse(ip, out IPAddress adr)) return false;
            return true;
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            var window = Application.Current.MainWindow as MainWindow;
            string ip = ip_ui.Text;
            if (ValidateIp(ip)) {

                if (window.IpIsExisting(ip)) {
                    Close();
                    return;
                }

                window.AddMessage(ip);
                window.ipList_ui.SelectedItem = window.ipList_ui.Items[window.ipList_ui.Items.Count - 1];
            }
            else {
                if (!_isAddedInformation) {
                    addComputer_ui.Height = 140;
                    var text = new TextBlock();
                    text.Text = "Nieprawidłowy adress ip!";
                    text.Foreground = Brushes.Red;
                    text.FontStyle = FontStyles.Oblique;
                    stackPanel_ui.Children.Add(text);
                    _isAddedInformation = true;
                }

                return;
            }
            

            Close();
        }
        
    }
}