using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorSQLite
{
    public class File : INotifyPropertyChanged
    {
        private string name;
        private string format;
        private int size;
        private string dateOfCreation;
        private string dateOfChange;
        private byte[] fileBytes;

        public int Id { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Format
        {
            get { return format; }
            set
            {
                format = value;
                OnPropertyChanged("Format");
            }
        }
        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }

        public string DateOfCreation
        {
            get { return dateOfCreation; }
            set
            {
                dateOfCreation = value;
                OnPropertyChanged("DateOfCreation");
            }
        }
        public string DateOfChange
        {
            get { return dateOfChange; }
            set
            {
                dateOfChange = value;
                OnPropertyChanged("DateOfChange");
            }
        }
        public byte[] FileBytes
        {
            get
            {
                return fileBytes;
            }
            set
            {
                fileBytes = value;
                OnPropertyChanged("FileBytes");
            }
        }

        public File() { }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
