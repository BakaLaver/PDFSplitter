using PDFSplitter.BusinessLogic.BusinessModels;
using PDFSplitter.BusinessLogic.BusninessModels;
using PDFSplitter.BusinessLogic.Services;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test2()
        {
            CombineDocument combineDocument = new CombineDocument();

            string[] stringsPath = new string[] { @"F:\Downloads\PDF\teest.pdf", @"F:\Downloads\PDF\test.pdf" };
            string outPut = @"F:\Downloads\PDF\test3.pdf";
            //combineDocument.CombineMultiplePDFs(stringsPath, outPut);
        }

        [Test]
        public void Test1()
        {
            var TextCharpModel = new TextCharpField();

            PDFService service = new PDFService(TextCharpModel);

            service.ExtractPageFromTo(@"D:\\TestPDF\test.pdf", @"D:\\TestPDF\test1.pdf", 2, 5);
        }
    }
}