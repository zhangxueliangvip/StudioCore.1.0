using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using System.Data;


namespace Infrastructure.Utility
{
    /// <summary>
    /// XML Helper扩展
    /// </summary>
    public class XmlCore
    {
        #region Fields and Properties

        public enum XmlType
        {
            File,
            String
        }

        #endregion

        #region  Methods

        /// <summary>
        ///     创建XML文档
        /// </summary>
        /// <param name="name">根节点名称</param>
        /// <param name="type">根节点的一个属性值</param>
        /// <returns></returns>
        public static XmlDocument CreateXmlDocument(string name, string type)
        {
            /**************************************************
            * .net中调用方法：写入文件中,则：
            *document = XmlOperate.CreateXmlDocument("sex", "sexy");
            *document.Save("c:/bookstore.xml");
            ************************************************/
            XmlDocument xmlDocument;
            try
            {
                xmlDocument = new XmlDocument();
                xmlDocument.LoadXml("<" + name + "/>");
                var rootElement = xmlDocument.DocumentElement;
                rootElement.SetAttribute("type", type);
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                return null;
            }
            return xmlDocument;
        }

        /// <summary>
        ///     删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <returns></returns>
        public static void Delete(string path, string node, string attribute)
        {
            /**************************************************
             * 使用示列:
             * XmlHelper.Delete(path, "/Node", "")
             * XmlHelper.Delete(path, "/Node", "Attribute")
             ************************************************/
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                var selectSingleNode = xmlDocument.SelectSingleNode(node);
                var xmlElement = (XmlElement) selectSingleNode;
                if (attribute.Equals(""))
                    selectSingleNode.ParentNode.RemoveChild(selectSingleNode);
                else
                    xmlElement.RemoveAttribute(attribute);
                xmlDocument.Save(path);
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
            }
        }


        /// <summary>
        ///     读取XML资源到DataSet中
        /// </summary>
        /// <param name="source">XML资源，文件为路径，否则为XML字符串</param>
        /// <param name="xmlType">XML资源类型</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string source, XmlType xmlType)
        {
            try
            {
                var dataSet = new DataSet();
                if (xmlType == XmlType.File)
                {
                    dataSet.ReadXml(source);
                }
                else
                {
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(source);
                    var xmlNodeReader = new XmlNodeReader(xmlDocument);
                    dataSet.ReadXml(xmlNodeReader);
                }
                return dataSet;
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                return null;
            }
        }


        /// <summary>
        ///     获得xml文件中指定节点的节点数据
        /// </summary>
        /// <returns></returns>
        public static string GetNodeInfoByNodeName(string path, string nodeName)
        {
            try
            {
                var xmlString = "";
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                var documentElementRoot = xmlDocument.DocumentElement;
                var selectSingleNode = documentElementRoot.SelectSingleNode("//" + nodeName);
                if (selectSingleNode != null)
                    xmlString = selectSingleNode.InnerText;
                return xmlString;
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                return null;
            }
        }


        /// <summary>
        ///     读取XML资源中的指定节点内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>节点内容</returns>
        public static string GetNodeValue(string source, XmlType xmlType, string nodeName)
        {
            var xmlDocument = new XmlDocument();
            if (xmlType == XmlType.File)
                xmlDocument.Load(source);
            else
                xmlDocument.LoadXml(source);
            var documentElement = xmlDocument.DocumentElement;
            var selectSingleNode = documentElement.SelectSingleNode("//" + nodeName);
            return selectSingleNode.InnerText;
        }


        /// <summary>
        ///     读取XML资源中的指定节点属性的内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="nodeName">属性节点名称</param>
        /// <param name="attributeString"></param>
        /// <returns>节点内容</returns>
        public static string GetNodeAttributeValue(string source, XmlType xmlType, string nodeName,
            string attributeString)
        {
            var xmlDocument = new XmlDocument();
            if (xmlType == XmlType.File)
                xmlDocument.Load(source);
            else
                xmlDocument.LoadXml(source);
            var documentElement = xmlDocument.DocumentElement;
            var selectSingleNode = (XmlElement) documentElement.SelectSingleNode("//" + nodeName);
            //if (selectSingleNode != null)
            return selectSingleNode.GetAttribute(attributeString);
        }

        /// <summary>
        ///     读取XML资源中的指定节点内容
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>节点内容</returns>
        public static string GetNodeValue(string source, string nodeName)
        {
            if (source == null || nodeName == null || source == "" || nodeName == "" ||
                source.Length < nodeName.Length * 2)
                return null;
            var start = source.IndexOf("<" + nodeName + ">", StringComparison.Ordinal) + nodeName.Length + 2;
            var end = source.IndexOf("</" + nodeName + ">", StringComparison.Ordinal);
            if (start == -1 || end == -1)
                return null;
            return start >= end ? null : source.Substring(start, end - start);
        }


