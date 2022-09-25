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
    public class DesignationManager : IDesignationManager
    {
        private IRepository<Designation> DesignationRepository;

        public DesignationManager()
        {
            this.DesignationRepository = new Repository<Designation>();
        }

        public async Task<ResponseModel> AddNewDesignation(DesignationModel designationModel)
        {
            try
            {
                if (string.IsNullOrEmpty(designationModel.DesignationName))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                if (designationModel.Mode + "" == "E" && designationModel.DesignationId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var designationdata = designationModel.Mode + "" != "E" ? (await DesignationRepository.GetAll())?.
                        Where(desig => desig.DesignationName.Equals(designationModel.DesignationName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                        (await DesignationRepository.GetAll())?.Where(desig => desig.DesignationName.Equals(designationModel.DesignationName, StringComparison.OrdinalIgnoreCase)
                            && desig.DesignationId != designationModel.DesignationId).FirstOrDefault();

                if (designationdata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (designationModel.Mode + "" == "E")
                {
                    var designation = await DesignationRepository.GetById(designationModel.DesignationId ?? 0);

                    if (designation == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    designation.DesignationName = designationModel.DesignationName;
                    designation.DesignationModifiedBy = designationModel.DesignationModifiedBy;
                    designation.DesignationModifiedDate = DateTime.Now;
                    await DesignationRepository.Update(designation);
                }
                else
                {
                    await DesignationRepository.Insert(new Designation
                    {
                        DesignationName = designationModel.DesignationName,
                        DesignationCreatedBy = designationModel.DesignationCreatedBy,
                        DesignationCreatedDate = DateTime.Now,
                        DesignationStatus = true
                    });
                }
                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = designationModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DesignationModel> GetEditDesignation(long designationid)
        {
            try
            {
                var designation = await DesignationRepository.GetById(designationid);
                var result = new DesignationModel();
                result.DesignationId = designation.DesignationId;
                result.DesignationName = designation.DesignationName;
                result.DesignationStatus = designation.DesignationStatus;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteDesignation(long designationid)
        {
            try
            {
                if (designationid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await DesignationRepository.GetById(designationid);

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
                    await DesignationRepository.Delete(designationid);

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

        public async Task<ResponseModel> UpdateDesignationStatus(long designationid, bool designationstatus, long modifiedby)
        {
            try
            {
                if (designationid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }
                var designationdata = await DesignationRepository.GetById(designationid);
                if (designationdata == null)
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
                    designationdata.DesignationStatus = designationstatus;
                    designationdata.DesignationModifiedBy = modifiedby;
                    designationdata.DesignationModifiedDate = DateTime.Now;
                    await DesignationRepository.Update(designationdata);
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

        public async Task<ResponseModel> GetAllDesignation()
        {
            try
            {
                var systemlist = await DesignationRepository.GetAll();
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
    }
}
