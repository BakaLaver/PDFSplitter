using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDFSplitter.BusinessLogic.BusinessModels
{
    public class TextCharpField
    {
        public PdfReader Reader { get; set; }
        public Document SourceDocument { get; set; }
        public PdfCopy PdfCopyProvider { get; set; }
        public PdfImportedPage ImportedPage { get; set; }
    }
}
