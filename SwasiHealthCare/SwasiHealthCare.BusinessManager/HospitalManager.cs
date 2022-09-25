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
    public class HospitalManager : IHospitalManager
    {
        private IRepository<Hospital> HospitalRepository;

        public HospitalManager()
        {
            this.HospitalRepository = new Repository<Hospital>();
        }
        public async Task<ResponseModel> AddHospital(HospitalModel hospitalModel)
        {
            try
            {
                //ip
                if (string.IsNullOrEmpty(hospitalModel.HospitalCode))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                // Edit Mode - User Id
                if (hospitalModel.Mode + "" == "E" && hospitalModel.HospitalId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var hospitaldata = hospitalModel.Mode + "" != "E" ? (await HospitalRepository.GetAll())?.
                        Where(hos => (hos.HospitalCode + "").Equals(hospitalModel.HospitalCode, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                        (await HospitalRepository.GetAll())?.Where(hos => (hos.HospitalCode + "").Equals(hospitalModel.HospitalCode, StringComparison.OrdinalIgnoreCase)
                            && hos.HospitalId != hospitalModel.HospitalId).FirstOrDefault();

                if (hospitaldata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (hospitalModel.Mode + "" == "E")
                {
                    var hospital = await HospitalRepository.GetById(hospitalModel.HospitalId ?? 0);

                    if (hospital == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    hospital.HospitalContactPersonEmail = hospitalModel.HospitalContactPersonEmail;
                    hospital.HospitalContactPersonName = hospitalModel.HospitalContactPersonName;
                    hospital.HospitalAddress = hospitalModel.HospitalAddress;
                    hospital.HospitalCode = hospitalModel.HospitalCode;
                    hospital.HospitalEmail = hospitalModel.HospitalEmail;
                    hospital.HospitalMobilNumber = hospitalModel.HospitalMobilNumber;
                    hospital.HospitalName = hospitalModel.HospitalName;
                    hospital.LandlineNNumber = hospitalModel.LandlineNumber;
                    hospital.HospitalName = hospitalModel.HospitalName;
                    hospital.HospitalModifiedBy = hospitalModel.HospitalModifiedBy;
                    hospital.HospitalModifiedDate = DateTime.Now;
                    hospital.PatientIdStartWith = hospitalModel.PatientIdStartWith;
                    await HospitalRepository.Update(hospital);
                }
                else
                {
                    await HospitalRepository.Insert(new Hospital
                    {
                        HospitalContactPersonEmail= hospitalModel.HospitalContactPersonEmail,
                        HospitalContactPersonName= hospitalModel.HospitalContactPersonName,
                        HospitalAddress= hospitalModel.HospitalAddress,
                        HospitalCode = hospitalModel.HospitalCode,
                        HospitalEmail= hospitalModel.HospitalEmail,
                        HospitalMobilNumber = hospitalModel.HospitalMobilNumber,
                        HospitalName= hospitalModel.HospitalName,
                        LandlineNNumber = hospitalModel.LandlineNumber,
                        HospitalStatus = true,
                        HospitalCreatedBy = hospitalModel.HospitalCreatedBy,
                        HospitalCreatedDate = DateTime.Now,
                        PatientIdStartWith = hospitalModel.PatientIdStartWith
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = hospitalModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HospitalModel> GetEditHospital(long hospitalid)
        {
            try
            {
                var hospital = await HospitalRepository.GetById(hospitalid);
                var result = new HospitalModel();
                if (hospital != null)
                {
                    result.HospitalAddress = hospital.HospitalAddress;
                    result.HospitalCode = hospital.HospitalCode;
                    result.HospitalContactPersonEmail = hospital.HospitalContactPersonEmail;
                    result.HospitalContactPersonName = hospital.HospitalContactPersonName;
                    result.HospitalCreatedBy = hospital.HospitalCreatedBy;
                    //result.HospitalCreatedDate = hospital.HospitalCreatedDate;
                    result.HospitalEmail = hospital.HospitalEmail;
                    result.HospitalId = hospital.HospitalId;
                    result.HospitalMobilNumber = hospital.HospitalMobilNumber;
                    result.HospitalName = hospital.HospitalName;
                    result.LandlineNumber = hospital.LandlineNNumber;
                    result.PatientIdStartWith = hospital.PatientIdStartWith;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteHospital(long hospitalid)
        {
            try
            {
                if (hospitalid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await HospitalRepository.GetById(hospitalid);

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
                    await HospitalRepository.Delete(hospitalid);

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

        public async Task<ResponseModel> UpdateHospitalStatus(long hospitalid, bool hospitalstatus, long modifiedby)
        {
            try
            {
                if (hospitalid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }

                var hospitaldata = await HospitalRepository.GetById(hospitalid);

                if (hospitaldata == null)
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
                    hospitaldata.HospitalStatus = hospitalstatus;
                    hospitaldata.HospitalModifiedBy = modifiedby;
                    hospitaldata.HospitalModifiedDate = DateTime.Now;
                    await HospitalRepository.Update(hospitaldata);
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

        public async Task<ResponseModel> GetAllHospital()
        {
            try
            {
                var hospitallist = await HospitalRepository.GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = hospitallist,
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
