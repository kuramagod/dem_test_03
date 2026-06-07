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
    /// Логика взаимодействия для OrderAddEditWindow.xaml
    /// </summary>
    public partial class OrderAddEditWindow : Window
    {
        Order currentOrder = new Order();
        bool isEdit = false;
        public OrderAddEditWindow()
        {
            InitializeComponent();
            LoadData();
            IdBox.Visibility = Visibility.Collapsed;
        }

        public OrderAddEditWindow(Order order)
        {
            InitializeComponent();
            currentOrder = order;
            LoadData();

            isEdit = true;

            IdBox.Text = currentOrder.Orderid.ToString();
            CodeBox.Text = currentOrder.Code;
            StatusBox.Text = currentOrder.Status;
            PickpointAddressBox.SelectedItem = currentOrder.Pickpoint;
            OrderDateBox.Text = currentOrder.OrderDate;
            DeliveryDateBox.Text = currentOrder.DeliveryDate;
        }

        private void LoadData()
        {
            PickpointAddressBox.ItemsSource = DemTest03Context.GetContext().Pickpoints.ToList();
            PickpointAddressBox.DisplayMemberPath = "Name";
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            currentOrder.Code = CodeBox.Text;
            currentOrder.Status = StatusBox.Text;
            currentOrder.Pickpoint = PickpointAddressBox.SelectedItem as Pickpoint;
            currentOrder.OrderDate = OrderDateBox.Text;
            currentOrder.DeliveryDate = DeliveryDateBox.Text;

            if (!isEdit)
            {
                DemTest03Context.GetContext().Orders.Add(currentOrder);
            }
            DemTest03Context.GetContext().SaveChanges();
            Close();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот заказ?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DemTest03Context.GetContext().Orders.Remove(currentOrder);
                DemTest03Context.GetContext().SaveChanges();

                Close();
            }
        }
    }
}
