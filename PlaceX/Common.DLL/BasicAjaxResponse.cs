using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DLL
{
    public class BasicAjaxResponse
    {
        public bool IsSuccess { get; set; }

        public string IsSuccessMessage { get; set; }

        public object ResponseObject { get; set; }
    }
}
