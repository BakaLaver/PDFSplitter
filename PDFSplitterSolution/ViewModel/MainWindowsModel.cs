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
        private PDFMergeService MergeService { get; set; }



        public MainWindowsModel(SplitPDFFromTo fromToModel, MergePDF mergePDFModel, PDFService takePagesService, PDFMergeService mergeService) 
        {
            FromToModel = fromToModel;
            MergePDFModel = mergePDFModel;
            TakePagesService = takePagesService;
            MergeService = mergeService;
        }

        #region FromToModel
        private RelayCommand _selectFromToInFileCommand;
        private RelayCommand _selectFromToOutFileCommand;
        private RelayCommand _takePagesFromToCommand;
        private RelayCommand _dragAndDropToCommand;
        public SplitPDFFromTo FromToModel {  get; set; }
        private void FromToCall()
        {
            string outPath = FromToModel.OutPutPath + @"\" + FromToModel.NewDocumentName + ".pdf";
            TakePagesService.ExtractPageFromTo(FromToModel.InPutPath, outPath, FromToModel.From, FromToModel.To);
        }

        public RelayCommand DragAndDropToCommand
        {
            get
            {
                return _dragAndDropToCommand ??
                  (_dragAndDropToCommand = new RelayCommand(obj =>
                  {
                      if (obj is DragEventArgs e)
                      {
                          if (e.Data.GetDataPresent(DataFormats.FileDrop))
                          {
                              string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                              if (files.Length > 0 && CheckDropedFile(files[0]))
                              {
                                  FromToModel.InPutPath = files[0];
                              }
                              else
                              {
                                  MessageBox.Show("Допустимы только PDF файлы");
                              }
                          }
                      }
                  }));
            }
        }

        private bool CheckDropedFile(string path)
        {
            bool result = false;

            FileInfo fileInf = new FileInfo(path);

            if (fileInf.Extension == ".pdf")
            {
                result = true;
            }
            return result;
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
                      var fullPath = FromToModel.OutPutPath + @"\" + FromToModel.NewDocumentName + ".pdf";
                      OpenFolerQuestion(fullPath);
                  }));
            }
        }

        #endregion

        #region MergeFileModel
        private RelayCommand _selectFirstFileCommand;
        private RelayCommand _selectSecondFileCommand;
        private RelayCommand _outPutFolderCommand;
        private RelayCommand _mergeFilesCommand;
        public MergePDF MergePDFModel { get; set; }

        public RelayCommand SelectFirstFileCommand
        {
            get
            {
                return _selectFirstFileCommand ??
                  (_selectFirstFileCommand = new RelayCommand(obj =>
                  {
                      MergePDFModel.Document1Path = SelectSourceFile();
                  }));
            }
        }

        public RelayCommand SelectSecondFileCommand
        {
            get
            {
                return _selectSecondFileCommand ??
                  (_selectSecondFileCommand = new RelayCommand(obj =>
                  {
                      MergePDFModel.Document2Path = SelectSourceFile();
                  }));
            }
        }

        public RelayCommand OutPutFolderCommand
        {
            get
            {
                return _outPutFolderCommand ??
                  (_outPutFolderCommand = new RelayCommand(obj =>
                  {
                      MergePDFModel.OutPutPath = SelectOutFlder();
                  }));
            }
        }

        public RelayCommand MergeFilesCommand
        {
            get
            {
                return _mergeFilesCommand ??
                  (_mergeFilesCommand = new RelayCommand(obj =>
                  {
                      var fullPath = MergePDFModel.OutPutPath + @"\" + MergePDFModel.NewDocumentName + ".pdf";
                      MergeService.MergePDF(MakePathToArray(MergePDFModel.Document1Path, MergePDFModel.Document2Path), fullPath);
                      OpenFolerQuestion(fullPath);
                  }));
            }
        }

        private List<string> MakePathToArray(string firstDoc, string secondDoc) 
        {
            List<string> pathArray = new List<string>();
            pathArray.Add(firstDoc);
            pathArray.Add(secondDoc);
            return pathArray;
        }



        #endregion

        private void OpenFolerQuestion(string path) 
        {
            var dialogResult = MessageBox.Show("Открыть папку с документом?", "Готово!", MessageBoxButton.YesNo);
            string fullPath = path;
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
            string path = "";
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok) 
            {
                path = dialog.FileName;
            }
            return path;
        }




    }
}
