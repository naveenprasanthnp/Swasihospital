using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwasiHealthCare.Data.Entities
{
    [Table("Roles", Schema = "dbo")]
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleStatus { get; set; }
        public long? RoleCreatedBy { get; set; }
        public DateTime? RoleCreatedDate { get; set; }
        public long? RoleModiifedBy { get; set; }
        public DateTime? RoleModifiedDate { get; set; }
    }
}
