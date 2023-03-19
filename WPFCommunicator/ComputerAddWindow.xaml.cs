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
            /// TODO: Make context menu for a ListViewItem
            ListViewItem item = null;
            if ((item = window.ipList_ui.Items[window.ipList_ui.Items.Count - 1] as ListViewItem) != null) {
                var cm = new ContextMenu();
                var menuItem = new MenuItem();
                menuItem.Header = "Usuń";
                menuItem.Click += MainWindow.RemoveItem_Click;

                cm.Items.Add(menuItem);
                item.ContextMenu = cm;
            }

            Close();
        }
    }
}