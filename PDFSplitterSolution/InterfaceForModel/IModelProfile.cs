﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFSplitter.InterfaceForModel
{
     public interface IModelProfile
    {
        string InPutPath { get; set; }
        string OutPutPath { get; set; }
        public string NewDocumentName { get; set; }
    }
}
