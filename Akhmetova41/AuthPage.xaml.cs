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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Akhmetova41
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LogBox.Text;
            string password = PassBox.Text;
            if(login == "" || password == "")
            {
                MessageBox.Show("Есть пустые поля");
                return;
            }

            User user = Akhmetova41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            if(user != null)
            {
                Manager.MainFrame.Navigate(new Shoes(user));
                LogBox.Text = "";
                PassBox.Text = "";
            }
            else
            {
                MessageBox.Show("Введены неверные данные");
                LogBtn.IsEnabled = false;
                LogBtn.IsEnabled = true;
            }
        }

        private void GuestBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = null;
            Manager.MainFrame.Navigate(new Shoes(user));
            LogBox.Text = "";
            PassBox.Text = "";
        }
    }
}
