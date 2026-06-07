using dem_test_03.database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dem_test_03
{
    /// <summary>
    /// Логика взаимодействия для ProductEditWindow.xaml
    /// </summary>
    public partial class ProductEditWindow : Window
    {
        Product currentProduct = new Product();
        bool isEdit = false;

        public ProductEditWindow()
        {
            InitializeComponent();
            LoadData();
            IdBox.Visibility = Visibility.Collapsed;
            DeleteBut.Visibility = Visibility.Collapsed;
        }

        public ProductEditWindow(Product product)
        {
            InitializeComponent();
            LoadData();

            currentProduct = product;
            isEdit = true;

            NameBox.Text = currentProduct.Name;
            CategoryBox.SelectedItem = currentProduct.Category;
            DescriptionBox.Text = currentProduct.Description;
            ManufacturerBox.SelectedItem = currentProduct.Manufacturer;
            SupplierBox.SelectedItem = currentProduct.Supplier;
            PriceBox.Text = currentProduct.Price?.ToString();
            UnitBox.Text = currentProduct.Unit;
            QuantityBox.Text = currentProduct.Quantity?.ToString();
            DiscountBox.Text = currentProduct.Discount?.ToString();
            IdBox.Text = $"ID {currentProduct.Productid.ToString()}";
        }

        private void LoadData()
        {
            var context = DemTest03Context.GetContext();
            CategoryBox.ItemsSource = context.Categories.ToList();
            ManufacturerBox.ItemsSource = context.Manufacturers.ToList();
            SupplierBox.ItemsSource = context.Suppliers.ToList();
            CategoryBox.DisplayMemberPath = "Name";
            ManufacturerBox.DisplayMemberPath = "Name";
            SupplierBox.DisplayMemberPath = "Name";
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            if (CategoryBox.SelectedItem == null || ManufacturerBox.SelectedItem == null || SupplierBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите категорию, производителя и поставщика");
                return;
            }
            if (!int.TryParse(PriceBox.Text, out int price) || price <= 0 ||
                !int.TryParse(QuantityBox.Text, out int quantity) || quantity < 0 ||
                !int.TryParse(DiscountBox.Text, out int discount) || discount < 0 || discount > 100)
            {
                MessageBox.Show("Проверьте правильность заполнения полей");
                return;
            }


            currentProduct.Name = NameBox.Text;
            currentProduct.Category = CategoryBox.SelectedItem as Category;
            currentProduct.Description = DescriptionBox.Text;
            currentProduct.Manufacturer = ManufacturerBox.SelectedItem as Manufacturer;
            currentProduct.Supplier = SupplierBox.SelectedItem as Supplier;
            currentProduct.Price = Convert.ToInt32(PriceBox.Text);
            currentProduct.Unit = UnitBox.Text;
            currentProduct.Quantity = Convert.ToInt32(QuantityBox.Text);
            currentProduct.Discount = Convert.ToInt32(DiscountBox.Text);

            if (!isEdit)
            {
                DemTest03Context.GetContext().Products.Add(currentProduct);
            }
            DemTest03Context.GetContext().SaveChanges();
            Close();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            if (DemTest03Context.GetContext().OrderDetails.Any(x => x.Productid == currentProduct.Productid))
            {
                MessageBox.Show("Нельзя удалить товар, так как он присутствует в заказе!", "Ошибка", MessageBoxButton.OK);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DemTest03Context.GetContext().Products.Remove(currentProduct);
                DemTest03Context.GetContext().SaveChanges();

                Close();
            }
        }
    }
}
