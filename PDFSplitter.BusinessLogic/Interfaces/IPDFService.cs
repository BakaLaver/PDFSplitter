﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSplitter.BusinessLogic.Interfaces
{
    public interface IPDFService
    {
        void ExtractPageFromTo(string sourcePDFpath, string outputPDFpath, int startpage, int endpage);
        void ExtractOnePage(string sourcePDFpath, string outputPDFpath, int page);
    }
}
