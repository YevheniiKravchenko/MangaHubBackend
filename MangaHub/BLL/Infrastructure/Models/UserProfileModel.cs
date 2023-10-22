namespace BLL.Infrastructure.Models
{
    public class UserProfileModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] Avatar { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public bool ShowConfidentialInformation { get; set; }
        
        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
