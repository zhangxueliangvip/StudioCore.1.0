using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace Infrastructure.Utility
{
    public class RedisCore
    {
        #region 单例模式
        private static readonly RedisCore instance = new RedisCore();
        private RedisCore() {
            this.DefaultTimeout = 600;
            Redis = ConnectionMultiplexer.Connect(Config.RedisConStr);
            Db = Redis.GetDatabase();
        }
        public static RedisCore GetInstance => instance;
        #endregion
        private int DefaultTimeout { get; set; }//默认超时时间（单位秒）
        private ConnectionMultiplexer Redis { get; set; }
        private IDatabase Db { get; set; }
        readonly JsonSerializerSettings jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };

        class CacheObject<T>
        {
            public int ExpireTime { get; set; }
            public bool ForceOutofDate { get; set; }
            public T Value { get; set; }
        }

        /// <summary>
        /// 连接超时设置
        /// </summary>
        public int TimeOut
        {
            get
            {
                return DefaultTimeout;
            }
            set
            {
                DefaultTimeout = value;
            }
        }

        public object Get(string key)
        {
            return Get<object>(key);
        }

        public T Get<T>(string key)
        {

            DateTime begin = DateTime.Now;
            var cacheValue = Db.StringGet(key);
            DateTime endCache = DateTime.Now;
            var value = default(T);
            if (!cacheValue.IsNull)
            {
                var cacheObject = JsonConvert.DeserializeObject<CacheObject<T>>(cacheValue, jsonConfig);
                if (!cacheObject.ForceOutofDate)
                    Db.KeyExpire(key, new TimeSpan(0, 0, cacheObject.ExpireTime));
                value = cacheObject.Value;
            }
            DateTime endJson = DateTime.Now;
            return value;

        }

        public void Set(string key, object data)
        {
            var jsonData = GetJsonData(data, TimeOut, false);
            Db.StringSet(key, jsonData);
        }

        public void Set(string key, object data, int cacheTime)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData(data, TimeOut, true);
            Db.StringSet(key, jsonData, timeSpan);
        }

        public void Set(string key, object data, DateTime cacheTime)
        {
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, true);
            Db.StringSet(key, jsonData, timeSpan);
        }

        public void Set<T>(string key, T data)
        {
            var jsonData = GetJsonData<T>(data, TimeOut, false);
            Db.StringSet(key, jsonData);
        }

        public void Set<T>(string key, T data, int cacheTime)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData<T>(data, TimeOut, true);
            Db.StringSet(key, jsonData, timeSpan);
        }

        public void Set<T>(string key, T data, DateTime cacheTime)
        {
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, true);
            Db.StringSet(key, jsonData, timeSpan);
        }


        string GetJsonData(object data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<object>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            return JsonConvert.SerializeObject(cacheObject, jsonConfig);//序列化对象
        }

        string GetJsonData<T>(T data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<T>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            return JsonConvert.SerializeObject(cacheObject, jsonConfig);//序列化对象
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            Db.KeyDelete(key, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public bool Exists(string key)
        {
            return Db.KeyExists(key);
        }
    }

}
