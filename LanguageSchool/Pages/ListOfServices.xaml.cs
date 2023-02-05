using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

namespace LanguageSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfServices.xaml
    /// </summary>
    public partial class ListOfServices : Page
    {
        public bool adm;
        public ListOfServices(bool adm)
        {
            InitializeComponent();
            this.adm = adm;
            if (adm)
            {
                stack.Visibility = Visibility.Visible;
            }
            lvService.ItemsSource = Model.tbe.Service.ToList();

            tbCount.Text = "Общее количество записей " + Model.tbe.Service.ToList().Count().ToString();
            tbTotal.Text = "Текущее количество записей " + lvService.Items.Count.ToString();
            lvService.ItemsSource = Model.tbe.Service.ToList();


        }

        private void tbOldPrice_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock text = (TextBlock)sender;
            if (text.Uid != null) { text.Visibility = Visibility.Visible; }
            else { text.Visibility = Visibility.Collapsed; }
        }

        private void tbActualPrice_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void tbSale_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            if (textBlock.Uid != null) { textBlock.Visibility = Visibility.Visible; }
            else { textBlock.Visibility = Visibility.Collapsed; }
        }
        private void cmbFiltres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtres();
        }
        private void cmbSorted_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtres();
        }
        private void tbNameDescription_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filtres();
        }
        void visabilityButton(object sender, RoutedEventArgs e)
        {
            Button btnEdit = sender as Button;
            Button btnDelete = sender as Button;
            Button btnSignUp = sender as Button;
            Button btnMoreImage  = sender as Button;
            if(adm == true)
            {
                btnEdit.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                btnSignUp.Visibility = Visibility.Visible;
                btnMoreImage.Visibility = Visibility.Visible;

            }
            else
            {
                btnEdit.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                btnSignUp.Visibility = Visibility.Collapsed;
                btnMoreImage.Visibility = Visibility.Collapsed;

            }

        }
        void Filtres()
        {
            List<Service> services = Model.tbe.Service.ToList();
            try
            {


                if (cmbFiltres.SelectedIndex != 0)
                {

                    ComboBoxItem comboBoxItemFilres = (ComboBoxItem)cmbFiltres.SelectedItem;
                    if(comboBoxItemFilres != null)
                    switch (comboBoxItemFilres.Content)
                    {
                        case "По умолчанию":
                            {
                                services = services;
                                break;
                            }
                        case "от 0 до 5%":
                            {
                                services = services.Where(x => x.Discount >= 0 && x.Discount < 5).ToList();
                                break;

                            }
                         
                        case "от 5 до 15%":
                            {

                                services = services.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
                                break;
                            }
                       
                        case "от 15 до 30%":
                            {

                                services = services.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
                                break;
                            }
                            
                        case "от 30 до 70%":
                            {

                                services = services.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
                                break;
                            }
                          
                        case "от 70 до 100%":
                            {

                                services = services.Where(x => x.Discount >= 70 && x.Discount <= 100).ToList();
                                break;
                            }
                           



                    }

                }
               



                if (cmbSorted != null)
                    if (cmbSorted.SelectedIndex != 0)
                    {
                        ComboBoxItem comboBoxItem = (ComboBoxItem)cmbSorted.SelectedItem;
                        if (comboBoxItem != null)
                        {
                            switch (comboBoxItem.Content)
                            {
                                case "По возрастанию":
                                    {
                                        services = services.OrderBy(x => x.ActualPrice).ToList();
                                        break;
                                    }
                                case "По убыванию":
                                    {
                                        services = services.OrderByDescending(x => x.ActualPrice).ToList();
                                        break;
                                    }
                                default:
                                    services = services;
                                    break;


                            }
                        }


                    }
               
                
                if(tbNameDescription.Text != "")
                {
                    if (tbNameDescription.Text != null)
                    {
                        services = Model.tbe.Service.Where(x => x.Title.ToLower().Contains(tbNameDescription.Text.ToLower()) || (x.Description.ToLower().Contains(tbNameDescription.Text.ToLower()))).ToList();

                    }


                }


                if(services.Count == null || services.Count == 0)
                {
                    MessageBox.Show("Отсутствие подходящих записей", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
             
                lvService.ItemsSource = services;
                tbTotal.Text = "Текущее количество записей " + lvService.Items.Count.ToString();
            }
            catch
            {
                MessageBox.Show("Что-то пошло явно не так");
            }


        }

 

        private void tbCount_Loaded(object sender, RoutedEventArgs e)
        {
            List<Service> services = Model.tbe.Service.ToList();
            tbCount.Text = "Общее количество записей " + Model.tbe.Service.ToList().Count().ToString();
              lvService.ItemsSource = services;
        }

        private void tbTotal_Loaded(object sender, RoutedEventArgs e)
        {
            List<Service> services = Model.tbe.Service.ToList();
          
            tbTotal.Text = "Текущее количество записей " + lvService.Items.Count.ToString();
            lvService.ItemsSource = services;

        }


        private void btnNearestService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Entries());
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) // удалить услугу
        {
            Button button = (Button)sender;
            int i = Convert.ToInt32(button.Uid);
            Service service = Model.tbe.Service.FirstOrDefault(x => x.ID == i);
            List<ClientService> clientServices = Model.tbe.ClientService.Where(x=>x.ServiceID == i).ToList();
            var messageBoxResult = MessageBox.Show("Вы хотите удалить эту услугу?", "Подвердите действие", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

      
            if(messageBoxResult == MessageBoxResult.Yes)
            {
                foreach (ClientService clientService in clientServices)
                {
                    if (clientService.ServiceID == i)
                    {
                        MessageBox.Show("На данную услугу имеется информаация о записях. Удаление не возможно.", "Alarm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                foreach (ServicePhoto servicePhoto in Model.tbe.ServicePhoto.ToList())
                {
                    if(servicePhoto.ServiceID == i)
                    {
                        Model.tbe.ServicePhoto.Remove(servicePhoto);
                    }
                }
                Model.tbe.Service.Remove(service);
                Model.tbe.SaveChanges();

                MessageBox.Show("Услуга " + service.Title + " удалена из списка предлагаемых услуг.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ListOfServices(adm));
               
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e) // изменить услугу
        {

            Button button = (Button)sender;
            int i = Convert.ToInt32(button.Uid);
            Service service = Model.tbe.Service.FirstOrDefault(x => x.ID == i);
            NavigationService.Navigate(new AddEditService(service));
        }

        private void btnAddNewService_Click(object sender, RoutedEventArgs e) // добавить услугу
        {
            NavigationService.Navigate(new AddEditService());
        }
        private void btnSignUp_Click(object sender, RoutedEventArgs e) // запись клиента на услугу
        {
            Button button = (Button)sender;
            int i = Convert.ToInt32(button.Uid);
            Service service = Model.tbe.Service.FirstOrDefault(x => x.ID == i);
            NavigationService.Navigate(new ClientRecordService(service));
        }

        private void btnMoreImage_Click(object sender, RoutedEventArgs e) // дополнительные изображения об услуге
        {
            Button button = (Button)sender;
            int i = Convert.ToInt32(button.Uid);
            Service service = Model.tbe.Service.FirstOrDefault(x => x.ID == i);
            NavigationService.Navigate(new ImageEdit(service));
        }
    }
}
