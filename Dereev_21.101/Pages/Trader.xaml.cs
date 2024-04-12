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
using Dereev_21._101.Models;

namespace Dereev_21._101.Pages
{
    /// <summary>
    /// Класс Trader представляет страницу для роли "Trader" и взаимодействует с элементами интерфейса.
    /// </summary>
    public partial class Trader : Page
    {
        Time time = new Time(); // Объект класса Time для работы с текущим временем

        /// <summary>
        /// Конструктор класса Trader. Инициализирует компоненты страницы и отображает приветствие для работника.
        /// </summary>
        /// <param name="workers">Объект Workers, представляющий информацию о работнике</param>
        public Trader(Workers workers)
        {
            InitializeComponent();

            // Отображение приветствия с учетом текущего времени и имени работника
            Hello.Text = $"{time.get_time()} {workers.Name} {workers.Surname}";

            // Отображение элементов интерфейса
            OK.Visibility = Visibility.Visible;
            Hello.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Обработчик события клика по кнопке "OK". Скрывает элементы интерфейса.
        /// </summary>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            OK.Visibility = Visibility.Hidden;
            Hello.Visibility = Visibility.Hidden;
        }
    }
}
