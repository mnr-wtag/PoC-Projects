using System;
using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.AuditableEntities
{
    public class DateAndAuthorDTO
    {
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Created By")]
        public int? CreatedBy { get; set; }
        [Display(Name = "Updated By")]
        public int? UpdatedBy { get; set; }
    }
}
