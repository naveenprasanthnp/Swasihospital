using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("Designation", Schema = "dbo")]
    public class Designation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DesignationId { get;set;}
        public string DesignationName { get; set; }
        public bool DesignationStatus { get; set; }
        public long? DesignationCreatedBy { get; set; }
        public DateTime? DesignationCreatedDate { get; set; }
        public long? DesignationModifiedBy { get; set; }
        public DateTime? DesignationModifiedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
