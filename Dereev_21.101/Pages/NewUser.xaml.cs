using Dereev_21._101.Models;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Office.Interop.Word;
using HashPassword;

namespace Dereev_21._101.Pages
{
    public partial class NewUser : System.Windows.Controls.Page // Явное указание System.Windows.Controls.Page
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
                    imageData = File.ReadAllBytes(openFileDialog.FileName);
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
                Workers newWorker = new Workers
                {
                    Surname = tbFam.Text,
                    Name = tbName.Text,
                    Otchestvo = tbOtch.Text,
                };

                int? roleId = tbRole.SelectedIndex + 1;

                if (roleId == null)
                {
                    MessageBox.Show("Выбранная роль не найдена в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string hashedPassword = Hash.Pass(tbPass.Text);

                User newUser = new User
                {
                    email = tbEmail.Text,
                    id_roles = roleId,
                    login = tbLogin.Text,
                    password = hashedPassword,
                    Workers = new HashSet<Workers> { newWorker }
                };

                using (var context = new AtelieEntities())
                {
                    context.User.Add(newUser);
                    context.SaveChanges();
                }

                MessageBox.Show("Пользователь успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
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

        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e) { }

        private void tbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (selectedItem != null)
            {
                int idRoles = Convert.ToInt32(selectedItem.Tag);
            }
        }
    }
}
