﻿using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dto
{
    public class DropDownMenuItemDto : IDropDownMenuITem
    {
        public object Group { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
