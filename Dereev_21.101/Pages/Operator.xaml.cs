﻿using Dereev_21._101.Models;
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


namespace Dereev_21._101.Pages
{
    /// <summary>
    /// Логика взаимодействия для Operator.xaml
    /// </summary>
    public partial class Operator : Page
    {
        Time time = new Time();
        public Operator(Workers workers)
        {
            InitializeComponent();
            Hello.Text = $"{time.get_time()} {workers.Name} {workers.Surname}";
            OK.Visibility = Visibility.Visible;
            Hello.Visibility = Visibility.Visible;

        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            OK.Visibility = Visibility.Collapsed;
            Hello.Visibility = Visibility.Hidden;
        }
    }
}
