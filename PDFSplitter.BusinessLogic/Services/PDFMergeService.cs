using PDFSplitter.BusinessLogic.BusninessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSplitter.BusinessLogic.Services
{
    public class PDFMergeService
    {
        private CombineDocument _combineDocument;

        public PDFMergeService() 
        {
            _combineDocument = new CombineDocument();
        }

        public void MergePDF(List<string> Paths, string outPut) 
        {
            _combineDocument.CombineMultiplePDFs(Paths, outPut);
        }
    }
}
