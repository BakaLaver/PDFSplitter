using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDFSplitter.BusinessLogic.BusninessModels
{
    public class SplitPage
    {
        private PdfReader reader { get; set; }
        private Document sourceDocument { get; set; }
        private PdfCopy pdfCopyProvider { get; set; }
        private PdfImportedPage importedPage { get; set; }

        public void OnePageExtract(string sourcePDFpath, string outputPDFpath, int page)
        {
            reader = new PdfReader(sourcePDFpath);
            sourceDocument = new Document(reader.GetPageSize(page));
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPDFpath, System.IO.FileMode.Create));

            sourceDocument.Open();

            importedPage = pdfCopyProvider.GetImportedPage(reader, page);
            pdfCopyProvider.AddPage(importedPage);

            sourceDocument.Close();
            reader.Close();
        }
        public void ExtractPages(string sourcePDFpath, string outputPDFpath, int startpage, int endpage)
        {
            reader = new PdfReader(sourcePDFpath);
            sourceDocument = new Document(reader.GetPageSizeWithRotation(startpage));
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPDFpath, System.IO.FileMode.Create));

            sourceDocument.Open();

            for (int i = startpage; i <= endpage; i++)
            {
                importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                pdfCopyProvider.AddPage(importedPage);
            }
            sourceDocument.Close();
            reader.Close();
        }
    }
}
