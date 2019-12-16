using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditorSQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateNewDocument(object sender, RoutedEventArgs e)
        {
            var textRange = new TextRange(this.textEditor.Document.ContentStart, this.textEditor.Document.ContentEnd);
            if (textRange.Text.Trim().Length > 0)
            {
                var askSave = new AskSaveDialog();
                askSave.ShowDialog();
                if(askSave.DialogResult == true)
                {
                    SaveOnDbDialog saveDialog = new SaveOnDbDialog(textRange);
                    saveDialog.ShowDialog();
                }
            }
            this.textEditor.Document.Blocks.Clear();
        }

        private void SaveCurrentDocumentOnDb(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(this.textEditor.Document.ContentStart, this.textEditor.Document.ContentEnd);
            SaveOnDbDialog saveDialog = new SaveOnDbDialog(textRange);
            saveDialog.ShowDialog();

        }

        private void SaveCurrentDocumentOnStorage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.Filter =
                "Файл TXT (*.txt)|*.txt|RTF-files (*.rtf)|*.rtf";

            TextRange documentContent = new TextRange(this.textEditor.Document.ContentStart, this.textEditor.Document.ContentEnd);

            if (save.ShowDialog() == true)
            {
                using (FileStream fs = System.IO.File.Create(save.FileName))
                {
                    if (System.IO.Path.GetExtension(save.FileName).ToLower() == ".rtf")
                    {
                        documentContent.Save(fs, DataFormats.Rtf);
                    }
                    else
                    {
                        documentContent.Save(fs, DataFormats.Text);
                    }
                }
            }
        }

        private void OpenDocumentFromDb(object sender, RoutedEventArgs e)
        {
            OpenFileFromDbDialog openDialoag = new OpenFileFromDbDialog();
            openDialoag.ShowDialog();
            if(openDialoag.DialogResult == true)
            {
                var openingFile = openDialoag.SelectedFile;
                TextRange openDocument = new TextRange(textEditor.Document.ContentStart, textEditor.Document.ContentEnd);
                openDocument.Load(new MemoryStream(openingFile.FileBytes), DataFormats.Rtf);
                Console.WriteLine(new TextRange(textEditor.Document.ContentStart, textEditor.Document.ContentEnd).Text);
            }
        }

        private void OpenDocumentFromStorage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile =
                new Microsoft.Win32.OpenFileDialog();

            openFile.Filter = "txt files (*.txt) | *.txt| RTF - files(*.rtf) | *.rtf";
            if (openFile.ShowDialog() == true)
            {
                TextRange tr = new TextRange(
                       textEditor.Document.ContentStart, textEditor.Document.ContentEnd);

                using (FileStream fs = System.IO.File.Open(openFile.FileName, FileMode.Open))
                {
                    tr.Load(fs, DataFormats.Rtf);
                }
            }
        }

    }
}
