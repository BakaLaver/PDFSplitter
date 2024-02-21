using PDFSplitter.BusinessLogic.BusninessModels;
using PDFSplitter.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSplitter.BusinessLogic.Services
{
    public class PDFService:IPDFService
    {
        private SplitPage _SplitPageCommand;
        public PDFService() 
        {
            _SplitPageCommand = new SplitPage();
        }

        public void ExtractPageFromTo(string sourcePDFpath, string outputPDFpath, int startpage, int endpage) 
        {
            _SplitPageCommand.ExtractPages(sourcePDFpath, outputPDFpath, startpage, endpage);
        }

        public void ExtractOnePage(string sourcePDFpath, string outputPDFpath, int page) 
        {
            _SplitPageCommand.OnePageExtract(sourcePDFpath, outputPDFpath, page);
        }
    }
}
