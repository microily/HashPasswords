using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Dereev_21._101.Models;
using HashPassword;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace Dereev_21._101.Pages
{
    public partial class EditUser : Page
    {
        private static AtelieEntities s_firstDBEntities;

         Workers worker = new Workers();
         User user = new User();

        public EditUser(Workers selectedUser)
        {
            InitializeComponent();
            this.worker = selectedUser;
            s_firstDBEntities = new AtelieEntities();
            tbFam.Text = worker.Surname;
            tbName.Text = worker.Name;
            tbOtch.Text = worker.Otchestvo;
            var user = AuthHelp.GetContext().User.Where(u => u.id_user == worker.id_user).FirstOrDefault();
            tbLogin.Text = user.login;
            tbPass.Text = user.password;
        
            // Получение данных пользователя из базы данных
            
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Обновление данных работника с новыми значениями из элементов управления
                worker.Surname = tbFam.Text;
                worker.Name = tbName.Text;
                worker.Otchestvo = tbOtch.Text;

                user.login = tbLogin.Text;
                user.password = tbPass.Text;

                // Обновление email из текстового поля
                user.email = tbEmail.Text;

                // Обновление роли из комбо бокса
                user.id_roles = tbRole.SelectedIndex + 1; // +1, так как индексы в комбо боксе начинаются с 0, а в базе данных роли начинаются с 1

                // Сохранение изменений в базе данных
                using (var context = new AtelieEntities())
                {
                    // Обновляем объект Worker
                    context.Entry(worker).State = EntityState.Modified;

                    // Обновляем объект User
                    context.Entry(user).State = EntityState.Modified;

                    // Сохраняем изменения
                    context.SaveChanges();
                }

                MessageBox.Show("Данные пользователя успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
