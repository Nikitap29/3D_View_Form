using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharpGL;
using _3D_View_Form;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace WindowsFormsApplication317
{

    /*----------------------------23.06.2021----------------------------------------------------------
     1 добавлен параметр "допуск"
     2 изменен первоначальный масштаб модели
     3 вместо своего параметра при масштабировании теперь используется параметр из PDF_Report
     4 отключил отрисовку системы координат
     5 добавил использование функции масштабирования из PDF_Report
     6 обновление параметров функции генерации отчета в связи с тем, что добавлен параметр "допуск"
    ----------------------------24.06.2021------------------------------------------------------------
     1 добавил расчет размеров для каждой картинки
     2 изменил наименование файла конфигурации с config.json на config_report.json
    ----------------------------25.06.2021------------------------------------------------------------
     1 поправил формулу расчета размеров изображения в циклической процедуре (убрал умножение на 2)
     2 добавил работу с освещением
    ----------------------------28.06.2021------------------------------------------------------------
     1 добавил обработку нормалей при построении модели
     2 исправил освещение - добавил второй источник света и исправил первый
    ----------------------------01.07.2021------------------------------------------------------------
     1 убрал масштабирование в первом цикле
     2 поменял порядок действий - сначала снимок, затем поворот в следующую позицию
    ----------------------------09.07.2021------------------------------------------------------------
     1 продублировал цикл снятия кадров
     2 увеличил длину цикла с 12 до 20
     3 один масштаб на все 4 картинки
     4 ввел коэффициент зависимости величины size от размеров окна (win)
    */

    /// <summary>
    /// Форма отображения
    /// </summary>
    public partial class ViewModel : Form
    {
        //Ключи приложения
        /// <summary>
        /// Путь к файлу OBJ
        /// </summary>
        public string path = "";
        /// <summary>
        /// Путь к папке с эталонами
        /// </summary>
        public string folder = "";
        /// <summary>
        /// Признак генерации отчета
        /// </summary>
        public bool snap = false;
        /// <summary>
        /// Наибольшее отклонение внутрь в мм
        /// </summary>
        public double inMM = 0;
        /// <summary>
        /// Наибольшее отклонение наружу в мм
        /// </summary>
        public double outMM = 0;
        /// <summary>
        /// Наибольшее отклонение наружу в литрах
        /// </summary>
        public double outL = 0;
        /// <summary>
        /// Наибольшее отклонение внутрь в литрах
        /// </summary>
        public double inL = 0;
        /// <summary>
        /// Наименование детали
        /// </summary>
        public string detail = "Наковальня";
        /// <summary>
        /// Допуск при сравнении
        /// </summary>
        public double eps = 0.5;

        //прочие параметры
        /// <summary>
        /// Объект-парсер OBJ
        /// </summary>
        ParseOBJ po;
        /// <summary>
        /// Угол поворота ось Х
        /// </summary>
        float rotateX = 0;
        /// <summary>
        /// Угол поворота ось Y
        /// </summary>
        float rotateY = 0;
        /// <summary>
        /// Угол поворота ось Z
        /// </summary>
        float rotateZ = 0;
        /// <summary>
        /// Максимальное значения по оси Z
        /// </summary>
        float zMax = 0;
        /// <summary>
        /// Минимальное значения по оси Z
        /// </summary>
        float zMin = 1000;
        /// <summary>
        /// Максимальное значения по оси X
        /// </summary>
        float xMax = 0;
        /// <summary>
        /// Минимальное значения по оси X
        /// </summary>
        float xMin = 1000;
        /// <summary>
        /// Максимальное значения по оси Z
        /// </summary>
        float yMax = 0;
        /// <summary>
        /// Минимальное значения по оси Z
        /// </summary>
        float yMin = 1000;
        /// <summary>
        /// Признак первого цикла работы программы
        /// </summary>
        bool firstCycle = false;
        /// <summary>
        /// Номер цикла работы программы
        /// </summary>
        int cycle = 0;
        /// <summary>
        /// Путь для сохранения файлов отчетов
        /// </summary>
        string report = "";
        /// <summary>
        /// Позиции съемки
        /// </summary>
        Positions pos = new Positions();
        /// <summary>
        /// Признак поворота модели
        /// </summary>
        bool click = false;
        /// <summary>
        /// Масштаб
        /// </summary>
        float size = 1;

        /// <summary>
        /// Инициализация компонента
        /// </summary>
        public ViewModel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load(object sender, EventArgs e)
        {
            if (path == "")
            {
                openFileDialog1.ShowDialog();
                path = openFileDialog1.FileName;
            }
            if (File.Exists(path))
            {
                po = new ParseOBJ();
                var res = po.Read_File(path);
                if (res < 0) //если результат чтения файла меньше нуля (ошибка) то
                {
                    string mes = "Ошибка";
                    switch (res)
                    {
                        case -1: mes = "Ошибка чтения файла!"; break;
                        case -2: mes = "Ошибка считывания данных из файла!"; break;
                    }
                    MessageBox.Show(mes);
                    //Application.Exit(); //закрываем приложение
                }
                openGLControl1.Visible = true;
            }
            else MessageBox.Show("Файл не существует!");
        }

        /// <summary>
        /// Отрисовка осей координат
        /// </summary>
        /// <param name="gl"></param>
        private void Draw_Axis(OpenGL gl, double length)
        {
            double[] color = new double[4];
            //ось X
            color[0] = 0; color[1] = 0; color[2] = 0; color[3] = 125/255;
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(color);
            gl.Vertex(-length/2, 0);
            gl.Vertex(length/2, 0);
            gl.End();
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(color);
            gl.Vertex(length/2, 0);
            gl.Vertex(length/2-1, 1);
            gl.Vertex(length/2-1, -1);
            gl.End();
            //ось Y
            color[0] = 0/255; color[1] = 38/255; color[2] = 255/255;
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(color);
            gl.Vertex(0, -length/2);
            gl.Vertex(0, length/2);
            gl.End();
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(color);
            gl.Vertex(0, length/2);
            gl.Vertex(1, length/2-1);
            gl.Vertex(-1, length/2-1);
            gl.End();
            //ось Z
            color[0] = 0 / 255; color[1] = 38 / 255; color[2] = 255 / 255;
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(color);
            gl.Vertex(0, 0, -length/2);
            gl.Vertex(0, 0, length/2);
            gl.End();
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(color);
            gl.Vertex(0, 0, length/2);
            gl.Vertex(1, 0, length/2-1);
            gl.Vertex(-1, 0, length/2-1);
            gl.End();
        }

        /// <summary>
        /// Объект для работы с графикой
        /// </summary>
        OpenGL gl;

        /// <summary>
        /// Инициализация всего
        /// </summary>
        private void Init()
        {
            
            gl.Enable(OpenGL.GL_LIGHTING);
            gl.LightModel(SharpGL.Enumerations.LightModelParameter.Ambient, OpenGL.GL_TRUE);
            gl.Enable(OpenGL.GL_NORMALIZE);
            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
        }

        /// <summary>
        /// Включение освещения
        /// </summary>
        private void Light_On()
        {
            float[] lightDiffuse = { 1.0f, 1.0f, 1.0f };
            float[] lightPosition = {10000.0f, 10000.0f, -10000.0f, 0.0f};
            gl.Enable(OpenGL.GL_LIGHT0);
            
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightPosition);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, lightDiffuse);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_CONSTANT_ATTENUATION, 0.0f);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_LINEAR_ATTENUATION, 0.2f);
            //gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_QUADRATIC_ATTENUATION, 0.4f);

            float[] lightDiffuse2 = { 1.0f, 1.0f, 1.0f };
            float[] lightPosition2 = { -10000.0f, -10000.0f, 10000.0f, 0.0f };
            gl.Enable(OpenGL.GL_LIGHT1);

            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_POSITION, lightPosition2);
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_DIFFUSE, lightDiffuse2);
        }

        /// <summary>
        /// Отрисовка модели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Draw_Model(object sender, RenderEventArgs args)
        {
            gl = openGLControl1.OpenGL;
            gl.ClearColor(255, 255, 255, 0);
            Init();
            
            // Очистка экрана и буфера глубин
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            Light_On();

            // Сбрасываем модельно-видовую матрицу
            gl.LoadIdentity();

            aSize.Text = size.ToString();
            xA.Text = rotateX.ToString();
            yA.Text = rotateY.ToString();
            zA.Text = rotateZ.ToString();

            float win;
            if ((Math.Abs(xMin)+ Math.Abs(xMax)>= Math.Abs(yMin) + Math.Abs(yMax))&& 
                (Math.Abs(xMin) + Math.Abs(xMax) >= Math.Abs(zMin) + Math.Abs(zMax)))
            {
                win = Math.Abs(xMin) + Math.Abs(xMax);
            }
            else if ((Math.Abs(xMin) + Math.Abs(xMax) <= Math.Abs(yMin) + Math.Abs(yMax)) &&
                (Math.Abs(yMin) + Math.Abs(yMax) >= Math.Abs(zMin) + Math.Abs(zMax)))
            {
                win = Math.Abs(yMin) + Math.Abs(yMax);
            }
            else
            {
                win = Math.Abs(zMin) + Math.Abs(zMax);
            }

            // Двигаем перо вглубь экрана
            gl.Translate(0, 0, PDF_Report.coef);

            gl.Rotate(rotateY, 0.0f, 1.0f, 0.0f);
            gl.Rotate(rotateX, 1.0f, 0.0f, 0.0f);
            gl.Rotate(rotateZ, 0.0f, 0.0f, 1.0f);

            gl.Ortho(-win / 2, win / 2, -win / 2, win / 2, 0.5, win);

            int k = 0;

            //Draw_Axis(gl, 700);

            if (po.F.Count>0)
            {
                foreach (List<int> f in po.F)
                {
                    switch (f.Count)
                    {
                        case 0: continue;
                        case 1: gl.Begin(OpenGL.GL_POINTS); break;
                        case 2: gl.Begin(OpenGL.GL_LINES); break;
                        case 3: gl.Begin(OpenGL.GL_TRIANGLES); break;
                        case 4: gl.Begin(OpenGL.GL_QUADS); break;
                        default: gl.Begin(OpenGL.GL_POLYGON); break;
                    }
                    foreach (int j in f)
                    {
                        // Указываем цвет вершин
                        gl.Color(Convert.ToByte(po.V[j - 1][3]*255), Convert.ToByte(po.V[j - 1][4]*255), Convert.ToByte(po.V[j - 1][5]*255));
                        gl.Normal(po.VN[j-1][0], po.VN[j - 1][1], po.VN[j - 1][2]);
                        gl.Vertex(po.V[j - 1][0], po.V[j - 1][1], po.V[j - 1][2]);
                        
                        if (po.V[j - 1][0] > xMax) xMax = po.V[j - 1][0];
                        if (po.V[j - 1][0] < xMin) xMin = po.V[j - 1][0];
                        if (po.V[j - 1][1] > yMax) yMax = po.V[j - 1][1];
                        if (po.V[j - 1][1] < yMin) yMin = po.V[j - 1][1];
                        if (po.V[j - 1][2] > zMax) zMax = po.V[j - 1][2];
                        if (po.V[j - 1][2] < zMin) zMin = po.V[j - 1][2];
                    }
                    k++;
                    gl.End();
                }
            }
            else
            {
                gl.Begin(OpenGL.GL_TRIANGLES);
                // Указываем цвет вершин
                gl.Color(1.0f, 1.0f, 1.0f);
                foreach (float[] v in po.V)
                {
                    gl.Vertex(v[0], v[1], v[2]);
                    if (v[0] > xMax) xMax = v[0];
                    if (v[0] < xMin) xMin = v[0];
                    if (v[1] > yMax) yMax = v[1];
                    if (v[1] < yMin) yMin = v[1];
                    if (v[2] > zMax) zMax = v[2];
                    if (v[2] < zMin) zMin = v[2];
                }
                gl.End();
            }
            
            gl.Disable(OpenGL.GL_LIGHT0);
            gl.Flush();
            Cycle_Work(win);
        }

        /// <summary>
        /// Обработка циклов программы
        /// </summary>
        void Cycle_Work(float win)
        {
            string config = folder + @"config_report.json";
            switch (cycle)
            {
                case 1:
                    if (File.Exists(config))
                    {
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true
                        };
                        string s = File.ReadAllText(config);
                        pos = JsonSerializer.Deserialize<Positions>(s, options);
                    }
                    else
                    {
                        //MessageBox.Show("Отсутствует файл конфигурации!");
                        Close();
                    }
                    rotateX = pos.p1.angleX;
                    rotateY = pos.p1.angleY;
                    rotateZ = pos.p1.angleZ;
                    break;
                case 3:
                    PDF_Report.b1 = PDF_Report.Make_Snapshot(Bounds);
                    PDF_Report.m1 = PDF_Report.Check_Img(PDF_Report.b1);
                    if (PDF_Report.m1.ms == null) return;
                    rotateX = pos.p2.angleX;
                    rotateY = pos.p2.angleY;
                    rotateZ = pos.p2.angleZ;
                    break;
                case 5:
                    PDF_Report.b2 = PDF_Report.Make_Snapshot(Bounds);
                    PDF_Report.m2 = PDF_Report.Check_Img(PDF_Report.b2);
                    if (PDF_Report.m2.ms == null) return;
                    rotateX = pos.p3.angleX;
                    rotateY = pos.p3.angleY;
                    rotateZ = pos.p3.angleZ;
                    break;
                case 7:
                    PDF_Report.b3 = PDF_Report.Make_Snapshot(Bounds);
                    PDF_Report.m3 = PDF_Report.Check_Img(PDF_Report.b3);
                    if (PDF_Report.m3.ms == null) return;
                    rotateX = pos.p4.angleX;
                    rotateY = pos.p4.angleY;
                    rotateZ = pos.p4.angleZ;
                    break;
                case 9:
                    PDF_Report.b4 = PDF_Report.Make_Snapshot(Bounds);
                    PDF_Report.m4 = PDF_Report.Check_Img(PDF_Report.b4);
                    if (PDF_Report.m4.ms == null) return;
                    rotateX = pos.p1.angleX;
                    rotateY = pos.p1.angleY;
                    rotateZ = pos.p1.angleZ;
                    break;
                case 11:
                    PDF_Report.b1 = PDF_Report.Make_Snapshot(Bounds);
                    PDF_Report.m1 = PDF_Report.Check_Img(PDF_Report.b1);
                    if (PDF_Report.m1.ms == null) return;
                    else
                    {
                        PDF_Report.m1.lX = Math.Tan(1.5643676143304) * Math.Abs(PDF_Report.coef)*win/166.9;
                        PDF_Report.m1.lY = Math.Tan(1.557938606692) * Math.Abs(PDF_Report.coef)* win / 166.9;
                    }
                    rotateX = pos.p2.angleX;
                    rotateY = pos.p2.angleY;
                    rotateZ = pos.p2.angleZ;
                    break;
                case 13:
                    PDF_Report.b2 = PDF_Report.Make_Snapshot(Bounds);
                    PDF_Report.m2 = PDF_Report.Check_Img(PDF_Report.b2);
                    rotateX = pos.p3.angleX;
                    rotateY = pos.p3.angleY;
                    rotateZ = pos.p3.angleZ;
                    break;
                case 15:
                    PDF_Report.b3 = PDF_Report.Make_Snapshot(Bounds);
                    PDF_Report.m3 = PDF_Report.Check_Img(PDF_Report.b3);
                    rotateX = pos.p4.angleX;
                    rotateY = pos.p4.angleY;
                    rotateZ = pos.p4.angleZ;
                    break;
                case 17:
                    PDF_Report.b4 = PDF_Report.Make_Snapshot(Bounds);
                    PDF_Report.m4 = PDF_Report.Check_Img(PDF_Report.b4);
                    break;
                case 19:
                    report = PDF_Report.Generate(inMM, outMM, inL, outL, detail, folder, win * Math.Abs(PDF_Report.coef), eps);
                    break;
            }
            if (snap)
            {
                RotBut.Visible = false;
                UP.Visible = false;
                DOWN.Visible = false;
                LEFT.Visible = false;
                RIGHT.Visible = false;
                RIGHT_Z.Visible = false;
                LEFT_Z.Visible = false;
                ZoomPlus.Visible = false;
                ZoomMinus.Visible = false;
                SpeedPlus.Visible = false;
                SpeedMinus.Visible = false;
                label3.Visible = false;
                label5.Visible = false;
                speed.Visible = false;
                xLabel.Visible = false;
                yLabel.Visible = false;
                zLabel.Visible = false;
                xA.Visible = false;
                yA.Visible = false;
                zA.Visible = false;
                aSize.Visible = false;
                cycle++;
            }
            if (cycle == 20)
            {
                if (File.Exists(report))
                    Process.Start(report);
                cycle = 0;
                snap = false;
                RotBut.Visible = true;
                UP.Visible = true;
                DOWN.Visible = true;
                LEFT.Visible = true;
                RIGHT.Visible = true;
                RIGHT_Z.Visible = true;
                LEFT_Z.Visible = true;
                ZoomPlus.Visible = true;
                ZoomMinus.Visible = true;
                SpeedPlus.Visible = true;
                SpeedMinus.Visible = true;
                label3.Visible = true;
                label5.Visible = true;
                speed.Visible = true;
                xLabel.Visible = true;
                yLabel.Visible = true;
                zLabel.Visible = true;
                xA.Visible = true;
                yA.Visible = true;
                zA.Visible = true;
                aSize.Visible = true;
                Close();
            }
        }

        /// <summary>
        /// Нажатие кнопки Х-
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void X_Minus_MouseDown(object sender, MouseEventArgs e)
        {
            click = true;
            Thread t = new Thread(X_Minus_Code);
            t.Start();
        }

        /// <summary>
        /// Поворот по оси X в отрицательную сторону
        /// </summary>
        void X_Minus_Code()
        {
            while (click)
            {
                Invoke(new MethodInvoker(delegate
               {
                   rotateX -= (float)speed.Value/100f;
               }));
            }
        }

        /// <summary>
        /// Действие при отпускании любой кнопки поворота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUp(object sender, MouseEventArgs e)
        {
            click = false;
        }

        /// <summary>
        /// Нажатие кнопки Y-
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_Minus_MouseDown(object sender, MouseEventArgs e)
        {
            click = true;
            Thread t = new Thread(Y_Minus_Code);
            t.Start();
        }

        /// <summary>
        /// Поворот по оси Y в отрицательную сторону
        /// </summary>
        void Y_Minus_Code()
        {
            while (click)
            {
                Invoke(new MethodInvoker(delegate
                {
                    rotateY -= (float)speed.Value / 100f;
                }));
            }
        }

        /// <summary>
        /// Нажатие кнопки X+
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void X_Plus_MouseDown(object sender, MouseEventArgs e)
        {
            click = true;
            Thread t = new Thread(X_Plus_Code);
            t.Start();
        }

        /// <summary>
        /// Поворот по оси Х в положительную сторону
        /// </summary>
        void X_Plus_Code()
        {
            while (click)
            {
                Invoke(new MethodInvoker(delegate
                {
                    rotateX += (float)speed.Value / 100f;
                }));
            }
        }

        /// <summary>
        /// Нажатие кнопки Y+
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_Plus_MouseDown(object sender, MouseEventArgs e)
        {
            click = true;
            Thread t = new Thread(Y_Plus_Code);
            t.Start();
        }

        /// <summary>
        /// Поворот по оси Y в положительную сторону
        /// </summary>
        void Y_Plus_Code()
        {
            while (click)
            {
                Invoke(new MethodInvoker(delegate
                {
                    rotateY += (float)speed.Value / 100f;
                }));
            }
        }
        
        /// <summary>
        /// Приблизить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Size_Plus(object sender, EventArgs e)
        {
            if (size < 0) size += 1;
        }
        
        /// <summary>
        /// Отдалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Size_Minus(object sender, EventArgs e)
        {
            if (size > -6*zMax) size -= 1;
        }

        /// <summary>
        /// Увеличить скорость прокрутки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Speed_Plus(object sender, EventArgs e)
        {
            if (speed.Value < 10000) speed.Value += 10;
        }

        /// <summary>
        /// Нажатие кнопки Z+
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_Plus_MouseDown(object sender, MouseEventArgs e)
        {
            click = true;
            Thread t = new Thread(Z_Plus_Code);
            t.Start();
        }

        /// <summary>
        /// Нажатие кнопки Z-
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_Minus_MouseDown(object sender, MouseEventArgs e)
        {
            click = true;
            Thread t = new Thread(Z_Minus_Code);
            t.Start();
        }

        /// <summary>
        /// Поворот по оси Z в отрицательную сторону
        /// </summary>
        void Z_Minus_Code()
        {
            while (click)
            {
                Invoke(new MethodInvoker(delegate
                {
                    rotateZ -= (float)speed.Value / 100f;
                }));
            }
        }

        /// <summary>
        /// Поворот по оси Z в положительную сторону
        /// </summary>
        void Z_Plus_Code()
        {
            while (click)
            {
                Invoke(new MethodInvoker(delegate
                {
                    rotateZ += (float)speed.Value / 100f;
                }));
            }
        }

        /// <summary>
        /// Уменьшение скорости прокрутки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Speed_Minus(object sender, EventArgs e)
        {
            if (speed.Value > 10) speed.Value -= 10;
        }

        /// <summary>
        /// Кнопка генерации отчета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Generate_Button(object sender, EventArgs e)
        {
            snap = true;
        }
    }
}