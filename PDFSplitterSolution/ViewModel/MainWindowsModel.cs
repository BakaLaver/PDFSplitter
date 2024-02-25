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

        private PDFService TakePagesService { get; set; }
        
        public TakeOnePage OnePageModel { get; set; }

        public MainWindowsModel() 
        {
            FromToModel = new SplitPDFFromTo();
            TakePagesService = new PDFService();
            OnePageModel = new TakeOnePage();
        }

        private void FromToCall()
        {
            string outPath = FromToModel.OutPutPath + @"\" + FromToModel.NewDocumentName + ".pdf";
            TakePagesService.ExtractPageFromTo(FromToModel.InPutPath, outPath, FromToModel.From, FromToModel.To);
        }

        #region FromToModel
        private RelayCommand _selectFromToInFileCommand;
        private RelayCommand _selectFromToOutFileCommand;
        private RelayCommand _takePagesFromToCommand;
        public SplitPDFFromTo FromToModel {  get; set; }

        public RelayCommand SelectFromToInFileCommand
        {
            get
            {
                return _selectFromToInFileCommand ??
                  (_selectFromToInFileCommand = new RelayCommand(obj =>
                  {
                      FromToModel.InPutPath = SelectSourceFile();
                  }));
            }
        }
        public RelayCommand SelectFromToOutFileCommand
        {
            get
            {
                return _selectFromToOutFileCommand ??
                  (_selectFromToOutFileCommand = new RelayCommand(obj =>
                  {
                      FromToModel.OutPutPath = SelectOutFlder();
                  }));
            }
        }

        public RelayCommand TakePagesFromToCommand
        {
            get
            {
                return _takePagesFromToCommand ??
                  (_takePagesFromToCommand = new RelayCommand(obj =>
                  {
                      FromToCall();
                  }));
            }
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
        #endregion 


    }
}
