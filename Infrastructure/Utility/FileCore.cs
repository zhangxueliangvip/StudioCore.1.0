using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Utility
{
    /// <summary>
    /// 文件工具类
    /// </summary>
    public class FileCore
    {
        public FileCore() { }

        public static System.Text.Encoding GetType(string path)
        {
            //if (!File.Exists(path)) return System.Text.Encoding.Default;

            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                System.Text.Encoding r = GetType(fs);

                return r;
            }
            catch { }
            finally
            {
                fs.Close();
                fs.Dispose();
            }

            return System.Text.Encoding.Default;

        }
        public static void Delete(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public static bool SaveFile(string content, string path)
        {
            try
            {

                if (File.Exists(path))
                {

                    FileInfo file = new FileInfo(path);
                    //if (file.Attributes == FileAttributes.ReadOnly)
                    //file.Attributes = FileAttributes.Normal;

                    //file.SetAccessControl(new )

                    file.Delete();

                }
                var sw = File.CreateText(path);
                using (var s = sw)
                {
                    s.WriteLine(content);
                    s.Flush();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static System.Text.Encoding GetType(FileStream fs)
        {

            BinaryReader r = null;

            byte[] ss = null;

            try
            {
                r = new BinaryReader(fs, System.Text.Encoding.Default);
                ss = r.ReadBytes((int)(fs.Length < 1000 ? fs.Length : 1000));
            }
            catch { }
            finally
            {
                r.Close();
            }



            //编码类型 Coding=编码类型.ASCII;  
            if (ss[0] >= 0xEF)
            {
                if (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF)
                {
                    return System.Text.Encoding.UTF8;
                }
                else if (ss[0] == 0xFE && ss[1] == 0xFF)
                {
                    return System.Text.Encoding.BigEndianUnicode;
                }
                else if (ss[0] == 0xFF && ss[1] == 0xFE)
                {
                    return System.Text.Encoding.Unicode;
                }
                else
                {
                    return System.Text.Encoding.Default;
                }
            }
            else
            {
                return GetNoBomType(ss, ss.Length);
            }


        }

        // 针对无BOM的内容做判断,不是分准确--http://www.joymo.cn
        public static System.Text.Encoding GetNoBomType(byte[] buf, int len)
        {
            byte chr;

            for (int i = 0; i < len; i++)
            {
                chr = buf[i];

                if (chr >= 0x80)
                {
                    if ((chr & 0xf0) == 0xe0)
                    {
                        i++;
                        chr = buf[i];
                        if ((chr & 0xc0) == 0x80)
                        {
                            i++;
                            chr = buf[i];
                            if ((chr & 0xc0) == 0x80)
                                return System.Text.Encoding.UTF8;
                            else return System.Text.Encoding.Default;
                        }
                        else
                            return System.Text.Encoding.Default;
                    }
                    else
                        return System.Text.Encoding.Default;
                }
            }
            return System.Text.Encoding.Default;
        }

        public static string ReadTextFileString(string filePath, System.Text.Encoding encode)
        {
            string strResult = string.Empty;

            StreamReader sr = null;

            try
            {
                if (File.Exists(filePath))
                {
                    sr = new StreamReader(filePath, encode);
                    strResult = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                strResult = ex.ToString();
            }

            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }

            return strResult;
        }

        public static string ReadTextFileString(string filePath)
        {
            return ReadTextFileString(filePath, System.Text.Encoding.Default);
        }
        /// <summary>
        /// 写文本文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="strContent">文件内容</param>
        /// <returns></returns>
        public static bool WriteTextFile(string filePath, string strContent, bool append, System.Text.Encoding encode)
        {
            StreamWriter sw = null;
            try
            {
                if (!File.Exists(filePath))
                {
                    FileInfo fi = new FileInfo(filePath);
                    Directory.CreateDirectory(fi.DirectoryName);
                }

                sw = new StreamWriter(filePath, append, encode);

                sw.Write(strContent);


            }
            catch
            {
                return false;
            }

            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }

            return true;
        }

        public static bool WriteTextFile(string filePath, string strContent, bool append)
        {
            return WriteTextFile(filePath, strContent, append, System.Text.Encoding.Default);
        }

        public static bool WriteLineTextFile(string fullName, string strContent, System.Text.Encoding encoder)
        {
            StreamWriter sw = null;
            try
            {

                if (!File.Exists(fullName))
                {
                    FileInfo fi = new FileInfo(fullName);
                    Directory.CreateDirectory(fi.DirectoryName);
                }
                sw = new StreamWriter(fullName, true, encoder);
                sw.WriteLine(strContent);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }

            return true;
        }
        public static string GetFileName(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            return fi.Name;
        }


        public static bool CreateDirectory(string path)
        {
            string pattern = @"[^\\]+\.[\.\w]+$";

            path = Regex.Replace(path, pattern, "", RegexOptions.IgnoreCase);

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 返回文件名中的扩展名
        /// </summary>
        /// <param name="fileName">文件名或Url或文件路径</param>
        /// <returns>扩展名</returns>
        public static string GetExtention(string fileName)
        {
            string ext = Regex.Replace(fileName, @"(\?|#).*", "");

            ext = Regex.Replace(ext, @".*\.", "");

            return ext.Trim();
        }


        #region 写文件
        /// <SUMMARY>  
        /// 写文件  
        /// </SUMMARY>  
        /// <PARAM name="Path">文件路径</PARAM>  
        /// <PARAM name="Strings">文件内容</PARAM>  
        public static void WriteFile(string Path, string Strings)
        {

            if (!System.IO.File.Exists(Path))
            {
                //Directory.CreateDirectory(Path);  

                System.IO.FileStream f = System.IO.File.Create(Path);
                f.Close();
                f.Dispose();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, true, System.Text.Encoding.UTF8);
            f2.WriteLine(Strings);
            f2.Close();
            f2.Dispose();


        }
        #endregion

    }
}
