using System;
using BL.Interfaces;
using Util.Enums;
using System.Collections.Generic;

namespace BL.Dto
{
    public class SubjectDto : ISubject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public Unit Units { get; set; }
        public YearLevel Level { get; set; }
        public Status Status { get; set; }
    }
}
