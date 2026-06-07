using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.WebSockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dem_test_03.database;

namespace dem_test_03
{
    public partial class MainWindow : Window
    {
        private User _current_user;
        public MainWindow() : this(null) { }

        public MainWindow(User user)
        {
            InitializeComponent();
            UserFullname.Text = user == null ? "Гость" : $"{user.Fullname} ({user.Role?.Name})";
            
            _current_user = user;
            UpdateUIState();

            LoadData();
            LoadSuppliers();
            UnitSortComboBox.SelectedIndex = 0;
            SupplierSortComboBox.SelectedIndex = 0;
        }

        private void UpdateUIState()
        {
            string role = _current_user?.Role?.Name;
            bool isAdmin = role == "Администратор";
            bool isManager = role == "Менеджер";
            bool hasAccess = isAdmin || isManager;

            // Определяем текущий режим
            bool isOrderMode = NameZone.Text == "Заявки";

            // Видимость панелей 
            ProductOptionsPanel.Visibility = (hasAccess && !isOrderMode) ? Visibility.Visible : Visibility.Collapsed;
            AddProductButton.Visibility = (isAdmin && !isOrderMode) ? Visibility.Visible : Visibility.Collapsed;
            AddOrderButton.Visibility = (isAdmin && isOrderMode) ? Visibility.Visible : Visibility.Collapsed;
            OrderButton.Visibility = (hasAccess && !isOrderMode) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LoadSuppliers()
        {
            SupplierSortComboBox.ItemsSource = new[] { new Supplier { Name = "Все поставщики", Supplierid = -1 } }.Concat(DemTest03Context.GetContext().Suppliers.ToList());
            SupplierSortComboBox.DisplayMemberPath = "Name";
        }

        private void LoadData()
        {
            NameZone.Text = "Каталог";
            ProductList.ItemTemplate = (DataTemplate)FindResource("ProductTemplate");
            UpdateUIState();

            var search = SearchTextBox.Text.Trim();
            var context = DemTest03Context.GetContext();

            var query = context.Products.Include(p => p.Manufacturer).Include(p => p.Supplier).Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));

            // Фильтрация по поставщикам
            if (SupplierSortComboBox.SelectedItem is Supplier s && s.Supplierid != -1)
                query = query.Where(p => p.Supplier.Supplierid == s.Supplierid);

            query = UnitSortComboBox.SelectedIndex switch
            {
                1 => query.OrderBy(p => p.Quantity),
                2 => query.OrderByDescending(p => p.Quantity),
                _ => query
            };

            ProductList.ItemsSource = query.ToList();
        }

        private void LoadOrder()
        {
            NameZone.Text = "Заявки";
            ProductList.ItemTemplate = (DataTemplate)FindResource("OrderTemplate");
            ProductList.ItemsSource = DemTest03Context.GetContext().Orders.Include(o => o.Pickpoint).ToList();
            UpdateUIState();
        }

        private void ProductViewChanged(object sender, EventArgs e)
        {
            if (ProductList != null) LoadData();
        }

        private void Product_Click(object sender, MouseButtonEventArgs e)
        {
            if (_current_user == null || _current_user.Role.Name != "Администратор")
            {
                e.Handled = true;
                return;
            }
            Border border = sender as Border;
            Product product = border.DataContext as Product;
            new ProductEditWindow(product).ShowDialog();
            LoadData();
        }

        private void Order_Click(object sender, MouseButtonEventArgs e)
        {
            if (_current_user == null || _current_user.Role.Name != "Администратор")
            {
                e.Handled = true;
                return;
            }
            Border border = sender as Border;
            Order order = border.DataContext as Order;
            new OrderAddEditWindow(order).ShowDialog();
            LoadOrder();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderAddEditWindow().ShowDialog();
            LoadOrder();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductEditWindow().ShowDialog();
            LoadData();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            LoadOrder();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
    }
}