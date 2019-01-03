using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Common
{
    public class DeviceParameter
    {
        public string DeviceId { get; set; }
        public string StatusFlag { get; set; }
        public Nullable<int> NowOutput { get; set; }
        public Nullable<int> TargetOutput { get; set; }
        public Nullable<short> SingleProgress { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> StopTime { get; set; }
        public System.DateTime SubTime { get; set; }
        public String RunTime { get; set; }

    }
}
