using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacenter.Models.Entities
{
    public class ResponseJson
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class ResponseJson<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
