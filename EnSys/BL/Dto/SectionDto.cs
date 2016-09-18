using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Dto
{
    public class SectionDto : ISection
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public YearLevel Level { get; set; }
        public Status Status { get; set; }
        public int Students { get; set; }
    }
}
