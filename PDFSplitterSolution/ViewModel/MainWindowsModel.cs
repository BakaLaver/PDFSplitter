using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
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
                      FromToModel.InPutPath = SelectSourceFile();
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
                      FromToModel.OutPutPath = SelectOutFlder();
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
                      FromToCall();
                  }));
            }
        }

        private void FromToCall() 
        {
            string outPath = FromToModel.OutPutPath + @"\" + FromToModel.NewDocumentName + ".pdf";
            FromToService.ExtractPageFromTo(FromToModel.InPutPath, outPath, FromToModel.From, FromToModel.To);
        }

        private string SelectSourceFile()
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


        private string SelectOutFlder() 
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            return dialog.FileName;
        }

    }
}
