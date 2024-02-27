using FileSelection = Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PDFSplitter.BusinessLogic.Services;
using PDFSplitter.Model;
using PDFSplitter.ViewModel.Command;
//using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PDFSplitter.InterfaceForModel;
using System.Windows;

namespace PDFSplitter.ViewModel
{
    public class MainWindowsModel 
    {

        private PDFService TakePagesService { get; set; }


        public MainWindowsModel() 
        {
            FromToModel = new SplitPDFFromTo();
            TakePagesService = new PDFService();
            OnePageModel = new TakeOnePage();
        }

        #region OnePageModel
        private RelayCommand _selectOnePageInFileCommand;
        private RelayCommand _selectOnePageOutFileCommand;
        private RelayCommand _takeOnePageCommand;
        public TakeOnePage OnePageModel { get; set; }

        public RelayCommand SelectOnePageInFileCommand
        {
            get
            {
                return _selectOnePageInFileCommand ??
                  (_selectOnePageInFileCommand = new RelayCommand(obj =>
                  {
                      OnePageModel.InPutPath = SelectSourceFile();
                  }));
            }
        }
        public RelayCommand SelectOnePageOutFileCommand
        {
            get
            {
                return _selectOnePageOutFileCommand ??
                  (_selectOnePageOutFileCommand = new RelayCommand(obj =>
                  {
                      OnePageModel.OutPutPath = SelectOutFlder();
                  }));
            }
        }

        public RelayCommand TakeOnePageCommand
        {
            get
            {
                return _takeOnePageCommand ??
                  (_takeOnePageCommand = new RelayCommand(obj =>
                  {
                      OnePageCall();
                      OpenFolerQuestion(OnePageModel.OutPutPath, OnePageModel);
                  }));
            }
        }

        private void OnePageCall()
        {
            string outPath = OnePageModel.OutPutPath + @"\" + OnePageModel.NewDocumentName + ".pdf";
            TakePagesService.ExtractOnePage(OnePageModel.InPutPath, outPath, OnePageModel.NumberOfPage);
        }

        #endregion

        #region FromToModel
        private RelayCommand _selectFromToInFileCommand;
        private RelayCommand _selectFromToOutFileCommand;
        private RelayCommand _takePagesFromToCommand;
        public SplitPDFFromTo FromToModel {  get; set; }
        private void FromToCall()
        {
            string outPath = FromToModel.OutPutPath + @"\" + FromToModel.NewDocumentName + ".pdf";
            TakePagesService.ExtractPageFromTo(FromToModel.InPutPath, outPath, FromToModel.From, FromToModel.To);
        }

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
                      OpenFolerQuestion(OnePageModel.OutPutPath, FromToModel);
                  }));
            }
        }

        #endregion 

        private void OpenFolerQuestion(string path, IModelProfile profile) 
        {
            var dialogResult = MessageBox.Show("Открыть папку с документом?", "Готово!", MessageBoxButton.YesNo);
            string fullName = profile.NewDocumentName + ".pdf";
            string fullPath = path + @"\" + fullName;
            if (dialogResult == MessageBoxResult.Yes)
            {
                fullPath = System.IO.Path.GetFullPath(fullPath);
                System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", fullPath));
            }
        }
        private string SelectSourceFile()
        {
            string path = "" ;
            FileSelection.OpenFileDialog op = new FileSelection.OpenFileDialog();
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
