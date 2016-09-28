using BL;
using BL.Dto;
using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UI.Helpers;
using Util.Enums;

namespace UI.Models
{
    public class RoomModel : IRoom
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Capacity { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
        public int StatusId { get { return Convert.ToInt32(Status); } }
    }
}