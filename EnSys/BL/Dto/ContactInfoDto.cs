namespace BL.Dto
{
    public class ContactInfoDto : IContactInfo
    {
        public int ContactInfoId { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
    }

    public interface IContactInfo
    {
        int ContactInfoId { get; set; }
        string Email { get; set; }
        string Telephone { get; set; }
        string Mobile { get; set; }
    }
}
