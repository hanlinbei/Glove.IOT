﻿using Glove.IOT.Common;
using Glove.IOT.Model;
using Glove.IOT.Model.Param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glove.IOT.IBLL
{
    public partial interface IGroupInfoService : IBaseService<GroupInfo>
    {
        IQueryable<dynamic> GetGroupInfo();
        IQueryable<dynamic> GetGroupDevices(int id, BaseParam baseParam);
        IQueryable<Device> LoagDevicePageData(DeviceQueryParam deviceQueryParam,int gId);
    }
}
