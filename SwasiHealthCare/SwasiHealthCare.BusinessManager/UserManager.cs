using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.Helper;
using SwasiHealthCare.IBusinessManager;
using SwasiHealthCare.IRepository;
using SwasiHealthCare.Model;
using SwasiHealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.BusinessManager
{
    public class UserManager : IUserManager
    {
        private IRepository<Users> UserRepository;

        public UserManager()
        {
            this.UserRepository = new Repository<Users>();
        }

        public async Task<ResponseModel> UpdateProfilePicture(UserModel userModel)
        {
            try
            {
                var user = await UserRepository.GetById(userModel.UserId ?? 0);
                if(user != null)
                {
                    user.ProfilePath = userModel.ProfilePath;
                    await UserRepository.Update(user);
                }
                return new ResponseModel
                {
                    Status = false,
                    ErrorMessage = Constants.SuccessMessage,
                    ErrorCode = "404"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> SaveUser(UserModel userModel, long rolid)
        {
            try
            {
                var username = (await UserRepository.GetAll())?.Where(usr => usr.UserEmail.Equals(userModel.UserEmail, StringComparison.OrdinalIgnoreCase)
                && usr.UserId != userModel.UserId).Any() ?? false;
                if (username)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.AlreadyExist,
                        ErrorCode = "404"
                    };
                }
                // Email
                if (string.IsNullOrEmpty(userModel.UserEmail))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                // First Name
                if (string.IsNullOrEmpty(userModel.FirstName))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.FirstNameMandatory,
                        ErrorCode = "404"
                    };
                }

                // Edit Mode - User Id
                if (userModel.Mode + "" == "E" && userModel.UserId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                // Role
                if (userModel.RoleId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.RoleNotfound,
                        ErrorCode = "404"
                    };
                }

                var userdata = userModel.Mode + "" != "E" ? (await UserRepository.GetAll())?.
                        Where(usr => usr.UserEmail.Equals(userModel.UserEmail, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                        (await UserRepository.GetAll())?.Where(usr => usr.UserEmail.Equals(userModel.UserEmail, StringComparison.OrdinalIgnoreCase)
                            && usr.UserId != userModel.UserId).FirstOrDefault();

                if (userdata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (userModel.Mode + "" == "E")
                {
                    var user = await UserRepository.GetById(userModel.UserId ?? 0);

                    if (user == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }

                    user.RegisterNumber = userModel.RegisterNumber + "" == "" ? user.RegisterNumber : "";
                    user.FirstName = userModel.FirstName;
                    user.LastName = userModel.LastName;
                    user.MiddleName = userModel.MiddleName;
                    user.UserAddress = userModel.UserAddress;
                    user.UserPassword = userModel.UserPassword + "" == "" ? user.UserPassword : Encryption.Encrypt(userModel.UserPassword, Constants.EncryptKey);
                    user.UserGender = userModel.UserGender;
                    user.DateOfBirth = userModel.DateOfBirth;
                    user.DateofJoin = userModel.DateofJoin;
                    user.UserMobile = userModel.UserMobile;
                    if (1 == userModel.RoleId)
                    {
                        user.UserRoleId = 1;
                        var medicine = (await new Repository<Medicine>().GetAll()).Where(x => x.MedicineName == "Others").ToList().Any();
                        if (!medicine)
                        {
                            await new Repository<Medicine>().Insert(new Medicine
                            {
                                MedicineName = "Others",
                                MedicineDescription = "Others",
                                MedicineCreatedBy = 1,
                                MedicineCreatedDate = DateTime.Now
                            });
                        }
                    }
                    else
                    {
                        user.UserRoleId = rolid == 1 ? 2 : userModel.RoleId;
                    }
                    user.UserStatus = userModel.UserStatus;
                    user.UserCreatedBy = user.UserCreatedBy;
                    user.UserCreatedDate = user.UserCreatedDate;
                    user.UserModifiedBy = userModel.UserModifiedBy;
                    user.UserModifiedDate = DateTime.Now;
                    user.UserQualification = userModel.UserQualification;
                    user.UserSpecialization = userModel.UserSpecialization;
                    user.SystemId = userModel.SystemId ?? 0;
                    user.UserStatus = userModel.UserStatus;
                    if (userModel.RoleId == 1)
                    {
                        
                    }
                    else
                    {
                        user.DesignationId = userModel.RoleId == 3 ? 2 : userModel.DesignationId;
                    }
                    //user.UserRoleId = rolid == 1 ? 2 : userModel.RoleId;
                    //if (userModel.RoleId == 4)
                    //{
                    //    user.DesignationId = 3;
                    //}
                    //else
                    //{
                    //    user.DesignationId = userModel.RoleId == 3 ? 2 : userModel.DesignationId;
                    //}
                    if (userModel.IsPasswordChange)
                    {
                        user.UserPassword = Encryption.Encrypt(userModel.UserPassword, Constants.EncryptKey);
                    }
                    await UserRepository.Update(user);
                }
                else
                {
                    var user = new Users
                    {
                        DesignationId = userModel.RoleId == 3 ? 2 : userModel.DesignationId,
                        HospitalId = userModel.HospitalId ?? 0,
                        RegisterNumber = userModel.RegisterNumber,
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        MiddleName = userModel.MiddleName,
                        UserAddress = userModel.UserAddress,
                        UserPassword = Encryption.Encrypt(userModel.UserPassword, Constants.EncryptKey),
                        UserGender = userModel.UserGender,
                        UserEmail = userModel.UserEmail,
                        DateOfBirth = userModel.DateOfBirth,
                        DateofJoin = userModel.DateofJoin,
                        UserMobile = userModel.UserMobile,
                        UserRoleId = rolid == 1 ? 2 : userModel.RoleId,
                        UserStatus = userModel.UserStatus,
                        UserCreatedBy = userModel.UserCreatedBy,
                        UserCreatedDate = DateTime.Now,
                        UserQualification = userModel.UserQualification,
                        UserSpecialization = userModel.UserSpecialization,
                        SystemId = userModel.SystemId ?? 0
                    };
                    await UserRepository.Insert(user);
                    var hospital = (await new Repository<Hospital>().GetById(userModel.HospitalId ?? 0));

                    var TemplateDet = (await new Repository<Templates>().GetAll())?
                    .Where(tmp => tmp.TemplateName.Equals("Register", StringComparison.OrdinalIgnoreCase)).ToList().FirstOrDefault();

                    var MailBody = TemplateDet.MailBody;
                    MailBody = MailBody.Replace("[{ContactPersonName}]", userModel.FirstName + userModel.MiddleName + userModel.LastName);
                    MailBody = MailBody.Replace("[{Name}]", userModel.UserEmail);
                    MailBody = MailBody.Replace("[{HospitalName}]", hospital.HospitalName);
                    MailBody = MailBody.Replace("[{Password}]", userModel.UserPassword);
                    MailBody = MailBody.Replace("[{UserMail}]", userModel.UserEmail);
                    EmailHelper.SendEmail(new EmailConfigModel { TemplateBody = MailBody, ToEmailList = userModel.UserEmail, Subject = TemplateDet.MailSubject });

                    var designation = (await new Repository<Designation>().GetAll()).Where(x => x.DesignationName == "Receptionist"
                    && x.HospitalId == userModel.HospitalId).ToList().Any();
                    if (!designation)
                    {
                        await new Repository<Designation>().Insert(new Designation
                        {
                            DesignationName = "Receptionist",
                            DesignationCreatedBy = 1,
                            DesignationCreatedDate = DateTime.Now,
                            DesignationStatus = true,
                            HospitalId = userModel.HospitalId
                        });
                    }

                    var doctordesig = (await new Repository<Designation>().GetAll()).Where(x => x.DesignationName == "Doctor"
                    && x.HospitalId == userModel.HospitalId).ToList().Any();
                    if (!doctordesig)
                    {
                        await new Repository<Designation>().Insert(new Designation
                        {
                            DesignationName = "Doctor",
                            DesignationCreatedBy = 1,
                            DesignationCreatedDate = DateTime.Now,
                            DesignationStatus = true,
                            HospitalId = userModel.HospitalId
                        });
                    }

                    var Therapist = (await new Repository<Designation>().GetAll()).Where(x => x.DesignationName == "Therapist"
                    && x.HospitalId == userModel.HospitalId).ToList().Any();
                    if (!Therapist)
                    {
                        await new Repository<Designation>().Insert(new Designation
                        {
                            DesignationName = "Therapist",
                            DesignationCreatedBy = 1,
                            DesignationCreatedDate = DateTime.Now,
                            DesignationStatus = true,
                            HospitalId = userModel.HospitalId
                        });
                    }
                    if (userModel.RoleId != 1 && userModel.RoleId != 2)
                    {
                        var menulist = await new Repository<Menu>().GetAll();
                        foreach (var item in menulist)
                        {
                            var list = new AccessRights
                            {
                                MenuId = item.MenuId,
                                IsCreate = (item.MenuId == 1 || item.MenuId == 2 || item.MenuId == 3 ||
                                        item.MenuId == 5) ? true : false,
                                IsView = (item.MenuId == 1 || item.MenuId == 2 || item.MenuId == 3 ||
                                        item.MenuId == 5) ? true : false,
                                IsEdit = (item.MenuId == 1 || item.MenuId == 2) ? true : false,
                                IsDelete = false,
                                CreatedDate = DateTime.Now,
                                UserId = user.UserId,
                                HospitalId = user.HospitalId,
                                CreatedBy = user.UserCreatedBy ?? 0
                            };
                            await new Repository<AccessRights>().Insert(list);
                        }
                    }
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = userModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> CreateSuperAdmin(UserModel userModel)
        {
            try
            {
                var exist = (await UserRepository.GetAll())?.Where(x => x.UserEmail == userModel.UserEmail && x.UserRoleId == 1).Any();
                if (exist == false)
                {
                    await UserRepository.Insert(new Users
                    {
                        RegisterNumber = userModel.RegisterNumber,
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        MiddleName = userModel.MiddleName,
                        UserAddress = userModel.UserAddress,
                        UserPassword = Encryption.Encrypt(userModel.UserPassword, Constants.EncryptKey),
                        UserGender = userModel.UserGender,
                        UserEmail = userModel.UserEmail,
                        DateOfBirth = DateTime.Now,
                        DateofJoin = DateTime.Now,
                        UserMobile = userModel.UserMobile,
                        UserRoleId = 1,
                        UserStatus = userModel.UserStatus,
                        UserCreatedBy = null,
                        UserCreatedDate = DateTime.Now,
                        UserQualification = userModel.UserQualification,
                        UserSpecialization = userModel.UserSpecialization
                    });
                    await CreateMasterRoles();
                    await CreateMasterMenu();
                }
                return new ResponseModel
                {
                    Data = Constants.SuccessMessage,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> CreateMasterRoles()
        {
            try
            {
                var superadmin = (await new Repository<Roles>().GetAll())?.Where(x => x.RoleName.ToLower() == "SuperAdmin".ToLower())?.Any();
                if (!superadmin == true)
                {
                    var roleslist = new Roles
                    {
                        RoleName = "SuperAdmin",
                        RoleStatus = true,
                        RoleCreatedDate = DateTime.Now
                    };
                    await new Repository<Roles>().Insert(roleslist);
                }

                var admin = (await new Repository<Roles>().GetAll())?.Where(x => x.RoleName.ToLower() == "Admin".ToLower())?.Any();
                if (!admin == true)
                {
                    var roleslist = new Roles
                    {
                        RoleName = "Admin",
                        RoleStatus = true,
                        RoleCreatedDate = DateTime.Now
                    };
                    await new Repository<Roles>().Insert(roleslist);
                }
                var doctor = (await new Repository<Roles>().GetAll())?.Where(x => x.RoleName.ToLower() == "Doctor".ToLower())?.Any();
                if (!doctor == true)
                {
                    var roleslist = new Roles
                    {
                        RoleName = "Doctor",
                        RoleStatus = true,
                        RoleCreatedDate = DateTime.Now
                    };
                    await new Repository<Roles>().Insert(roleslist);
                }
                var staff = (await new Repository<Roles>().GetAll())?.Where(x => x.RoleName.ToLower() == "Staff".ToLower())?.Any();
                if (!staff == true)
                {
                    var roleslist = new Roles
                    {
                        RoleName = "Staff",
                        RoleStatus = true,
                        RoleCreatedDate = DateTime.Now
                    };
                    await new Repository<Roles>().Insert(roleslist);
                }
                var manager = (await new Repository<Roles>().GetAll())?.Where(x => x.RoleName.ToLower() == "Manager".ToLower())?.Any();
                if (!manager == true)
                {
                    var roleslist = new Roles
                    {
                        RoleName = "Manager",
                        RoleStatus = true,
                        RoleCreatedDate = DateTime.Now
                    };
                    await new Repository<Roles>().Insert(roleslist);
                }

                var register = (await new Repository<Templates>().GetAll())?.Where(x => x.TemplateName.ToLower() == "Register".ToLower())?.Any();
                if (!register == true)
                {
                    var temp = new Templates
                    {
                        TemplateName = "Register",
                        MailSubject = "Welcome to Swasi nutro cure",
                        MailBody = "<div style='border: 1px solid;'><div style = 'margin-left:10px;color:black;line-height:35px'><b> Dear[{ ContactPersonName}]</ b >,<br>Your organization[{ Name}] is successfully registered in SWASI NUTRO CURE, A office management software for professionals<br>Please use below mentioned credentials to login SNC<br><div style = 'background:linear-gradient(45deg, #59b5ea, #3ee86300)' ><b> Hospital Name<b>: [{ HospitalName}]<br><b> User Id<b>: [{UserMail}]<br><b>Password<b>:[{Password}]</div><div style = background:linear-gradient(45deg, #59b5ea, #3ee86300) > URL:<b></b> http://www.swasicure.somee.com/ Click on the url or copy and paste in your browser address bar.<br>We recommended Google Chrome latest version for better experience</div><div style = background:linear-gradient(45deg, #59b5ea, #3ee86300) > For<b> </b> any queries,feel free to reach us at anbujohnson @swasicure.co.in </div><span>Thanks & Regards,<br>A Anbu Johnson<br>HR & Consultant</span></div></div>"
                    };
                    await new Repository<Templates>().Insert(temp);
                }

                //var treatment = (await new Repository<Treatment>().GetAll()).Where(x => x.TreatmentName == "Others").ToList().Any();
                //if (!treatment)
                //{
                //    await new Repository<Treatment>().Insert(new Treatment
                //    {
                //        TreatmentName = "Others",
                //        TreatmentDescription = "Others",
                //        TreatmentDuration = null,
                //        TreatmentMedicineNeeded = "Others",
                //        TreatmentCharges = 0,
                //        TreatmentStatus = true,
                //        TreatmentCreatedBy = 1,
                //        TreatmentCreatedDate = DateTime.Now
                //    });
                //}               
                return new ResponseModel
                {
                    Data = Constants.SuccessMessage,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> CreateMasterMenu()
        {
            try
            {
                var patiets = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Patients".ToLower())?.Any();
                if (!patiets == true)
                {
                    var addpatiets = new Menu
                    {
                        MenuName = "Patients",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addpatiets);
                }
                var opd = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "OPD".ToLower())?.Any();
                if (!opd == true)
                {
                    var addopd = new Menu
                    {
                        MenuName = "OPD",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addopd);
                }
                var expense = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Expense".ToLower())?.Any();
                if (!expense == true)
                {
                    var addexpense = new Menu
                    {
                        MenuName = "Expense",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addexpense);
                }
                var reports = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Reports".ToLower())?.Any();
                if (!reports == true)
                {
                    var addreports = new Menu
                    {
                        MenuName = "Reports",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addreports);
                }
                var purchasemedicine = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Purchase Medicine".ToLower())?.Any();
                if (!purchasemedicine == true)
                {
                    var addsettings = new Menu
                    {
                        MenuName = "Purchase Medicine",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addsettings);
                }
                var staff = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Staff".ToLower())?.Any();
                if (!staff == true)
                {
                    var addsettings = new Menu
                    {
                        MenuName = "Staff",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addsettings);
                }
                var medicine = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Medicine".ToLower())?.Any();
                if (!medicine == true)
                {
                    var addsettings = new Menu
                    {
                        MenuName = "Medicine",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addsettings);
                }
                var treatments = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Treatments".ToLower())?.Any();
                if (!treatments == true)
                {
                    var addsettings = new Menu
                    {
                        MenuName = "Treatments",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addsettings);
                }
                var hospital = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Hospital".ToLower())?.Any();
                if (!hospital == true)
                {
                    var addsettings = new Menu
                    {
                        MenuName = "Hospital",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addsettings);
                }
                var designation = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Designation".ToLower())?.Any();
                if (!designation == true)
                {
                    var addsettings = new Menu
                    {
                        MenuName = "Designation",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addsettings);
                }
                var system = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "System".ToLower())?.Any();
                if (!system == true)
                {
                    var addsettings = new Menu
                    {
                        MenuName = "System",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addsettings);
                }
                var plans = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Plans".ToLower())?.Any();
                if (!plans == true)
                {
                    var addsettings = new Menu
                    {
                        MenuName = "Plans",
                        MenuCreateBy = 1,
                        MenuCreateDate = DateTime.Now
                    };
                    await new Repository<Menu>().Insert(addsettings);
                }

                //var settings = (await new Repository<Menu>().GetAll())?.Where(x => x.MenuName.ToLower() == "Settings".ToLower())?.Any();
                //if (!settings == true)
                //{
                //    var addsettings = new Menu
                //    {
                //        MenuName = "Settings",
                //        MenuCreateBy = 1,
                //        MenuCreateDate = DateTime.Now
                //    };
                //    await new Repository<Menu>().Insert(addsettings);
                //}
                return new ResponseModel
                {
                    Data = Constants.SuccessMessage,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Roles>> GetRoles()
        {
            try
            {
                RoleManager roleManager = new RoleManager();
                return await roleManager.GetRolesList("name", "A", 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Hospital>> GetHospitals()
        {
            try
            {
                return (await new Repository<Hospital>().GetAll())?.OrderByDescending(rlist => rlist.HospitalCreatedDate).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserModel> GetEditUser(long userid)
        {
            try
            {
                var user = await UserRepository.GetById(userid);
                var desig = await new Repository<Designation>().GetById(user?.DesignationId ?? 0);
                var result = new UserModel();
                result.FullName = user.FirstName + " " + user.LastName;
                result.UserId = user.UserId;
                result.FirstName = user.FirstName;
                result.MiddleName = user.MiddleName;
                result.LastName = user.LastName;
                result.UserEmail = user.UserEmail;
                result.UserAddress = user.UserAddress;
                result.HospitalId = user.HospitalId;
                //result.UserPassword = user.UserPassword;
                result.UserGender = user.UserGender;
                result.DateOfBirth = user.DateOfBirth;
                result.DateofJoin = user.DateofJoin;
                result.UserMobile = user.UserMobile;
                result.RoleId = user.UserRoleId == null ? 0 : user.UserRoleId;
                result.UserStatus = user.UserStatus;
                result.UserCreatedBy = user.UserCreatedBy;
                result.UserCreatedDate = user.UserCreatedDate;
                result.UserModifiedBy = user.UserModifiedBy;
                result.UserModifiedDate = user.UserModifiedDate;
                result.RegisterNumber = user.RegisterNumber;
                result.UserSpecialization = user.UserSpecialization;
                result.UserQualification = user.UserQualification;
                result.UserPassword = Encryption.Decrypt(user.UserPassword);
                result.DesignationName = desig?.DesignationName;
                result.SystemId = user.SystemId;
                result.DesignationId = user.DesignationId;
                result.ProfilePath = user.ProfilePath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteUser(long userid)
        {
            try
            {
                if (userid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await UserRepository.GetById(userid);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    await UserRepository.Delete(userid);

                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.DelSuccessMessage,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> UpdateUserStatus(long userid, bool userstatus, long modiifedby)
        {
            try
            {
                if (userid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }

                var userdata = await UserRepository.GetById(userid);

                if (userdata == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    userdata.UserStatus = userstatus;
                    userdata.UserModifiedBy = modiifedby;
                    userdata.UserModifiedDate = DateTime.Now;
                    await UserRepository.Update(userdata);
                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.UserStatusChange,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllUsers()
        {
            try
            {
                var userlist = (from user in (await UserRepository.GetAll()).Where(x => x.UserRoleId != 1).ToList()
                                join roles in (await new Repository<Roles>().GetAll()) on user.UserRoleId equals roles.RoleId into rl
                                from role in rl.DefaultIfEmpty()
                                select new UserModel
                                {
                                    UserId = user?.UserId,
                                    RegisterNumber = user?.RegisterNumber,
                                    FirstName = user?.FirstName,
                                    MiddleName = user?.MiddleName,
                                    LastName = user?.LastName,
                                    UserAddress = user?.UserAddress,
                                    UserPassword = user?.UserPassword,
                                    UserEmail = user?.UserEmail,
                                    UserGender = user?.UserGender ?? null,
                                    DateOfBirth = user.DateOfBirth,
                                    DateofJoin = user.DateofJoin,
                                    UserMobile = user?.UserMobile,
                                    RoleId = user?.UserRoleId,
                                    RoleName = role?.RoleName,
                                    UserStatus = user?.UserStatus ?? false,
                                    UserCreatedBy = user?.UserCreatedBy,
                                    UserCreatedDate = user?.UserCreatedDate,
                                    UserModifiedBy = user?.UserModifiedBy,
                                    UserModifiedDate = user?.UserModifiedDate,
                                    UserQualification = user?.UserQualification,
                                    UserSpecialization = user?.UserSpecialization,
                                    HospitalId = user?.HospitalId,
                                    DesignationId = user?.DesignationId
                                }).ToList();
                return new ResponseModel
                {
                    Status = true,
                    Data = userlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LoginResponseModel> UserSignIn(string username, string password, string publicip)
        {
            try
            {
                if (username == string.Empty)
                {
                    return new LoginResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                if (password == string.Empty)
                {
                    return new LoginResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.PasswordDenied,
                        ErrorCode = "404"
                    };
                }

                var user = (await UserRepository.GetAll()).Where(usr => usr.UserEmail.Equals(username, StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();

                if (user == null)
                {
                    return new LoginResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UNAUTHORIZED_ACCESS,
                        ErrorCode = "404"
                    };
                }

                if (!user.UserStatus)
                {
                    return new LoginResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserInactive,
                        ErrorCode = "405"
                    };
                }
                if (user != null)
                {
                    string strHostName = null;
                    strHostName = System.Net.Dns.GetHostName();
                    string myIP = Dns.GetHostByName(strHostName).AddressList[0].ToString();
                    if (user.UserRoleId != 1 && user.UserRoleId != 2)
                    {
                        var sysIP = await new Repository<Data.Entities.System>().GetById(user.SystemId);
                        string systemIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                        if (sysIP?.SystemIp != systemIP)
                        {
                            //return new LoginResponseModel
                            //{
                            //    Status = false,
                            //    ErrorMessage = Constants.UNAUTHORIZED_ACCESS,
                            //    ErrorCode = "404"
                            //};
                        }
                    }
                }

                var passwordencypt = Encryption.Encrypt(password, Constants.EncryptKey);
                if (!user.UserPassword.Equals(Encryption.Encrypt(password, Constants.EncryptKey), StringComparison.OrdinalIgnoreCase))
                {
                    return new LoginResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.PasswordDenied,
                        ErrorCode = "404"
                    };
                }
                var accessrights = (await new Repository<AccessRights>().GetAll()).Where(usr => usr.UserId == user.UserId)?.ToList();
                return new LoginResponseModel
                {
                    AccessRights = accessrights,
                    Status = true,
                    UserProfile = user,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> ForgetPassword(string username)
        {
            try
            {
                if (username == string.Empty)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = (await UserRepository.GetAll()).Where(usr => usr.UserEmail.Equals(username, StringComparison.OrdinalIgnoreCase))?.FirstOrDefault();

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UNAUTHORIZED_ACCESS,
                        ErrorCode = "404"
                    };
                }

                if (!user.UserStatus)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserInactive,
                        ErrorCode = "405"
                    };
                }

                var TemplateDet = (await new Repository<Templates>().GetAll())?
                    .Where(tmp => tmp.TemplateName.Equals("ForgotPassword", StringComparison.OrdinalIgnoreCase)).ToList().FirstOrDefault();

                var MailBody = TemplateDet.MailBody;
                MailBody = MailBody.Replace("[{Password}]", Encryption.Decrypt(user.UserPassword, Constants.EncryptKey));
                MailBody = MailBody.Replace("[{userName}]", user.FirstName + ' ' + user.LastName);
                var SMSContent = TemplateDet.SMSContent;
                SMSContent = SMSContent.Replace("[{Password}]", Encryption.Decrypt(user.UserPassword, Constants.EncryptKey));

                EmailHelper.SendEmail(new EmailConfigModel { TemplateBody = MailBody, ToEmailList = user.UserEmail, Subject = TemplateDet.MailSubject });

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = Constants.UserPassword,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> ChangePassword(string username, string password)
        {
            try
            {
                if (username == string.Empty)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var userProfile = (await UserRepository.GetAll())?.Where(usr => usr.UserEmail.Equals(username, StringComparison.OrdinalIgnoreCase))?.ToList().FirstOrDefault();

                if (userProfile == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }
                else if (!userProfile.UserStatus)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserInactive,
                        ErrorCode = "405"
                    };
                }
                else
                {
                    userProfile.UserPassword = Encryption.Encrypt(password, Constants.EncryptKey);
                    await UserRepository.Update(userProfile);

                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.SetNewPasswordPass,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseModel GetLoginUserDetails(long userid)
        {
            try
            {
                var result = UserRepository.GetByIds(userid);
                return new ResponseModel
                {
                    Data = result,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> AddNewNotes(NotesModel notesmodel)
        {
            try
            {

                await new Repository<Notes>().Insert(new Notes
                {
                    NotesCreatedBy = notesmodel.NotesCreatedBy,
                    NotesCreatedDate = DateTime.Now,
                    Description = notesmodel.Description
                });
                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = Constants.SuccessMessage,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteNotes(long notesid)
        {
            try
            {
                if (notesid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await new Repository<Notes>().GetById(notesid);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    await new Repository<Notes>().Delete(notesid);

                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.DelSuccessMessage,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
