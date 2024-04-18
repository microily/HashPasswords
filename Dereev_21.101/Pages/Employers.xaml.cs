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
using Dereev_21._101.Pages;
using Dereev_21._101.Models;

namespace Dereev_21._101.Pages
{
    /// <summary>
    /// Логика взаимодействия для Employers.xaml
    /// </summary>
    public partial class Employers : Page
    {

        public Employers()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            using (var context = new AtelieEntities()) // Создание контекста базы данных
            {
                var employees = context.Workers.ToList(); // Выборка данных из базы данных

                LViewEmployee.ItemsSource = employees; // Привязка данных к ListView
            }
        }


        private void LViewEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedUser = (Workers)LViewEmployee.SelectedItem;

            if (selectedUser != null)
            {
                NavigationService.Navigate(new EditUser(selectedUser));
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddEmployer_Click(object sender, RoutedEventArgs e)
        {
            NewUser newUserPage = new NewUser();
            NavigationService.Navigate(newUserPage);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
