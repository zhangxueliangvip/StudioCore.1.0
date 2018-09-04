using System;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Threading;
using System.Collections.Generic;
using System.Web;

namespace Infrastructure.Utility
{
    /// <summary>
    /// 对日志存储的封装
    /// </summary>
    public class LOGCore
    {
        static LOGCore()
        {
            if (HttpContext.Current != null)
                LOGCore.LogPath = HttpContext.Current.Server.MapPath(Config.LogAbsolutePath);
            else
                LOGCore.LogPath = System.Windows.Forms.Application.StartupPath + Config.LogRelativePath;


        }

        #region 私有变量
        private static string LineBreak = new string('-', 100);
        #endregion

        #region 公开属性

        /// <summary>
        /// 存储路径
        /// </summary>
        public static string LogPath = Config.LogRelativePath;

        /// <summary>
        /// 文件生成规则
        /// </summary>
        public static ST SaveType = ST.Day;

        /// <summary>
        /// 日志文件默认前缀
        /// </summary>
        public static string DefaultPrefix =Config.LogDefaultPrefix;
        #endregion

        #region 公开方法


        /// <summary>
        /// 数据库记录错误的方法
        /// </summary>
        /// <param name="st"></param>
        /// <param name="prefix"></param>
        /// <param name="e"></param>
        /// <param name="funName"></param>
        /// <param name="sqlText_proName"></param>
        /// <param name="parms"></param>
        public static void DBExp(ST st, string prefix, Exception e, string funName, string sqlText_proName, SqlParameter[] parms)
        {
            string fp = GetFilePath(st, prefix, "DB");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("异常：" + e.Message + "\r\n");
            sb.Append("方法名称：" + funName + "\r\n");
            sb.Append("SQL语句&存储过程：" + sqlText_proName + "\r\n");
            if (parms != null)
            {
                sb.Append(LineBreak.Insert(50, "参数列表") + "\r\n\r\n");
                foreach (var parm in parms)
                {
                    sb.Append(string.Format("{0}：{1}\r\n", parm.ParameterName, parm.Value.ToString()));
                }
                sb.Append("\r\n");  
            }

            sb.Append(LineBreak + "\r\n\r\n");

            WriteFile(fp, sb.ToString());

        }


        /// <summary>
        /// 记录日志信息
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="Info">自定义信息</param>
        public static void Trace(ST st, string prefix, string Info, Dictionary<string, string> param)
        {
            string fp = GetFilePath(st, prefix, "INFO");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("信息：" + Info + "\r\n");

            if (param != null)
            {
                foreach (KeyValuePair<string, string> kv in param)
                {
                    sb.Append(string.Format("{0}：{1} \r\n", kv.Key, kv.Value));
                }
            }
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }

        /// <summary>
        /// 3个参数的重写
        /// </summary>
        /// <param name="st"></param>
        /// <param name="prefix"></param>
        /// <param name="Info"></param>
        public static void Trace(ST st, string prefix, string Info)
        {
            Trace(st, prefix, Info, null);
        }
        /// <summary>
        /// 错误信息记录 方法
        /// </summary>
        /// <param name="st"></param>
        /// <param name="prefix"></param>
        /// <param name="info"></param>
        /// <param name="param"></param>
        public static void Error(ST st, string prefix, string info, Dictionary<string, string> param)
        {
            string fp = GetFilePath(st, prefix, "ERR");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("错误：" + info + "\r\n");
            if (param != null)
            {
                foreach (KeyValuePair<string, string> kv in param)
                {
                    sb.Append(string.Format("{0}：{1} \r\n", kv.Key, kv.Value));
                }
            }

            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }

        /// <summary>
        /// 记录日常问题。
        /// </summary>
        /// <param name="st"></param>
        /// <param name="prefix"></param>
        /// <param name="info"></param>
        /// <param name="param"></param>
        public static void Excep(ST st, string prefix, Exception e, string info, Dictionary<string, string> param)
        {
            string fp = GetFilePath(st, prefix, "EX");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("位置：" + info + "\r\n");
            sb.Append("异常：" + e.ToString() + "\r\n");
            if (param != null)
            {
                foreach (KeyValuePair<string, string> kv in param)
                {
                    sb.Append(string.Format("{0}：{1} \r\n", kv.Key, kv.Value));
                }
            }

            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }




        /// <summary>
        /// 重写，增加Dic传值方法
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="Info">自定义信息</param>
        /// <param name="param">存储的变量列表</param>
        public static void Debug(ST st, string prefix, Dictionary<string, string> param)
        {
            string fp = GetFilePath(st, prefix, "DEB");
            StringBuilder sb = new StringBuilder();
            sb.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            if (param != null)
            {
                foreach (KeyValuePair<string, string> kv in param)
                {
                    sb.Append(string.Format("{0}：{1} \r\n", kv.Key, kv.Value));
                }
            }
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }




        #endregion

        #region 私有函数

        /// <summary>
        /// 返回文件完整路径
        /// </summary>
        /// <param name="st">文件生成规则</param>
        /// <param name="prefix">文件前缀</param>
        /// <param name="type">文件内容类型</param>
        /// <returns>文件完整路径</returns>
        public static string GetFilePath(ST st, string prefix, string type)
        {
            string ext = ".log";


            string path = LogPath.Replace("/", "\\");

            if (path.Substring(path.Length - 1) != "\\")
            {
                path += "\\";
            }


            if (prefix.IndexOf(@"\") > -1)//如果prefix 参数是当做目录使用 [sss\111\222 ]
            {

                path = path + prefix;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += type + "_";
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += prefix + "_" + type;
            }


            switch (st)
            {
                case ST.Fixed:
                    path += ext;
                    break;
                case ST.Day:
                    path += DateTime.Now.ToString("yyyyMMdd") + ext;
                    break;
                case ST.Hour:
                    path += DateTime.Now.ToString("yyyyMMddHH") + ext;
                    break;
            }
            return (path);
        }

        /// <summary>
        /// 返回由object[]连成的字符串
        /// </summary>
        /// <param name="param">object数组</param>
        /// <returns>连接好的字符串</returns>
        private static string GetParamList(object[] param)
        {
            if (param == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (object p in param)
            {
                sb.Append("(" + ((p != null) ? p.ToString() : "null") + ")");
                if (p != param[param.Length - 1])
                {
                    sb.Append(",");
                }
            }
            return (sb.ToString());
        }

        /// <summary>
        /// 负责将日志内容写入文件
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <param name="Content">日志内容</param>
        private static void WriteFile(string FilePath, string Content)
        {
            Type type = typeof(LOGCore);
            try
            {
                if (!Monitor.TryEnter(type))
                {
                    Monitor.Enter(type);
                    return;
                }
                using (StreamWriter writer = new StreamWriter(FilePath, true, System.Text.Encoding.Default))
                {
                    writer.Write(Content);
                    writer.Close();
                }
            }
            catch (System.IO.IOException e)
            {
                throw (e);
            }
            catch { }
            finally
            {
                Monitor.Exit(type);
            }
        }

        #endregion

        #region 枚举
        /// <summary>
        /// 文件生成规则
        /// </summary>
        public enum ST : int
        {
            /// <summary>
            /// 固定文件名
            /// </summary>
            Fixed = 1,
            /// <summary>
            /// 按天生成新的文件
            /// </summary>
            Day = 2,
            /// <summary>
            /// 按小时生成新的文件
            /// </summary>
            Hour = 3
        }
        #endregion

    }
}
