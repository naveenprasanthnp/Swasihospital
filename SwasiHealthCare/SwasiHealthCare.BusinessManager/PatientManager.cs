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
    public class PatientManager : IPatientManager
    {
        private IRepository<Patient> PatientRepository;

        public PatientManager()
        {
            this.PatientRepository = new Repository<Patient>();
        }

        public async Task<PatientResponseModel> SavePatient(PatientModel patientModel)
        {
            try
            {
                long patientid = 0;
                if (patientModel.Mode + "" == "E" && patientModel.PatientId <= 0)
                {
                    return new PatientResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }
                //long pid = patientModel.PatientId ?? 0;
                //var patientdata = patientModel.Mode + "" != "E" ? (await PatientRepository.GetAll())?.
                //        Where(pnt => pnt.PatientId == pid).FirstOrDefault() :
                //        (await PatientRepository.GetAll())?.Where(pnt => pnt.PatientIDNumber != patientModel.PatientIdNumber).FirstOrDefault();

                //if (patientdata != null)
                //{
                //    return new PatientResponseModel
                //    {
                //        Status = false,
                //        ErrorMessage = Constants.UserDuplicate,
                //        ErrorCode = "409"
                //    };
                //}

                if (patientModel.Mode + "" == "E")
                {
                    var patient = await PatientRepository.GetById(patientModel.PatientId ?? 0);
                    if (patient == null)
                    {
                        return new PatientResponseModel
                        {
                            Status = false,
                            ErrorMessage = Constants.UserNameNotFound,
                            ErrorCode = "404"
                        };
                    }
                    if (patientModel.Flag == "0")
                    {
                        patient.PatientHospitalId = patientModel.PatientHospitalId;
                        patient.PatientGender = patientModel.PatientGender;
                        patient.PatientAge = patientModel.PatientAge;
                        patient.PatientAddress = patientModel.PatientAddress;
                        patient.PatientMobileNumber = patientModel.PatientMobileNumber;
                        patient.PatientWhatsappNumber = patientModel.PatientWhatsappNumber;
                        patient.PatientEducation = patientModel.PatientEducation;
                        patient.PatientOccupation = patientModel.PatientOccupation;
                        patient.PatientMaritalStatus = patientModel.PatientMaritalStatus;
                        patient.PatientPrimaryComplaints = patientModel.PatientPrimaryComplaints;
                        patient.PatientAssociateComplaints = patientModel.PatientAssociateComplaints;
                        patient.PatientHistoryOfPresentIllness = patientModel.PatientHistoryOfPresentIllness;
                        patient.PatientHistoryOfSurgery = patientModel.PatientHistoryOfSurgery;
                        patient.PatientFamilyHistory = patientModel.PatientFamilyHistory;
                        patient.PatientColor = patientModel.PatientColor;
                        patient.PatientHunger = patientModel.PatientHunger;
                        patient.PatientHeight = patientModel.PatientHeight;
                        patient.PatientWeight = patientModel.PatientWeight;
                        patient.PatientTaste = patientModel.PatientTaste;
                        patient.PatientEmotion = patientModel.PatientEmotion;
                        patient.PatientMotion = patientModel.PatientMotion;
                        patient.PatientUrine = patientModel.PatientUrine;
                        patient.PatientBP = patientModel.PatientBP;
                        patient.PatientSugar = patientModel.PatientSugar;
                        patient.PatientTemperature = patientModel.PatientTemperature;
                        patient.PatientPulse = patientModel.PatientPulse;
                        patient.PatientPalpitation = patientModel.PatientPalpitation;
                        patient.PatientTongue = patientModel.PatientTongue;
                        patient.PatientFoot = patientModel.PatientFoot;
                        patient.PatientEye = patientModel.PatientEye;
                        patient.PatientBloodReport = patientModel.PatientBloodReport;
                        patient.PatientUrineReport = patientModel.PatientUrineReport;
                        patient.PatientSputum = patientModel.PatientSputum;
                        patient.PatientCSF = patientModel.PatientCSF;
                        patient.PatientXRay = patientModel.PatientXRay;
                        patient.PatientCT = patientModel.PatientCT;
                        patient.PatientMRI = patientModel.PatientMRI;
                        patient.PatientOthers = patientModel.PatientOthers;
                        patient.FilePath = patientModel.FilePath;
                        patient.PatientIsDelete = false;
                        patient.OBGHistory = patientModel.OBGHistory;
                        patient.HistoryOfTreatment = patientModel.HistoryOfTreatment;
                        patient.KaphajaDisorder = patientModel.KaphajaDisorder;
                        patient.VatajaDisorder = patientModel.VatajaDisorder;
                        patient.PittajaDisorder = patientModel.PittajaDisorder;
                        patient.PatientDate = patientModel.PatientDate;
                        await PatientRepository.Update(patient);
                        patientid = patient.PatientId ?? 0;
                    }

                    if(patientModel.Flag == "1")
                    {
                        patient.PatientColor = patientModel.PatientColor;
                        patient.PatientHunger = patientModel.PatientHunger;
                        patient.PatientHeight = patientModel.PatientHeight;
                        patient.PatientWeight = patientModel.PatientWeight;
                        patient.PatientTaste = patientModel.PatientTaste;
                        patient.PatientEmotion = patientModel.PatientEmotion;
                        patient.PatientMotion = patientModel.PatientMotion;
                        patient.PatientUrine = patientModel.PatientUrine;
                        patient.PatientBP = patientModel.PatientBP;
                        patient.PatientSugar = patientModel.PatientSugar;
                        patient.PatientTemperature = patientModel.PatientTemperature;
                        await PatientRepository.Update(patient);
                        patientid = patient.PatientId ?? 0;
                    }

                    if (patientModel.Flag == "2")
                    {
                        patient.PatientPulse = patientModel.PatientPulse;
                        patient.PatientPalpitation = patientModel.PatientPalpitation;
                        patient.PatientTongue = patientModel.PatientTongue;
                        patient.PatientFoot = patientModel.PatientFoot;
                        patient.PatientEye = patientModel.PatientEye;
                        await PatientRepository.Update(patient);
                        patientid = patient.PatientId ?? 0;
                    }

                    if (patientModel.Flag == "3")
                    {
                        patient.PatientBloodReport = patientModel.PatientBloodReport;
                        patient.PatientUrineReport = patientModel.PatientUrineReport;
                        patient.PatientSputum = patientModel.PatientSputum;
                        patient.PatientCSF = patientModel.PatientCSF;
                        patient.PatientXRay = patientModel.PatientXRay;
                        patient.PatientCT = patientModel.PatientCT;
                        patient.PatientMRI = patientModel.PatientMRI;
                        patient.PatientOthers = patientModel.PatientOthers;
                        patient.FilePath = patientModel.FilePath;
                        await PatientRepository.Update(patient);
                        patientid = patient.PatientId ?? 0;
                    }
                }
                else
                {
                    var result = (new Patient
                    {
                        PatientIDNumber = patientModel.PatientIdNumber,
                        PlanId = patientModel.PlanId,
                        PatientName = patientModel.PatientName,
                        PatientGender = patientModel.PatientGender,
                        PatientAge = patientModel.PatientAge,
                        PatientAddress = patientModel.PatientAddress,
                        PatientMobileNumber = patientModel.PatientMobileNumber,
                        PatientWhatsappNumber = patientModel.PatientWhatsappNumber,
                        PatientEducation = patientModel.PatientEducation,
                        PatientOccupation = patientModel.PatientOccupation,
                        PatientMaritalStatus = patientModel.PatientMaritalStatus,
                        PatientPrimaryComplaints = patientModel.PatientPrimaryComplaints,
                        PatientAssociateComplaints = patientModel.PatientAssociateComplaints,
                        PatientHistoryOfPresentIllness = patientModel.PatientHistoryOfPresentIllness,
                        PatientHistoryOfSurgery = patientModel.PatientHistoryOfSurgery,
                        PatientFamilyHistory = patientModel.PatientFamilyHistory,
                        PatientCreatedBy = patientModel.PatientCreatedBy,
                        PatientCreatedDate = DateTime.Now,
                        PatientDate = DateTime.Now,
                        PatientStatus = true,
                        //PatientHospitalId = 0,
                        PatientHospitalId = patientModel.PatientHospitalId,
                        PatientRegisterNumber = patientModel.PatientRegisterNumber,
                        PatientDoctorId = patientModel.PatientDoctorId,
                        PatientDoctorName = patientModel.PatientDoctorName,
                        PatientIsDelete = false,
                        HistoryOfTreatment = patientModel.HistoryOfTreatment,
                        OBGHistory = patientModel.OBGHistory
                    });

                    


                    await PatientRepository.Insert(result);
                    patientid = result.PatientId ?? 0;
                }

                return new PatientResponseModel
                {
                    Status = true,
                    SuccessMessage = patientModel.Mode + "" == "E" ? Constants.UserUpdated : Constants.UserCreated,
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty,
                    patientid = patientid
                };
            }
            catch (Exception ex)
            {
                if (patientModel.Mode + "" == "E")
                {
                    var patient = await PatientRepository.GetById(patientModel.PatientId ?? 0);
                }
                throw ex;
            }
        }

        public async Task<PatientResponseModel> SavePatientDocumnet(PatientDocument patientDocument)
        {
            try
            {
                await new Repository<PatientDocument>().Insert(patientDocument);
                return new PatientResponseModel
                {
                    Status = true,
                    SuccessMessage = "",
                    ErrorMessage = string.Empty,
                    ErrorCode = string.Empty,
                    patientid = patientDocument.PatientId
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OPDConsoltationResponseModel> SaveOPDConsoltation(OPDConsoltationModel opdconsoltationmodel)
        {
            try
            {
                if (opdconsoltationmodel.OPDConsultationId != null && opdconsoltationmodel.OPDConsultationId != 0)
                {
                    var result = (new Repository<Users>().GetByIds(opdconsoltationmodel.DoctorId ?? 0));
                    var opsconsult = (new Repository<OPDConsoltation>().GetByIds(opdconsoltationmodel.OPDConsultationId));
                    opsconsult.ConsultationIDNumber = opdconsoltationmodel.ConsultationIDNumber;
                    opsconsult.PatientId = opdconsoltationmodel.PatientId;
                    opsconsult.PatientName = opdconsoltationmodel.PatientName;
                    opsconsult.DoctorName = result.FirstName + result.MiddleName + result.LastName;
                    opsconsult.DoctorId = opdconsoltationmodel.DoctorId;
                    opsconsult.MobileNo = opdconsoltationmodel.MobileNo;
                    opsconsult.PatientIDNumber = opdconsoltationmodel.PatientIDNumber;
                    opsconsult.PaymentMode = opdconsoltationmodel.TreatmentPaymentMode;
                    opsconsult.SplDiscount = opdconsoltationmodel.TreatmentSplDiscount;
                    opsconsult.OPDConsultationCreatedBy = opdconsoltationmodel.OPDConsoltationCreatedBy;
                    opsconsult.OPDConsultationModifiedDate = DateTime.Now;
                    opsconsult.NatureOfIllness = opdconsoltationmodel.NatureOfIllness;
                    opsconsult.NetCharges = opdconsoltationmodel.NetCharges;
                    opsconsult.DoctorsNote = opdconsoltationmodel.DoctorsNote;
                    opsconsult.Gender = opdconsoltationmodel.PatientGender;
                    opsconsult.LMP = opdconsoltationmodel.PatientLMP;
                    opsconsult.Height = opdconsoltationmodel.PatientHeight;
                    opsconsult.Pulse = opdconsoltationmodel.PatientPulse;
                    opsconsult.BP = opdconsoltationmodel.PatientBP;
                    opsconsult.SpO2 = opdconsoltationmodel.PatientSpO2;
                    opsconsult.TotalCharges = opdconsoltationmodel.TreatmentTotalServicesCharges;
                    opsconsult.Temp = opdconsoltationmodel.PatientTemp;
                    opsconsult.VisitType = opdconsoltationmodel.PatientVisitType;
                    opsconsult.Weight = opdconsoltationmodel.PatientWeight;
                    opsconsult.Age = opdconsoltationmodel.PatientAge;
                    opsconsult.HospitalId = opdconsoltationmodel.HospitalId;
                    opsconsult.IDNumber = opdconsoltationmodel.IDNumber;
                    opsconsult.ConsultationDate = opdconsoltationmodel.ConsultationDate;
                    opsconsult.PlanId = opdconsoltationmodel.PlanId;

                    await new Repository<OPDConsoltation>().Update(opsconsult);
                    return new OPDConsoltationResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.UserCreated,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty,
                        patientid = opsconsult.PatientId,
                        Data = "",
                        opdconsoltationid = opsconsult.OPDConsultationId
                    };
                }
                else
                {
                    var result = (new Repository<Users>().GetByIds(opdconsoltationmodel.DoctorId ?? 0));
                    var data = new OPDConsoltation
                    {
                        ConsultationIDNumber = opdconsoltationmodel.ConsultationIDNumber,
                        PatientId = opdconsoltationmodel.PatientId,
                        PatientName = opdconsoltationmodel.PatientName,
                        DoctorName = result.FirstName + result.MiddleName + result.LastName,
                        DoctorId = opdconsoltationmodel.DoctorId,
                        MobileNo = opdconsoltationmodel.MobileNo,
                        PatientIDNumber = opdconsoltationmodel.PatientIDNumber,
                        PaymentMode = opdconsoltationmodel.TreatmentPaymentMode,
                        SplDiscount = opdconsoltationmodel.TreatmentSplDiscount,
                        OPDConsultationCreatedBy = opdconsoltationmodel.OPDConsoltationCreatedBy,
                        OPDConsultationCreatedDate = DateTime.Now,
                        NatureOfIllness = opdconsoltationmodel.NatureOfIllness,
                        NetCharges = opdconsoltationmodel.NetCharges,
                        DoctorsNote = opdconsoltationmodel.DoctorsNote,
                        Gender = opdconsoltationmodel.PatientGender,
                        LMP = opdconsoltationmodel.PatientLMP,
                        Height = opdconsoltationmodel.PatientHeight,
                        Pulse = opdconsoltationmodel.PatientPulse,
                        BP = opdconsoltationmodel.PatientBP,
                        SpO2 = opdconsoltationmodel.PatientSpO2,
                        TotalCharges = opdconsoltationmodel.TreatmentTotalServicesCharges,
                        Temp = opdconsoltationmodel.PatientTemp,
                        VisitType = opdconsoltationmodel.PatientVisitType,
                        Weight = opdconsoltationmodel.PatientWeight,
                        Age = opdconsoltationmodel.PatientAge,
                        HospitalId = opdconsoltationmodel.HospitalId,
                        IDNumber = opdconsoltationmodel.IDNumber,
                        ConsultationDate = opdconsoltationmodel.ConsultationDate,
                        PlanId= opdconsoltationmodel.PlanId
                    };

                    await new Repository<OPDConsoltation>().Insert(data);
                    return new OPDConsoltationResponseModel
                    {
                        Status = true,
                        SuccessMessage = Constants.UserCreated,
                        ErrorMessage = string.Empty,
                        ErrorCode = string.Empty,
                        patientid = data.PatientId,
                        Data = "",
                        opdconsoltationid = data.OPDConsultationId
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> UpdateOPDTotalAmount(OPDConsoltationModel opdconsoltationmodel)
        {
            try
            {
                var result = (new Repository<OPDConsoltation>().GetByIds(opdconsoltationmodel.OPDConsultationId));
                if(result!= null)
                {
                    result.SplDiscount = opdconsoltationmodel.TreatmentSplDiscount;
                    result.NetCharges = opdconsoltationmodel.NetCharges;
                    result.TotalCharges = opdconsoltationmodel.TreatmentTotalServicesCharges;
                    await new Repository<OPDConsoltation>().Update(result);
                }
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

        public async Task<PatientModel> GetEditPatient(long PatientId)
        {
            try
            {
                var user = await PatientRepository.GetById(PatientId);
                var result = new PatientModel();
                result.PlanId = user.PlanId;
                result.PatientXRay = user.PatientXRay;
                result.PlanId = user.PlanId;
                result.PatientWhatsappNumber = user.PatientWhatsappNumber;
                result.PatientWeight = user.PatientWeight;
                result.PatientUrineReport = user.PatientUrineReport;
                result.PatientUrine = user.PatientUrine;
                result.PatientTreatment = user.PatientTreatment;
                result.PatientTongue = user.PatientTongue;
                result.PatientTemperature = user.PatientTemperature;
                result.PatientTaste = user.PatientTaste;
                result.PatientSugar = user.PatientSugar;
                result.PatientStatus = user.PatientStatus;
                result.PatientSputum = user.PatientSputum;
                result.PatientPulse = user.PatientPulse;
                result.PatientPrimaryComplaints = user.PatientPrimaryComplaints;
                result.PatientPrescription = user.PatientPrescription;
                result.PatientPalpitation = user.PatientPalpitation;
                result.PatientOthers = user.PatientOthers;
                result.PatientOccupation = user.PatientOccupation;
                result.PatientName = user.PatientName;
                result.PatientMRI = user.PatientMRI;
                result.PatientMotion = user.PatientMotion;
                result.PatientModiifedBy = user.PatientModiifedBy;
                result.PatientMobileNumber = user.PatientMobileNumber;
                result.PatientMaritalStatus = user.PatientMaritalStatus;
                result.PatientIdNumber = user.PatientIDNumber;
                result.PatientId = user.PatientId;
                result.PatientHunger = user.PatientHunger;
                result.PatientHistoryOfSurgery = user.PatientHistoryOfSurgery;
                result.PatientHistoryOfPresentIllness = user.PatientHistoryOfPresentIllness;
                result.PatientGender = user.PatientGender;
                result.PatientHeight = user.PatientHeight;
                result.PatientFoot = user.PatientFoot;
                result.PatientFamilyHistory = user.PatientFamilyHistory;
                result.PatientEye = user.PatientEye;
                result.PatientEmotion = user.PatientEmotion;
                result.PatientEducation = user.PatientEducation;
                result.PatientDiet = user.PatientDiet;
                result.PatientDiagnosis = user.PatientDiagnosis;
                result.PatientCT = user.PatientCT;
                result.PatientCSF = user.PatientCSF;
                result.PatientCreatedDate  =  user.PatientCreatedDate;
                result.PatientCreatedBy = user.PatientCreatedBy;
                result.PatientColor = user.PatientColor;
                result.PatientBP = user.PatientBP;
                result.PatientBloodReport = user.PatientBloodReport;
                result.PatientAssociateComplaints = user.PatientAssociateComplaints;
                result.PatientAge = user.PatientAge;
                result.PatientAddress = user.PatientAddress;
                result.PatientSugar = user.PatientSugar;
                result.PatientBP = user.PatientBP;
                result.PatientDoctorId = user.PatientDoctorId;
                result.PatientDoctorName = user.PatientDoctorName;
                result.OBGHistory = user.OBGHistory;
                result.HistoryOfTreatment = user.HistoryOfTreatment;
                result.VatajaDisorder = user.VatajaDisorder;
                result.PittajaDisorder = user.PittajaDisorder;
                result.KaphajaDisorder = user.KaphajaDisorder;
                result.PatientDate = user.PatientDate;
                result.PlanId = user.PlanId;
                result.PatientHospitalId = user.PatientHospitalId;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PatientDocumentModel GetDocument(long documentid)
        {
            try
            {
                var doc = new Repository<PatientDocument>().GetByIds(documentid);
                var result = new PatientDocumentModel
                {
                    PatientId = doc.PatientId,
                    PatientIdNumber = doc.PatientIdNumber,
                    FileName = doc.FileName,
                    FolderName = doc.FolderName
                };
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> DeletePatient(long PatientId)
        {
            try
            {
                if (PatientId <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = await PatientRepository.GetById(PatientId);

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
                    //await PatientRepository.Delete(PatientId);
                    user.PatientIsDelete = true;
                    await PatientRepository.Update(user);
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

        public async Task<ResponseModel> DeleteOPDService(long opdserviceid)
        {
            try
            {
                if (opdserviceid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user =  new Repository<OPDServices>().GetByIds(opdserviceid);

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
                    var pps = (await new Repository<PatientPlanSubscription>().GetAll()).Where(x => x.OPDServicesId == opdserviceid).FirstOrDefault();
                    if(pps != null)
                    {
                        pps.BalanceServiceAmount = pps.BalanceServiceAmount + user.ServiceCharge;
                        pps .BalanceServiceCount = pps.BalanceServiceCount + 1;
                        await new Repository<PatientPlanSubscription>().Update(pps);
                    }
                    await new Repository<OPDServices>().Delete(opdserviceid);
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

        public async Task<ResponseModel> DeleteOPDPrescription(long opdprescriptionid)
        {
            try
            {
                if (opdprescriptionid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNameNotFound,
                        ErrorCode = "404"
                    };
                }

                var user = new Repository<OPDPrescription>().GetByIds(opdprescriptionid);

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
                    await new Repository<OPDPrescription>().Delete(opdprescriptionid);

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

        public async Task<OPDServicesModel> EditOPDService(long opdserviceid)
        {
            try
            {
                var user = new Repository<OPDServices>().GetByIds(opdserviceid);
                //var list = (await new Repository<PatientPlanSubscription>().GetAll()).ToList();
                var opdser = (await new Repository<PatientPlanSubscription>().GetAll()).Where(x => x.PatientId == user?.PatientId).FirstOrDefault();
                //var opddeatils = new Repository<OPDConsoltation>().GetByIds(user.OPDConsoltationId);
                var result = new OPDServicesModel
                {
                    OPDServicesId = opdser == null ? user.OPDServicesId : opdser.PatientPlanSubscriptionId,
                    ServiceCharge = user.ServiceCharge,
                    ServiceName = user.ServiceName,
                    Remarks = user.Remarks,
                    Count = user.Count,
                    PatientId = user.PatientId,
                    OPDConsoltationId = user.OPDConsoltationId,
                    TherapistName = user.TherapistName,
                    TherapistId = user.TherapistId,
                    PaymentMode = opdser == null ? 0.ToString() : 1.ToString(),
                    ServiceId = user == null ? 0 : user.ServiceId
                };
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OPDPrescriptionModel> EditOPDPrescription(long opdprescriptionid)
        {
            try
            {
                var user = new Repository<OPDPrescription>().GetByIds(opdprescriptionid);

                var result = new OPDPrescriptionModel
                {
                   OPDConsultationId = user.OPDConsultationId,
                   OPDPrescriptionCreatedBy = user.PatientId,
                   OPDPrescriptionId = user.OPDPrescriptionId,
                   MedicineInstructions = user.MedicineInstructions,
                   //MedicineDosage = user.MedicineDosage,
                   MedicineName = user.MedicineName,
                   //MedicineDuration = user.MedicineDuration,
                   //MedicineQuantity = user.MedicineQuantity,
                   //MedicineSpecification = user.MedicineSpecification,
                   MedicineAmount= user.MedicineAmount,
                   MedicineId = user.MedicineId,
                   PatientId = user.PatientId
                };
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllPatients()
        {
            try

            {
                var userlist = (await PatientRepository.GetAll()).Where(x => !x.PatientIsDelete).ToList();
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

        public async Task<ResponseModel> GetAllNotes()
        {
            try
            {
                var userlist = (from user in (await new Repository<Notes>().GetAll())
                                join roles in (await new Repository<Users>().GetAll()) on user.NotesCreatedBy equals roles.UserId into rl
                                from role in rl.DefaultIfEmpty()
                                select new
                                {
                                    NotesId = user.NotesId,
                                    Description = user.Description,
                                    NotesCreatedBy = user.NotesCreatedBy,
                                    NotesCreatedDate = user.NotesCreatedDate,
                                    NotesCreatedByName = role?.FirstName + role?.LastName
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

        public long? GetPatientsCount(long hospitalid)
        {
            try
            {
                var patientlist = PatientRepository.GetAllList().Where(x => x.PatientHospitalId == hospitalid).OrderByDescending(x => x.PatientCreatedDate).
                    FirstOrDefault();
                if (patientlist != null)
                {
                    return patientlist?.PatientRegisterNumber;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long? GetPatientsCountCurrentMonth(long hospitalid)
        {
            try
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var patientlist = PatientRepository.GetAllList()?.Where(x => x.PatientHospitalId == hospitalid && x.PatientCreatedDate >= startDate && x.PatientCreatedDate <= endDate)?.Count();
                if (patientlist != null)
                {
                    return patientlist;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long? GetPatientsCountHospital(long hospitalid)
        {
            try
            {
                var patientlist = PatientRepository.GetAllList()?.Where(x => x.PatientHospitalId == hospitalid)?.Count();
                if (patientlist != null)
                {
                    return patientlist;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> UpdatePatientStatus(long patientid, bool patientstatus, long modifiedby)
        {
            try
            {
                if (patientid <= 0)
                {
                    return new ResponseModel
                    {
                        Status = false,
                        ErrorMessage = Constants.UserNotFound,
                        ErrorCode = "404"
                    };
                }

                var patientdata = await PatientRepository.GetById(patientid);

                if (patientdata == null)
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
                    patientdata.PatientStatus = patientstatus;
                    patientdata.PatientModiifedBy = modifiedby;
                    patientdata.PatientModifiedDate = DateTime.Now;
                    await PatientRepository.Update(patientdata);
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

        public long? GetOPDCount(long hospitalid,bool? isall)
        {
            try
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                if (isall == false)
                {
                    var opdconsultionlist = new Repository<OPDConsoltation>().GetAllList()?.Where(x =>
                    (x.ConsultationDate >= startDate && x.ConsultationDate <= endDate )&& x.HospitalId== hospitalid)?.Count();
                    return opdconsultionlist;
                }
                else
                {
                    var opdconsultionlist = new Repository<OPDConsoltation>().GetAllList()?.Where(x =>x.HospitalId == hospitalid)?.Count();
                    return opdconsultionlist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<ResponseModel> GetAllOPDs(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                var opdconsultionlist = new List<OPDConsoltation>();
                if ( FromDate != null && ToDate != null ) {
                    opdconsultionlist = (await new Repository<OPDConsoltation>().GetAll())?.
                        Where(x => x.OPDConsultationCreatedDate >= FromDate && x.OPDConsultationCreatedDate <= ToDate ).ToList();
                }
                else
                {
                    opdconsultionlist = (await new Repository<OPDConsoltation>().GetAll()).ToList();
                }
                return new ResponseModel
                {
                    Status = true,
                    Data = opdconsultionlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllOPDsCommonSearch(DateTime? FromDate, DateTime? ToDate,long hospitalid)
        {
            try
            {
                var opdconsultionlist = new List<OPDConsoltation>();
                if (FromDate != null && ToDate != null)
                {
                    //opdconsultionlist = (await new Repository<OPDConsoltation>().GetAll())?.
                    //    Where(x => x.OPDConsultationCreatedDate >= FromDate && x.OPDConsultationCreatedDate <= ToDate).ToList();

                    opdconsultionlist = (from pnt in (await new Repository<OPDConsoltation>().GetAll()).ToList()
                                         join roles in (await new Repository<Patient>().GetAll()) on pnt.PatientId equals roles.PatientId into rl
                                         from role in rl.DefaultIfEmpty()
                                         select new OPDConsoltation
                                         {
                                             PatientIDNumber = pnt?.PatientIDNumber,
                                             PatientName = role?.PatientName,
                                             MobileNo = role?.PatientMobileNumber,
                                             DoctorName = pnt?.DoctorName,
                                             OPDConsultationCreatedBy = pnt?.OPDConsultationCreatedBy,
                                             OPDConsultationCreatedDate = pnt.OPDConsultationCreatedDate,
                                             Age = pnt?.Age,
                                             Gender = pnt?.Gender,
                                             BP = pnt?.BP,
                                             Weight = pnt?.Weight,
                                             Height = pnt?.Height,
                                             NatureOfIllness = pnt?.NatureOfIllness,
                                             Pulse = pnt.Pulse,
                                             PatientId = pnt?.PatientId,
                                             OPDConsultationId = pnt.OPDConsultationId,
                                             LMP = pnt?.LMP,
                                             SpO2 = pnt?.SpO2,
                                             VisitType = pnt?.VisitType,
                                             Temp = pnt?.Temp,
                                             DoctorsNote = pnt?.DoctorsNote,
                                             PaymentMode = pnt?.PaymentMode,
                                             TotalCharges = pnt.TotalCharges,
                                             NetCharges = pnt.NetCharges,
                                             PlanId = pnt?.PlanId,
                                             HospitalId = pnt.HospitalId ?? 0,
                                             ConsultationDate = pnt.ConsultationDate
                                         }).Where(x => x.ConsultationDate >= FromDate && x.ConsultationDate <= ToDate && x.HospitalId == hospitalid).ToList();
                }
                else
                {
                    opdconsultionlist = (from pnt in (await new Repository<OPDConsoltation>().GetAll()).ToList()
                                         join roles in (await new Repository<Patient>().GetAll()) on pnt.PatientId equals roles.PatientId into rl
                                         from role in rl.DefaultIfEmpty()
                                         select new OPDConsoltation
                                         {
                                             PatientIDNumber = pnt?.PatientIDNumber,
                                             PatientName = role?.PatientName,
                                             MobileNo = role?.PatientMobileNumber,
                                             DoctorName = pnt?.DoctorName,
                                             OPDConsultationCreatedBy = pnt?.OPDConsultationCreatedBy,
                                             OPDConsultationCreatedDate = pnt.OPDConsultationCreatedDate,
                                             Age = pnt?.Age,
                                             Gender = pnt?.Gender,
                                             BP = pnt?.BP,
                                             Weight = pnt?.Weight,
                                             Height = pnt?.Height,
                                             NatureOfIllness = pnt?.NatureOfIllness,
                                             Pulse = pnt.Pulse,
                                             PatientId = pnt?.PatientId,
                                             OPDConsultationId = pnt.OPDConsultationId,
                                             LMP = pnt?.LMP,
                                             SpO2 = pnt?.SpO2,
                                             VisitType = pnt?.VisitType,
                                             Temp = pnt?.Temp,
                                             DoctorsNote = pnt?.DoctorsNote,
                                             PaymentMode = pnt?.PaymentMode,
                                             TotalCharges = pnt.TotalCharges,
                                             NetCharges = pnt.NetCharges,
                                             PlanId = pnt?.PlanId,
                                             HospitalId = pnt.HospitalId ?? 0,
                                             ConsultationDate = pnt.ConsultationDate
                                         }).ToList();
                }
                return new ResponseModel
                {
                    Status = true,
                    Data = opdconsultionlist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal?> GetAllOPDServicePrescription(DateTime? FromDate, DateTime? ToDate,long? hospitalid,bool isopdser)
        {
            try
            {
                decimal? opdconsultionlist = 0;
                if (isopdser)
                {
                    opdconsultionlist = (from pnt in (await new Repository<OPDConsoltation>().GetAll()).ToList()
                                         join roles in (await new Repository<OPDServices>().GetAll()) on pnt.OPDConsultationId equals roles.OPDConsoltationId into rl
                                         from role in rl.DefaultIfEmpty()
                                         select new
                                         {
                                             role?.ServiceCharge,
                                             pnt?.ConsultationDate,
                                             pnt?.HospitalId
                                         }).Where(x => x.ConsultationDate >= FromDate && x.ConsultationDate <= ToDate &&  x.HospitalId == hospitalid).ToList().Sum(x => x.ServiceCharge);

                }
                else
                {
                    opdconsultionlist = (from pnt in (await new Repository<OPDConsoltation>().GetAll()).ToList()
                                         join roles in (await new Repository<OPDPrescription>().GetAll()) on pnt.OPDConsultationId equals roles.OPDConsultationId into rl
                                         from role in rl.DefaultIfEmpty()
                                         select new
                                         {
                                             role?.MedicineAmount,
                                             pnt?.ConsultationDate,
                                             pnt?.HospitalId
                                         }).Where(x => x.ConsultationDate >= FromDate && x.ConsultationDate <= ToDate && x.HospitalId == hospitalid).ToList().Sum(x => x.MedicineAmount);
                }
                return opdconsultionlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllPatientList(long? PatientId,DateTime? FromDate, DateTime? ToDate,long? HospitalId)
        {
            try
            {
                //var patientlist = new List<Patient>();
                if ((FromDate != null && ToDate != null))
                {
                    var patientlist = (from pnt in (await new Repository<Patient>().GetAll()).ToList()
                                       join roles in (await new Repository<Hospital>().GetAll()) on pnt.PatientHospitalId equals roles.HospitalId into rl
                                       from role in rl.DefaultIfEmpty()
                                       select new
                                       {
                                           PatientId = pnt?.PatientId,
                                           PatientIDNumber = pnt?.PatientIDNumber,
                                           PlanId = pnt?.PlanId,
                                           PatientName = pnt?.PatientName,
                                           PatientGender = pnt?.PatientGender,
                                           PatientAge = pnt?.PatientAge,
                                           PatientAddress = pnt?.PatientAddress,
                                           PatientMobileNumber = pnt?.PatientMobileNumber,
                                           PatientWhatsappNumber = pnt?.PatientWhatsappNumber,
                                           PatientEducation = pnt?.PatientEducation,
                                           PatientOccupation = pnt?.PatientOccupation,
                                           PatientMaritalStatus = pnt?.PatientMaritalStatus,
                                           PatientPrimaryComplaints = pnt?.PatientPrimaryComplaints,
                                           PatientAssociateComplaints = pnt?.PatientAssociateComplaints,
                                           PatientHistoryOfPresentIllness = pnt?.PatientHistoryOfPresentIllness,
                                           PatientHistoryOfSurgery = pnt?.PatientHistoryOfSurgery,
                                           PatientFamilyHistory = pnt?.PatientFamilyHistory,
                                           PatientColor = pnt?.PatientColor,
                                           PatientHunger = pnt?.PatientHunger,
                                           PatientHeight = pnt?.PatientHeight,
                                           PatientWeight = pnt?.PatientWeight,
                                           PatientTaste = pnt?.PatientTaste,
                                           PatientEmotion = pnt?.PatientEmotion,
                                           PatientMotion = pnt?.PatientMotion,
                                           PatientUrine = pnt?.PatientUrine,
                                           PatientBP = pnt?.PatientBP,
                                           PatientSugar = pnt?.PatientSugar,
                                           PatientTemperature = pnt?.PatientTemperature,
                                           PatientPulse = pnt?.PatientPulse,
                                           PatientPalpitation = pnt?.PatientPalpitation,
                                           PatientTongue = pnt?.PatientTongue,
                                           PatientFoot = pnt?.PatientFoot,
                                           PatientEye = pnt?.PatientEye,
                                           PatientBloodReport = pnt?.PatientBloodReport,
                                           FilePath = pnt?.FilePath,
                                           PatientUrineReport = pnt?.PatientUrineReport,
                                           PatientSputum = pnt?.PatientSputum,
                                           PatientCSF = pnt?.PatientCSF,
                                           PatientXRay = pnt?.PatientXRay,
                                           PatientCT = pnt?.PatientCT,
                                           PatientMRI = pnt?.PatientMRI,
                                           PatientOthers = pnt?.PatientOthers,
                                           PatientStatus = pnt?.PatientStatus,
                                           PatientIsDelete = pnt?.PatientIsDelete,
                                           PatientDiagnosis = pnt?.PatientDiagnosis,
                                           PatientTreatment = pnt?.PatientTreatment,
                                           PatientDiet = pnt?.PatientDiet,
                                           PatientPrescription = pnt?.PatientPrescription,
                                           PatientCreatedBy = pnt?.PatientCreatedBy,
                                           PatientCreatedDate = pnt?.PatientCreatedDate,
                                           PatientModiifedBy = pnt?.PatientModiifedBy,
                                           PatientHospitalId = pnt?.PatientHospitalId,
                                           PatientModifiedDate = pnt?.PatientModifiedDate,
                                           PatientRegisterNumber = pnt?.PatientRegisterNumber,
                                           PatientDoctorId = pnt?.PatientDoctorId,
                                           PatientDoctorName = pnt?.PatientDoctorName,
                                           HospitalName = role?.HospitalName,
                                           PatientDate = pnt?.PatientDate
                                       }).ToList()?.
                        Where(x => (x.PatientId == PatientId || PatientId == 0)&& (x.PatientHospitalId == HospitalId || HospitalId == 0) && x.PatientDate >= FromDate && x.PatientDate <= ToDate).ToList();
                    return new ResponseModel
                    {
                        Status = true,
                        Data = patientlist,
                        SuccessMessage = Constants.SuccessMessage
                    };
                }
                else
                {
                    var patientlist = (from pnt in (await new Repository<Patient>().GetAll()).ToList()
                                       join roles in (await new Repository<Hospital>().GetAll()) on pnt.PatientHospitalId equals roles.HospitalId into rl
                                       from role in rl.DefaultIfEmpty()
                                       select new
                                       {
                                           PatientId = pnt?.PatientId,
                                           PatientIDNumber = pnt?.PatientIDNumber,
                                           PlanId = pnt?.PlanId,
                                           PatientName = pnt?.PatientName,
                                           PatientGender = pnt?.PatientGender,
                                           PatientAge = pnt?.PatientAge,
                                           PatientAddress = pnt?.PatientAddress,
                                           PatientMobileNumber = pnt?.PatientMobileNumber,
                                           PatientWhatsappNumber = pnt?.PatientWhatsappNumber,
                                           PatientEducation = pnt?.PatientEducation,
                                           PatientOccupation = pnt?.PatientOccupation,
                                           PatientMaritalStatus = pnt?.PatientMaritalStatus,
                                           PatientPrimaryComplaints = pnt?.PatientPrimaryComplaints,
                                           PatientAssociateComplaints = pnt?.PatientAssociateComplaints,
                                           PatientHistoryOfPresentIllness = pnt?.PatientHistoryOfPresentIllness,
                                           PatientHistoryOfSurgery = pnt?.PatientHistoryOfSurgery,
                                           PatientFamilyHistory = pnt?.PatientFamilyHistory,
                                           PatientColor = pnt?.PatientColor,
                                           PatientHunger = pnt?.PatientHunger,
                                           PatientHeight = pnt?.PatientHeight,
                                           PatientWeight = pnt?.PatientWeight,
                                           PatientTaste = pnt?.PatientTaste,
                                           PatientEmotion = pnt?.PatientEmotion,
                                           PatientMotion = pnt?.PatientMotion,
                                           PatientUrine = pnt?.PatientUrine,
                                           PatientBP = pnt?.PatientBP,
                                           PatientSugar = pnt?.PatientSugar,
                                           PatientTemperature = pnt?.PatientTemperature,
                                           PatientPulse = pnt?.PatientPulse,
                                           PatientPalpitation = pnt?.PatientPalpitation,
                                           PatientTongue = pnt?.PatientTongue,
                                           PatientFoot = pnt?.PatientFoot,
                                           PatientEye = pnt?.PatientEye,
                                           PatientBloodReport = pnt?.PatientBloodReport,
                                           FilePath = pnt?.FilePath,
                                           PatientUrineReport = pnt?.PatientUrineReport,
                                           PatientSputum = pnt?.PatientSputum,
                                           PatientCSF = pnt?.PatientCSF,
                                           PatientXRay = pnt?.PatientXRay,
                                           PatientCT = pnt?.PatientCT,
                                           PatientMRI = pnt?.PatientMRI,
                                           PatientOthers = pnt?.PatientOthers,
                                           PatientStatus = pnt?.PatientStatus,
                                           PatientIsDelete = pnt?.PatientIsDelete,
                                           PatientDiagnosis = pnt?.PatientDiagnosis,
                                           PatientTreatment = pnt?.PatientTreatment,
                                           PatientDiet = pnt?.PatientDiet,
                                           PatientPrescription = pnt?.PatientPrescription,
                                           PatientCreatedBy = pnt?.PatientCreatedBy,
                                           PatientCreatedDate = pnt?.PatientCreatedDate,
                                           PatientModiifedBy = pnt?.PatientModiifedBy,
                                           PatientHospitalId = pnt?.PatientHospitalId,
                                           PatientModifiedDate = pnt?.PatientModifiedDate,
                                           PatientRegisterNumber = pnt?.PatientRegisterNumber,
                                           PatientDoctorId = pnt?.PatientDoctorId,
                                           PatientDoctorName = pnt?.PatientDoctorName,
                                           HospitalName = role?.HospitalName,
                                           PatientDate = pnt?.PatientDate
                                       }).ToList();
                    return new ResponseModel
                    {
                        Status = true,
                        Data = patientlist,
                        SuccessMessage = Constants.SuccessMessage
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetDocumentById(long patientid)
        {
            try
            {
                var patientdocuments = (await new Repository<PatientDocument>().GetAll()).Where(x => x.PatientId == patientid);
                return new ResponseModel
                {
                    Status = true,
                    Data = patientdocuments,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllOpdService()
        {
            try
            {
                var opdservicelist = await new Repository<OPDServices>().GetAll();
                return new ResponseModel
                {
                    Status = true,
                    Data = opdservicelist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetAllOpdPrescription()
        {
            try
            {
                var opdservicelist = (from user in (await new Repository<OPDPrescription>().GetAll())
                                      join roles in (await new Repository<Medicine>().GetAll()) on user.MedicineId equals roles.MedicineId into rl
                                      from role in rl.DefaultIfEmpty()
                                      select new
                                      {
                                          OPDPrescriptionId = user.OPDPrescriptionId,
                                          OPDConsultationId = user?.OPDConsultationId,
                                          PatientId = user?.PatientId,
                                          MedicineId = user?.MedicineId,
                                          MedicineName = user?.MedicineName,
                                          MedicineQuantity = user?.MedicineQuantity,
                                          MedicineAmount = user?.MedicineAmount,
                                          MedicineInstructions = user?.MedicineInstructions,
                                          OPDPrescriptionCreatedBy = user?.OPDPrescriptionCreatedBy,
                                          OPDPrescriptionCreatedDate = user?.OPDPrescriptionCreatedDate,
                                          OPDPrescriptionModiifedBy = user?.OPDPrescriptionModiifedBy,
                                          OPDPrescriptionModifiedDate = user?.OPDPrescriptionModifiedDate,
                                          HospitalId = user.HospitalId,
                                          MedicineRate = role?.MedicineSalesRate
                                      }).ToList();
                
                return new ResponseModel
                {
                    Status = true,
                    Data = opdservicelist,
                    SuccessMessage = Constants.SuccessMessage
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> SaveOPDService(OPDServicesModel opdservicemodel)
        {
            try
            {
                var res = new Treatment();
                long? trid = 0;
                //if (opdservicemodel.IsNewService == true)
                //{
                //    res = new Treatment
                //    {
                //        TreatmentName = opdservicemodel.ServiceName,
                //        TreatmentDescription = opdservicemodel.ServiceName,
                //        TreatmentDuration = "",
                //        TreatmentMedicineNeeded = "",
                //        TreatmentCharges = opdservicemodel.ServiceCharge,
                //        TreatmentStatus = true,
                //        TreatmentCreatedBy = opdservicemodel.OPDServicesCreatedBy,
                //        TreatmentCreatedDate = DateTime.Now
                //    };
                //    await new Repository<Treatment>().Insert(res);
                //    trid = res.TreatmentId;
                //}
                //else
                //{
                //    trid = opdservicemodel.ServiceId;
                //}

                var tmid = new PatientPlanSubscription();
                var plan = new Plan();
                var treatmentname = new Treatment();
                if (opdservicemodel.PatientPlanSubscriptionId != 0)
                {
                     tmid = (new Repository<PatientPlanSubscription>().GetByIds(opdservicemodel.PatientPlanSubscriptionId));
                     plan = (new Repository<Plan>().GetByIds(tmid.PlanId));
                     treatmentname = (new Repository<Treatment>().GetByIds(Int64.Parse(plan.TreatmentName)));
                }
                else
                {
                    treatmentname = (new Repository<Treatment>().GetByIds(opdservicemodel.ServiceId ?? 0));
                }
                long theropistid = 0;
                if (opdservicemodel.TherapistName != "")
                {
                    theropistid = long.Parse(opdservicemodel.TherapistName);
                    var f1  = (await new Repository<Users>().GetAll()).Where(x => x.UserId
                    == theropistid).Select(x => x.FirstName).FirstOrDefault();
                    var f2  = (await new Repository<Users>().GetAll()).Where(x => x.UserId
                    == theropistid).Select(x => x.MiddleName).FirstOrDefault();
                    var f3 = (await new Repository<Users>().GetAll()).Where(x => x.UserId
                    == theropistid).Select(x => x.LastName).FirstOrDefault();
                    opdservicemodel.TherapistName = f1 + f2 + f3;
                }

                    var data = new OPDServices
                {
                    ServiceId = treatmentname.TreatmentId,
                    OPDConsoltationId = opdservicemodel.OPDConsoltationId,
                    PatientId = opdservicemodel.PatientId,
                    ServiceName = treatmentname.TreatmentName,
                    Count = opdservicemodel.Count,
                    ServiceCharge = opdservicemodel.ServiceCharge,
                    Remarks = opdservicemodel.Remarks,
                    OPDServicesCreatedBy = opdservicemodel.OPDServicesCreatedBy,
                    OPDServicesCreatedDate = DateTime.Now,
                    HospitalId= opdservicemodel.HospitalId,
                    TherapistId = theropistid,
                    TherapistName = opdservicemodel.TherapistName
                    };
                await new Repository<OPDServices>().Insert(data);

                if(opdservicemodel.TreatmentPaymentMode.Equals("1"))
                {
                    var subs = (await new Repository<PatientPlanSubscription>().GetById(opdservicemodel.PatientPlanSubscriptionId));
                    if(subs != null)
                    {
                        subs.OPDServicesId = data.OPDServicesId;
                        subs.BalanceServiceAmount = subs.BalanceServiceAmount - opdservicemodel.ServiceCharge;
                        subs.BalanceServiceCount = subs.BalanceServiceCount - 1;
                        await new Repository<PatientPlanSubscription>().Update(subs);
                    }
                }

                //decimal opdserviceamt = (await new Repository<OPDServices>().GetAll()).Where(x => x.OPDConsoltationId
                //== opdservicemodel.OPDConsoltationId && x.HospitalId == opdservicemodel.HospitalId).Sum(x => x.ServiceCharge);

                var opdcons = (await new Repository<OPDConsoltation>().GetById(opdservicemodel.OPDConsoltationId));
                if(opdcons!= null)
                {
                    opdcons.TotalCharges = (opdcons.TotalCharges) + (opdservicemodel.ServiceCharge);
                    await new Repository<OPDConsoltation>().Update(opdcons);
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = Constants.UserCreated,
                    Data = data,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> SaveOPDPrescription(OPDPrescriptionModel opdprescriptionmodel)
        {
            try
            {
                var drugname = ( new Repository<Medicine>().GetByIds(opdprescriptionmodel.MedicineId));
                var data = new OPDPrescription
                {
                    OPDConsultationId = opdprescriptionmodel.OPDConsultationId,
                    PatientId = opdprescriptionmodel.PatientId,
                    MedicineName = drugname?.MedicineName,
                    MedicineId = drugname?.MedicineId ?? 0,
                    MedicineAmount = opdprescriptionmodel.MedicineAmount,
                    //MedicineDuration = opdprescriptionmodel.MedicineDuration,
                    //MedicineDosage = opdprescriptionmodel. MedicineDosage,
                    MedicineQuantity = opdprescriptionmodel.MedicineQuantity,
                    MedicineInstructions = opdprescriptionmodel.MedicineInstructions,
                    //MedicineSpecification = opdprescriptionmodel.MedicineSpecification,
                    OPDPrescriptionCreatedBy = opdprescriptionmodel.OPDPrescriptionCreatedBy,
                    OPDPrescriptionCreatedDate = DateTime.Now,
                    HospitalId= opdprescriptionmodel.HospitalId
                };
                await new Repository<OPDPrescription>().Insert(data);

                var purchasemedicine = (await new Repository<PurchaseMedicine>().GetAll()).
                    Where(x => x.MedicineId == drugname?.MedicineId ).OrderByDescending(x=>x.PurchaseMedicineCreatedDate).FirstOrDefault();
                if(purchasemedicine != null)
                {
                    purchasemedicine.AvailableMedicineCount =
                         (purchasemedicine?.MedicineCurrentStock ?? 0) -
                         (opdprescriptionmodel?.MedicineQuantity ?? 0);
                    purchasemedicine.MedicineCurrentStock = (purchasemedicine?.MedicineCurrentStock ?? 0) -
                         (opdprescriptionmodel?.MedicineQuantity ?? 0);
                    await new Repository<PurchaseMedicine>().Update(purchasemedicine);

                    drugname.MedicineCurrentStack = drugname.MedicineCurrentStack - opdprescriptionmodel.MedicineQuantity;
                    drugname.MedicineBalanceStack = drugname.MedicineBalanceStack - opdprescriptionmodel.MedicineQuantity;
                    await new Repository<Medicine>().Update(drugname);
                }


                //decimal opdpreseamt = (await new Repository<OPDPrescription>().GetAll()).Where(x => x.OPDConsultationId
                //== opdprescriptionmodel.OPDConsultationId && x.HospitalId == opdprescriptionmodel.HospitalId).Sum(x => x.MedicineAmount);

                var opdcons = (await new Repository<OPDConsoltation>().GetById(opdprescriptionmodel.OPDConsultationId));
                if (opdcons != null)
                {
                    opdcons.TotalCharges = (opdcons.TotalCharges) + (opdprescriptionmodel.MedicineAmount);
                    await new Repository<OPDConsoltation>().Update(opdcons);
                }

                return new ResponseModel
                {
                    Status = true,
                    SuccessMessage = Constants.UserCreated,
                    Data = data,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------------------------------
        public async Task <List<OPDReportModel>> GetOPDReport(DateTime? FromDate, DateTime? ToDate, long? HospitalId, FilterModel FilterModel)
        {
            try
            {
                var opdconsultionlist = new List<OPDReportModel>();
                if ((FromDate != null && ToDate != null))
                {
                    opdconsultionlist = (from opdc in (await new Repository<OPDConsoltation>().GetAll())
                                         join opds in (await new Repository<OPDServices>().GetAll()) on opdc.OPDConsultationId equals opds.OPDConsoltationId into os
                                         from opds in os.DefaultIfEmpty()
                                         join opdp in (await new Repository<OPDPrescription>().GetAll()) on opdc.OPDConsultationId equals opdp.OPDConsultationId into op
                                         from opp in op.DefaultIfEmpty()
                                         select new OPDReportModel
                                         {
                                             OPDConsultationId = opdc?.OPDConsultationId ?? 0,
                                             PatientId = opdc?.PatientId,
                                             DoctorId = opdc?.DoctorId,
                                             ConsultationDate = opdc== null ? DateTime.Now : opdc.ConsultationDate,
                                             DoctorName = opdc?.DoctorName,
                                             ConsultationIDNumber = opdc?.ConsultationIDNumber,
                                             PatientIDNumber = opdc?.PatientIDNumber,
                                             PatientName = opdc?.PatientName,
                                             ServiceName = opds?.ServiceName,
                                             TherapistId = opds?.TherapistId,
                                             TherapistName = opds?.TherapistName,
                                             MedicineAmount = opp?.MedicineAmount ?? 0,
                                             ServiceCharge = opds?.ServiceCharge ?? 0,
                                             HospitalId = opdc?.HospitalId,
                                             filterModel= FilterModel
                                         }).Where(x => (x.ConsultationDate >= FromDate && x.ConsultationDate <= ToDate) || (x.HospitalId == HospitalId)).ToList();
                }
                else
                {
                    opdconsultionlist = (from opdc in (await new Repository<OPDConsoltation>().GetAll())
                                         join opds in (await new Repository<OPDServices>().GetAll()) on opdc.OPDConsultationId equals opds.OPDConsoltationId into os
                                         from opds in os.DefaultIfEmpty()
                                         join opdp in (await new Repository<OPDPrescription>().GetAll()) on opdc.OPDConsultationId equals opdp.OPDConsultationId into op
                                         from opp in op.DefaultIfEmpty()
                                         select new OPDReportModel
                                         {
                                             OPDConsultationId = opdc?.OPDConsultationId ?? 0,
                                             PatientId = opdc?.PatientId,
                                             DoctorId = opdc?.DoctorId,
                                             ConsultationDate = opdc == null ? DateTime.Now : opdc.ConsultationDate,
                                             DoctorName = opdc?.DoctorName,
                                             ConsultationIDNumber = opdc?.ConsultationIDNumber,
                                             PatientIDNumber = opdc?.PatientIDNumber,
                                             PatientName = opdc?.PatientName,
                                             ServiceName = opds?.ServiceName,
                                             TherapistId = opds?.TherapistId,
                                             TherapistName = opds?.TherapistName,
                                             MedicineAmount = opp?.MedicineAmount ?? 0,
                                             ServiceCharge = opds?.ServiceCharge ?? 0,
                                             ModeofPayment = opdc?.PaymentMode,
                                             filterModel= FilterModel
                                         }).ToList();
                }
                return opdconsultionlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Dashboard> GetOPDServiceCount(long hospitalid)
        {
            try
            {
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                var opdlist = (await new Repository<OPDConsoltation>().GetAll()).Where(x => x.HospitalId  == hospitalid && x.ConsultationDate >= startDate && x.ConsultationDate <= endDate).ToList();

                var result = (from pnt in (opdlist)
                              join ser in (await new Repository<OPDServices>().GetAll()) on pnt.OPDConsultationId equals ser.OPDConsoltationId into rl
                              from serv in rl.DefaultIfEmpty()
                              select new
                              {
                                  serv?.ServiceId
                              }).Where(x => x.ServiceId != null).ToList();

                var result1 = (from pnt in (opdlist)
                              join ser in (await new Repository<OPDPrescription>().GetAll()) on pnt.OPDConsultationId equals ser.OPDConsultationId into rl
                              from serv in rl.DefaultIfEmpty()
                              select new
                              {
                                  serv?.OPDPrescriptionId
                              }).Where(x => x.OPDPrescriptionId != null).ToList();

                var finalresult = new Dashboard();
                finalresult.DashboardMedicineCount = result1.Count;
                finalresult.DashboardTreatmentCount = result.Count;
                if (finalresult != null)
                {
                    return finalresult;
                }
                else
                {
                    return new Dashboard { DashboardMedicineCount = 0, DashboardTreatmentCount = 0 };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
