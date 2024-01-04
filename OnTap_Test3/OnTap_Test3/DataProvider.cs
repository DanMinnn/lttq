using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTap_Test3
{
    class DataProvider
    {
        string connecString = "Data Source=DESKTOP-DANGMIN;Initial Catalog=QuanLyDoanhThu;Integrated Security=True";

        private static DataProvider instance;

        internal static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }
        }

        public DataProvider() { }

        public DataTable execSql(string sql, params object[] args)
        {
            DataTable dat = new DataTable();

            using(SqlConnection connection = new SqlConnection(connecString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                if(args.Length > 0)
                {
                    string[] processSql = sql.Split(' ');
                    List<string> paramList = new List<string>();

                    foreach(string s in processSql)
                    {
                        if (s.StartsWith("@"))
                        {
                            if (s.EndsWith(","))
                            {
                                s.Remove(args.Length - 1);
                            }
                            paramList.Add(s);
                        }
                    }
                    for(int i = 0; i < args.Length; i++)
                    {
                        command.Parameters.AddWithValue(paramList[i], args[i]);
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dat);
                connection.Close();
            }
            return dat;
        }

        public int execNonSql(string sql, params object[] args)
        {
            int effectRows;
            using(SqlConnection connection = new SqlConnection(connecString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                if(args.Length > 0)
                {
                    string[] processSql = sql.Split(' ');
                    List<string> paramsList = new List<string>();

                    foreach(string s in processSql)
                    {
                        if (s.StartsWith("@"))
                        {
                            paramsList.Add(s.TrimEnd(','));
                        }
                    }
                    for(int i = 0; i < args.Length; i++)
                    {
                        command.Parameters.AddWithValue(paramsList[i], args[i]);
                    }
                }
                effectRows = command.ExecuteNonQuery();
                connection.Close();
            }
            return effectRows;
        }

    }
}
