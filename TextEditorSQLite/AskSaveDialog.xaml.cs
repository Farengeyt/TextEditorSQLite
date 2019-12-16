﻿using System;
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
using System.Windows.Shapes;

namespace TextEditorSQLite
{
    /// <summary>
    /// Interaction logic for AskSaveDialog.xaml
    /// </summary>
    public partial class AskSaveDialog : Window
    {
        public AskSaveDialog()
        {
            InitializeComponent();
        }
        private void No_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Yes_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
