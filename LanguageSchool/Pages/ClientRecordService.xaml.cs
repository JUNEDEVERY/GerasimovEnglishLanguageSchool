using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ClientRecordService.xaml
    /// </summary>
    public partial class ClientRecordService : Page
    {
        Service service;
        public ClientRecordService(Service service)
        {
            InitializeComponent();
            this.service = service;
            tbTitle.Text = "Услуга " + service.Title;
            tbTime.Text = "Длительность услуги " + service.Duration + " минут";

            cmbClient.ItemsSource = Model.tbe.Client.ToList();
            cmbClient.SelectedValuePath = "ID";
            cmbClient.DisplayMemberPath = "allClient";
            

        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListOfServices(true));
        }

        private void btnAddClien_Click(object sender, RoutedEventArgs e)
        {

            if (cmbClient.SelectedIndex != -1)
            {
                if (datePicker.SelectedDate != null)
                {
                    if (tbTime.Text != null && tbTime.Text.Length != 0)
                    {
                        if (Regex.IsMatch(tbMinute.Text, "^(([0,1][0-9])|(2[0-3])):[0-5][0-9]$") && cmbClient.SelectedIndex != -1)
                        {
                            try
                            {
                                DateTime date = new DateTime();
                                date = datePicker.SelectedDate.Value;
                                date = date.AddHours(Convert.ToInt32(tbMinute.Text.Substring(0, 2)));
                                date = date.AddMinutes(Convert.ToInt32(tbMinute.Text.Substring(3, 2)));
                                ClientService clientService = new ClientService()
                                {
                                    ClientID = Convert.ToInt32(cmbClient.SelectedValue),
                                    ServiceID = service.ID,
                                    StartTime = date,
                                 

                                };
                                Model.tbe.ClientService.Add(clientService);
                                Model.tbe.SaveChanges();
                                MessageBox.Show("Успешная запись на услугу " + service.Title);
                                NavigationService.Navigate(new ListOfServices(true));

                            }
                            catch
                            {
                                MessageBox.Show("Произошла ошибка при записи клиента на услугу", "Попробуем еще раз?", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                        else
                        {
                            tbError.Foreground = Brushes.Red;
                            MessageBox.Show("Проверьте корректность ввода даты", "Попробуем еще раз?", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    }
                    else
                    {
                        tbError.Text = "Время записи не указано!";
                        tbError.Foreground = Brushes.Red;
                        MessageBox.Show("Поле времени, возможно, не заполнено", "Попробуем еще раз?", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                }
                else
                {

                    MessageBox.Show("Не выбрана дата ", "Попробуем еще раз?", MessageBoxButton.OK, MessageBoxImage.Error);


                }

            }

            else
            {
                MessageBox.Show("Не выбран клиент", "Попробуем еще раз?", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }
        bool checkedDate(string data)
        {
            try
            {
                Convert.ToDateTime(data);
                return true;    
            }
            catch
            {
                return false;
            }
        }
      
        private void tbMinute_SelectionChanged(object sender, RoutedEventArgs e)
        {

            if (!Regex.IsMatch(tbMinute.Text, "^(([0,1][0-9])|(2[0-3])):[0-5][0-9]$")) // регулярка с нулем
            {
                tbError.Text = "Проверьте корректность ввода даты";
                tbError.Visibility = Visibility.Visible;
                tbError.Foreground = Brushes.Red;
                tbEndTime.Visibility = Visibility.Collapsed;
            }

            
            else
            {
                if (tbMinute.Text == null)
                {
                    tbEndTime.Visibility = Visibility.Collapsed;
                }
            
                else
                {
                    if (datePicker.SelectedDate == null)
                    {
                        MessageBox.Show("Заполните дату!");
                    }
                    else
                    {
                        if (checkedDate(datePicker.Text))
                        {
                            if (Regex.IsMatch(tbMinute.Text, "^(([0,1][0-9])|(2[0-3])):[0-5][0-9]$") ) // регулярка с нулем
                            {


                                tbError.Visibility = Visibility.Collapsed;
                                DateTime dateTime = new DateTime();

                                dateTime = datePicker.SelectedDate.Value;

                                dateTime = dateTime.AddHours(Convert.ToInt32(tbMinute.Text.Substring(0, 2)));
                                dateTime = dateTime.AddMinutes(Convert.ToInt32(tbMinute.Text.Substring(3, 2)));

                                dateTime = dateTime.AddSeconds(service.DurationInSeconds);
                                tbEndTime.Visibility = Visibility.Visible;

                                tbEndTime.Text = "Итоговое время окончания услуги - " + dateTime.ToString("dd MMMM yyyy HH:mm");

                            }
                            else
                            {
                                MessageBox.Show("Проверьте корректность ввода даты", "Попробуем еще раз?", MessageBoxButton.OK, MessageBoxImage.Error);

                            }
                        }
                      
                    }
                 
                }
               

            }


        
          

            



        }
            
        private void tbMinute_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
