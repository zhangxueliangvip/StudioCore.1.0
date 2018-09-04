using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Infrastructure.Utility
{
    public class FileTypeCore
    {
        public static string[] fileExtensions = { "JPG", "GIF", "BMP", "PNG", "DOC", "XLS", "DOCX", "XLSX", "MID", "RM", "RMVB", "AVI", "MPG", "DAT", "ASX", "WMV", "APK", "MP4", "MP3" };
        /// <summary>
        /// 检测文件真实 扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileExtension CheckTrueFileName(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string bx = " ";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                bx = buffer.ToString();
                buffer = r.ReadByte();
                bx += buffer.ToString();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            r.Close();
            fs.Close();
            int type = 0;
            int.TryParse(bx, out type);
            return (FileExtension)type;
        }

        /// <summary>
        /// 判断文件是否为安全文件（不会释放stream，请手动释放）
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="exts"></param>
        /// <returns></returns>
        public static bool IsSafeFile(Stream stream, params FileExtension[] exts)
        {
            if (stream == null || exts.Length < 1)
            {
                return false;
            }
            var bytes = new byte[0];
            stream.Read(bytes, 0, (int)stream.Length);
            return IsSafeFile(bytes, exts);
        }
        /// <summary>
        /// 判断文件是否为安全文件 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="exts"></param>
        /// <returns></returns>
        public static bool IsSafeFile(byte[] content, params FileExtension[] exts)
        {
            if (content == null || content.Length < 2 || exts == null || exts.Length < 1)
            {
                return false;
            }

            var typeStr = content[0].ToString() + content[1].ToString();
            var fileType = (FileExtension)IntegerCore.Parse(typeStr);
            return exts.Contains(fileType);

        }
        /// <summary>
        /// 判断文件是否为安全文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool IsSafeFile(string filePath, params FileExtension[] exts)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;
            FileInfo f = new FileInfo(filePath);
            try
            {
                long length = f.Length;
            }
            catch
            {
                return false;
            }
            foreach (FileExtension fe in exts)
            {
                if (string.Compare("." + fe.ToString(), f.Extension, true) == 0 || CheckTrueFileName(f.FullName) == fe)
                {
                    return true;
                }
            }
            //判断不成功则返回false
            return false;
        }

        /// <summary>
        /// 根据前两个字节获取文件后缀名，获取不到时使用文件名截取方法获取
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="filecontent"></param>
        /// <returns></returns>
        public static string GetFileExt(string filename, byte[] filecontent)
        {
            var result = string.Empty;
            if (filecontent != null && filecontent.Length > 1)
            {
                var fileCode = filecontent[0].ToString() + filecontent[1].ToString();
                switch ((FileExtension)IntegerCore.Parse(fileCode))
                {
                    case FileExtension.JPG:
                        result = "jpg";
                        break;
                    case FileExtension.JS:
                        result = "js";
                        break;
                    case FileExtension.GIF:
                        result = "gif";
                        break;
                    case FileExtension.BMP:
                        result = "bmp";
                        break;
                    case FileExtension.PNG:
                        result = "png";
                        break;
                    case FileExtension.RAR:
                        result = "rar";
                        break;
                    //case FileExtension.ZIP:
                    //    result = "zip";
                    //    break;
                    case FileExtension.MP3:
                        result = "mp3";
                        break;
                    default:
                        if (filecontent[0] == 0 && filecontent[1] == 0 && filecontent[2] == 0)
                            result = "mp4";
                        break;
                }
            }
            else if (!string.IsNullOrEmpty(filename) && filename.IndexOf('.') > -1)
            {
                result = filename.Substring(filename.LastIndexOf('.') + 1);
            }
            return result;
        }

        #region
        /// <summary>
        /// 根据前两个字节获取文件的传输type，获取不到时使用截取后缀名方式获取
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="filecontent"></param>
        /// <returns></returns>
        public static string GetFileMimeType(string filename, byte[] filecontent)
        {
            var result = string.Empty;
            var fileExt = GetFileExt(filename, filecontent);
            if (fileExt.Length > 0)
            {
                switch (fileExt)
                {
                    case "jpg":
                    case "jpeg":
                        result = "image/jpeg";
                        break;
                    case "gif":
                        result = "image/gif";
                        break;
                    case "bmp":
                        result = "image/bmp";
                        break;
                    case "png":
                        result = "image/png";
                        break;
                    case "js":
                        result = "application/x-javascript";
                        break;

                }
            }
            return result;
        }
        #endregion
    }

    public enum FileExtension
    {
        JPG = 255216,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        COM = 7790,
        EXE = 7790,
        DLL = 7790,
        RAR = 8297,
        //ZIP = 8075,
        XML = 6063,
        HTML = 6033,
        ASPX = 239187,
        CS = 117115,
        JS = 119105,
        TXT = 210187,
        SQL = 255254,
        BAT = 64101,
        BTSEED = 10056,
        RDP = 255254,
        PSD = 5666,
        PDF = 3780,
        CHM = 7384,
        LOG = 70105,
        REG = 8269,
        HLP = 6395,
        DOC = 8075,
        XLS = 208207,
        DOCX = 208207,
        XLSX = 208207,
        MP3 = 7368,//MP4前三位是0
    }

}
