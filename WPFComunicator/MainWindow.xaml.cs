using System.Windows;
using System.Windows.Controls;

namespace WPFComunicator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        
        private Communicator _communicator = new Communicator();
        public MainWindow() {
            InitializeComponent();
        }

        private void Refresh_ui_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void IpList_ui_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            
        }

        private void Add_ui_OnClick(object sender, RoutedEventArgs e) {
            var window = new ComputerAddWindow();
            window.Show();
        }

        public static void RemoveItem_Click(object sender, RoutedEventArgs e) {
            
        }
    }
}