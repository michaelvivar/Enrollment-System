using Util.Enums;

namespace BL.Dto
{
    public class RoomDto : IRoom
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Capacity { get; set; }
        public string Remarks { get; set; }
        public Status Status { get; set; }
    }

    public interface IRoom
    {
        int Id { get; set; }
        string Number { get; set; }
        string Remarks { get; set; }
        int Capacity { get; set; }
        Status Status { get; set; }
    }
}
