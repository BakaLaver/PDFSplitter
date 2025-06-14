using iTextSharp.text;
using iTextSharp.text.pdf;
using PDFSplitter.BusinessLogic.BusinessModels;

namespace PDFSplitter.BusinessLogic.Services
{
    public class PDFMergeService
    {
        private TextCharpField _textCharpFields { get; set; }

        public PDFMergeService(TextCharpField textCharpField)
        {
            _textCharpFields = textCharpField;
        }

        public void MergePDF(List<string> fileNames, string outFile)
        {
            using (FileStream newFileStream = new FileStream(outFile, FileMode.Create))
            {
                SetFealds(newFileStream);
                _textCharpFields.SourceDocument.Open();

                foreach (string fileName in fileNames)
                {
                    _textCharpFields.Reader = new PdfReader(fileName);
                    _textCharpFields.Reader.ConsolidateNamedDestinations();

                    for (int i = 1; i <= _textCharpFields.Reader.NumberOfPages; i++)
                    {
                        _textCharpFields.ImportedPage = _textCharpFields.PdfCopyProvider.GetImportedPage(_textCharpFields.Reader, i);
                        _textCharpFields.PdfCopyProvider.AddPage(_textCharpFields.ImportedPage);
                    }

                    PRAcroForm form = _textCharpFields.Reader.AcroForm;
                    if (form != null)
                    {
                        _textCharpFields.PdfCopyProvider.AddDocument(_textCharpFields.Reader);
                    }

                    _textCharpFields.Reader.Close();
                }

                _textCharpFields.PdfCopyProvider.Close();
                _textCharpFields.SourceDocument.Close();
            }
        }
        private void SetFealds(FileStream newFileStream)
        {
            _textCharpFields.SourceDocument = new Document();
            _textCharpFields.PdfCopyProvider = new PdfCopy(_textCharpFields.SourceDocument, newFileStream);
        }
    }
}
