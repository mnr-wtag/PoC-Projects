using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public byte[] UserPhoto { get; set; }

        [Required]
        public Gender Gender { get; set; }

        //public int CurrentAddressId { get; set; }
        //[ForeignKey("CurrentAddressId")]
        //public virtual CurrentAddress CurrentAddress { get; set; }
        //public int PermenantAddressId { get; set; }
        //[ForeignKey("PermenantAddressId")]
        //public virtual PermenantAddress PermenantAddress { get; set; }

        public virtual ICollection<MobileNumber> MobileNumbers { get; set; }

        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }

        public virtual AuthUser AuthUser { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}