using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;


namespace LAB5
{
    class Program
    {
        public static OleDbConnection Conn;
        public static string ConnStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='C:\Users\dimay\Desktop\с#_5\База данных11.mdb';";
        public static List<string> Tables = new List<string>(){
                    "PUPIL",
                    "CLASS",
                    "TEACHER",
                    "CABINET",
                    "DAY_LIST",
                    "THING"
        };
        static void lineCh(char s)
        {
            for (int i = 0; i < 80; i++)
                Console.Write(s);
            Console.WriteLine();
        }
        public static DataSet1 data_set;
        public static void Fill()
        {
            Program.data_set = new DataSet1();
            DataSet1TableAdapters.TableAdapterManager adapters = new DataSet1TableAdapters.TableAdapterManager();
            //заполняем константы
            for (int i = 0; i < 6; i++)
                switch (i)
                {
                    case 1:
                        DataSet1TableAdapters.PUPILTableAdapter ad = new DataSet1TableAdapters.PUPILTableAdapter(); 
                        ad.Fill(Program.data_set.PUPIL);
                        break;
                    case 2:
                        DataSet1TableAdapters.CLASSTableAdapter c = new DataSet1TableAdapters.CLASSTableAdapter();
                        c.Fill(Program.data_set.CLASS);
                        break;
                    case 3:
                        DataSet1TableAdapters.TEACHERTableAdapter a1 = new DataSet1TableAdapters.TEACHERTableAdapter();
                        a1.Fill(Program.data_set.TEACHER);
                        break;
                    case 4:
                        DataSet1TableAdapters.CABINETTableAdapter a2 = new DataSet1TableAdapters.CABINETTableAdapter();
                        a2.Fill(Program.data_set.CABINET);
                        break;
                    case 5:
                        DataSet1TableAdapters.DAY_LISTTableAdapter a3 = new DataSet1TableAdapters.DAY_LISTTableAdapter();
                        a3.Fill(Program.data_set.DAY_LIST);
                        break;
                    case 6:
                        DataSet1TableAdapters.THINGTableAdapter a4 = new DataSet1TableAdapters.THINGTableAdapter();
                        a4.Fill(Program.data_set.THING);
                        break;
                }
        }
        public static void Update(DataSet1 dataset)
        {
            DataSet1TableAdapters.TableAdapterManager adapters = new DataSet1TableAdapters.TableAdapterManager();
            //заполняем константы
            adapters.UpdateAll(dataset);
        }
        static void Main(string[] args)
        {
            Program.Conn = new OleDbConnection(Program.ConnStr);
            Program.Fill();
            Console.WriteLine("lab_5 Yarcev");
            lineCh('=');
            //управляющий алгоритм
            {
                Facad link_f = new Facad();
                int i = 0;
                bool key = true;
                while (key)
                {
                    bool key_input = true;
                    while (key_input)
                    {
                        key_input = false;
                        Console.WriteLine(" Выберите действие:");
                        Console.WriteLine(" 1 - edit_table");
                        Console.WriteLine(" 2 - select_table");
                        Console.WriteLine(" 3 - exit");
                        Console.Write(" № действия:");
                        i = Int32.Parse(Console.ReadLine());
                        if (i < 1 || i > 4)
                        {
                            Console.WriteLine(" Не верно, повторите ввод.");
                            key_input = true;
                        }
                    }
                    lineCh('-');
                    switch (i)
                    {
                        case 1: link_f.go_edit(); break;
                        case 2: link_f.go_select(); break;
                        case 3: key = false; break;
                    }
                    lineCh('=');
                }
                
            }
            Console.WriteLine("Program is ending...");
            Console.ReadLine();
        }
    }
}
