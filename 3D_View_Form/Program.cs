using System;
using System.Windows.Forms;
using WindowsFormsApplication317;

namespace _3D_View_Form
{

    /*----------------------------23.06.2021---------------------------------------------------
     1 изменено число параметров с 8 на 9
     2 добавлен параметр "допуск"
    */

    static class Program
    {

        //e:\Folder\etalons\compare.obj e:\Folder\ true 32 3 1 2 Bold 3


        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ViewModel f = new ViewModel();
            if (args.Length > 0)
            {
                f.path = @args[0]; //путь к файлу 3Д 
                if (args.Length == 9)
                {
                    f.folder = @args[1]; //путь к папке эталонов
                    f.snap = Convert.ToBoolean(args[2]); //команда на генерацию отчета
                    f.inMM = Convert.ToDouble(args[3]); //внутреняя разница в мм
                    f.outMM = Convert.ToDouble(args[4]); //внешняя разница в мм
                    f.inL = Convert.ToDouble(args[5]); //внутренняя разница в л
                    f.outL = Convert.ToDouble(args[6]); //внешняя разница в л
                    f.detail = args[7]; //наименование детали
                    f.eps = Convert.ToDouble(args[8]); //допуск при сравнении
                }
            }
            Application.Run(f);
        }
    }
}
