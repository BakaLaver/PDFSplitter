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
    public class TakeOnePage : INotifyPropertyChanged, IModelProfile
    {
        private string _inPutPath;
        private string _outPutPath;
        private string _newDocumentName;
        private int _numberOfPage;

        public int NumberOfPage
        {
            get { return _numberOfPage; }
            set
            {
                _numberOfPage = value;
                OnPropertyChanged("From");
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
