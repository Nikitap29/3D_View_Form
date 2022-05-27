using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Drawing;
using System.IO;
using System.Text.Json;

namespace _3D_View_Form
{
    /*----------------------------23.06.2021---------------------------------------------------
     - добавлена функция проверки масштаба по белым пикселям
     - добавлена структура MyStruct, которая содержит поток с изображением и коэффициенты 
    масштабирования относительно исходного изображения. Также добавлены 4 переменных данной структуры
    для 4 изображений в отчете
    1 добавлен параметр "допуск" в отчет
-   2 добавлено умножение на коэффициент координат из-за того, что изображение вырезается
-   3 добавлены ед. изм. (мм)
    4 добавлен параметр масштабирования
    ----------------------------24.06.2021---------------------------------------------------
    1 добавил округление чисел на координатных осях
    2 переименовал структуру хранения изображений в ImgData
    3 добавил в структуру ImgData значения размеров области
    4 изменил формулу расчета значений по осям - заменил размер окна на значения размера из структуры ImgData
    5 увеличил область просмотра процедурой проверки масштаба Check_Img
    6 увеличил толщину линий на осях координат в отчете
    7 добавил к пути поиска эталонов папку etalons\
    ----------------------------25.06.2021---------------------------------------------------
    1 реализовал выравнивание по краям относительно наибольшего измерения Х или Y 
    2 уменьшил белую рамку вокруг детали
    ----------------------------01.07.2021---------------------------------------------------
    1 добавил функцию для округления масштабов осей
    2 увеличил белую рамку вокруг детали
    3 изменил начальное значения коэффициента масштабирования с -1 на 0
    ----------------------------02.07.2021---------------------------------------------------
    1 квадратный формат изображения вместо прямоугольного
    2 исправил баг с повторяющимися величинами по краям
    3 убрал параметры path у функций 
    */

    /// <summary>
    /// Отчет по сравнению
    /// </summary>
    public class PDF_Report
    {
        /// <summary>
        /// Объект для создания PDF
        /// </summary>
        static XGraphics gfx;
        /// <summary>
        /// Координата X листа
        /// </summary>
        static int x = 50;
        /// <summary>
        /// Координата Y листа
        /// </summary>
        static int y = 50;
        /// <summary>
        /// Шрифт для простого текста
        /// </summary>
        static XFont simpleFont = new XFont("Times New Roman", 12, XFontStyle.Regular);
        /// <summary>
        /// Шрифт для заголовка
        /// </summary>
        static XFont headerFont = new XFont("Times New Roman", 20, XFontStyle.Bold);
        /// <summary>
        /// Поток для изображения 1
        /// </summary>
        public static MemoryStream b1;
        /// <summary>
        /// Поток для изображения 2
        /// </summary>
        public static MemoryStream b2;
        /// <summary>
        /// Поток для изображения 3
        /// </summary>
        public static MemoryStream b3;
        /// <summary>
        /// Поток для изображения 4
        /// </summary>
        public static MemoryStream b4;
        /// <summary>
        /// Страница PDF
        /// </summary>
        static PdfPage page;
        /// <summary>
        /// Поток для лого
        /// </summary>
        public static MemoryStream logo;

        /// <summary>
        /// Генерация отчета
        /// </summary>
        /// <param name="mm"></param>
        /// <param name="liters"></param>
        /// <param name="img_path1"></param>
        /// <param name="img_path2"></param>
        /// <param name="out_path"></param>
        /// <returns></returns>
        public static string Generate(double inMM, double outMM, double inL, double outL,
            string detail, string folder, double win, double eps)
        {
            string etalon = folder + @"etalons\" + detail + @"\etalon.obj";
            if (!File.Exists(etalon))
            {
                return "File not exist";
            }
            DateTime dt = File.GetCreationTime(etalon);
            string dtString = Get_Data(dt.Day) + "." + Get_Data(dt.Month) + "." + dt.Year.ToString() + " "
                + dt.Hour.ToString() + ":" + Get_Data(dt.Minute);
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Отчёт по сканированию детали " + detail;
            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            Add_Title(detail);
            logo = new MemoryStream();
            Bitmap bmp = new Bitmap(Properties.Resources.logo);
            bmp.Save(logo, System.Drawing.Imaging.ImageFormat.Png);
            Add_Image(logo, 88, 88, 500, 0);
            string nowDate = Get_Data(DateTime.Now.Day) + "." + Get_Data(DateTime.Now.Month) + "." + DateTime.Now.Year.ToString();
            string nowTime = DateTime.Now.Hour.ToString() + ":" + Get_Data(DateTime.Now.Minute);
            double absL = Math.Abs(inL) + Math.Abs(outL);
            Add_Line("Дата/время измерения: " + nowDate + " " + nowTime);
            Add_Line("Дата/время формирования эталона: " + dtString);
            Add_Line("Максимальное отклонение от эталона в большую сторону, мм: " + outMM);
            Add_Line("Максимальное отклонение от эталона в меньшую сторону, мм: " + inMM);
            Add_Line("Объемное отклонение от эталона в большую сторону, мм3: " + outL);
            Add_Line("Объемное отклонение от эталона в меньшую сторону, мм3: " + inL);
            Add_Line("Абсолютное отклонение от эталона, мм3: " + absL.ToString());
            Add_Line("Допуск, учитываемый при сравнении, мм: " + eps.ToString());
            Add_Image(m1.ms, 200, 200, 030, 300, Calc_Ceil(m1.lX * m1.mX), Calc_Ceil(m1.lY * m1.mY), true);
            Add_Image(m2.ms, 200, 200, 030, 600, Calc_Ceil(m1.lX * m1.mX), Calc_Ceil(m1.lY * m1.mY), true);
            Add_Image(m3.ms, 200, 200, 310, 300, Calc_Ceil(m1.lX * m1.mX), Calc_Ceil(m1.lY * m1.mY), true);
            Add_Image(m4.ms, 200, 200, 310, 600, Calc_Ceil(m1.lX * m1.mX), Calc_Ceil(m1.lY * m1.mY), true);
            string filename = nowDate.Replace(".", "") + "_" + nowTime.Replace(":", "") + "_report.pdf";
            string out_path = folder + @"\report\";
            if (!Directory.Exists(out_path))
            {
                Directory.CreateDirectory(out_path);
            }
            document.Save(out_path + filename);
            return out_path + filename;
        }

