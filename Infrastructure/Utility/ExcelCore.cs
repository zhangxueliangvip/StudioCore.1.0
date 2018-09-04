using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace Infrastructure.Utility
{
    public class ExcelCore
    {
        /// <summary>
        /// 读取excel ,默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            IWorkbook workbook = null;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    if (strFileName.IndexOf(".xlsx") > 0) // 2007版本
                        workbook = new XSSFWorkbook(file);
                    else if (strFileName.IndexOf(".xls") > 0) // 2003版本
                        workbook = new HSSFWorkbook(file);
                }
                catch (Exception)
                {

                    workbook = new XSSFWorkbook(file);
                }
            }
            ISheet sheet = workbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// 读取excel ,默认第一行为标头
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, Stream fileStream)
        {
            DataTable dt = new DataTable();

            IWorkbook workbook = null;
            try
            {
                if (strFileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fileStream);
                else if (strFileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fileStream);
            }
            catch (Exception ex)
            {
                LOGCore.Trace(LOGCore.ST.Day, "【ExcelHelper】", ex.ToString());
                workbook = new HSSFWorkbook(fileStream);
            }
            ISheet sheet = workbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }


        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filePath"></param>
        public static void DownloadExcel(DataTable dt, string filePath)
        {
            try
            {
                IWorkbook book = null;
                if (!string.IsNullOrEmpty(filePath) && null != dt && dt.Rows.Count > 0)
                {
                    if (filePath.IndexOf(".xlsx") > 0) // 2007版本
                    {
                        book = new XSSFWorkbook();
                    }
                    else if (filePath.IndexOf(".xls") > 0) // 2003版本
                    {
                        book = new HSSFWorkbook();
                    }
                    else
                    {
                        filePath += ".xls";
                        book = new HSSFWorkbook();
                    }
                    ISheet sheet = book.CreateSheet(dt.TableName);
                    IRow row = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row2 = sheet.CreateRow(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i][j]));
                        }
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        book.Write(ms);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            byte[] data = ms.ToArray();
                            fs.Write(data, 0, data.Length);
                            fs.Flush();
                        }
                        book = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LOGCore.Trace(LOGCore.ST.Day, "ExcelHelper", ex.ToString());
            }
        }
        public static MemoryStream Export(DataTable dt)
        {
            //创建Excel文件的对象  
            var book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet  
            var sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题  
           var row1 = sheet1.CreateRow(0);
            //row1.RowStyle.FillBackgroundColor = "";  
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row1.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
            }
            //将数据逐步写入sheet1各个行  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var rowtemp = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    rowtemp.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString().Trim());
                }
            }
            // 写入到客户端   
           var ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        /// <summary>
        /// 表格导出
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filePath"></param>
        /// <param name="isExportTitle">true:导出标题 false:不导出标题</param>
        public static void DownloadExcel(DataTable dt, string filePath, bool isExportTitle)
        {
            if (!string.IsNullOrEmpty(filePath) && null != dt && dt.Rows.Count > 0)
            {
                IWorkbook book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet(dt.TableName);
                int tempRowNum = 0;
                if (isExportTitle)
                {
                    #region 包含标头导出
                    IRow row = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                    }
                    tempRowNum++;
                    #endregion
                }
                #region 默认格式
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row2 = sheet.CreateRow(i + tempRowNum);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i][j]));
                    }
                }
                #endregion

                using (MemoryStream ms = new MemoryStream())
                {
                    #region 写入文件
                    book.Write(ms);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    book = null;
                    #endregion
                }
            }
        }

        /// <summary>
        /// 导出多张表(必须要有表名)
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="filePath"></param>
        public static void DownloadExcel(DataSet ds, string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && null != ds && ds.Tables.Count > 0)
            {
                IWorkbook book = new HSSFWorkbook();
                #region 默认格式

                for (int s = 0; s < ds.Tables.Count; s++)
                {
                    ISheet sheet = book.CreateSheet(ds.Tables[s].TableName);
                    IRow row = sheet.CreateRow(0);
                    IDataFormat dataformat = book.CreateDataFormat();//样式
                    for (int i = 0; i < ds.Tables[s].Columns.Count; i++)
                    {
                        row.CreateCell(i).SetCellValue(ds.Tables[s].Columns[i].ColumnName);//标题
                    }

                    for (int i = 0; i < ds.Tables[s].Rows.Count; i++)
                    {
                        IRow row2 = sheet.CreateRow(i + 1);
                        for (int j = 0; j < ds.Tables[s].Columns.Count; j++)
                        {
                            Type t = ds.Tables[s].Rows[i][j].GetType();
                            switch (t.Name)
                            {
                                case "Int32":
                                    row2.CreateCell(j).SetCellValue(Convert.ToInt32(ds.Tables[s].Rows[i][j]));
                                    break;
                                case "Double":
                                    row2.CreateCell(j).SetCellValue(Convert.ToDouble(ds.Tables[s].Rows[i][j]));
                                    break;
                                case "Decimal":
                                    row2.CreateCell(j).SetCellValue(Convert.ToDouble(ds.Tables[s].Rows[i][j]));
                                    break;
                                case "DateTime":
                                    row2.CreateCell(j).SetCellValue(Convert.ToDateTime(ds.Tables[s].Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss"));
                                    ICellStyle style0 = book.CreateCellStyle();
                                    style0.DataFormat = dataformat.GetFormat("@");//文本样式
                                    row2.GetCell(j).CellStyle = style0;
                                    break;
                                default:
                                    row2.CreateCell(j).SetCellValue(Convert.ToString(ds.Tables[s].Rows[i][j]));
                                    break;
                            }

                        }
                    }
                }

                #endregion

                using (MemoryStream ms = new MemoryStream())
                {
                    #region 写入文件
                    book.Write(ms);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    book = null;
                    #endregion
                }
            }
        }


    }
}
