using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;

namespace TextEditorSQLite
{
    /// <summary>
    /// Interaction logic for OpenFileFromDb.xaml
    /// </summary>
    public partial class OpenFileFromDbDialog : Window
    {
        public List<File> CurrentFiles { get; }
        public File SelectedFile { get; set; }
        public OpenFileFromDbDialog()
        {
            ApplicationContext.Instance.Files.LoadAsync().Wait();
            CurrentFiles = ApplicationContext.Instance.Files.ToListAsync<File>().Result;
            this.DataContext = this;
            InitializeComponent();
        }

        private void FilesGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedFile = (File)((DataGrid)sender).SelectedItem;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = this.SelectedFile != null;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
