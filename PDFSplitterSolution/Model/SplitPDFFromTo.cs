using PDFSplitter.InterfaceForModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PDFSplitter.Model
{
    public class SplitPDFFromTo : INotifyPropertyChanged
    {
        private string _inPutPath;
        private string _outPutPath;
        private string _newDocumentName;
        private int _from;
        private int _to;

        public int From 
        {
            get { return _from; }
            set 
            { 
                _from = value;
                OnPropertyChanged("From");
            }
        }

        public int To 
        {
            get { return _to; }
            set 
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        public string NewDocumentName 
        {
            get { return _newDocumentName; } 
            set 
            {
                _newDocumentName = value;
                OnPropertyChanged("NewDocumentName");
            }
        }
        public string InPutPath 
        {
            get { return _inPutPath; } 
            set 
            {
                _inPutPath = value;
                OnPropertyChanged("InPutPath");
            }
        }

        public string OutPutPath 
        {
            get { return _outPutPath; }
            set 
            {
                _outPutPath = value;
                OnPropertyChanged("OutPutPath");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
