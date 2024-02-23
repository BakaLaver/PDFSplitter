using PDFSplitter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PDFSplitter.ViewModel
{
    public class MainWindowsModel 
    {
        public SplitPDFFromTo FromToModel {  get; set; }
        public MainWindowsModel() 
        {
            FromToModel = new SplitPDFFromTo();
        }

       
    }
}
