using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public bool IsPrimary { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }

    public class MobileNumber : Contact
    {
        public string Number { get; set; }
    }

    public class EmailAddress : Contact
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class MobileNumberList
    {
        public ICollection<MobileNumber> MobileNumbers { get; set; }

    }

    public class EmailAddressList
    {
        public ICollection<EmailAddress> EmailAddresses { get; set; }

    }
}