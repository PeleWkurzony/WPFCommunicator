using System.Windows;
using System.Windows.Controls;

namespace WPFCommunicator {
    public partial class ComputerAddWindow : Window {
        public ComputerAddWindow() {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            var window = Application.Current.MainWindow as MainWindow;
            window.ipList_ui.Items.Add(ip_ui.Text);

            Close();
        }
    }
}