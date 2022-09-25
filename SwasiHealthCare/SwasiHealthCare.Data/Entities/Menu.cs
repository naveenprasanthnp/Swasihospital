using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("Menu", Schema = "dbo")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public long MenuCreateBy { get; set; }
        public DateTime? MenuCreateDate { get; set; }
    }
}