        /// <summary>
        ///     读取XML资源到DataTable中
        /// </summary>
        /// <param name="source">XML资源，文件为路径，否则为XML字符串</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="tableName">表名称</param>
        /// <returns>DataTable</returns>
        public static DataTable GetTable(string source, XmlType xmlType, string tableName)
        {
            try
            {
                var dataSet = new DataSet();
                if (xmlType == XmlType.File)
                {
                    dataSet.ReadXml(source);
                }
                else
                {
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(source);
                    var xmlNodeReader = new XmlNodeReader(xmlDocument);
                    dataSet.ReadXml(xmlNodeReader);
                }
                return dataSet.Tables[tableName];
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                return null;
            }
        }


        /// <summary>
        ///     读取XML资源中指定的DataTable的指定行指定列的值
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colName">列名</param>
        /// <returns>值，不存在时返回Null</returns>
        public static object GetTableCell(string source, XmlType xmlType, string tableName, int rowIndex, string colName)
        {
            try
            {
                var dataSet = new DataSet();
                if (xmlType == XmlType.File)
                {
                    dataSet.ReadXml(source);
                }
                else
                {
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(source);
                    var xmlNodeReader = new XmlNodeReader(xmlDocument);
                    dataSet.ReadXml(xmlNodeReader);
                }
                return dataSet.Tables[tableName].Rows[rowIndex][colName];
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                return null;
            }
        }


        /// <summary>
        ///     读取XML资源中指定的DataTable的指定行指定列的值
        /// </summary>
        /// <param name="source">XML资源</param>
        /// <param name="xmlType">XML资源类型：文件，字符串</param>
        /// <param name="tableName">表名</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <returns>值，不存在时返回Null</returns>
        public static object GetTableCell(string source, XmlType xmlType, string tableName, int rowIndex, int colIndex)
        {
            try
            {
                var dataSet = new DataSet();
                if (xmlType == XmlType.File)
                {
                    dataSet.ReadXml(source);
                }
                else
                {
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(source);
                    var xmlNodeReader = new XmlNodeReader(xmlDocument);
                    dataSet.ReadXml(xmlNodeReader);
                }
                return dataSet.Tables[tableName].Rows[rowIndex][colIndex];
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                return null;
            }
        }


