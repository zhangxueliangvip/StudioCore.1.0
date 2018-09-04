using System.Collections.Generic;

namespace Domain.BaseModels
{
    public class BaseResult
    {
        #region 返回结果
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public byte[] ByData { get; set; }
        #endregion
    }

    public class TBaseResult<T>: BaseResult
    {
        #region 返回结果
        public T TData { get; set; }
        public List<T> TList { get; set; }
        public IEnumerable<T> TIenumerable { get; set; }
        #endregion
    }
}
