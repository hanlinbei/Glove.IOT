using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.Common
{
    public class UserInfoRoleInfo
    {
        public int RId { get; set; }
        public int SId { get; set; }
        public string RoleName { get; set; }
        public string Pwd { get; set; }
        public int UId { get; set; }
        public string UCode { get; set; }
        public string UName { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool  StatusFlag { get; set; }
        public string Remark { get; set; }
        public System.DateTime SubTime { get; set; }
    }
}
