using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
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
    /// Interaction logic for SaveOnDbDialog.xaml
    /// </summary>
    public partial class SaveOnDbDialog : Window
    {
        private TextRange DocumentContent { get; set; }
        public ObservableCollection<string> FileFormats { get; private set; } = new ObservableCollection<string> { ".txt", ".rtf" };
        public List<File> CurrentFiles { get; }
        public SaveOnDbDialog(TextRange textRange)
        {
            this.DataContext = this;
            ApplicationContext.Instance.Files.LoadAsync().Wait();
            CurrentFiles = ApplicationContext.Instance.Files.ToListAsync<File>().Result;
            DocumentContent = textRange;
            InitializeComponent();
            this.formatsList.SelectedIndex = 0;
        }

        private async void SaveOnDb_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationContext.Instance.Files.Count(item => (item.Name == fileName.Text) && (item.Format == this.formatsList.SelectedItem.ToString())) > 0)
            {
                var fileExistDialog = new FileExistDialog();
                fileExistDialog.ShowDialog();
                if(fileExistDialog.DialogResult == true)
                {
                    var file = ApplicationContext.Instance.Files.First(item => (item.Name == fileName.Text) && (item.Format == this.formatsList.SelectedItem.ToString()));
                    ApplicationContext.Instance.Files.Remove(file);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        if (this.formatsList.SelectedItem.ToString() == ".rtf")
                        {
                            this.DocumentContent.Save(ms, DataFormats.Rtf);
                        }
                        else
                        {
                            this.DocumentContent.Save(ms, DataFormats.Text);
                        }
                        var arr = new byte[ms.Length];
                        arr = ms.GetBuffer();
                        ApplicationContext.Instance.Files.Add(new File()
                        {
                            Name = this.fileName.Text,
                            Format = formatsList.SelectedItem.ToString(),
                            DateOfCreation = DateTime.Now.ToString(),
                            DateOfChange = DateTime.Now.ToString(),
                            Size = (int)arr.Length,
                            FileBytes = arr
                        });
                        await ApplicationContext.Instance.SaveChangesAsync();
                        this.DialogResult = true;
                    }
                    
                }
            }
            else
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    if (this.formatsList.SelectedItem.ToString() == ".rtf")
                    {
                        this.DocumentContent.Save(ms, DataFormats.Rtf);
                    }
                    else
                    {
                        this.DocumentContent.Save(ms, DataFormats.Text);
                    }
                    var arr = new byte[ms.Length];
                    arr = ms.GetBuffer();
                    ApplicationContext.Instance.Files.Add(new File()
                    {
                        Name = this.fileName.Text,
                        Format = formatsList.SelectedItem.ToString(),
                        DateOfCreation = DateTime.Now.ToString(),
                        DateOfChange = DateTime.Now.ToString(),
                        Size = (int)arr.Length,
                        FileBytes = arr
                    });
                    await ApplicationContext.Instance.SaveChangesAsync();
                    this.DialogResult = true;
                }
            }
        }

        private void FilesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFile = (File)((DataGrid)sender).SelectedItem;
            this.fileName.Text = selectedFile.Name;
            this.formatsList.SelectedIndex = this.FileFormats.IndexOf(selectedFile.Format);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
