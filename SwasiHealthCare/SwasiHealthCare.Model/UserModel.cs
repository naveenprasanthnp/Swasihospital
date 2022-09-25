using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class UserModel
    {
        public long? UserId { get; set; }
        public long? HospitalId { get; set; }
        public long?  DesignationId { get; set; }
        public string RegisterNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserGender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateofJoin { get; set; }
        public string UserMobile { get; set; }
        public long? RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserAddress { get; set; }
        public string UserQualification { get; set; }
        public string UserSpecialization { get; set; }
        public bool UserStatus { get; set; }
        public long? UserCreatedBy { get; set; }
        public DateTime? UserCreatedDate { get; set; }
        public long? UserModifiedBy { get; set; }
        public DateTime? UserModifiedDate { get; set; }
        public string FullName { get; set; }
        public string Mode { get; set; }
        public string DesignationName { get; set; }
        public string NewPassword { get; set; }
        public bool IsPasswordChange { get; set; }
        public long? SystemId { get; set; }
        public string ProfilePath { get; set; }
    }
}
