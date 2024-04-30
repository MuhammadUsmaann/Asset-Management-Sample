using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Server.Infrastructure.Helpers
{
    public class ResponseTypeView<T>
    {
        public T? Result { get; set; }
        public string? MessageHeader { get; set; }
        public string? MessageDescription { get; set; }
        public int? StatusCode { get; set; }
    }
}
