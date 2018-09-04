using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Infrastructure.Utility
{
    public class CsvCore
    {
        public static void WriteCSV(string filePathName, List<String[]> ls, System.Text.Encoding encoding)
        {
            WriteCSV(filePathName, false, ls, encoding);
        }
        //write a file, existed file will be overwritten if append = false
        public static void WriteCSV(string filePathName, bool append, List<String[]> ls, System.Text.Encoding encoding)
        {
            StreamWriter fileWriter = new StreamWriter(filePathName, append, encoding);
            foreach (String[] strArr in ls)
            {
                fileWriter.WriteLine(String.Join(",", strArr));
            }
            fileWriter.Flush();
            fileWriter.Close();
        }
        public static void WriteCSV(string filePathName, DataTable dt)
        {
            List<string[]> ls = new List<string[]>();
            string[] tempArray = new string[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                tempArray[i] = dt.Columns[i].ToString();
            }
            ls.Add(tempArray);
            foreach (DataRow dr in dt.Rows)
            {
                string[] contentArray = new string[dt.Columns.Count];
                for (int i = 0; i < dr.ItemArray.Length; i++)
                {
                    contentArray[i] = dr.ItemArray[i].ToString();
                }
                ls.Add(contentArray);
            }
            WriteCSV(filePathName, ls, Encoding.GetEncoding("GB2312"));
        }
        public static List<String[]> ReadCSV(string filePathName)
        {
            List<String[]> ls = new List<String[]>();
            StreamReader fileReader = new StreamReader(filePathName);
            string strLine = "";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length > 0)
                {
                    ls.Add(strLine.Split(','));
                    //Debug.WriteLine(strLine);
                }
            }
            fileReader.Close();
            return ls;
        }
        public static string DatatableToString(DataTable da, bool isRowOneHeader)
        {
            StringBuilder sb = new StringBuilder();
            if (isRowOneHeader)
            {
                foreach (DataColumn k in da.Columns)
                {
                    sb.Append(k.ColumnName);
                    sb.Append(",");

                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
            }
            foreach (DataRow dr in da.Rows)
            {
                foreach (DataColumn k in da.Columns)
                {
                    sb.Append(dr[k.ColumnName]);
                    sb.Append(",");

                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static DataTable stringToDataTable(string content, bool isRowOneHeader, System.Text.Encoding encoding)
        {
            DataTable csvDataTable = new DataTable();
            String[] csvData = content.Split('\n');
            int index = 0; //will be zero or one depending on isRowOneHeader
            string[] title = csvData[0].Split(',');
            for (int i = 0; i < title.Length; i++)
            {
                if (isRowOneHeader)
                {
                    csvDataTable.Columns.Add(new DataColumn(title[i]));
                    index = 1;
                }
                else
                {
                    csvDataTable.Columns.Add(new DataColumn("title" + i.ToString()));
                }
            }
            //populate the DataTable
            for (int i = index; i < csvData.Length; i++)
            {
                string temp = "";
                for (int tt = 0; tt < csvDataTable.Columns.Count - 2; tt++)
                {
                    temp += ",";
                }

                if (csvData[i].IndexOf(temp) >= 0)
                {
                    continue;
                }

                //create new rows
                DataRow row = csvDataTable.NewRow();

                string[] items = csvData[i].Split(',');

                if (items.Length != csvDataTable.Columns.Count)
                    continue;

                for (int j = 0; j < title.Length; j++)
                {
                    string value = items[j];
                    value = new System.Text.RegularExpressions.Regex("^\"|\"$").Replace(value, "").Trim();
                    //fill them
                    row[j] = value;
                }



                //add rows to over DataTable
                csvDataTable.Rows.Add(row);
            }

            //return the CSV DataTable
            return csvDataTable;

        }

        public static DataTable csvToDataTable(string file, bool isRowOneHeader, System.Text.Encoding encoding)
        {

            DataTable csvDataTable = new DataTable();
            //no try/catch - add these in yourselfs or let exception happen
            String[] csvData = File.ReadAllLines(file, encoding);
            int index = 0; //will be zero or one depending on isRowOneHeader
            string[] title = csvData[0].Split(',');
            for (int i = 0; i < title.Length; i++)
            {
                if (isRowOneHeader)
                {
                    csvDataTable.Columns.Add(new DataColumn(title[i]));
                    index = 1;
                }
                else
                {
                    csvDataTable.Columns.Add(new DataColumn("title" + i.ToString()));
                }
            }
            //populate the DataTable
            for (int i = index; i < csvData.Length; i++)
            {
                //create new rows
                DataRow row = csvDataTable.NewRow();

                string[] items = csvData[i].Split(',');

                if (items.Length != csvDataTable.Columns.Count) continue;

                for (int j = 0; j < title.Length; j++)
                {
                    //fill them
                    row[j] = items[j];
                }

                //add rows to over DataTable
                csvDataTable.Rows.Add(row);
            }

            //return the CSV DataTable
            return csvDataTable;

        }
    }
}
