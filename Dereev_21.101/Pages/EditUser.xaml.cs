using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Dereev_21._101.Models;
using HashPassword;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Collections.Generic;

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
                tbEmail.Text = user.email; // Установка значения email
                                           // Установка значения роли из ComboBox по id_roles
                int roleId = user.id_roles ?? 0; // Если id_roles null, используем значение по умолчанию (0)
                tbRole.SelectedIndex = roleId - 1; // -1, так как индексы в ComboBox начинаются с 0, а в базе данных роли начинаются с 1
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

        private void doc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, что рабочий и пользовательские данные не равны null
                if (worker == null || user == null)
                {
                    MessageBox.Show("Невозможно создать договор: данные работника или пользователя отсутствуют", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создаем словарь для заполнения шаблона договора
                var items = new Dictionary<string, string>()
        {
            {"<TodayDate>", DateTime.Now.ToString("dd.MM.yyyy")},
            {"<Sotrudnik>", user.Roles.name}, // Используем роль пользователя из базы данных
            {"<FullNameWorker>", $"{worker.Surname} {worker.Name} {worker.Otchestvo}"},
            {"<NumDogovora>", worker.id_worker.ToString()},
            {"<EmailUser>", user.email} // Добавляем адрес электронной почты пользователя
        };

                // Путь к шаблону договора
                string templateFilePath = "C:\\Users\\microily\\source\\repos\\HashPasswords\\Dereev_21.101\\Resources\\blank-trudovogo-dogovora.docx";
                // Путь для сохранения нового договора
                string newFilePath = "C:\\Users\\microily\\Desktop\\заполненный_договор.docx";

                // Создаем экземпляр приложения Word
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                // Создаем новый документ Word
                Microsoft.Office.Interop.Word.Document wordDoc = wordApp.Documents.Open(templateFilePath);

                // Заменяем метки в шаблоне на соответствующие значения
                foreach (var item in items)
                {
                    Microsoft.Office.Interop.Word.Find find = wordApp.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    object wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    object replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    find.Execute(FindText: Type.Missing,
                    MatchCase: false, MatchWholeWord: false, MatchWildcards: false,
                    MatchSoundsLike: Type.Missing, MatchAllWordForms: false, Forward: true,
                    Wrap: wrap, Format: false, ReplaceWith: Type.Missing, Replace: replace);
                }

                // Сохраняем новый договор
                wordDoc.SaveAs2(newFilePath);
                wordDoc.Close();
                wordApp.Quit();

                MessageBox.Show("Документ успешно создан и сохранен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании документа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}