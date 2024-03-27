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
        private PdfDocument PdfDocument { get; set; }


        public void CombineMultiplePDFs(string[] fileNames, string outFile)
        {
            // step 1: creation of a document-object
            document = new Document();
            //create newFileStream object which will be disposed at the end
            using (FileStream newFileStream = new FileStream(outFile, FileMode.Create))
            {
                // step 2: we create a writer that listens to the document
                pdfCopyProvider = new PdfCopy(document, newFileStream);

                // step 3: we open the document
                document.Open();

                foreach (string fileName in fileNames)
                {
                    // we create a reader for a certain document
                    reader = new PdfReader(fileName);
                    reader.ConsolidateNamedDestinations();

                    // step 4: we add content
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

                // step 5: we close the document and writer
                pdfCopyProvider.Close();
                document.Close();
            }//disposes the newFileStream object
        }

       
    }
}
