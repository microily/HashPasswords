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
        private Workers worker;
        private User user;

        public EditUser(Workers selectedUser)
        {
            InitializeComponent();
            this.worker = selectedUser;
            s_firstDBEntities = new AtelieEntities();
            tbFam.Text = worker.Surname;
            tbName.Text = worker.Name;
            tbOtch.Text = worker.Otchestvo;

            // Получаем пользователя из контекста базы данных
            user = s_firstDBEntities.User.FirstOrDefault(u => u.id_user == worker.id_user);

            if (user != null)
            {
                tbLogin.Text = user.login;
                tbPass.Text = user.password;
            }
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, что все обязательные поля заполнены
                if (string.IsNullOrWhiteSpace(tbLogin.Text) || string.IsNullOrWhiteSpace(tbPass.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все обязательные поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Другие проверки на корректность данных могут быть добавлены здесь

                // Получаем актуальные данные работника из базы данных
                worker = s_firstDBEntities.Workers.FirstOrDefault(w => w.id_user == worker.id_user);

                // Обновляем данные работника с новыми значениями из элементов управления
                worker.Surname = tbFam.Text;
                worker.Name = tbName.Text;
                worker.Otchestvo = tbOtch.Text;

                // Обновляем данные пользователя
                user.login = tbLogin.Text;
                user.password = tbPass.Text;
                user.email = tbEmail.Text;
                user.id_roles = tbRole.SelectedIndex + 1; // +1, так как индексы в комбо боксе начинаются с 0, а в базе данных роли начинаются с 1

                // Сохраняем изменения в базе данных
                s_firstDBEntities.SaveChanges();

                MessageBox.Show("Данные пользователя успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            // Действие при добавлении изображения
        }
    }
}