        /// <summary>
        /// Округление и приведение к нужному формату подписи оси
        /// </summary>
        /// <param name="inVal"></param>
        /// <returns></returns>
        static string Calc_Ceil(double inVal)
        {
            string outVal = (Math.Round(inVal / 100) * 100).ToString() + " мм";
            //string outVal = Math.Round(inVal).ToString() + " мм";
            return outVal;
        }

        /// <summary>
        /// Формирование даты
        /// </summary>
        /// <param name="_in"></param>
        /// <returns></returns>
        static string Get_Data(int _in)
        {
            string _out;
            if (_in < 10) _out = "0" + _in.ToString();
            else _out = _in.ToString();
            return _out;
        }

        /// <summary>
        /// Добавление строки текста в отчет
        /// </summary>
        /// <param name="text"></param>
        static void Add_Line(string text)
        {
            gfx.DrawString(text, simpleFont, XBrushes.Black, x, y, XStringFormats.Default);
            y += 20;
        }

        /// /// <summary>
        /// Добавление заголовка в отчет
        /// </summary>
        /// <param name="text"></param>
        static void Add_Title(string text)
        {
            gfx.DrawString("Отчет по сканированию детали", headerFont, XBrushes.Black, new XRect(30, 30, page.Width - 30, page.Height - 30), XStringFormats.TopCenter);
            gfx.DrawString(text, headerFont, XBrushes.Black, new XRect(30, 60, page.Width - 30, page.Height - 30), XStringFormats.TopCenter);
            y += 60;
        }

        /// <summary>
        /// Добавление картинки в отчет из потока
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        static void Add_Image(MemoryStream strm, int width, int height, int x, int y,
            string win1 = "", string win2 = "", bool line = false)
        {
            XPen pen = new XPen(XColor.FromArgb(0, 0, 0), 3);
            XImage image = XImage.FromStream(strm);
            if (line)
            {
                gfx.DrawLine(pen, x, y, x, y + height);
                gfx.DrawLine(pen, x, y + height, x + width, y + height);
            }
            gfx.DrawString(win1, simpleFont, XBrushes.Black, x - 20, y - 5, XStringFormat.Default); //левый верхний угол
            gfx.DrawString(win2, simpleFont, XBrushes.Black, x + width - 30, y + height + 20, XStringFormat.Default); //правый нижний угол
            gfx.DrawImage(image, x, y, width, height);
            //y += height + 30;
        }

        /// <summary>
        /// Зум по оси Z
        /// </summary>
        public static double coef = 0;

        /// <summary>
        /// Данные по изображению
        /// </summary>
        public struct ImgData
        {
            public MemoryStream ms; //поток для сохранения фото
            public double mX; //коэффициент преобразования значения по Х
            public double mY; //коэффициент преобразования значения по Y
            public double lX; //размер области по X
            public double lY; //размер области по Y
        }

        public static ImgData m1, m2, m3, m4;

