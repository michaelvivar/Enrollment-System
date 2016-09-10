using System;
using BL.Interfaces;
using Util.Enums;

namespace BL.Dto
{
    public class SubjectDto : ISubject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public YearLevel Level { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
        public int Units { get; set; }
        public string Course { get; set; }
    }
}
