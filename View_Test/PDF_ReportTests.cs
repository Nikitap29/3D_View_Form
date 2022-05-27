using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;

namespace _3D_View_Form.Tests
{
    [TestClass()]
    public class PDF_ReportTests
    {
        [TestMethod()]
        public void GenerateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Make_SnapshotTest()
        {
            MemoryStream str = new MemoryStream();
            Image bmp = 
            PDF_Report.Check_Img();
        }
    }
}