using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для AddEditService.xaml
    /// </summary>
    public partial class AddEditService : Page
    {
        Service service;
        private bool change;
        string path;
        public AddEditService()
        {
            InitializeComponent();
            checmDose();
           
        }
  
        public AddEditService(Service service)
        {
            InitializeComponent();
          
            change = true;
            this.service = service;

            tbID.Visibility = Visibility.Visible;
          
            tbID.Text = "Идентификатор - " + service.ID;
            btnAdd.Content = "Изменить";
            btnChangeImage.Content = "Изменить изображение";
            tbID.Text = Convert.ToString(service.ID);
            tbNameService.Text = service.Title;
            tbMoneyService.Text = Convert.ToString(service.Cost);
            tbTimeService.Text = Convert.ToString(service.DurationInSeconds);
            tbSaleService.Text = Convert.ToString(service.Discount);
            tbDesctiptionService.Text = Convert.ToString(service.Description);
            if (service.MainImagePath != null)
            {
                path = service.MainImagePath;
                imgService.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                btnChangeImage.Visibility = Visibility.Visible;
                btnDeleteImage.Visibility = Visibility.Visible;

             
            }
            else
            {
                path = null;
                imgService.Source = new BitmapImage(new Uri("..\\resources\\noPhoto.png", UriKind.Relative));
            }

        }
        private void tbID_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnChangeImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();      
                openFileDialog.ShowDialog();
                string[] aPath  = openFileDialog.FileName.Split('\\');
                path = "\\" + aPath[aPath.Length - 2] + "\\" + aPath[aPath.Length - 1];
                imgService.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                btnDeleteImage.Visibility = Visibility.Visible;
                    
               




            }
            catch
            {
                MessageBox.Show("Произошел сбой при попытке изменения изображения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }



        

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            checmDose();
            btnDeleteImage.Visibility = Visibility.Collapsed;
            service.MainImagePath = path;
            Model.tbe.SaveChanges();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (change)
            {
                try
                {
                    if (string.IsNullOrEmpty(tbNameService.Text) || string.IsNullOrEmpty(tbMoneyService.Text) || string.IsNullOrEmpty(tbTimeService.Text))
                    {
                        MessageBox.Show("Одно или несколько полей при измении не были заполнены", "Удостоверьтесь в корректности!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (!string.IsNullOrWhiteSpace(tbSaleService.Text))
                    {
                        if (Convert.ToDouble(tbSaleService.Text) > 100)
                        {
                            MessageBox.Show("Скидка не может быть больше 100%");
                            return;
                        }
                    }
           
                    int a = Convert.ToInt32(tbTimeService.Text);
                    if (a <= 0)
                    {
                        MessageBox.Show("Время не может быть отрицательным", "Удостоверьтесь в корректности!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    double b = Convert.ToDouble(service.DurationInSeconds / 60);
                    if (b > 240)
                    {
                        MessageBox.Show("Длительность не может быть более 4х часов", "Удостоверьтесь в корректности!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    List<Service> services = Model.tbe.Service.Where(x => x.Title == tbNameService.Text).ToList();
                  
                    service.Title = tbNameService.Text;
                    service.Cost = Convert.ToDecimal(tbMoneyService.Text);
                    service.DurationInSeconds = Convert.ToInt32(tbTimeService.Text);
                    if (string.IsNullOrEmpty(tbDesctiptionService.Text))
                    {
                        service.Description = null;

                    }
                    else
                    {
                        service.Description = tbDesctiptionService.Text;

                    }
                    if (string.IsNullOrEmpty(tbSaleService.Text))
                    {
                        service.Discount = null;
                    }
                    else
                    {
                        service.Discount = Convert.ToDouble(tbSaleService.Text);
                    }

                    service.MainImagePath = path;

                    Model.tbe.SaveChanges();
                    MessageBox.Show("Изменение произошло успешно!");
                    NavigationService.Navigate(new ListOfServices(true));
                }

                catch
                {

                }
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(tbNameService.Text) || string.IsNullOrEmpty(tbMoneyService.Text) || string.IsNullOrEmpty(tbTimeService.Text))
                    {
                        MessageBox.Show("Одно или несколько полей при измении не были заполнены", "Удостоверьтесь в корректности!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    
                    }

                    if (!string.IsNullOrWhiteSpace(tbSaleService.Text))
                    {
                        if (Convert.ToDouble(tbSaleService.Text) > 100)
                        {
                            MessageBox.Show("Скидка не может быть больше 100%");
                            return;
                        }
                    }
                    int a = Convert.ToInt32(tbTimeService.Text);
                    if (a <= 0)
                    {
                        MessageBox.Show("Время не может быть отрицательным", "Удостоверьтесь в корректности!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                 
                    if (Convert.ToDouble(tbTimeService.Text) / 60 > 240)
                    {
                        MessageBox.Show("Длительность не может быть более 4х часов", "Удостоверьтесь в корректности!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    List<Service> services = Model.tbe.Service.Where(x => x.Title == tbNameService.Text).ToList();
                    if (services.Count > 0)
                    {
                        MessageBox.Show("Услуга уже существует", "Удостоверьтесь в корректности!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    service = new Service();
                    service.Title = tbNameService.Text;
                    service.Cost = Convert.ToDecimal(tbMoneyService.Text);
                    service.DurationInSeconds = Convert.ToInt32(tbTimeService.Text);
                    if (string.IsNullOrEmpty(tbDesctiptionService.Text))
                    {
                        service.Description = null;

                    }
                    else
                    {
                        service.Description = tbDesctiptionService.Text;

                    }
                    if (string.IsNullOrEmpty(tbSaleService.Text))
                    {
                        service.Discount = null;
                    }
                    else
                    {
                        service.Discount = Convert.ToDouble(tbSaleService.Text);
                    }

                    service.MainImagePath = path;

                    Model.tbe.Service.Add(service);
                    Model.tbe.SaveChanges();
                    MessageBox.Show("Добавление произошло успешно!");
                    NavigationService.Navigate(new ListOfServices(true));
                }
                catch
                {

                }
            }
           
        }
        private void checmDose()
        {
            imgService.Source = new BitmapImage(new Uri("..\\resources\\noPhoto.png", UriKind.Relative));
            path = null;
        }
       
    }
}
