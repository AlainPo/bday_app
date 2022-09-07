using System;
using System.IO;
using System.IO.Enumeration;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Xml.Serialization;

namespace bdays_app2
{
    class Program
    {

        static int PrintMenu(BDayFile file)
        {
            int choice=0;

            try
            {
                Console.WriteLine("\nChoose action:\n\n1-Add record\n2-Edit record\n3-Delete record\n4-Full table\n5-Quit\n");

                choice = Convert.ToInt32(Console.ReadLine());
                ChoosePath(choice, file);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return choice ;
        }

        static void ChoosePath(int choice, BDayFile file)
        {
            try {
                switch (choice)
                {
                    case 1:
                        AddRecord(file);
                        break;
                    case 2:
                        ChangeRecord(file);
                        break;
                    case 3:
                        DeleteRecord(file);
                        break;
                    case 4:
                        file.PrintFile();
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void AddRecord(BDayFile file)
        {
            try
            {
                Console.WriteLine("Type name:");
                string name = Console.ReadLine().ToString();
                DateTime bday=DateTime.Today;

                if (file.Exist(name, bday)!=-1)
                    Console.WriteLine("The record with that name already exists!");
                else
                {
                    Console.WriteLine("Type birth date (format dd/mm/yyyy or dd.mm.yyyy):");
                    bday = Convert.ToDateTime(Console.ReadLine()).Date;
                    string rec = name + " " + bday.ToShortDateString();
                    file.AddRec(rec);
                    Console.WriteLine("The record has been added successfully!");
                }

                file.PrintTodayNames(file.path);
                file.PrintSoonNames(file.path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ChangeRecord(BDayFile file)
        {
            try
            {
                Console.WriteLine("Type existing name:");
                string name = Console.ReadLine().ToString();
                DateTime bday=DateTime.Today;


                int pos = file.Exist(name, bday);
                if (pos==-1)
                    Console.WriteLine("The record doesn't exists!");
                else
                {
                    Console.WriteLine("Type new birth date (format dd/mm/yyyy or dd.mm.yyyy):");
                    bday = Convert.ToDateTime(Console.ReadLine()).Date;
                    string rec = name + " " + bday.ToShortDateString();
                    file.ChangeRec(pos, rec);
                    Console.WriteLine("The record has been changed successfully!");
                }

                file.PrintTodayNames(file.path);
                file.PrintSoonNames(file.path);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void DeleteRecord(BDayFile file)
        {
            try
            {
                Console.WriteLine("Type existing name:");
                string name = Console.ReadLine().ToString();
                //Console.WriteLine("Type its birth date (format dd/mm/yyyy):");
                DateTime bday;
                //bday = Convert.ToDateTime(Console.ReadLine()).Date;
                bday = DateTime.Today;
                int pos = file.Exist(name, bday);
                if (pos == -1)
                    Console.WriteLine("The record doesn't exists!");
                else
                {
                    file.DeleteRec(pos);
                    Console.WriteLine("The record has been deleted successfully!");
                }

                file.PrintTodayNames(file.path);
                file.PrintSoonNames(file.path);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static int Main(string[] args)
        {
            string path = "..\\bdays.txt";
            BDayFile file = new BDayFile(path);

            file.PrintTodayNames(path);
            file.PrintSoonNames(path);
            int choice = 0;
            while (choice!=5)
            {
                choice=PrintMenu(file);
            }
            Console.WriteLine("Come back soon!");
            return 0;

        }
    }
}
