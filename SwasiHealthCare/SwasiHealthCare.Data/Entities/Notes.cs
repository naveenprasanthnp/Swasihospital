using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwasiHealthCare.Data.Entities
{
    [Table("Notes", Schema = "dbo")]
    public class Notes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotesId { get; set; }
        public string Description { get; set; }
        public long? NotesCreatedBy { get; set; }
        public DateTime? NotesCreatedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
