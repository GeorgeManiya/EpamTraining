using System.Windows;
using SalesBisnessLogic;

namespace SalesInfoApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SalesDataCore _salesCore;

        public MainWindow()
        {
            InitializeComponent();
            _salesCore = new SalesDataCore();
            BindingData();
        }

        private void BindingData()
        {
            SalesDataGrid.ItemsSource = _salesCore.Sales;
            ManagersListBox.ItemsSource = _salesCore.Managers;
            ClientsListBox.ItemsSource = _salesCore.Clients;
            ProductsListBox.ItemsSource = _salesCore.Products;
        }

        private void OnUpdateSalesButtonClick(object sender, RoutedEventArgs e)
        {
            _salesCore.Refill();
            BindingData();
        }
    }
}
