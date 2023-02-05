using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для ImageEdit.xaml
    /// </summary>
    public partial class ImageEdit : Page
    {
        Service service;
        public ImageEdit(Service service)
        {
            InitializeComponent();
            this.service = service;
            tbNameService.Text = "Дополнительные изображения к услуге " + service.Title;
            lvImage.ItemsSource = Model.tbe.ServicePhoto.Where(x => x.ServiceID == service.ID).ToList();

        }

        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int id = Convert.ToInt32(button.Uid);
            ServicePhoto servicePhoto = Model.tbe.ServicePhoto.Where(x => x.ID == id).FirstOrDefault();
            Model.tbe.ServicePhoto.Remove(servicePhoto);
            Model.tbe.SaveChanges();
            MessageBox.Show("Успешное удаление фотографии");
            NavigationService.Navigate(new ImageEdit(service));

        }

        private void btnAddPicture_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.ShowDialog();

                string sourcePath = openFileDialog.FileName;
               
                string targetPath = System.IO.Path.Combine(System.IO.Directory.GetParent(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length - 9)).FullName + "\\image\\") + System.IO.Path.GetFileName(sourcePath);



                try
                {
                    if (!File.Exists(targetPath))
                    {
                        File.Copy(sourcePath, targetPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удается переместить файл из-за исключения: " + ex.Message);
                }



                ServicePhoto servicePhoto = new ServicePhoto()
                {
                    ServiceID = service.ID,
                    PhotoPath = targetPath,


                };

                Model.tbe.ServicePhoto.Add(servicePhoto);
                Model.tbe.SaveChanges();

                NavigationService.Navigate(new ImageEdit(service));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удается переместить файл из-за исключения: " + ex.Message);
            }
           

                

           

        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListOfServices(true));
        }
    }
}
