using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterView.RemoteServer.Helper
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
    }
    public class DataSourceResultWrapper<T>
    {
        public DataSourceResult<T> Data { get; set; }
        public bool Status { get; set; }
        public string ResultCode { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
    public class DataSourceResult<T>
    {
        public long Total { get; set; }
        public int lastPage { get; set; }
        public long? Filtered { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
