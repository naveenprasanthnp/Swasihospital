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
    public class TreatmentManager : ITreatmentManager
    {
        private IRepository<Treatment> TreatmentRepository;

        public TreatmentManager()
        {
            this.TreatmentRepository = new Repository<Treatment>();
        }
        public async Task<ResponseModel> AddNewTreatment(TreatmentModel treatmentModel)
        {
            try
            {
                //ip
                if (string.IsNullOrEmpty(treatmentModel.TreatmentName))
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                // Edit Mode - User Id
                if (treatmentModel.Mode + "" == "E" && treatmentModel.TreatmentId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }


                var treatmentdata = treatmentModel.Mode + "" != "E" ? (await TreatmentRepository.GetAll())?.
                        Where(trmnt => trmnt.TreatmentName.Equals(treatmentModel.TreatmentName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
                        (await TreatmentRepository.GetAll())?.Where(trmnt => trmnt.TreatmentName.Equals(treatmentModel.TreatmentName, StringComparison.OrdinalIgnoreCase)
                            && trmnt.TreatmentId != treatmentModel.TreatmentId).FirstOrDefault();

                if (treatmentdata != null)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserDuplicate,
                        ErrorCode = "409"
                    };
                }

                if (treatmentModel.Mode + "" == "E")
                {
                    var treatment = await TreatmentRepository.GetById(treatmentModel.TreatmentId ?? 0);

                    if (treatment == null)
                    {
                        return new ResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    treatment.TreatmentName = treatmentModel.TreatmentName;
                    treatment.TreatmentDescription = treatmentModel.TreatmentDescription;
                    treatment.TreatmentDuration = treatmentModel.TreatmentDuration;
                    treatment.TreatmentMedicineNeeded = treatmentModel.TreatmentMedicineNeeded;
                    treatment.TreatmentCharges = treatmentModel.TreatmentCharges;
                    treatment.TreatmentModifiedBy = treatmentModel.TreatmentModifiedBy;
                    treatment.TreatmentModifiedDate = DateTime.Now;
                    await TreatmentRepository.Update(treatment);
                }
                else
                {
                    await TreatmentRepository.Insert(new Treatment
                    {
                        TreatmentName = treatmentModel.TreatmentName,
                        TreatmentDescription = treatmentModel.TreatmentDescription,
                        TreatmentDuration = treatmentModel.TreatmentDuration,
                        TreatmentMedicineNeeded = treatmentModel.TreatmentMedicineNeeded,
                        TreatmentCharges = treatmentModel.TreatmentCharges,
                        TreatmentStatus = true,
                        TreatmentCreatedBy = treatmentModel.TreatmentCreatedBy,
                        TreatmentCreatedDate = DateTime.Now
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = treatmentModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TreatmentModel> GetEditTreatment(long treatmentid)
        {
            try
            {
                var tmt = await TreatmentRepository.GetById(treatmentid);
                var result = new TreatmentModel();
                result.TreatmentCharges = tmt.TreatmentCharges;
                result.TreatmentCreatedBy = tmt.TreatmentCreatedBy;
                result.TreatmentCreatedDate = tmt.TreatmentCreatedDate;
                result.TreatmentDescription = tmt.TreatmentDescription;
                result.TreatmentId = tmt.TreatmentId;
                result.TreatmentMedicineNeeded = tmt.TreatmentMedicineNeeded;
                result.TreatmentName = tmt.TreatmentName;
                result.TreatmentStatus = tmt.TreatmentStatus;
                result.TreatmentDuration = tmt.TreatmentDuration;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeleteTreatment(long treatmentid)
        {
            try
            {
                if (treatmentid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await TreatmentRepository.GetById(treatmentid);

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
                    await TreatmentRepository.Delete(treatmentid);

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

        public async Task<ResponseModel> UpdateTreatmentStatus(long treatmentid, bool treatmentstatus, long modifiedby)
        {
            try
            {
                if (treatmentid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }

                var treatmentdata = await TreatmentRepository.GetById(treatmentid);

                if (treatmentdata == null)
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
                    treatmentdata.TreatmentStatus = treatmentstatus;
                    treatmentdata.TreatmentModifiedBy = modifiedby;
                    treatmentdata.TreatmentModifiedDate = DateTime.Now;
                    await TreatmentRepository.Update(treatmentdata);
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

        public async Task<ResponseModel> GetAllTreatment()
        {
            try
            {
                var systemlist = await TreatmentRepository.GetAll();
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

        //public async Task<ResponseModel> GetAllTreatment1()
        //{
        //    try
        //    {
        //        var systemlist = await new Repository<PatientPlanSubscription>().GetAll();
        //        return new ResponseModel
        //        {
        //            Status = true,
        //            Data = systemlist,
        //            SuccessMessage = Constants.SuccessMessage
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
