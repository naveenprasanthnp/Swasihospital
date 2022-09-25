using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SwasiHealthCare.Data.Entities
{
    [Table("Users", Schema = "dbo")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        public long SystemId { get; set; }
        public long HospitalId { get; set; }
        public long? DesignationId { get; set; }
        public string RegisterNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserAddress { get; set; }
        public string UserQualification { get; set; }
        public string UserSpecialization { get; set; }
        public string AvailableDays { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserGender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateofJoin { get; set; }
        public string UserMobile { get; set; }
        public long? UserRoleId { get; set; }
        public bool UserStatus { get; set; }
        public long? UserCreatedBy { get; set; }
        public DateTime? UserCreatedDate { get; set; }
        public long? UserModifiedBy { get; set; }
        public DateTime? UserModifiedDate { get; set; }
        public string ProfilePath { get; set; }
    }
}
