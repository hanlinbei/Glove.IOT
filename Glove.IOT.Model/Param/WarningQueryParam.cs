using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Model.Param
{
    public class WarningQueryParam:BaseParam
    {
        public string SchStartTime { get; set; }
        public string SchMessage { get; set; }
        public int SchDeviceName { get; set; }
    }
}
