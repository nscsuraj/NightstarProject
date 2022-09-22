using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace PartnerPortal.Utility
{
    public static class Converter
    {
        public static DataTable ConvertCsvToDataTable(string strFilePath)
        {
            var dt = new DataTable();
            using (var sr = new StreamReader(strFilePath))
            {
                var headers = sr.ReadLine().Split(',');
                foreach (var header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        var dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }
            return dt;
        }

        public static DataTable ConvertXslxToDataTable(string strFilePath, string connString)
        {
            var oledbConn = new OleDbConnection(connString);
            var dt = new DataTable();
            try
            {

                oledbConn.Open();
                var cmd = new OleDbCommand("SELECT * FROM [ItemAuditMkt$]", oledbConn);
                var oleda = new OleDbDataAdapter();
                oleda.SelectCommand = cmd;
                var ds = new DataSet();
                oleda.Fill(ds);

                dt = ds.Tables[0];

            }
            catch(Exception ex)
            {
                DataColumn dc=new DataColumn();
                dc.DataType = typeof (string);
                dt.Columns.Add(dc);
                DataRow dr = dt.NewRow();
                dr[0] = ex.ToString();
                dt.Rows.Add(dr);
                Console.Write(ex.ToString());
            }
            finally
            {

                oledbConn.Close();
            }

            return dt;

        }  
    }
}