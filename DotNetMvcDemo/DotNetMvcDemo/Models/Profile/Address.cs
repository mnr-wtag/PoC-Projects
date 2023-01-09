using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetMvcDemo.Models
{
    public class Address : DateAndAuthor
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //public int UserProfileId { get; set; }
        //[ForeignKey("UserProfileId")]
        //public virtual UserProfile UserProfile { get; set; }
    }

    public class CurrentAddress : Address
    {

        public int UserProfileId { get; set; }
        [ForeignKey("UserProfileId")]
        public virtual UserProfile UserProfile { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual AuthUser UpdaterUser { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual AuthUser CreatorUser { get; set; }
    }

    public class PermenantAddress : Address
    {
        public int UserProfileId { get; set; }
        [ForeignKey("UserProfileId")]
        public virtual UserProfile UserProfile { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual AuthUser UpdaterUser { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual AuthUser CreatorUser { get; set; }

    }
}