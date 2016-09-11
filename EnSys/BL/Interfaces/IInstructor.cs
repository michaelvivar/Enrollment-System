﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Interfaces
{
    public interface IInstructor : IPersonalInfo
    {
        int Id { get; set; }
        Status Status { get; set; }
    }
}
