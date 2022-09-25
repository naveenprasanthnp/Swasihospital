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
    public class AccessRightManager : IAccessRightManager
    {
        private IRepository<AccessRights> AccessRepository;

        public AccessRightManager()
        {
            this.AccessRepository = new Repository<AccessRights>();
        }

        public async Task<ResponseModel> GetAllMenu()
        {
            try
            {
                var systemlist = await new Repository<Menu>().GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = systemlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllAccessRights(long? userid)
        {
            try
            {
                var accessrightlist = (await new Repository<AccessRights>().GetAll()).Where(x => (x.UserId == userid || userid == 0)).ToList();
                return new ResponseModel
                {
                    Status = true,
                    Data = accessrightlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> AddAccessRights(List<AccessRightsModel> accessmodels,long? hospitalid)
        {
            try
            { 
                if (accessmodels == null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.RoleDuplicate,
                        ErrorCode = "409"
                    };
                }

                foreach (var item in accessmodels)
                {
                    var result = (await new Repository<AccessRights>().GetAll()).Where(x => x.UserId == item.UserId
                    && x.MenuId == item.MenuId).FirstOrDefault();
                    if (result != null)
                    {
                        result.DesignationId = 0;
                        result.HospitalId = hospitalid;
                        result.UserId = item.UserId;
                        result.IsCreate = item.IsCreate;
                        result.IsEdit = item.IsEdit;
                        result.IsView = item.IsView;
                        result.IsDelete = item.IsDelete;
                        result.MenuId = item.MenuId;
                        await new Repository<AccessRights>().Update(result);
                    }
                    else
                    {
                        var access = new AccessRights
                        {
                            DesignationId = 0,
                            HospitalId = hospitalid,
                            UserId = item.UserId,
                            IsCreate = item.IsCreate,
                            IsEdit = item.IsEdit,
                            IsView = item.IsView,
                            IsDelete = item.IsDelete,
                            MenuId = item.MenuId,
                            CreatedDate = DateTime.Now,
                            CreatedBy = 1
                        };
                        await new Repository<AccessRights>().Insert(access);
                    }
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = Constants.RoleUpdated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
