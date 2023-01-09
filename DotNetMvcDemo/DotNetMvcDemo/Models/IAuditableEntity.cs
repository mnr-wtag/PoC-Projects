using System;

namespace DotNetMvcDemo.Models
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        int? CreatedBy { get; set; }
        int? UpdatedBy { get; set; }
    }
}
