﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    public interface IUserInfoDal
    {
        void show();
        string Name { get; set; }
    }
}
