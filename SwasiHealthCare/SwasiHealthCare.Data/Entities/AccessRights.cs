using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwasiHealthCare.Data.Entities
{
    [Table("AccessRights", Schema = "dbo")]
    public class AccessRights
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AccessRightsId { get; set; }
        public long? DesignationId { get; set; }
        public long? HospitalId { get; set; }
        public long MenuId { get; set; }
        public long UserId { get; set; }
        public bool IsView { get; set; }
        public bool IsCreate { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete{ get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
