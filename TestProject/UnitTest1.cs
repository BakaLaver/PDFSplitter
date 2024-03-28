using PDFSplitter.BusinessLogic.BusninessModels;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            CombineDocument combineDocument = new CombineDocument();

            string[] stringsPath = new string[] { @"F:\Downloads\PDF\teest.pdf", @"F:\Downloads\PDF\test.pdf" };
            string outPut = @"F:\Downloads\PDF\test3.pdf";
            //combineDocument.CombineMultiplePDFs(stringsPath, outPut);
        }
    }
}