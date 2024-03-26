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
        }

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
                      SelectOutFlder(FromToModel);
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
                      OpenFolerQuestion(FromToModel.OutPutPath, FromToModel);
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


        private void SelectOutFlder(IModelProfile profile) 
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok) 
            {
                profile.OutPutPath = dialog.FileName;
            }
        }




    }
}
