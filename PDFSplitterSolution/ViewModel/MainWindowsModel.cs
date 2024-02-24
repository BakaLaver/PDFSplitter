using Microsoft.Win32;
using PDFSplitter.BusinessLogic.Services;
using PDFSplitter.Model;
using PDFSplitter.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PDFSplitter.ViewModel
{
    public class MainWindowsModel 
    {
        private RelayCommand _selectInFileCommand;
        private RelayCommand _selectOutFileCommand;
        private RelayCommand _takePageFromToCommand;
        public SplitPDFFromTo FromToModel {  get; set; }

        private PDFService FromToService { get; set; }

        public MainWindowsModel() 
        {
            FromToModel = new SplitPDFFromTo();
            FromToService = new PDFService();
        }

        public RelayCommand SelectInFileCommand
        {
            get
            {
                return _selectInFileCommand ??
                  (_selectInFileCommand = new RelayCommand(obj =>
                  {
                      FromToModel.InPutPath = ExecuteOpemFile();
                  }));
            }
        }
        public RelayCommand SelectOutFileCommand
        {
            get
            {
                return _selectOutFileCommand ??
                  (_selectOutFileCommand = new RelayCommand(obj =>
                  {
                      FromToModel.OutPutPath = (string)ExecuteOpemFile();
                  }));
            }
        }

        public RelayCommand TakePageFromToCommand
        {
            get
            {
                return _takePageFromToCommand ??
                  (_takePageFromToCommand = new RelayCommand(obj =>
                  {
                      FromToService.ExtractPageFromTo(FromToModel.InPutPath, FromToModel.OutPutPath + @"\" + FromToModel.NewDocumentName + ".pdf", FromToModel.From, FromToModel.To);
                  }));
            }
        }

        private string ExecuteOpemFile()
        {
            string path = "" ;
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "PDFfile|*.pdf";
            op.DefaultExt = "pdf";

            if (op.ShowDialog() == true)
            {
                path = op.FileName;
            }
            return path;
        }


    }
}
