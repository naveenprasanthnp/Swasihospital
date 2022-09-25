using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("PatientDocument", Schema = "dbo")]
    public class PatientDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? DocumentId { get; set; }
        public long? PatientId { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string PatientIdNumber { get; set; }
        public long? DocumentCreatedBy { get; set; }
        public DateTime? DocumentCreatedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
