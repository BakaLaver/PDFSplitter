using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSplitter.BusinessLogic.BusninessModels
{
    public class CombineDocument
    {
        private PdfReader reader { get; set; }
        private Document document { get; set; }
        private PdfCopy pdfCopyProvider { get; set; }
        private PdfImportedPage importedPage { get; set; }


        public void CombineMultiplePDFs(List<string> fileNames, string outFile)
        {
            document = new Document();
            using (FileStream newFileStream = new FileStream(outFile, FileMode.Create))
            {
                pdfCopyProvider = new PdfCopy(document, newFileStream);

                document.Open();

                foreach (string fileName in fileNames)
                {
                    reader = new PdfReader(fileName);
                    reader.ConsolidateNamedDestinations();

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                        pdfCopyProvider.AddPage(importedPage);
                    }

                    PRAcroForm form = reader.AcroForm;
                    if (form != null)
                    {
                        pdfCopyProvider.AddDocument(reader);
                    }

                    reader.Close();
                }

                pdfCopyProvider.Close();
                document.Close();
            }
        }

       
    }
}
