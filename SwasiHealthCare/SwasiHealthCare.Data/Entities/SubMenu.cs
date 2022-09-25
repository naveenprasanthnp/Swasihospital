using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("SubMenu", Schema = "dbo")]
    public class SubMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DropdownMenuId { get; set; }
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public long MenuCreatedBy { get; set; }
        public DateTime MenuCreatedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
