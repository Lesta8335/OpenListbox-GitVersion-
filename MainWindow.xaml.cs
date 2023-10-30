using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OpenListbox
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;
            bool isCorrect = login == "admin" && password == "12345";
            if (isCorrect)
            {
                // Скрыть текущее окно
                this.Hide();

                // Показать второе окно
                AuthWindow window = new AuthWindow();
                window.Show();
            }
            else
            {
                MessageBox.Show("Вы указали неверные данные для входа.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}