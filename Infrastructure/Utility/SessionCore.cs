using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;

namespace Infrastructure.Utility
{
   public class SessionCore
    {
       public delegate T CreateItem<T>();
       public static T GetValue<T>(string key)
       {
           string sessionName = key;

           HttpSessionState state = System.Web.HttpContext.Current.Session;
           if (state == null) return default(T);

           object result = state[sessionName];

           return (T)result;
       }

       public static void SetValue(string key, object value)
       {
           string sessionName = key;
           HttpSessionState state = System.Web.HttpContext.Current.Session;
           if (state == null) return;
           state[sessionName] = value;
       }
       public static void Remove(string key)
       {
           HttpSessionState state = System.Web.HttpContext.Current.Session;
           if (state != null)
           {
               state.Remove(key);
           }
       }
    }
}
