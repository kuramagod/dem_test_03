using Microsoft.EntityFrameworkCore;
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
using dem_test_03.database;

namespace dem_test_03
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Получение пользователя из базы данных при нажатии на кнопку, logtxt и passtxt прописываем в x:Name в тегах xaml окна, по сути считываем из них введенных значения
            var curentUser = DemTest03Context.GetContext().Users.Include(p => p.Role).FirstOrDefault(p => p.Login == logtxt.Text & p.Password == passtxt.Password);

            if (curentUser != null)
            {
                // Открытие главного окна и передача пользователя
                new MainWindow(curentUser).Show();
                this.Close();
            }
            else
            {
                // Обработка ошибки
                MessageBox.Show("Пользователь не найден.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Открытие главного окна и передачи гостя, тут вписываем в аргументы null, обработаем это в главном окне
            new MainWindow().Show();
            this.Close();
        }
    }
}
