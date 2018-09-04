using System;
using System.Data;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web;
using System.Collections.Specialized;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net.Sockets;

namespace Infrastructure.Utility
{
    /// <summary>
    /// Http 网络资源通用类。
    /// 具有对远程网络请求的统一资源处理功能.
    /// </summary>
    public class HttpCore
    {
        public HttpCore()
        {
        }

        public static string sRefresh(string url, string ip)
        {
            //远程主机 
            string hostName = url.Replace("http://", "").Split('/')[0];
            string page = url.Replace("http://", "").Replace(hostName, "");
            IPEndPoint hostEP;
            //创建Socket 实例 
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //尝试连接 
                hostEP = new IPEndPoint(IPAddress.Parse(ip), 80);
                socket.Connect(hostEP);
            }
            catch (Exception se)
            {
                string ex = se.Message;
                //socket.Shutdown(SocketShutdown.Both); 
                //关闭Socket 
                return ex;
            }
            //发送给远程主机的请求内容串 
            string sendStr = "GET " + page + " HTTP/1.1\r\nHost: " + hostName + "\r\nConnection: Close\r\n\r\n";
            //创建bytes字节数组以转换发送串 
            byte[] bytesSendStr = new byte[1024];
            //将发送内容字符串转换成字节byte数组 
            bytesSendStr = Encoding.ASCII.GetBytes(sendStr);
            try
            {
                //向主机发送请求 
                socket.Send(bytesSendStr, bytesSendStr.Length, 0);
            }
            catch (Exception ce)
            {
                string ex = ce.Message;
                //socket.Shutdown(SocketShutdown.Both); 
                //关闭Socket 
                socket.Close();
                return ex;
            }
            //声明接收返回内容的字符串 
            string recvStr = "";
            //声明字节数组，一次接收数据的长度为1024字节 
            byte[] recvBytes = new byte[1024];
            //返回实际接收内容的字节数 
            int bytes = 0;
            //循环读取，直到接收完所有数据 
            try
            {
                while (true)
                {
                    bytes = socket.Receive(recvBytes, recvBytes.Length, 0);
                    //读取完成后退出循环 
                    if (bytes <= 0)
                        break;
                    //将读取的字节数转换为字符串 
                    recvStr += Encoding.UTF8.GetString(recvBytes, 0, bytes);
                }
            }
            catch { }
            finally
            {
                socket.Close();
            }
            return recvStr;
        }
        /// <summary>
        /// post
        /// </summary>
        /// <param name="url">POST请求的地址</param>
        /// <param name="paramList">参数列表 例如 name=zhangsan&pass=lisi</param>
        /// <param name="referer">来源地址</param>
        /// <returns></returns>
        public static string Post(String url, String paramList, string referer)
        {
            HttpWebResponse res = null;
            HttpWebRequest req = null;
            string strResult = "";
            try
            {
                req = (HttpWebRequest)WebRequest.Create(url);
                //配置请求header
                req.Headers.Add(HttpRequestHeader.AcceptCharset, "GBK,utf-8;q=0.7,*;q=0.3");
                req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                req.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                req.Accept = "application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
                req.KeepAlive = true;
                req.Referer = referer;
                req.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
                req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US) AppleWebKit/534.7 (KHTML, like Gecko) Chrome/7.0.517.5 Safari/534.7";
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.AllowAutoRedirect = true;
                StringBuilder UrlEncoded = new StringBuilder();
                //对参数进行encode
                Char[] reserved = { '?', '=', '&' };
                byte[] SomeBytes = null;
                if (paramList != null)
                {
                    int i = 0, j;
                    while (i < paramList.Length)
                    {
                        j = paramList.IndexOfAny(reserved, i);
                        if (j == -1)
                        {
                            UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, paramList.Length - i)));
                            break;
                        }
                        UrlEncoded.Append(HttpUtility.UrlEncode(paramList.Substring(i, j - i)));
                        UrlEncoded.Append(paramList.Substring(j, 1));
                        i = j + 1;
                    }
                    SomeBytes = Encoding.UTF8.GetBytes(UrlEncoded.ToString());
                    req.ContentLength = SomeBytes.Length;
                    Stream newStream = req.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                else
                {
                    req.ContentLength = 0;
                }
                //返回请求
                res = (HttpWebResponse)req.GetResponse();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                Stream responseStream = null;
                if (res.ContentEncoding.ToLower() == "gzip")
                {
                    responseStream = new System.IO.Compression.GZipStream(res.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
                }
                else if (res.ContentEncoding.ToLower() == "deflate")
                {
                    responseStream = new System.IO.Compression.DeflateStream(res.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
                }
                else
                {
                    responseStream = res.GetResponseStream();
                }
                StreamReader sr = new StreamReader(responseStream, encode);
                strResult = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                LOGCore.Trace(LOGCore.ST.Day, "【HttpHelper】", ex.ToString());
            }
            finally
            {
                res.Close();
            }
            return strResult;
        }



        /// <summary>
        /// 获取远程Web资源
        /// </summary>
        /// <param name="uri">需要请求获取的Url路径</param>
        /// <returns></returns>
        public static string HttpGetHTML(string uri)
        {
            return HttpGetHTML(uri, System.Text.Encoding.UTF8);
        }


        public static string HttpGetHTML(string uri, System.Text.Encoding code)
        {
            return HttpGetHTML(uri, code, new System.Net.CookieContainer());
        }
        public static string HttpGetHTML(string uri, System.Text.Encoding code, System.Net.CookieContainer cotainer)
        {
            if (uri == null || (uri.ToLower().IndexOf("http://") == -1 && uri.ToLower().IndexOf("https://") == -1))
                return "";
            StreamReader sr = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Headers.Add("UA-CPU", "x86");
                request.Referer = uri;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727;)";
                request.KeepAlive = false;
                request.CookieContainer = cotainer;
                request.Timeout = 8000; //设置远程页面请求超时时间


                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    sr = new StreamReader(myResponse.GetResponseStream(), code);
                    string serverResponse = sr.ReadToEnd().Trim();
                    return serverResponse;
                }
                else
                {
                    //return "失败:Status:" + myResponse.StatusCode.ToString();
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                LOGCore.Trace(LOGCore.ST.Day, "【HttpHelper】", ex.ToString());
                return string.Empty;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        public static string HttpGetHTML(string uri, Encoding code, NetworkCredential nc)
        {
            if (uri == null || (uri.ToLower().IndexOf("http://") == -1 && uri.ToLower().IndexOf("https://") == -1))
                return "";
            StreamReader sr = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Headers.Add("UA-CPU", "x86");
                request.Referer = uri;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727;)";
                request.KeepAlive = false;
                request.Credentials = nc;
                request.Timeout = 8000;

                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();

                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    sr = new StreamReader(myResponse.GetResponseStream(), code);
                    string serverResponse = sr.ReadToEnd().Trim();

                    return serverResponse;
                }
                else
                {
                    return "失败:Status:" + myResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return "失败:ex:" + ex.ToString();
            }

            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }

        /// <summary>
        /// Http Post 资源请求
        /// </summary>
        /// <param name="uri">需要请求获取的Url路径</param>
        /// <returns></returns>
        public static string HttpPost(string uri)
        {
            return HttpPost(uri, System.Text.Encoding.Default);
        }
        public static string HttpPost(string uri, System.Text.Encoding code)
        {
            return HttpPost(uri, code, new System.Net.CookieContainer());
        }
        public static string HttpPost(string uri, System.Text.Encoding code, System.Net.CookieContainer container)
        {
            string[] ps = uri.Split('?');
            string param = ps.Length > 1 ? ps[1] : "";
            Stream stream = null;
            byte[] postData = code.GetBytes(param);
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(uri);
                myRequest.CookieContainer = container;
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = postData.Length;
                stream = myRequest.GetRequestStream();
                stream.Write(postData, 0, postData.Length);

                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), code);
                    string rs = sr.ReadToEnd().Trim();
                    sr.Close();
                    return rs;
                }
                else
                {
                    return "失败:Status:" + myResponse.StatusCode.ToString();
                }
            }
            catch (Exception ex)
            {
                return "失败:ex:" + ex.ToString();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public static int HttpPost(string url, string data, Encoding encoding, out string result)
        {
            try
            {
                byte[] bData = encoding.GetBytes(data);
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(url);
                WebReq.Method = "POST";
                WebReq.ContentType = "application/x-www-form-urlencoded";
                WebReq.ContentLength = bData.Length;
                Stream PostData = WebReq.GetRequestStream();
                PostData.Write(bData, 0, bData.Length);
                PostData.Close();
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                Stream Answer = WebResp.GetResponseStream();
                result = getResult(Answer, encoding);
                return (int)WebResp.StatusCode;
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
                return 0;
            }
        }
        /// <summary>
        /// UTF-8
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string data)
        {
           return HttpPost(url, data, System.Text.Encoding.UTF8);
        }
        public static string HttpPost(string url, string data, System.Text.Encoding encoding)
        {
            try
            {
                byte[] bData = encoding.GetBytes(data);
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(url);
                WebReq.Method = "POST";
                WebReq.ContentType = "application/x-www-form-urlencoded";
                WebReq.ContentLength = bData.Length;
                Stream PostData = WebReq.GetRequestStream();
                PostData.Write(bData, 0, bData.Length);
                PostData.Close();
                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                Stream Answer = WebResp.GetResponseStream();
                return  getResult(Answer, encoding);
            }
            catch (System.Exception ex)
            {
                LOGCore.Trace(LOGCore.ST.Day, "【HttpPost】", ex.ToString());
                return null;
            }
        }

        public static string getResult(Stream ret, Encoding ef)
        {
            StreamReader _Answer = new StreamReader(ret, ef);
            string retStr = _Answer.ReadToEnd();
            return retStr;
        }


    }
}