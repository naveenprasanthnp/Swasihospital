using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("System", Schema = "dbo")]
    public class System
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SystemId { get; set; }
        public string SystemName { get; set; }
        public string SystemIp { get; set; }
        public string SystemModel { get; set; }
        public bool SystemStatus { get; set; }
        public long? SystemCreatedBy { get; set; }
        public DateTime? SystemCreatedDate { get; set; }
        public long? SystemModifiedBy { get; set; }
        public DateTime? SystemModifiedDate { get; set; }
        public long? HospitalId { get; set; }
    }
}
