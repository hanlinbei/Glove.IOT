using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Model.Param
{
    public class WarningQueryParam:BaseParam
    {
        public string FirstTime { get; set; }
        public string SchMessage { get; set; }
        public string SchDeviceId { get; set; }
    }
}
