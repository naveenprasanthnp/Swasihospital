using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.Helper;
using SwasiHealthCare.IBusinessManager;
using SwasiHealthCare.IRepository;
using SwasiHealthCare.Model;
using SwasiHealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.BusinessManager
{
    public class RoleManager : IRoleManager
    {
        private IRepository<Roles> RolesRepository;

        public RoleManager()
        {
            this.RolesRepository = new Repository<Roles>();
        }

        public async Task<IEnumerable<Roles>> GetRolesList(string sortby, string sortvalue, long roleid)
        {
            try
            {
                return (await RolesRepository.GetAll())?.OrderByDescending(rlist => rlist.RoleName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> SaveRoles(RolesModel role)
        {
            try
            {
                if (role.RoleName + "" == "")
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.RoleNotfound,
                        ErrorCode = "404"
                    };
                }

                if (role.Mode + "" == "E")
                {
                    if (role.RoleId <= 0)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.RoleNotfound,
                            ErrorCode = "404"
                        };
                    }
                }

                var rolesdata = role.Mode + "" != "E" ? (await RolesRepository.GetAll())?.Where(usr => usr.RoleName.Equals(role.RoleName.Trim(), StringComparison.OrdinalIgnoreCase)).
                    FirstOrDefault() : (await RolesRepository.GetAll())?.Where(rl => rl.RoleName.Equals(role.RoleName.Trim(), StringComparison.OrdinalIgnoreCase)
                                            && rl.RoleId != role.RoleId).FirstOrDefault();

                if (rolesdata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.RoleDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (role.Mode + "" == "E")
                {
                    var roles = await RolesRepository.GetById(role.RoleId ?? 0);
                    roles.RoleName = role.RoleName.Trim();
                    roles.RoleStatus = roles.RoleStatus;
                    roles.RoleCreatedBy = roles.RoleCreatedBy;
                    roles.RoleCreatedDate = roles.RoleCreatedDate;
                    roles.RoleModiifedBy = role.RoleModiifedBy;
                    roles.RoleModifiedDate = DateTime.Now;
                    await RolesRepository.Update(roles);
                }
                else
                {
                    await RolesRepository.Insert(new Roles
                    {
                        RoleName = role.RoleName.Trim(),
                        RoleStatus = role.RoleStatus,
                        RoleCreatedBy = role.RoleCreatedBy,
                        RoleCreatedDate = DateTime.Now
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = role.Mode + "" == "E" ? Constants.RoleUpdated : Constants.RoleCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> RemoveRoles(long roleid)
        {
            try
            {
                if (roleid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.RoleNotfound,
                        ErrorCode = "404"
                    };
                }

                var roles = RolesRepository.GetById(roleid);

                if (roles == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.RoleNotfound,
                        ErrorCode = "404"
                    };
                }

                var userdetail = (await new Repository<Users>().GetAll())?.Where(x => x.UserRoleId == roleid).FirstOrDefault();

                if (userdetail != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.ReferInUsers,
                        ErrorCode = "405"
                    };
                }
                else
                { 
                    await RolesRepository.Delete(roleid);
                    return new ResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.DelRoleSuccessMessage,
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

        public async Task<ResponseModel> GetRoleById(long roleid)
        {
            try
            {
                if (roleid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.RoleNotfound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    var roles = await RolesRepository.GetById(roleid);

                    return new ResponseModel
                    {
                        Status = roles == null ? false : true,
                        Data = roles,
                        ErrorMessage = roles == null ? Constants.RoleNotfound : string.Empty,
                        ErrorCode = roles == null ? "404" : string.Empty
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResponseModel> UpdateRoleStatus(long roleid, bool rolestatus, long modifiedby)
        {
            try
            {
                if (roleid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.RoleNotfound,
                        ErrorCode = "404"
                    };
                }

                var roledata = await RolesRepository.GetById(roleid);

                if (roledata == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.RoleNotfound,
                        ErrorCode = "404"
                    };
                }
                else
                {
                    var userdetail = (await new Repository<Users>().GetAll())?.Where(x => x.UserRoleId == roleid).Any();

                    if (userdetail == true)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.ReferInUsers,
                            ErrorCode = "405"
                        };
                    }
                    else
                    {
                        roledata.RoleStatus = rolestatus;
                        roledata.RoleModiifedBy = modifiedby;
                        roledata.RoleModifiedDate = DateTime.Now;
                        await RolesRepository.Update(roledata);
                        return new ResponseModel
                        {
                            Status = true,
                            SuccessMessage = Constants.RoleStatusChange,
                            ErrorMessage = string.Empty,
                            ErrorCode = string.Empty
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
