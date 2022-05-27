using System;
using System.Collections.Generic;
using System.IO;

namespace _3D_View_Form
{
    /// <summary>
    /// Парсер для файлов OBJ
    /// </summary>
    class ParseOBJ
    {
        /// <summary>
        /// Набор вершин
        /// </summary>
        public List<float[]> V = new List<float[]>();
        /// <summary>
        /// Набор нормалей
        /// </summary>
        public List<float[]> VN = new List<float[]>();
        /// <summary>
        /// Набор поверхностей
        /// </summary>
        public List<List<int>> F = new List<List<int>>();
        /// <summary>
        /// Набор цветов
        /// </summary>
        public List<List<int>> FN = new List<List<int>>();

        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int Read_File(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return -1;
            }
            StreamReader sr = new StreamReader(fileName);
            string[] line2;
            List<string> line = new List<string>();
            while (!sr.EndOfStream)
            {
                float[] vert = new float[6];
                float[] vn = new float[6];
                List<int> f = new List<int>();
                List<int> fn = new List<int>();
                line2 = sr.ReadLine().Split(' ');
                line.Clear();
                if (line2[0] != "v" && line2[0] != "f" && line2[0] != "vn") continue;
                foreach (string s in line2) 
                    if (s != "") line.Add(s);
                if (line.Count < 4) continue;
                switch (line[0])
                {
                    case "v":
                        vert[0] = Convert.ToSingle(line[1].Replace('.',','));
                        vert[1] = Convert.ToSingle(line[2].Replace('.', ','));
                        vert[2] = Convert.ToSingle(line[3].Replace('.', ','));
                        if (line.Count>5)
                        {
                            vert[3] = Convert.ToSingle(line[4].Replace('.', ','));
                            vert[4] = Convert.ToSingle(line[5].Replace('.', ','));
                            vert[5] = Convert.ToSingle(line[6].Replace('.', ','));
                        }
                        else
                        {
                            vert[3] = 1f;
                            vert[4] = 1f;
                            vert[5] = 1f;
                        }
                        V.Add(vert);
                        break;
                    case "vn":
                        vn[0] = Convert.ToSingle(line[1].Replace('.', ','));
                        vn[1] = Convert.ToSingle(line[2].Replace('.', ','));
                        vn[2] = Convert.ToSingle(line[3].Replace('.', ','));
                        VN.Add(vn);
                        break;
                    case "f":
                        for (int i = 1; i < line.Count; i++)
                        {
                            f.Add(Convert.ToInt32(line[i].Split('/')[0]));
                            try 
                            { 
                                fn.Add(Convert.ToInt32(line[i].Split('/')[2])); 
                            }
                            catch 
                            {
                                return -2;
                            }
                        }
                        F.Add(f);
                        FN.Add(fn);
                        break;
                }
            }
            sr.Close();
            return 0;
        }
    }
}
