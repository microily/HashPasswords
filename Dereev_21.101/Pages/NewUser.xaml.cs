using Dereev_21._101.Models;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using System;
using System.Linq;
using HashPassword;
using System.Collections.Generic;

namespace Dereev_21._101.Pages
{
    public partial class NewUser : Page
    {
        private byte[] imageData;

        public NewUser()
        {
            InitializeComponent();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Открываем выбранный файл изображения и считываем его байты
                    imageData = File.ReadAllBytes(openFileDialog.FileName);

                    // Отображаем выбранное изображение
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(imageData);
                    bitmap.EndInit();
                    ImageUser.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем новый объект работника с данными из полей формы
                Workers newWorker = new Workers
                {
                    Surname = tbFam.Text,
                    Name = tbName.Text,
                    Otchestvo = tbOtch.Text,
                    // Здесь могут быть и другие свойства работника, которые вы хотите сохранить
                };

                // Получаем выбранное значение комбобокса
                int? roleId = tbRole.SelectedIndex + 1; // С учетом, что id_roles начинаются с 1

                if (roleId == null)
                {
                    MessageBox.Show("Выбранная роль не найдена в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Хешируем пароль с помощью метода Pass из класса Hash
                string hashedPassword = Hash.Pass(tbPass.Text);

                // Создаем новый объект пользователя с данными из полей формы и идентификатором роли
                User newUser = new User
                {
                    email = tbEmail.Text,
                    id_roles = roleId,
                    login = tbLogin.Text,
                    password = hashedPassword,
                    Workers = new HashSet<Workers> { newWorker } // Исправление связи между объектами
                };

                // Сохраняем работника и пользователя в базе данных
                using (var context = new AtelieEntities())
                {
                    context.User.Add(newUser);
                    context.SaveChanges();
                }

                MessageBox.Show("Пользователь успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Очищаем поля формы после добавления пользователя
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем поля формы
            ClearForm();
        }

        private void ClearForm()
        {
            tbFam.Text = "";
            tbName.Text = "";
            tbOtch.Text = "";
            tbEmail.Text = "";
            tbRole.SelectedIndex = -1;
            tbLogin.Text = "";
            tbPass.Text = "";
            ImageUser.Source = new BitmapImage(new Uri("/Resources/profile.png", UriKind.Relative));
            imageData = null;
        }

        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (selectedItem != null)
            {
                // Получаем id_roles из свойства Tag выбранного элемента
                int idRoles = Convert.ToInt32(selectedItem.Tag);
            }
        }
    }
}