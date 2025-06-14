using iTextSharp.text;
using iTextSharp.text.pdf;
using PDFSplitter.BusinessLogic.BusinessModels;
using System.Reflection.PortableExecutable;

namespace PDFSplitter.BusinessLogic.Services
{
    public class PDFService
    {
        private TextCharpField _textCharpFields { get; set; }

        public PDFService(TextCharpField textCharpField) 
        {
            _textCharpFields = textCharpField;
        }
        
        public void ExtractPageFromTo(string sourcePDFpath, string outputPDFpath, int startPage, int endPage) 
        {
            SetFealds(sourcePDFpath, outputPDFpath, startPage);

            _textCharpFields.SourceDocument.Open();

            for (int i = startPage; i <= endPage; i++)
            {
                _textCharpFields.ImportedPage = _textCharpFields.PdfCopyProvider.GetImportedPage(_textCharpFields.Reader, i);
                _textCharpFields.PdfCopyProvider.AddPage(_textCharpFields.ImportedPage);
            }
            _textCharpFields.SourceDocument.Close();
            _textCharpFields.Reader.Close();
        }

        private void SetFealds(string sourcePDFpath, string outputPDFpath, int startPage) 
        {
            _textCharpFields.Reader = new PdfReader(sourcePDFpath);
            _textCharpFields.SourceDocument = new Document(_textCharpFields.Reader.GetPageSizeWithRotation(startPage));
            _textCharpFields.PdfCopyProvider = new PdfCopy(_textCharpFields.SourceDocument, new System.IO.FileStream(outputPDFpath, System.IO.FileMode.Create));
        }
    }
}
