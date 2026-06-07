using dem_test_03.database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dem_test_03
{
    /// <summary>
    /// Логика взаимодействия для OrderAddEditWindow.xaml
    /// </summary>
    public partial class OrderAddEditWindow : Window
    {
        OrderDetail currentOrder = new OrderDetail();
        bool isEdit = false;
        public OrderAddEditWindow()
        {
            InitializeComponent();
            LoadData();
            IdBox.Visibility = Visibility.Collapsed;
        }

        public OrderAddEditWindow(OrderDetail order)
        {
            InitializeComponent();
            currentOrder = order;
            LoadData();

            isEdit = true;

            IdBox.Text = currentOrder.Orderid.ToString();
            ProductBox.SelectedItem = currentOrder.Product;
            StatusBox.SelectedItem = currentOrder.Order.Status;
            PickpointAddressBox.SelectedItem = currentOrder.Order.Pickpoint;
            OrderDateBox.Text = currentOrder.Order.OrderDate;
            DeliveryDateBox.Text = currentOrder.Order.DeliveryDate;
        }

        private void LoadData()
        {
            StatusBox.ItemsSource = DemTest03Context.GetContext().Statuses.ToList();
            PickpointAddressBox.ItemsSource = DemTest03Context.GetContext().Pickpoints.ToList();
            ProductBox.ItemsSource = DemTest03Context.GetContext().Products.ToList();
            StatusBox.DisplayMemberPath = "Name";
            PickpointAddressBox.DisplayMemberPath = "Name";
            ProductBox.DisplayMemberPath = "Article";
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {

            if (!isEdit && currentOrder.Order == null)
            {
                currentOrder.Order = new Order();
            }

            currentOrder.Product = ProductBox.SelectedItem as Product;
            currentOrder.Order.Status = StatusBox.SelectedItem as Status;
            currentOrder.Order.Pickpoint = PickpointAddressBox.SelectedItem as Pickpoint;
            currentOrder.Order.OrderDate = OrderDateBox.Text;
            currentOrder.Order.DeliveryDate = DeliveryDateBox.Text;

            if (!isEdit)
            {
                DemTest03Context.GetContext().OrderDetails.Add(currentOrder);
            }
            DemTest03Context.GetContext().SaveChanges();
            Close();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот заказ?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DemTest03Context.GetContext().OrderDetails.Remove(currentOrder);
                DemTest03Context.GetContext().SaveChanges();

                Close();
            }
        }
    }
}
