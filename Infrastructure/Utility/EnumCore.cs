using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infrastructure.Utility
{
    public class EnumCore
    {

        public enum CodeType
        {
            失败=0,
            成功=1
        }

        public enum PwdType
        {
            未知=0,
            后台登录=1

        }

        public enum LogType
        {
            未知=0,
            正常=1,
            一般=2,
            严重=3
        }

        public enum PagesType
        {
            未知=0,
            后台=1,
            前台=2
        }

        #region Business
        public static List<string> GetPageTypeEnum(int keyDefault = 0, string valueDefault = "")
        {
            return ConvertList<PagesType>(keyDefault, valueDefault);
        }
        #endregion

        #region Helper
        /// <summary>  
        /// 枚举转字典  
        /// </summary>  
        /// <typeparam name="T">枚举类名称</typeparam>  
        /// <param name="keyDefault">默认key值</param>  
        /// <param name="valueDefault">默认value值</param>  
        /// <returns>返回生成的字典集合</returns>  
        public static Dictionary<string, object> ConvertDic<T>(string keyDefault = "", string valueDefault = "")
        {
            var dicEnum = new Dictionary<string, object>();
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }
            if (!string.IsNullOrEmpty(keyDefault)) //判断是否添加默认选项  
            {
                dicEnum.Add(keyDefault, valueDefault);
            }
            var fieldstrs = Enum.GetNames(enumType); //获取枚举字段数组  
            foreach (var item in fieldstrs)
            {
                var description = string.Empty;
                var field = enumType.GetField(item);
                var arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true); //获取属性字段数组  
                if (arr != null && arr.Length > 0)
                {
                    description = ((DescriptionAttribute)arr[0]).Description;   //属性描述  
                }
                else
                {
                    description = item;  //描述不存在取字段名称  
                }
                dicEnum.Add(description, (int)Enum.Parse(enumType, item));  //不用枚举的value值作为字典key值的原因从枚举例子能看出来，其实这边应该判断他的值不存在，默认取字段名称  
            }
            return dicEnum;
        }

        /// <summary>
        /// 枚举转List
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="keyDefault">默认值key</param>
        /// <param name="valueDefault">默认值value</param>
        /// <returns>返回字符串类型List</returns>
        public static List<string> ConvertList<T>(int keyDefault = 0, string valueDefault = "")
        {
            var list = new List<string>();
            var enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return list;
            }
            if (keyDefault > 0 && !string.IsNullOrWhiteSpace(valueDefault))
            {
                list.Insert(keyDefault, valueDefault);
            }
            var enums = Enum.GetNames(enumType);
            foreach (var item in enums)
            {
                var description = string.Empty;
                var field = enumType.GetField(item);
                var arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true); //获取属性字段数组  
                if (arr != null && arr.Length > 0)
                {
                    description = ((DescriptionAttribute)arr[0]).Description;   //属性描述  
                }
                else
                {
                    description = item;
                }
                if (!list.Contains(description))
                {
                    list.Insert((int)Enum.Parse(enumType, item), description);
                }

            }
            return list;
        }
        #endregion
    }
}
