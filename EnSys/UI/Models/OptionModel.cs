﻿using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Util.Enums;

namespace UI.Models
{
    public class OptionModel : IOption
    {
        public int Id { get; set; }
        public OptionType Type { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        public object Group { get; set; }
    }
}