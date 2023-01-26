using RestApiNetDemo.BEL.AuditableEntities;
using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.Entities.UserProfile
{
    public class CreateUserProfileDTO : DateAndAuthorDTO
    {
        public CreateUserProfileDTO() : base()
        {
        }

        public byte[] UserPhoto { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [EmailAddress]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string EmailAddress { get; set; }

        [Phone]
        public string MobileNumber { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}