        /// <summary>
        ///     获取一个字符串xml文档中的dataSet
        /// </summary>
        /// <param name="xmlString">含有xml信息的字符串</param>
        /// <param name="dataSet"></param>
        public static void GetXmlValueDataSet(string xmlString, ref DataSet dataSet)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlString);
                var xmlNodeReader = new XmlNodeReader(xmlDocument);
                dataSet.ReadXml(xmlNodeReader);
                xmlNodeReader.Close();
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
            }
        }


        /// <summary>
        ///     插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            /**************************************************
            * 使用示列:
            * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
            * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
            * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
            ************************************************/
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                var selectSingleNode = xmlDocument.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        var xmlElement = (XmlElement) selectSingleNode;
                        xmlElement.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    var xmlElement = xmlDocument.CreateElement(element);
                    if (attribute.Equals(""))
                        xmlElement.InnerText = value;
                    else
                        xmlElement.SetAttribute(attribute, value);
                    selectSingleNode.AppendChild(xmlElement);
                }
                xmlDocument.Save(path);
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
            }
        }


        /// <summary>
        ///     读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        public static string Read(string path, string node, string attribute)
        {
            /**************************************************
             * 使用示列:
             * XmlHelper.Read(path, "/Node", "")
             * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
             ************************************************/
            var value = "";
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                var selectSingleNode = xmlDocument.SelectSingleNode(node);
                value = attribute.Equals("")? selectSingleNode.InnerText: selectSingleNode.Attributes[attribute].Value;
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
            }
            return value;
        }

        /// <summary>
        ///     读取xml的节点
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public static string ReadXml(string path, string nodeName, string name) //读取XML
        {
            try
            {
                var xmlValue = "";
                if (!File.Exists(path))
                    return xmlValue;
                var myFile = new FileStream(path, FileMode.Open); //打开xml文件 
                var xmlTextReader = new XmlTextReader(myFile); //xml文件阅读器 
                while (xmlTextReader.Read())
                    if (xmlTextReader.Name == nodeName)
                    {
                        //获取服务器的地址//获取升级文档的最后一次更新日期 
                        xmlValue = xmlTextReader.GetAttribute(name);
                        break;
                    }
                xmlTextReader.Close();
                myFile.Close();
                return xmlValue;
            }
            catch (Exception exception)
            {
                 LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                return null;
            }
        }


        /// <summary>
        ///     读取xml文件，并将文件序列化为类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T ReadXML<T>(string path)
        {
            try
            {
                var reader = new XmlSerializer(typeof(T));
                var file = new StreamReader(path);
                return (T)reader.Deserialize(file);
            }
            catch (Exception exception)
            {
                LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                return default(T);
            }
        }


        /// <summary>
        ///     将DataTable写入XML文件中
        /// </summary>
        /// <param name="dataTable">含有数据的DataTable</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveTableToFile(DataTable dataTable, string filePath)
        {
            try
            {
                var dataSet = new DataSet("Config");
                dataSet.Tables.Add(dataTable.Copy());
                dataSet.WriteXml(filePath);
            }
            catch (Exception exception)
            {
                LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
            }
        }


        /// <summary>
        ///     将DataTable以指定的根结点名称写入文件
        /// </summary>
        /// <param name="dataTable">含有数据的DataTable</param>
        /// <param name="rootName">根结点名称</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveTableToFile(DataTable dataTable, string rootName, string filePath)
        {
            try
            {
                var dataSet = new DataSet(rootName);
                dataSet.Tables.Add(dataTable.Copy());
                dataSet.WriteXml(filePath);
            }
            catch (Exception exception)
            {
                LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
            }
        }


        /// <summary>
        ///     静态扩展
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <returns></returns>
        public static string SerializeToXmlStr<T>(T obj, bool omitXmlDeclaration)
        {
            try
            {
                return Serialize(obj, omitXmlDeclaration);
            }
            catch (Exception exception)
            {
                LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                return null;
            }
        }

        /// <summary>
        ///     修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void Update(string path, string node, string attribute, string value)
        {
            /**************************************************
             * 使用示列:
             * XmlHelper.Insert(path, "/Node", "", "Value")
             * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
             ************************************************/
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                var selectSingleNode = xmlDocument.SelectSingleNode(node);
                var xmlElement = (XmlElement) selectSingleNode;
                if (attribute.Equals(""))
                {
                    if (xmlElement != null) xmlElement.InnerText = value;
                }
                else
                {
                    xmlElement.SetAttribute(attribute, value);
                }
                xmlDocument.Save(path);
            }
            catch (Exception exception)
            {
               LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
            }
        }


        /// <summary>
        ///     更新XML文件中的指定节点内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeValue">更新内容</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateNode(string filePath, string nodeName, string nodeValue)
        {
            bool flag;

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);
            var documentElement = xmlDocument.DocumentElement;
            var selectSingleNode = documentElement.SelectSingleNode("//" + nodeName);
            if (selectSingleNode != null)
            {
                selectSingleNode.InnerText = nodeValue;
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        ///     使用DataSet方式更新XML文件节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="tableName">表名称</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colName">列名</param>
        /// <param name="content">更新值</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateTableCell(string filePath, string tableName, int rowIndex, string colName,
            string content)
        {
            bool flag;
            var dataSet = new DataSet();
            dataSet.ReadXml(filePath);
            var dataTable = dataSet.Tables[tableName];

            if (dataTable.Rows[rowIndex][colName] != null)
            {
                dataTable.Rows[rowIndex][colName] = content;
                dataSet.WriteXml(filePath);
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        ///     使用DataSet方式更新XML文件节点
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="tableName">表名称</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <param name="content">更新值</param>
        /// <returns>更新是否成功</returns>
        public static bool UpdateTableCell(string filePath, string tableName, int rowIndex, int colIndex, string content)
        {
            bool flag;

            var dataSet = new DataSet();
            dataSet.ReadXml(filePath);
            var dataTable = dataSet.Tables[tableName];

            if (dataTable.Rows[rowIndex][colIndex] != null)
            {
                dataTable.Rows[rowIndex][colIndex] = content;
                dataSet.WriteXml(filePath);
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        ///     将对象写入XML文件
        /// </summary>
        /// <typeparam name="T">C#对象名</typeparam>
        /// <param name="item">对象实例</param>
        /// <param name="path">路径</param>
        /// <param name="jjdbh">标号</param>
        /// <param name="ends">结束符号（整个xml的路径类似如下：C:\xmltest\201111send.xml，其中path=C:\xmltest,jjdbh=201111,ends=send）</param>
        /// <returns></returns>
        public static string WriteXML<T>(T item, string path, string jjdbh, string ends)
        {
            if (string.IsNullOrEmpty(ends))
                ends = "send";
            var i = 0; //控制写入文件的次数，
            var serializer = new XmlSerializer(item.GetType());
            object[] obj = { path, "\\", jjdbh, ends, ".xml" };
            var xmlPath = string.Concat(obj);
            while (true)
                try
                {
                    var fileStream = File.Create(xmlPath); //用filestream方式创建文件不会出现“文件正在占用中，用File.create”则不行
                    fileStream.Close();
                    TextWriter writer = new StreamWriter(xmlPath, false, Encoding.UTF8);
                    var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add(string.Empty, string.Empty);
                    serializer.Serialize(writer, item, xmlSerializerNamespaces);
                    writer.Flush();
                    writer.Close();
                    break;
                }
                catch (Exception exception)
                {
                    if (i < 5)
                        i++;
                    else
                    {
                        LOGCore.Trace(LOGCore.ST.Day, "【XMLHelper】", exception.ToString());
                        break;
                    }
                }
            return SerializeToXmlStr(item, true);
        }

        /// <summary>
        ///     使用XmlSerializer反序列化对象
        /// </summary>
        /// <param name="xmlOfObject">需要反序列化的xml字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T Deserialize<T>(string xmlOfObject) where T : class
        {
            var xmlReader = XmlReader.Create(new StringReader(xmlOfObject), new XmlReaderSettings());
            return (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
        }

        /// <summary>
        ///     从文件读取并反序列化为对象 （解决: 多线程或多进程下读写并发问题）
        /// </summary>
        /// <typeparam name="T">返回的对象类型</typeparam>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static T XmlFileDeserialize<T>(string path)
        {
            var bytes = ShareReadFile(path);
            if (bytes.Length < 1) //当文件正在被写入数据时，可能读出为0
                for (var i = 0; i < 5; i++)
                {
                    bytes = ShareReadFile(path); //5次机会,采用这样诡异的做法避免独占文件和文件正在被写入时读出来的数据为0字节的问题。
                    if (bytes.Length > 0) break;
                    Thread.Sleep(50); //悲观情况下总共最多消耗1/4秒，读取文件
                }
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(new MemoryStream(bytes));
            if (xmlDocument.DocumentElement != null)
                return (T)new XmlSerializer(typeof(T)).Deserialize(new XmlNodeReader(xmlDocument.DocumentElement));
            return default(T);
            /*var xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.CloseInput = true;
            using (var xmlReader = XmlReader.Create(path, xmlReaderSettings))
            {
            var obj = (T) new XmlSerializer(typeof(T)).Deserialize(xmlReader);
            return obj;
            }*/
        }


        /// <summary>
        ///     使用XmlSerializer序列化对象
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <returns>序列化后的字符串</returns>
        public static string Serialize<T>(T obj, bool omitXmlDeclaration)
        {
            /* This property only applies to XmlWriter instances that output text content to a stream; otherwise, this setting is ignored.
            可能很多朋友遇见过 不能转换成Xml不能反序列化成为UTF8XML声明的情况，就是这个原因。
            */
            var xmlSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Encoding = new UTF8Encoding(false)
            };
            var stream = new MemoryStream(); //var writer = new StringWriter();
            var xmlwriter = XmlWriter.Create(stream /*writer*/, xmlSettings);
            //这里如果直接写成：Encoding = Encoding.UTF8 会在生成的xml中加入BOM(Byte-order Mark) 信息(Unicode 字节顺序标记) ， 所以new System.Text.UTF8Encoding(false)是最佳方式，省得再做替换的麻烦
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add(string.Empty, string.Empty); //在XML序列化时去除默认命名空间xmlns:xsd和xmlns:xsi
            var xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(xmlwriter, obj, xmlSerializerNamespaces);

            return Encoding.UTF8.GetString(stream.ToArray()); //writer.ToString();
        }

        /// <summary>
        ///     使用XmlSerializer序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <param name="removeDefaultNamespace">是否移除默认名称空间(如果对象定义时指定了:XmlRoot(Namespace = "http://www.xxx.com/xsd")则需要传false值进来)</param>
        /// <returns>序列化后的字符串</returns>
        public static void Serialize<T>(string path, T obj, bool omitXmlDeclaration, bool removeDefaultNamespace)
        {
            var xmlWriterSettings = new XmlWriterSettings { OmitXmlDeclaration = omitXmlDeclaration };
            using (var xmlWriter = XmlWriter.Create(path, xmlWriterSettings))
            {
                var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                if (removeDefaultNamespace)
                    xmlSerializerNamespaces.Add(string.Empty, string.Empty); //在XML序列化时去除默认命名空间xmlns:xsd和xmlns:xsi
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(xmlWriter, obj, xmlSerializerNamespaces);
            }
        }

        private static byte[] ShareReadFile(string filePath)
        {
            byte[] bytes;
            //避免"正由另一进程使用,因此该进程无法访问此文件"造成异常 共享锁 flieShare必须为ReadWrite，但是如果文件不存在的话，还是会出现异常，所以这里不能吃掉任何异常，但是需要考虑到这些问题
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                bytes = new byte[fileStream.Length];
                var numBytesToRead = (int)fileStream.Length;
                var numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    var bytesRead = fileStream.Read(bytes, numBytesRead, numBytesToRead);
                    if (bytesRead == 0)
                        break;
                    numBytesRead += bytesRead;
                    numBytesToRead -= bytesRead;
                }
            }
            return bytes;
        }

        #endregion

    }
}

