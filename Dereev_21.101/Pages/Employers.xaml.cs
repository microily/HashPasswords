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
using Excel = Microsoft.Office.Interop.Excel;


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
            using (var context = new AtelieEntities())
            {
                var employees = (from user in context.User
                                 join worker in context.Workers on user.id_user equals worker.id_user into workerJoin
                                 from worker in workerJoin.DefaultIfEmpty()
                                 select worker).ToList();

                LViewEmployee.ItemsSource = employees;
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

        private void AddEmployer_Click(object sender, RoutedEventArgs e)
        {
            NewUser newUserPage = new NewUser();
            NavigationService.Navigate(newUserPage);
        }
        private void tbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void excel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание нового экземпляра приложения Excel
                Excel.Application excelApp = new Excel.Application();
                if (excelApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }

                // Создание новой книги Excel
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();

                // Добавление нового листа в книгу
                Excel.Worksheet excelWorksheet = excelWorkbook.Sheets.Add();
                excelWorksheet.Name = "Employees";

                // Заполнение заголовков
                excelWorksheet.Cells[1, 1] = "ID";
                excelWorksheet.Cells[1, 2] = "Login";
                excelWorksheet.Cells[1, 3] = "Password";
                excelWorksheet.Cells[1, 4] = "Role ID";
                excelWorksheet.Cells[1, 5] = "Email";
                excelWorksheet.Cells[1, 6] = "Surname";
                excelWorksheet.Cells[1, 7] = "Name";
                excelWorksheet.Cells[1, 8] = "Otchestvo";

                // Заполнение данными
                int row = 2;
                foreach (var employee in LViewEmployee.Items)
                {
                    excelWorksheet.Cells[row, 1] = (employee as dynamic).id_user;
                    excelWorksheet.Cells[row, 2] = (employee as dynamic).login;
                    excelWorksheet.Cells[row, 3] = (employee as dynamic).password;
                    excelWorksheet.Cells[row, 4] = (employee as dynamic).id_roles;
                    excelWorksheet.Cells[row, 5] = (employee as dynamic).email;
                    excelWorksheet.Cells[row, 6] = (employee as dynamic).Surname;
                    excelWorksheet.Cells[row, 7] = (employee as dynamic).Name;
                    excelWorksheet.Cells[row, 8] = (employee as dynamic).Otchestvo;
                    row++;
                }

                // Сохранение книги
                string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Employees.xlsx");
                excelWorkbook.SaveAs(fileName);

                // Открытие книги
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                try
                {
                    using (var context = new AtelieEntities())
                    {
                        var employees = (from user in context.User
                                         join worker in context.Workers on user.id_user equals worker.id_user into workerJoin
                                         from worker in workerJoin.DefaultIfEmpty()
                                         select worker).ToList();

                        var filteredEmployees = employees.Where(emp =>
                            (emp != null &&
                            ((emp.Surname != null && emp.Surname.ToLower().Contains(searchText)) ||
                            (emp.Name != null && emp.Name.ToLower().Contains(searchText)) ||
                            (emp.Otchestvo != null && emp.Otchestvo.ToLower().Contains(searchText))))
                        ).ToList();

                        LViewEmployee.ItemsSource = filteredEmployees;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while searching: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                LoadData(); // Если поле поиска пустое, отобразите все записи
            }
        }
    }
}