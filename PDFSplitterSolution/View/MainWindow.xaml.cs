using PDFSplitter.ViewModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace PDFSplitter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowsModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowsModel();
            DataContext = ViewModel;
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

        #region FromToLocation
        private void SplitPageDropFile_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
            {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (CheckDropedFile(file[0]))
                {
                    ViewModel.FromToModel.InPutPath = file[0];
                }
                else 
                {
                    MessageBox.Show("Допустимы только PDF файлы");
                }
            }
        }

        private void To_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void From_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void NewFileName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[.]+");
        }



        #endregion

        private void NewFileOnePageName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[.]+");
        }

        private void PageNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void NewNameAfterMerge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[.]+");
        }
    }
}