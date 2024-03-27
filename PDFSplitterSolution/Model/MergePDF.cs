using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PDFSplitter.Model
{
    public class MergePDF: INotifyPropertyChanged
    {
        private string _document1Path;
        private string _document2Path;
        private string _outPutPath;
        private string _newDocumentName;


        public string Document1Path
        {
            get { return _document1Path; }
            set
            {
                _document1Path = value;
                OnPropertyChanged("Document1Path");
            }
        }
        public string Document2Path
        {
            get { return _document2Path; }
            set
            {
                _document2Path = value;
                OnPropertyChanged("Document2Path");
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
