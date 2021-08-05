using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class UserInfoManager
    {
        private static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }
        public static DataRow GetUserInfoByAccount(string account)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                @"SELECT [ID], [Account], [PWD], [Name], [Email]
                    FROM UserInfo
                    WHERE [Account] = @account
                 ";
            // using連線、下命令
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {   //單數化查詢
                    command.Parameters.AddWithValue("@account", account);

                    try
                    {   //開始連線
                        connection.Open();
                        //查資料
                        SqlDataReader reader = command.ExecuteReader();
                        //沒問題把資料放到 DataTable
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();
                        //假設沒有值回傳 null
                        if (dt.Rows.Count == 0)
                            return null;
                        //假設有值回傳 dr
                        DataRow dr = dt.Rows[0];
                        return dr;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }
    }
}
