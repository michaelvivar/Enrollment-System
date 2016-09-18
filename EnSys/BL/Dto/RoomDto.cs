using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Enums;

namespace BL.Dto
{
    public class RoomDto : IRoom
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int? Capacity { get; set; }
        public string Remarks { get; set; }
        public Status? Status { get; set; }
    }
}
