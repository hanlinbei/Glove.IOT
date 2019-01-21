using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Common
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string StatusFlag { get; set; }
        public bool LAY_CHECKED { get; set; }
        public DateTime Date { get; set; }
        public int SumOutput { get; set; }

    }
}
