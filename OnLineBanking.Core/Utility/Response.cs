using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Utility
{
    public  class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
