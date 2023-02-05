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
using System.Windows.Threading;

namespace LanguageSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для Entries.xaml
    /// </summary>
    public partial class Entries : Page
    {
        public Entries()
        {
            InitializeComponent();
            timerSpan();
        }


        void timerSpan()
        {
            // лепим к листу дату начала
            List<ClientService> clients = Model.tbe.ClientService.ToList().Where(x => x.StartTime >= DateTime.Now).ToList();

            DateTime dateTime = DateTime.Today.AddDays(2);
          
            clients = clients.Where(x => x.StartTime <= dateTime).ToList();
            clients = clients.OrderBy(x => x.StartTime).ToList();

          
            lvEntriesEncoming.ItemsSource = clients;
        



        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // создаем таймер на 30 секунд при загрузке
            DispatcherTimer dispatcherTime = new DispatcherTimer();
            dispatcherTime.Interval = new TimeSpan(0, 0, 30);
            dispatcherTime.Tick += DispatcherTime_Tick;
            dispatcherTime.Start();
        }

        private void DispatcherTime_Tick(object sender, EventArgs e)
        {
            timerSpan();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