        /// <summary>
        /// Функция для проверки масштаба изображения
        /// </summary>
        /// <param name="stream"></param>
        public static ImgData Check_Img(MemoryStream streamIn)
        {
            Image img = Image.FromStream(streamIn);
            Bitmap bmp = (Bitmap)img;
            Size s = bmp.Size;
            s.Width -= 10;
            s.Height -= 10;
            int xMax = 0, yMax = 0, xMin = s.Width, yMin = s.Height;
            for (int i = 10; i < s.Width; i += 10)
            {
                for (int j = 100; j < s.Height; j += 10)
                {
                    Color clr1 = bmp.GetPixel(i, j);
                    if (clr1.R != 255 || clr1.G != 255 || clr1.B != 255)
                    {
                        if (i > xMax) xMax = i;
                        if (j > yMax) yMax = j;
                        if (i < xMin) xMin = i;
                        if (j < yMin) yMin = j;
                    }
                }
            }
            double kX = Convert.ToDouble(s.Width) / Math.Abs(xMax - xMin), kY = Convert.ToDouble(s.Height) / Math.Abs(yMax - yMin);
            ImgData struc = new ImgData();
            //отдаление, если деталь не видно вообще или если деталь занимает полотно целиком
            if ((xMax == 0 && yMax == 0 && xMin == s.Width && yMin == s.Height) ||
                xMin <= 100 || yMin <= 100 || xMax >= s.Width - 100 || yMax >= s.Height - 100)
            {
                coef -= 0.5;
            }
            else
            {
                    int siz;
                    if (xMax - xMin > yMax - yMin)
                    {
                        siz = xMax - xMin + 100;
                    }
                    else
                    {
                        siz = yMax - yMin + 100;
                    }
                    int cX1 = (xMax + xMin) / 2 - siz / 2,
                        cY1 = (yMax + yMin) / 2 - siz / 2,
                        cX2 = (xMax + xMin) / 2 + siz / 2,
                        cY2 = (yMax + yMin) / 2 + siz / 2;
                    if (cX1 < 0)
                    {
                        cX2 -= cX1;
                        cX1 = 0;
                    }
                    if (cX2 > s.Width)
                    {
                        cX1 -= cX2 - s.Width;
                        cX2 = s.Width;
                    }
                    if (cY1 < 0)
                    {
                        cY2 -= cX1;
                        cY1 = 0;
                    }
                    if (cY2 > s.Height)
                    {
                        cY1 -= cY2 - s.Height;
                        cY2 = s.Height;
                    }
                    bmp = Cut(bmp, cX1, cY1, cX2, cY2);
                    struc.mX = siz / Convert.ToDouble(s.Width);
                    struc.mY = siz / Convert.ToDouble(s.Height);

                    struc.ms = new MemoryStream();
                    bmp.Save(struc.ms, System.Drawing.Imaging.ImageFormat.Png);
                
            }
            return struc;
        }


        /// <summary>
        /// Отрисовка линии
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        static void Add_Line(double x1, double y1, double x2, double y2)
        {
            XPen pen = new XPen(XColor.FromArgb(0, 0, 0));
            gfx.DrawLine(pen, x1, y1, x2, y2);
        }

        /// <summary>
        /// Снимок экрана
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bounds"></param>
        public static MemoryStream Make_Snapshot(Rectangle bounds)
        {
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
            }
            MemoryStream strm = new MemoryStream();
            bitmap.Save(strm, System.Drawing.Imaging.ImageFormat.Png);
            return strm;
        }

        /// <summary>
        /// Обрезка снимка
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        static Bitmap Cut(Bitmap bmp, int x1, int y1, int x2, int y2)
        {
            var img = bmp;
            int width = x2 - x1 + 1;
            int height = y2 - y1 + 1;
            var result = new Bitmap(width, height);
            for (int i = x1; i <= x2; i++)
                for (int j = y1; j <= y2; j++)
                    result.SetPixel(i - x1, j - y1, img.GetPixel(i, j));
            return result;
        }
    }

    /// <summary>
    /// Позиции для съемки
    /// </summary>
    public class Positions
    {
        /// <summary>
        /// Точка 1
        /// </summary>
        public PointView p1 { get; set; }

        /// <summary>
        /// Точка 2
        /// </summary>
        public PointView p2 { get; set; }

        /// <summary>
        /// Точка 3
        /// </summary>
        public PointView p3 { get; set; }

        /// <summary>
        /// Точка 4
        /// </summary>
        public PointView p4 { get; set; }

        public Positions()
        {
            p1 = new PointView();
            p2 = new PointView();
            p3 = new PointView();
            p4 = new PointView();
        }

        /// <summary>
        /// Сохранение в JSON
        /// </summary>
        /// <returns></returns>
        public string JSON_Serialize()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize<Positions>(this, options);
        }
    }

    /// <summary>
    /// Точка зрения
    /// </summary>
    public class PointView
    {
        /// <summary>
        /// Угол поворота ось X
        /// </summary>
        public float angleX { get; set; }

        /// <summary>
        /// Угол поворота ось Y
        /// </summary>
        public float angleY { get; set; }

        /// <summary>
        /// Угол поворота ось Z
        /// </summary>
        public float angleZ { get; set; }

        /// <summary>
        /// Расстояние
        /// </summary>
        public float zoom { get; set; }

        /// <summary>
        /// Сохранение в JSON
        /// </summary>
        /// <returns></returns>
        public string JSON_Serialize()
        {
            return JsonSerializer.Serialize<PointView>(this);
        }
    }
}
