using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace LAB5
{
    class Select_data
    {
        public OleDbConnection Conn;
        static void lineCh(char s)
        {
            for (int i = 0; i < 80; i++)
                Console.Write(s);
            Console.WriteLine();
        }
        public Select_data()
        {
            //заполняем значения атрибутов
            this.Conn = this.Conn = Program.Conn; 
            //действие
            int i = 0;
            //таблица
            int j = 0;
            //выбираем действие
            {
                bool key = true;
                while (key)
                {
                    key = false;
                    Console.WriteLine(" Выберите действие:");
                    Console.WriteLine(" 1 - select only");
                    Console.WriteLine(" 2 - select with where");
                    Console.Write(" № действия:");
                    i = Int32.Parse(Console.ReadLine());
                    if (i < 1 || i > 2)
                    {
                        Console.WriteLine(" Не верно, повторите ввод.");
                        key = true;
                    }
                }
            }
            lineCh('-');
            //выбираем таблицу
            {
                bool key = true;
                while (key)
                {
                    key = false;
                    Console.WriteLine(" Выберите таблицу:");
                    Console.WriteLine(" 1 - PUPIL");
                    Console.WriteLine(" 2 - CLASS");
                    Console.WriteLine(" 3 - TEACHER");
                    Console.WriteLine(" 4 - CABINET");
                    Console.WriteLine(" 5 - DAY_LIST");
                    Console.WriteLine(" 6 - THING");
                    Console.Write(" № таблицы:");
                    j = Int32.Parse(Console.ReadLine());
                    if (j < 1 || j > 6)
                    {
                        Console.WriteLine(" Не верно, повторите ввод.");
                        key = true;
                    }
                }
            }
            //вызываем действие, предаем знание о № таблицы
            lineCh('_');
            switch (i)
            {
                case 1: selectOnly(j); break;
                case 2: selectWhere(j); break;
            }
        }
        public void selectOnly(int i)
        {
            i--;
            try
            {
                Conn.Open();
                //Console.WriteLine(new string('-',40));
               // Console.WriteLine(Conn.State);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DataSet ds = new DataSet();
                string commStr = "SELECT * FROM "+
                    Program.Tables[i]+";";

                OleDbDataAdapter adapter = new OleDbDataAdapter(commStr, Program.ConnStr);
                adapter.MissingSchemaAction = System.Data.MissingSchemaAction.AddWithKey;
                adapter.Fill(ds);

                DataTable table = ds.Tables[0];
                foreach (DataColumn column in table.Columns)
                {
                    Console.Write("[" + column.ColumnName + "] ");
                }
                Console.WriteLine("\n"+new string('-',80));
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        Console.Write("[" + row[column]+"] ");
                    }
                    Console.WriteLine();
                }
                //------------------------------------
                Conn.Close();
                //Console.WriteLine(Conn.State);
            }
        }
        public void selectWhere(int i)
        {
            i--;
            try
            {
                Conn.Open();
                //Console.WriteLine(new string('-',40));
                // Console.WriteLine(Conn.State);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                DataSet ds = new DataSet();
                string commStr = "SELECT * FROM " +
                    Program.Tables[i] + ";";

                OleDbDataAdapter adapter = new OleDbDataAdapter(commStr, Program.ConnStr);
                adapter.MissingSchemaAction = System.Data.MissingSchemaAction.AddWithKey;
                adapter.Fill(ds);

                DataTable table = ds.Tables[0];
                foreach (DataColumn column in table.Columns)
                {
                    Console.Write("[" + column.ColumnName + "] ");
                }
                Console.WriteLine("\n" + new string('-', 80));
                Console.Write(" Введите условие: where ");
                string where = Console.ReadLine();
                DataRow[] findrows = table.Select(where);
                Console.WriteLine("" + new string('-', 80));
                foreach (DataRow row in findrows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        Console.Write("[" + row[column] + "] ");
                    }
                    Console.WriteLine();
                }
                //------------------------------------
                Conn.Close();
                //Console.WriteLine(Conn.State);
            }
        }
    }
}
