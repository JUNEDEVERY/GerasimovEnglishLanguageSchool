
using System;

using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Controls;
using LanguageSchool.Pages;
using LanguageSchool.WindowAdmin;

namespace LanguageSchool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool adm;
        public MainWindow()
        {


            InitializeComponent();

            Model.tbe = new Entities1();
            MainFrame.frame = fMain;
            adm = false;
            MainFrame.frame.Navigate(new ListOfServices(adm));

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(adm == false)
            {
                AdminAuthetification admin = new AdminAuthetification();
                admin.ShowDialog();
                if (adm == true)
                {
                    tbAdminAuthOut.Visibility = Visibility.Visible;
                    tbAdminAuth.Visibility = Visibility.Collapsed;
                    MainFrame.frame.Navigate(new ListOfServices(adm));

                }
            }
            else
            {
                var messageBoxResult = MessageBox.Show("Деактивировать режим администратора?", "Подвердите выход из админ панель", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    adm = false;
                    tbAdminAuthOut.Visibility = Visibility.Collapsed;
                    tbAdminAuth.Visibility = Visibility.Visible;
                    MainFrame.frame.Navigate(new ListOfServices(adm));
                }
                

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
