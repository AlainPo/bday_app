using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace bdays_app2
{
    internal class BDayFile
    {
        public string path = "";    //путь к файлу

        public BDayFile(string path = "")
        {
            this.path = path;
        }

        public bool PrintFile()
            {
                StreamReader sr;

                try
                {
                    sr = new StreamReader(path);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                    return false;
                }

                string s; // дополнительная переменная

                Console.WriteLine("The content of file {0}:", path);

                s = sr.ReadLine(); // прочитать первую строку из файла
                while (s != null) // конець файла - значение null
                {
                    s= s.Replace(" ", "\t\t");
    
                    Console.WriteLine(s);

                    s = sr.ReadLine();
                }

                sr.Close();

                return true;
        }

        public bool PrintTodayNames(string filename)
        {
            StreamReader sr;

            try
            {
                sr = new StreamReader(filename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return false;
            }

            string s; // дополнительная переменная

            Console.WriteLine("\n\nToday bdays:");

            s = sr.ReadLine(); // прочитать первую строку из файла
            while (s != null) // конець файла - значение null
            {
                string[] info = s.Split(" ");
                DateTime readDate = new DateTime(DateTime.Today.Year, Convert.ToInt32(Convert.ToDateTime(info[1]).Month), Convert.ToInt32(Convert.ToDateTime(info[1]).Day));
                if (readDate==DateTime.Today)
                    Console.WriteLine($"{info[0]}\t\t{info[1]}");
                s = sr.ReadLine();
            }

            sr.Close();

            return true;
        }

        public bool PrintSoonNames(string filename)
        {
            Console.WriteLine("\n\nUpcoming bdays:");
            StreamReader sr;

            try
            {
                sr = new StreamReader(filename);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return false;
            }

            string s; // дополнительная переменная

            s = sr.ReadLine(); // прочитать первую строку из файла
            while (s != null) // конець файла - значение null
            {
                string[] info = s.Split(" ");
                DateTime readDate = new DateTime(DateTime.Today.Year, Convert.ToInt32(Convert.ToDateTime(info[1]).Month), Convert.ToInt32(Convert.ToDateTime(info[1]).Day));
                if (readDate.Date > DateTime.Today && readDate.Date < DateTime.Today.AddDays(7))
                    Console.WriteLine($"{info[0]}\t\t{info[1]}");
                s = sr.ReadLine();
            }

            sr.Close();

            return true;
        }


        public bool ChangeRec(int position, string str)
        {

            if (!File.Exists(path)) return false;

            string[] arrayS = File.ReadAllLines(path);

            if ((position < 0) || (position >= arrayS.Length))
                return false;

            arrayS[position] = str;

            File.WriteAllLines(path, arrayS);

            return true;
        }

        public bool DeleteRec(int index)
        {
            StreamReader sr;

            try
            {
                sr = File.OpenText(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            List<string> lst = new List<string>(0); // создать пустой список
            string s;
            while ((s = sr.ReadLine()) != null)
                lst.Add(s); // добавить строку в список

            sr.Close();

            if ((index < 0) || (index >= lst.Count))
                return false;

            lst.RemoveAt(index);

            StreamWriter sw = new StreamWriter(path); // открыть файл для записи

            for (int i = 0; i < lst.Count; i++)
                sw.WriteLine(lst[i]);

            sw.Close();

            return true;
        }

        public bool AddRec(string str)
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            List<string> lst = new List<string>(0); // создать пустой список
            string s;
            while ((s = sr.ReadLine()) != null) { 
                lst.Add(s); // добавить строку в список

             }
            sr.Close();

            lst.Add(str);

            StreamWriter sw = new StreamWriter(path);

            foreach (string ts in lst)
                sw.WriteLine(ts);

            sw.Close();

            return true;
        }



        public int Exist(string str, DateTime date) //1-both fields, 0-only name
        {
            int flag = 0;
            StreamReader sr;
            try
            {
                sr = new StreamReader(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

            string s;
            int position = 0;

            while ((s = sr.ReadLine()) != null)
            {
                string[] rec = s.Split(" ");
                string name = rec[0];

                if (name == str)
                {
                    sr.Close();
                    return position;
                }
                position++;
            }

            position = -1;
            sr.Close();
            return position;
        }
    }
}
