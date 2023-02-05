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

namespace LanguageSchool.WindowAdmin
{
    /// <summary>
    /// Логика взаимодействия для AdminAuthetification.xaml
    /// </summary>
    public partial class AdminAuthetification : Window
    {
        public AdminAuthetification()
        {
            InitializeComponent();
        }

        private void btnActivation_Click(object sender, RoutedEventArgs e)
        {
            if(tbCode.Text == "0000")
            {
                MainWindow.adm = true;
                this.Close();

            }
            else
            {
                MessageBox.Show("Проверьте корректность ввода пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
