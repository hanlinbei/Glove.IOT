using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Model.Param
{
    public class OperationLogQueryParam:BaseParam
    {
        public string FirstTime { get; set; }
        public string LastTime { get; set; }
        public string UName { get; set; }
        public string ActionType { get; set; }
        public string ActionName { get; set; }

      
    }
}
