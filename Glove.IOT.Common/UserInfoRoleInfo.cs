﻿using Glove.IOT.Model;
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
        public string RoleName { get; set; }
        public int UId { get; set; }
        public string UCode { get; set; }
        public string UName { get; set; }
        public short StatusFlag { get; set; }
        public string Remark { get; set; }
        public System.DateTime SubTime { get; set; }
    }
}