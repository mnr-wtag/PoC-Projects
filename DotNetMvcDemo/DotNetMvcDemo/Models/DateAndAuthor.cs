using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.Models
{
    public class DateAndAuthor : IAuditableEntity
    {
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; }


        [Display(Name = "Created By")]
        public int? CreatedBy { get; set; }


        [Display(Name = "Updated By")]
        public int? UpdatedBy { get; set; }
    }
}