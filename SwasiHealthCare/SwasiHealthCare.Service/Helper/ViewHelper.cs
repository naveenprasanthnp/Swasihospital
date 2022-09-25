using Newtonsoft.Json;
using SwasiHealthCare.BusinessManager;
using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.IBusinessManager;
using SwasiHealthCare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SwasiHealthCare.Service.Helper
{
    public static class ViewHelper
    {
        public static async Task<List<UserModel>> GetAllUsers()
        {
            try
            {
                IUserManager userManager = new UserManager();
                var userlist = (await userManager.GetAllUsers());
                List<dynamic> data = Enumerable.ToList<dynamic>(userlist.Data);
                return GetUserList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<PatientModel>> GetAllPatients()
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var patientlist = await patientManager.GetAllPatients();
                List<dynamic> data = Enumerable.ToList<dynamic>(patientlist.Data);
                return GetPatientList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<NotesModel>> GetAllNotesList()
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var notelist = await patientManager.GetAllNotes();
                List<dynamic> data = Enumerable.ToList<dynamic>(notelist.Data);
                return GetNotesList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<PatientDocumentModel>> GetAllDocumentById(long patientid)
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var patientdoclist = await patientManager.GetDocumentById(patientid);
                List<dynamic> data = Enumerable.ToList<dynamic>(patientdoclist.Data);
                return GetDocuList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<OPDConsoltationModel>> GetAllOPDConsoltation(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var opdlist = await patientManager.GetAllOPDs( FromDate,  ToDate);
                List<dynamic> data = Enumerable.ToList<dynamic>(opdlist.Data);
                return GetOPDConsultList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<OPDConsoltationModel>> GetAllOPDCommonSearch(DateTime? FromDate, DateTime? ToDate,long hospitalid)
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var opdlist = await patientManager.GetAllOPDsCommonSearch(FromDate, ToDate, hospitalid);
                List<dynamic> data = Enumerable.ToList<dynamic>(opdlist.Data);
                return GetOPDConsultList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<OPDConsoltationModel>> GetAllOPDConsoltationService(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var opdlist = await patientManager.GetAllOPDs(FromDate, ToDate);
                List<dynamic> data = Enumerable.ToList<dynamic>(opdlist.Data);
                return GetOPDConsultList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<PatientModel>> GetAllPatient(long? PatientId,DateTime? FromDate, DateTime? ToDate,long? HospitalId)
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var opdlist = await patientManager.GetAllPatientList(PatientId,FromDate, ToDate, HospitalId);
                List<dynamic> data = Enumerable.ToList<dynamic>(opdlist.Data);
                return GePatientList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<SystemViewModel>> GetAllSystem()
        {
            try
            {
                ISystemManager systemManager = new SystemManager();
                var systemlist = await systemManager.GetAllSystem();
                List<dynamic> data = Enumerable.ToList<dynamic>(systemlist.Data);
                return GetSystemList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<MenuModel>> GetAllMenu()
        {
            try
            {
                IAccessRightManager accessManager = new AccessRightManager();
                var menulist = await accessManager.GetAllMenu();
                List<dynamic> data = Enumerable.ToList<dynamic>(menulist.Data);
                return GetMenuList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<AccessRightsModel>> GetAllAccessRights(long? userid)
        {
            try
            {
                IAccessRightManager accessManager = new AccessRightManager();
                var menulist = await accessManager.GetAllAccessRights(userid);
                List<dynamic> data = Enumerable.ToList<dynamic>(menulist.Data);
                return GetAccessList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<PlanModel>> GetAllPlan()
        {
            try
            {
                ISystemManager systemManager = new SystemManager();
                var systemlist = await systemManager.GetAllPlan();
                List<dynamic> data = Enumerable.ToList<dynamic>(systemlist.Data);
                return GetPlanList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<PatientPlanSubscriptionModel>> GetAllPatientPlanSubscription()
        {
            try
            {
                ISystemManager systemManager = new SystemManager();
                var systemlist = await systemManager.GetAllPatientPlanSubscription();
                List<dynamic> data = Enumerable.ToList<dynamic>(systemlist.Data);
                return GetPatientPlanSubscriptionList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<PatientPlanSubscriptionModel>> GetAllPatientPlanSubscriptionDropdown()
        {
            try
            {
                ISystemManager systemManager = new SystemManager();
                var systemlist = await systemManager.GetAllPatientPlanSubscriptionDropdown();
                List<dynamic> data = Enumerable.ToList<dynamic>(systemlist.Data);
                return GetPatientPlanSubscriptionListDropdown(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<PatientPlanSubscriptionModel>> GetAllPatientPlan()
        {
            try
            {
                ISystemManager systemManager = new SystemManager();
                var systemlist = await systemManager.GetAllPatientPlanSubscriptionttt();
                List<dynamic> data = Enumerable.ToList<dynamic>(systemlist.Data);
                return GetPatientPlanSubscriptionList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task<List<TreatmentModel>> GetAllTreatment()
        {
            try
            {
                ITreatmentManager treatmentManager = new TreatmentManager();
                var treatmentlist = await treatmentManager.GetAllTreatment();
                List<dynamic> data = Enumerable.ToList<dynamic>(treatmentlist.Data);
                return GetTreatmentList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<DesignationModel>> GetAllDesignation()
        {
            try
            {
                IDesignationManager designationManager = new DesignationManager();
                var designationlist = await designationManager.GetAllDesignation();
                List<dynamic> data = Enumerable.ToList<dynamic>(designationlist.Data);
                return GetDesignationList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<MedicineModel>> GetAllMedicine(DateTime? FromDate, DateTime? ToDate,long? hospitalid)
        {
            try
            {
                IMedicineManager medicineManager = new MedicineManager();
                var medicinelist = await medicineManager.GetAllMedicine(FromDate, ToDate, hospitalid);
                List<dynamic> data = Enumerable.ToList<dynamic>(medicinelist.Data);
                return GetMedicineList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<PurchaseMedicineModel>> GetAllPurchaseMedicine(DateTime? FromDate, DateTime? ToDate, long? hospitalid)
        {
            try
            {
                IMedicineManager medicineManager = new MedicineManager();
                var medicinelist = await medicineManager.GetAllPurchaseMedicine(FromDate, ToDate, hospitalid);
                List<dynamic> data = Enumerable.ToList<dynamic>(medicinelist.Data);
                return GetPurchaseMedicineList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<ExpenseModel>> GetAllExpense()
        {
            try
            {
                IMedicineManager medicineManager = new MedicineManager();
                var medicinelist = await medicineManager.GetAllExpense();
                List<dynamic> data = Enumerable.ToList<dynamic>(medicinelist.Data);
                return GetExpenseList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<TreatmentOpeningBalanceModel>> GetAllTmtOB()
        {
            try
            {
                ISystemManager medicineManager = new SystemManager();
                var medicinelist = await medicineManager.GetAllTmtOpeningBalance();
                List<dynamic> data = Enumerable.ToList<dynamic>(medicinelist.Data);
                return GetTmtOBList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<MedicineOpeningBalanceModel>> GetAllMedOB()
        {
            try
            {
                ISystemManager medicineManager = new SystemManager();
                var medicinelist = await medicineManager.GetAllMedOpeningBalance();
                List<dynamic> data = Enumerable.ToList<dynamic>(medicinelist.Data);
                return GetMedOBList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<TreatmentReceiptModel>> GetAllTmtReceipt()
        {
            try
            {
                ISystemManager medicineManager = new SystemManager();
                var medicinelist = await medicineManager.GetAllTmtReceipt();
                List<dynamic> data = Enumerable.ToList<dynamic>(medicinelist.Data);
                return GetTmtReceiptList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<MedicineReceiptModel>> GetAllMedReceipt()
        {
            try
            {
                ISystemManager medicineManager = new SystemManager();
                var medicinelist = await medicineManager.GetAllMedReceipt();
                List<dynamic> data = Enumerable.ToList<dynamic>(medicinelist.Data);
                return GetMedReceiptList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<HospitalModel>> GetAllHospital()
        {
            try
            {
                IHospitalManager hospitalManager = new HospitalManager();
                var hospitallist = await hospitalManager.GetAllHospital();
                List<dynamic> data = Enumerable.ToList<dynamic>(hospitallist.Data);
                return GetHospitalList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<OPDServicesModel>> GetAllOPDServices()
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var opdserviceslist = await patientManager.GetAllOpdService();
                List<dynamic> data = Enumerable.ToList<dynamic>(opdserviceslist.Data);
                return GetOPDServiceList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<OPDPrescriptionModel>> GetAllOPDPrescription()
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var opdserviceslist = await patientManager.GetAllOpdPrescription();
                List<dynamic> data = Enumerable.ToList<dynamic>(opdserviceslist.Data);
                return GetOPDPrescriptionList(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<UserModel> GetUserList(List<dynamic> data)
        {
            try
            {
                var usrusrtype = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<UserModel>>(usrusrtype);

                if (result == null)
                {
                    return null;
                }

                var userlist = result.Select(x => new UserModel
                {
                    UserId = x.UserId,
                    FullName = x.FirstName + " " + x.LastName + "",
                    UserMobile = x.UserMobile,
                    UserEmail = x.UserEmail,
                    UserCreatedDate = x.UserCreatedDate,
                    UserStatus = x.UserStatus,
                    DateOfBirth = x.DateOfBirth,
                    DateofJoin = x.DateofJoin,
                    UserAddress = x.UserAddress,
                    RoleId = x.RoleId,
                    RoleName = x.RoleName,
                    UserGender = x.UserGender,
                    HospitalId= x.HospitalId,
                    DesignationId= x.DesignationId
                }).OrderBy(x => x.UserCreatedDate).ToList();

                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PatientModel> GetPatientList(List<dynamic> data)
        {
            try

            {
                var patients = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<PatientModel>>(patients);
                if (result == null)
                {
                    return null;
                }

                var patientlist = result.Select(x => new PatientModel
                {
                    PatientIdNumber = x.PatientIdNumber,
                    PatientName = x.PatientName,
                    PatientAddress = x.PatientAddress,
                    PatientMobileNumber = x.PatientMobileNumber,
                    PatientWhatsappNumber = x.PatientWhatsappNumber,
                    PatientId = x.PatientId,
                    PatientHospitalId = x.PatientHospitalId
                }).OrderByDescending(x => x.PatientRegisterNumber ).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<NotesModel> GetNotesList(List<dynamic> data)
        {
            try
            {
                var notes = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<NotesModel>>(notes);
                if (result == null)
                {
                    return null;
                }
                var noteslist = result.Select(x => new NotesModel
                {
                    NotesId = x.NotesId,
                    Description = x.Description,
                    NotesCreatedBy = x.NotesCreatedBy,
                    NotesCreatedDate = x.NotesCreatedDate,
                    NotesCreatedByName = x.NotesCreatedByName,
                }).OrderBy(x => x.NotesCreatedDate).ToList();
                return noteslist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PatientDocumentModel> GetDocuList(List<dynamic> data)
        {
            try

            {
                var doc = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<PatientDocumentModel>>(doc);
                if (result == null)
                {
                    return null;
                }

                var patientlist = result.Select(x => new PatientDocumentModel
                {
                    PatientIdNumber = x.PatientIdNumber,
                    DocumentId = x.DocumentId,
                    FileName = x.FileName,
                    FolderName = x.FolderName,
                    DocumentCreatedBy = x.DocumentCreatedBy,
                    DocumentCreatedDate = x.DocumentCreatedDate,
                    PatientId = x.PatientId
                }).OrderBy(x => x.DocumentCreatedDate).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<OPDConsoltationModel> GetOPDConsultList(List<dynamic> data)
        {
            try

            {
                var opds = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<OPDConsoltationModel>>(opds);

                if (result == null)
                {
                    return null;
                }

                var patientlist = result.Select(x => new OPDConsoltationModel
                {
                    PatientIDNumber = x.PatientIDNumber,
                    PatientName = x.PatientName,
                    MobileNo = x.MobileNo,
                    DoctorName = x.DoctorName,
                    OPDConsoltationCreatedBy = x.OPDConsoltationCreatedBy,
                    PatientAge = x.PatientAge,
                    PatientGender = x.PatientGender,
                    PatientBP = x.PatientBP,
                    PatientWeight = x.PatientWeight,
                    PatientHeight = x.PatientHeight,
                    NatureOfIllness = x.NatureOfIllness,
                    PatientPulse = x.PatientPulse,
                    PatientId = x.PatientId,
                    OPDConsultationId = x.OPDConsultationId,
                    PatientLMP = x.PatientLMP,
                    PatientSpO2 = x.PatientSpO2,
                    PatientVisitType = x.PatientVisitType,
                    PatientTemp = x.PatientTemp,
                    DoctorsNote = x.DoctorsNote,
                    TreatmentPaymentMode = x.TreatmentPaymentMode,
                    TotalCharges = x.TotalCharges,
                    TreatmentTotalServicesCharges = x.TreatmentTotalServicesCharges,
                    NetCharges = x.NetCharges,
                    PlanId = x.PlanId
                }).OrderByDescending(x => x.OPDConsultationCreatedDate).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PatientModel> GePatientList(List<dynamic> data)
        {
            try
            {
                var opds = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<PatientModel>>(opds);
                if (result == null)
                {
                    return null;
                }
                var patientlist = result.Select(x => new PatientModel
                {
                    PatientCreatedDate = x.PatientCreatedDate,
                    PatientIdNumber = x.PatientIdNumber,
                    PatientDoctorId = x.PatientDoctorId,
                    PatientDoctorName = x.PatientDoctorName,
                    PatientName = x.PatientName,
                    PatientMobileNumber = x.PatientMobileNumber,
                    PatientPrimaryComplaints = x.PatientPrimaryComplaints,
                    HospitalName = x.HospitalName,
                    PatientDate = x.PatientDate
                }).OrderBy(x => x.PatientCreatedDate).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PlanModel> GetPlanList(List<dynamic> data)
        {
            try
            {
                var planlist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<PlanModel>>(planlist);

                if (result == null)
                {
                    return null;
                }

                var allplan = result.Select(x => new PlanModel
                {
                    TreatmentName = x.TreatmentName,
                    NoofTreatment = x.NoofTreatment,
                    Amount = x.Amount,
                    Offers = x.Offers,
                    PackageCode = x.PackageCode,
                    isfree = x.isfree,
                    PlanCreatedBy = x.PlanCreatedBy,
                    PlanId = x.PlanId,
                    PlanExpiryDate = x.PlanExpiryDate,
                    PlanStatus = x.PlanStatus
                }).OrderBy(x => x.PlanCreatedDate).ToList();

                return allplan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PatientPlanSubscriptionModel> GetPatientPlanSubscriptionList(List<dynamic> data)
        {
            try
            {
                var planlist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<PatientPlanSubscriptionModel>>(planlist);

                if (result == null)
                {
                    return null;
                }

                var allplan = result.Select(x => new PatientPlanSubscriptionModel
                {
                    PatientIDNumber = x.PatientIDNumber,
                    BalanceServiceAmount = x.BalanceServiceAmount == null ? 0 : x.BalanceServiceAmount,
                    BalanceServiceCount = x.BalanceServiceCount == null ? 0 : x.BalanceServiceCount,
                    Mode= x.Mode,
                    PatientId= x.PatientId,
                    PatientName = x.PatientName,
                    PatientPlanSubscriptionCreatedBy= x.PatientPlanSubscriptionCreatedBy,
                    PatientPlanSubscriptionCreatedDate= x.PatientPlanSubscriptionCreatedDate,
                    PatientPlanSubscriptionStatus= x.PatientPlanSubscriptionStatus,
                    TotalServiceAmount= x.TotalServiceAmount,
                    TotalServiceCount= x.TotalServiceCount,
                    PlanId= x.PlanId,
                    PatientPlanSubscriptionDate= x.PatientPlanSubscriptionDate,
                    PatientPlanSubscriptionHospitalId= x.PatientPlanSubscriptionHospitalId,
                    PatientPlanSubscriptionId= x.PatientPlanSubscriptionId,
                    PatientPlanSubscriptionModifiedBy= x.PatientPlanSubscriptionModifiedBy,
                    PatientPlanSubscriptionModifiedDate= x.PatientPlanSubscriptionModifiedDate,
                    PlanName= x.PlanName
                }).OrderBy(x => x.PatientPlanSubscriptionCreatedDate).ToList();

                return allplan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PatientPlanSubscriptionModel> GetPatientPlanSubscriptionListDropdown(List<dynamic> data)
        {
            try
            {
                var planlist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<PatientPlanSubscriptionModel>>(planlist);

                if (result == null)
                {
                    return null;
                }

                var allplan = result.Select(x => new PatientPlanSubscriptionModel
                {
                    PatientPlanSubscriptionId = x.PatientPlanSubscriptionId,
                    PatientId = x.PatientId,
                    PlanId = x.PlanId,
                    PlanName = x.PlanName
                }).OrderBy(x => x.PatientPlanSubscriptionCreatedDate).ToList();

                return allplan;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SystemViewModel> GetSystemList(List<dynamic> data)
        {
            try
            {
                var systemlist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<SystemViewModel>>(systemlist);

                if (result == null)
                {
                    return null;
                }

                var userlist = result.Select(x => new SystemViewModel
                {
                    SystemName = x.SystemName,
                    SystemIp = x.SystemIp,
                    SystemStatus = x.SystemStatus,
                    SystemCreatedBy = x.SystemCreatedBy,
                    SystemId = x.SystemId,
                    SystemCreatedDate = x.SystemCreatedDate,
                    SystemModel = x.SystemModel
                }).OrderBy(x => x.SystemCreatedDate).ToList();

                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MenuModel> GetMenuList(List<dynamic> data)
        {
            try
            {
                var systemlist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<MenuModel>>(systemlist);

                if (result == null)
                {
                    return null;
                }

                var userlist = result.Select(x => new MenuModel
                {
                    MenuId = x.MenuId,
                    MenuName = x.MenuName,
                    MenuCreatedBy = x.MenuCreatedBy,
                    MenuCreatedDate = x.MenuCreatedDate
                }).ToList();

                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<AccessRightsModel> GetAccessList(List<dynamic> data)
        {
            try
            {
                var accesslist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<AccessRightsModel>>(accesslist);

                if (result == null)
                {
                    return null;
                }

                var userlist = result.Select(x => new AccessRightsModel
                {
                   DesignationId = x.DesignationId,
                   AccessRightsId = x.AccessRightsId,
                   MenuId = x.MenuId,
                   IsCreate = x.IsCreate,
                   IsDelete = x.IsDelete,
                   IsView = x.IsView,
                   IsEdit = x.IsEdit
                }).ToList();

                return userlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TreatmentModel> GetTreatmentList(List<dynamic> data)
        {
            try
            {
                var treatments = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<Treatment>>(treatments);

                if (result == null)
                {
                    return null;
                }

                var treatmentlist = result.Select(x => new TreatmentModel
                {
                    TreatmentId = x.TreatmentId,
                    TreatmentName = x.TreatmentName,
                    TreatmentDuration = x.TreatmentDuration,
                    TreatmentDescription = x.TreatmentDescription,
                    TreatmentMedicineNeeded = x.TreatmentMedicineNeeded,
                    TreatmentCharges = x.TreatmentCharges,
                    TreatmentStatus = x.TreatmentStatus,
                    TreatmentCreatedDate = x.TreatmentCreatedDate
                }).OrderBy(x => x.TreatmentCreatedDate).ToList();

                return treatmentlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DesignationModel> GetDesignationList(List<dynamic> data)
        {
            try
            {
                var designationlist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<DesignationModel>>(designationlist);

                if (result == null)
                {
                    return null;
                }

                var designationviewlist = result.Select(x => new DesignationModel
                { 
                    DesignationId = x.DesignationId,
                    DesignationName = x.DesignationName,
                    DesignationStatus= x.DesignationStatus,
                    DesignationCreatedBy = x.DesignationCreatedBy,
                    DesignationCreatedDate = x.DesignationCreatedDate
                }).OrderBy(x => x.DesignationCreatedDate).ToList();

                return designationviewlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MedicineModel> GetMedicineList(List<dynamic> data)
        {
            try
            {
                var medicinelist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<MedicineModel>>(medicinelist);

                if (result == null)
                {
                    return null;
                }

                var medicinelistviewlist = result.Select(x => new MedicineModel
                {
                    MedicineName= x.MedicineName,
                    MedicineDescription= x.MedicineDescription,
                    MedicinePurchaseRate=x.MedicinePurchaseRate,
                    MedicineSalesRate= x.MedicineSalesRate,
                    MedicineManufacturer= x.MedicineManufacturer,
                    MedicineExpiryDate= x.MedicineExpiryDate,
                    MedicineCurrentStack = x.MedicineCurrentStack,
                    MedicinePurchaseStack = x.MedicinePurchaseStack,
                    MedicineStatus = x.MedicineStatus,
                    MedicineId= x.MedicineId
                }).OrderByDescending(x => x.MedicineCreatedBy).ToList();

                return medicinelistviewlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PurchaseMedicineModel> GetPurchaseMedicineList(List<dynamic> data)
        {
            try
            {
                var medicinelist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<PurchaseMedicineModel>>(medicinelist);

                if (result == null)
                {
                    return null;
                }

                var medicinelistviewlist = result.Select(x => new PurchaseMedicineModel
                {
                    PurchaseMedicineId = x.PurchaseMedicineId,
                    PurchaseDate = x.PurchaseDate,
                    MedicineName = x.MedicineName,
                    MedicineId = x.MedicineId,
                    MedicineExpiryDate = x.MedicineExpiryDate,
                    MedicineCurrentStock = x.MedicineCurrentStock,
                    MedicinePurchaseQuanity = x.MedicinePurchaseQuanity,
                    MedicineManufacturer = x.MedicineManufacturer,
                    PurchaseCost = x.PurchaseCost,
                    PurchaseMedicineCreatedBy = x.PurchaseMedicineCreatedBy,
                    PurchaseMedicineCreatedDate = x.PurchaseMedicineCreatedDate,
                    PurchaseMedicineModifiedBy = x.PurchaseMedicineModifiedBy,
                    PurchaseMedicineModifiedDate = x.PurchaseMedicineModifiedDate,
                    HospitalId = x.HospitalId,
                    MedicineRate  = x.MedicineRate,
                    
                }).OrderBy(x => x.PurchaseMedicineCreatedDate).ToList();

                return medicinelistviewlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<HospitalModel> GetHospitalList(List<dynamic> data)
        {
            try
            {
                var hospitallist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<HospitalModel>>(hospitallist);

                if (result == null)
                {
                    return null;
                }

                var hospitallistviewlist = result.Select(x => new HospitalModel
                {
                    HospitalCode= x.HospitalCode,
                    HospitalName=x.HospitalName,
                    HospitalAddress=x.HospitalAddress,
                    HospitalContactPersonEmail= x.HospitalContactPersonEmail,
                    HospitalContactPersonName= x.HospitalContactPersonName,
                    HospitalCreatedDate = x.HospitalCreatedDate,
                    HospitalStatus= x.HospitalStatus,
                    HospitalEmail= x.HospitalEmail,
                    HospitalMobilNumber= x.HospitalMobilNumber,
                    HospitalId=x.HospitalId,
                }).OrderBy(x => x.HospitalCreatedDate).ToList();

                return hospitallistviewlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<OPDServicesModel> GetOPDServiceList(List<dynamic> data)
        {
            try
            {
                var opdservicelist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<OPDServicesModel>>(opdservicelist);

                if (result == null)
                {
                    return null;
                }

                var opdserviceviewlist = result.Select(x => new OPDServicesModel
                {
                    ServiceName = x.ServiceName,
                    Count =x.Count,
                    ServiceCharge = x.ServiceCharge,
                    OPDConsoltationId = x.OPDConsoltationId,
                    OPDServicesId = x.OPDServicesId,
                    OPDServicesCreatedBy = x.OPDServicesCreatedBy,
                    OPDServicesCreatedDate = x.OPDServicesCreatedDate,
                    PatientId = x.PatientId,
                    Remarks = x.Remarks,
                    HospitalId= x.HospitalId
                }).OrderBy(x => x.OPDServicesCreatedBy).ToList();

                return opdserviceviewlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<OPDPrescriptionModel> GetOPDPrescriptionList(List<dynamic> data)
        {
            try
            {
                var opdservicelist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<OPDPrescriptionModel>>(opdservicelist);

                if (result == null)
                {
                    return null;
                }

                var opdserviceviewlist = result.Select(x => new OPDPrescriptionModel
                {
                    OPDConsultationId = x.OPDConsultationId,
                    OPDPrescriptionCreatedBy = x.OPDPrescriptionCreatedBy,
                    OPDPrescriptionCreatedDate = x.OPDPrescriptionCreatedDate,
                    OPDPrescriptionId = x.OPDPrescriptionId,
                    OPDPrescriptionModifiedDate = x.OPDPrescriptionModifiedDate,
                    OPDPrescriptionModiifedBy = x.OPDPrescriptionModiifedBy,
                    //MedicineDosage = x.MedicineDosage,
                    //MedicineDuration = x.MedicineDuration,
                    MedicineId = x.MedicineId,
                    MedicineInstructions = x.MedicineInstructions,
                    MedicineName = x.MedicineName,
                    MedicineQuantity = x.MedicineQuantity,
                    //MedicineSpecification = x.MedicineSpecification,
                    MedicineAmount = x.MedicineAmount,
                    PatientId = x.PatientId,
                    HospitalId= x.HospitalId,
                    MedicineRate =x.MedicineRate
                }).OrderBy(x => x.OPDPrescriptionCreatedDate).ToList();

                return opdserviceviewlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ExpenseModel> GetExpenseList(List<dynamic> data)
        {
            try
            {
                var expenselist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<ExpenseModel>>(expenselist);

                if (result == null)
                {
                    return null;
                }

                var getexpenselist = result.Select(x => new ExpenseModel
                {
                    Description = x.Description,
                    StoreName = x.StoreName,
                    BillAmount = x.BillAmount,
                    ApprovedFrom = x.ApprovedFrom,
                    BillCopyFileName = x.BillCopyFileName,
                    ExpenseCreatedDate = x.ExpenseCreatedDate,
                    ExpenseId = x.ExpenseId,
                    ExpenseStatus= x.ExpenseStatus,
                    ExpenseDate = x.ExpenseDate,
                    IsCompanyExpense = x.IsCompanyExpense
                    
                }).OrderBy(x => x.ExpenseCreatedDate).ToList();

                return getexpenselist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TreatmentOpeningBalanceModel> GetTmtOBList(List<dynamic> data)
        {
            try
            {
                var expenselist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<TreatmentOpeningBalanceModel>>(expenselist);

                if (result == null)
                {
                    return null;
                }

                var getexpenselist = result.Select(x => new TreatmentOpeningBalanceModel
                {
                    OpeningBalanceId = x.OpeningBalanceId,
                    Date = x.Date,
                    Amount = x.Amount,
                    HospitalId = x.HospitalId,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy

                }).OrderBy(x => x.CreatedDate).ToList();

                return getexpenselist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MedicineOpeningBalanceModel> GetMedOBList(List<dynamic> data)
        {
            try
            {
                var expenselist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<MedicineOpeningBalanceModel>>(expenselist);

                if (result == null)
                {
                    return null;
                }

                var getexpenselist = result.Select(x => new MedicineOpeningBalanceModel
                {
                    OpeningBalanceId = x.OpeningBalanceId,
                    Date = x.Date,
                    Amount = x.Amount,
                    HospitalId = x.HospitalId,
                    CreatedDate = x.CreatedDate,
                    CreatedBy = x.CreatedBy

                }).OrderBy(x => x.CreatedDate).ToList();

                return getexpenselist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TreatmentReceiptModel> GetTmtReceiptList(List<dynamic> data)
        {
            try
            {
                var expenselist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<TreatmentReceiptModel>>(expenselist);

                if (result == null)
                {
                    return null;
                }

                var getexpenselist = result.Select(x => new TreatmentReceiptModel
                {
                    ReceiptAmount = x.ReceiptAmount,
                    ReceiptCreatedBy = x.ReceiptCreatedBy,
                    ReceiptCreatedDate = x.ReceiptCreatedDate,
                    ReceiptDate = x.ReceiptDate,
                    ReceiptHospitalId = x.ReceiptHospitalId,
                    TreatmentReceiptId = x.TreatmentReceiptId
                }).OrderBy(x => x.ReceiptCreatedDate).ToList();

                return getexpenselist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MedicineReceiptModel> GetMedReceiptList(List<dynamic> data)
        {
            try
            {
                var expenselist = JsonConvert.SerializeObject(data);
                var result = JsonConvert.DeserializeObject<List<MedicineReceiptModel>>(expenselist);

                if (result == null)
                {
                    return null;
                }

                var getexpenselist = result.Select(x => new MedicineReceiptModel
                {
                    ReceiptAmount = x.ReceiptAmount,
                    ReceiptCreatedBy = x.ReceiptCreatedBy,
                    ReceiptCreatedDate = x.ReceiptCreatedDate,
                    ReceiptDate = x.ReceiptDate,
                    ReceiptHospitalId = x.ReceiptHospitalId,
                    MedicineReceiptId = x.MedicineReceiptId
                }).OrderBy(x => x.ReceiptCreatedDate).ToList();

                return getexpenselist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}