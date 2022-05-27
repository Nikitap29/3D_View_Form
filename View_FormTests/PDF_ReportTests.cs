using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3D_View_Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

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
        public void Check_ImgTest()
        {
            Bitmap bmp = new Bitmap(@"C:\Users\Elreg_00125\Desktop\Img\2.jpg");
            MemoryStream str = new MemoryStream();
            bmp.Save(str, System.Drawing.Imaging.ImageFormat.Png);
            bool res = PDF_Report.Check_Img(str);
            Assert.AreEqual(false, res);
        }

        [TestMethod()]
        public void Make_SnapshotTest()
        {
            Assert.Fail();
        }
    }
}