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
    class Edit_data
    {
        public OleDbConnection Conn;
        public Edit_data()
        {
            //заполняем значения атрибутов
            this.Conn = Program.Conn;
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
                    Console.WriteLine(" 1 - add");
                    Console.WriteLine(" 2 - delete");
                    Console.WriteLine(" 3 - update");
                    Console.Write(" № действия:");
                    i = Int32.Parse(Console.ReadLine());
                    if (i < 1 || i > 3)
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
                    j--;
                }
            }
            //вызываем действие, предаем знание о № таблицы
            lineCh('_');
            switch (i) {
                case 1: add(j); break;
                case 2: delete(j); break;
                case 3: update(j); break;
            }
        }
        public void add(int i) {
            Conn.Open();

            DataSet1.PUPILDataTable dt = new DataSet1.PUPILDataTable();
            DataSet1TableAdapters.PUPILTableAdapter da = new DataSet1TableAdapters.PUPILTableAdapter();
            da.Fill(dt);

            foreach (DataColumn column in dt.Columns)
            {
                Console.Write("[" + column.ColumnName + "] ");
            }
            Console.Write("\n insert into " + Program.Tables[i] + " values( ");
            string end = Console.ReadLine();
            Console.WriteLine(");");
            Console.WriteLine("\n" + new string('-', 80));
            OleDbConnection con = new OleDbConnection(Program.ConnStr);
            OleDbCommand cmd = new OleDbCommand("insert into " + Program.Tables[i] + " values( " + end+ ");", con);

            var objDataSet = new DataSet();
            var objDataAdapter = new OleDbDataAdapter(cmd);
            objDataAdapter.Fill(objDataSet);

            Console.WriteLine(" Complete!");

            Conn.Close();
        }
        /*public void add(int i)
        {
            Conn.Open();
            Program.Fill();

            OleDbCommand c0 = new OleDbCommand("select count(*) from " + Program.Tables[i] + ";", Program.Conn);
            Console.WriteLine(" Количество столбцов = " + c0.ExecuteScalar().ToString());
            c0.CommandText = "select max(" + Program.data_set.Tables[i].Columns[0].ColumnName +
                ") from " + Program.data_set.Tables[i].TableName + ";";
            Console.WriteLine(" Максимальный " + Program.data_set.Tables[i].Columns[0].ColumnName +
                " = " + c0.ExecuteScalar().ToString());
            Console.Write(" Какое количество стврок хотите ввести? (input > 0) = ");
            int counter = 0, max = Int32.Parse(Console.ReadLine());
            bool key = true;

            while (key)
            {
                Console.WriteLine("<table name = "+ Program.data_set.Tables[i].TableName+ ">");
                for (int j = 0; j < Program.data_set.Tables[i].Columns.Count; j++)
                {
                    Console.Write("[" + Program.data_set.Tables[i].Columns[j].ColumnName + "] ");
                }
                Console.WriteLine();

                var NewRow = Program.data_set.Tables[i].NewRow();
                for (int j = 0; j < Program.data_set.Tables[i].Columns.Count; j++)
                {
                    Console.Write("[" + Program.data_set.Tables[i].Columns[j].ColumnName + "]");
                    if (NewRow.GetType() == typeof(int))
                    {
                        Console.Write("=(int)= ");
                        NewRow[j] = Int32.Parse(Console.ReadLine());
                    }
                    else if (NewRow.GetType() == typeof(string))
                    {
                        Console.Write("=(string)= ");
                        NewRow[j] = Console.ReadLine();
                    }
                }

                try {
                    Program.data_set.Tables[i].Rows.Add(NewRow);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
                DataSet1TableAdapters.TableAdapterManager tableAdapterManager = new DataSet1TableAdapters.TableAdapterManager();
                tableAdapterManager.CABINETTableAdapter = new DataSet1TableAdapters.CABINETTableAdapter();
                tableAdapterManager.CLASSTableAdapter = new DataSet1TableAdapters.CLASSTableAdapter();
                tableAdapterManager.DAY_LISTTableAdapter = new DataSet1TableAdapters.DAY_LISTTableAdapter();
                tableAdapterManager.PUPILTableAdapter = new DataSet1TableAdapters.PUPILTableAdapter();
                try
                {
                    tableAdapterManager.UpdateAll(Program.data_set);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }

                counter++;
                if (counter >= max)
                    key = false;
            }

            Program.Update(Program.data_set);
            Conn.Close();
        }
        /*public void add_old(int i)
        {
            Conn.Open();

            DataTable table = Program.data_set.Tables[i];
            foreach (DataColumn column in table.Columns)
            {
                Console.Write("[" + column.ColumnName + "] ");
            }
            Console.WriteLine("\n" + new string('-', 80));
            bool key = true;
            OleDbCommand c0 = new OleDbCommand("select count(*) from "+Program.Tables[i]+";", Program.Conn);
            Console.WriteLine(" Количество столбцов = "+c0.ExecuteScalar().ToString());
            c0.CommandText = "select max("+table.Columns[0].ColumnName+
                ") from "+Program.Tables[i]+ ";";
            Console.WriteLine(" Максимальный " + table.Columns[0].ColumnName +
                " = " + c0.ExecuteScalar().ToString());
            int x = 0;
            Console.Write(" Какое количество стврок хотите ввести? (input > 0)");
            int counter = Int32.Parse(Console.ReadLine());
                while (key)
                {
                    Console.WriteLine("add_row:");

                    DataRow row = table.NewRow();
                    int n = table.Columns.Count;
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write("[" + table.Columns[j].ColumnName + "] =");
                        if (table.Rows[0][j].GetType() == typeof(int))
                        {
                            Console.Write("(int)= ");
                            row[j] = Int32.Parse(Console.ReadLine());
                        }
                        else if (table.Rows[0][j].GetType() == typeof(string))
                        {
                            Console.Write("(string)= ");
                            row[j] = Console.ReadLine();
                        }
                    }
                    try
                    {
                        Program.data_set.Tables[i].Rows.Add(row);
                    }
                    catch (Exception e)
                    {
                        Console.Write("\n!!![" + e.Message + "]");
                        Console.WriteLine(" => Повтори ввод данных вновь.");

                        continue;
                    }
                    //string commStr = "SELECT * FROM " +
                    //    Program.Tables[i] + ";";
                    //OleDbDataAdapter adapter = new OleDbDataAdapter(commStr, Program.ConnStr);
                    //OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                    //adapter.UpdateCommand = builder.GetUpdateCommand();
                     
                    try
                    {
                        adapter.Update(Program.data_set, Program.data_set.Tables[i].TableName);
                    }
                    catch (Exception e)
                    {
                        Console.Write("\n!!![" + e.Message + "]");
                        Console.WriteLine(" => Повтори ввод данных вновь.");

                        continue;
                    }
                    x++;
                    if (counter <= x)
                        key = false;
                }

            //------------------------------
            Conn.Close();
           //Console.WriteLine(Conn.State);
        }*/
        public void delete(int i)
        {
            Conn.Open();
            
                    DataSet1.PUPILDataTable dt = new DataSet1.PUPILDataTable();
                    DataSet1TableAdapters.PUPILTableAdapter da = new DataSet1TableAdapters.PUPILTableAdapter();
                    da.Fill(dt);

                    foreach (DataColumn column in dt.Columns)
                    {
                        Console.Write("[" + column.ColumnName + "] ");
                    }
                    Console.Write("\n delete from "+Program.Tables[i]+" where ");
                    string end = Console.ReadLine();
                    Console.WriteLine("\n" + new string('-', 80));
                    OleDbConnection con = new OleDbConnection(Program.ConnStr);
                    OleDbCommand cmd = new OleDbCommand("delete from " + Program.Tables[i] + " where " + end, con);

                    var objDataSet = new DataSet();
                    var objDataAdapter = new OleDbDataAdapter(cmd);
                        objDataAdapter.Fill(objDataSet);

                    Console.WriteLine(" Complete!");

            Conn.Close();

        }
        public void update(int i)
        {
            Conn.Open();

            DataSet1.PUPILDataTable dt = new DataSet1.PUPILDataTable();
            DataSet1TableAdapters.PUPILTableAdapter da = new DataSet1TableAdapters.PUPILTableAdapter();
            da.Fill(dt);

            foreach (DataColumn column in dt.Columns)
            {
                Console.Write("[" + column.ColumnName + "] ");
            }
            Console.Write("\n update " + Program.Tables[i] + " set ");
            string beetwen = Console.ReadLine();
            Console.Write(" where ");
            string end = Console.ReadLine();
            Console.WriteLine("\n" + new string('-', 80));
            OleDbConnection con = new OleDbConnection(Program.ConnStr);
            OleDbCommand cmd = new OleDbCommand("UPDATE " + Program.Tables[i] + " SET "+beetwen+" WHERE " + end, con);

            Console.WriteLine(new string('-',80)+"\n "+ "UPDATE " + Program.Tables[i] + " SET " + beetwen + " WHERE " + end+"\n"+
                new string('-', 80)+"\n");

            var objDataSet = new DataSet();
            var objDataAdapter = new OleDbDataAdapter(cmd);
            try
            {
                objDataAdapter.Fill(objDataSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            Console.WriteLine(" Complete!");

            Conn.Close();
        }
        static void lineCh(char s)
        {
            for (int i = 0; i < 80; i++)
                Console.Write(s);
            Console.WriteLine();
        }
    }
}
