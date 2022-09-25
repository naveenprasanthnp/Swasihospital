using SwasiHealthCare.BusinessManager;
using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.IBusinessManager;
using SwasiHealthCare.Model;
using SwasiHealthCare.Repository;
using SwasiHealthCare.Service.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SwasiHealthCare.Service.FilterConfig;

namespace SwasiHealthCare.Service.Controllers
{
    public class PatientController : Controller
    {
        [HttpGet]
        [Gzip]
        public async Task<ActionResult> Index()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
            try
            {
                IPatientManager patientmanager = new PatientManager();
                var patientlist = await ViewHelper.GetAllPatients();
                foreach (var item in patientlist)
                {
                    if (item.FilePath != null && item.FilePath != string.Empty)
                    {
                        var documentlist = await ViewHelper.GetAllDocumentById(item.PatientId ?? 0);
                        item.PatientDocumentList = documentlist;
                    }
                }
                patientlist = patientlist.OrderByDescending(x => x.PatientRegisterNumber)
                    .Where(x=>x.PatientHospitalId == hospitalid).ToList();
                return View(patientlist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewPatient()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                long userid = Convert.ToInt64(Session["UserId"]);
                IUserManager userManager = new UserManager();
                IHospitalManager hospitalManager = new HospitalManager();
                var userdetails = (await userManager.GetEditUser(userid));
                var hospitaldetails = await hospitalManager.GetEditHospital(userdetails.HospitalId ?? 0);
                List<PlanModel> planlist = await ViewHelper.GetAllPlan();
                List<PatientModel> paientlist = (await ViewHelper.GetAllPatients()).Where(x=>x.PatientHospitalId == userdetails.HospitalId).ToList();
                ViewBag.planlist = planlist;
                List<Hospital> hospital = (await userManager.GetHospitals()).ToList();
                ViewBag.hospitallist = hospital;
                IPatientManager patientmanager = new PatientManager();
               
                List<UserModel> doctorlist = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId == 3).ToList();
                if (hospitaldetails.PatientIdStartWith == string.Empty || hospitaldetails.PatientIdStartWith == null)
                {
                    ViewBag.doctorlist = doctorlist;
                    ViewBag.planlist = planlist;
                    ViewBag.hospitallist = hospital;
                    ViewBag.PatientIdStartWith = "Please create patient id start with settings hospital menu!.";
                    return View();
                }
                ViewBag.doctorlist = doctorlist;
                var patientcount = patientmanager.GetPatientsCount(userdetails.HospitalId ?? 0);
                var opidnumber = patientcount + 1;
                return View(new Model.PatientModel { PatientIdNumber = hospitaldetails.PatientIdStartWith + "-" + opidnumber ,PatientRegisterNumber = opidnumber });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Gzip]
        public async Task<ActionResult> NewPatient(PatientModel patientModel)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                long userid = Convert.ToInt64(Session["UserId"]);
                IUserManager userManager = new UserManager();
                var usedetails = userManager.GetLoginUserDetails(userid);
                var doctotdetails = (await userManager.GetEditUser(patientModel.PatientDoctorId ?? 0));
                IPatientManager patientManager = new PatientManager();
                patientModel.PatientCreatedBy = Convert.ToInt64(Session["UserId"]);
                patientModel.PatientStatus = true;
                patientModel.PatientHospitalId = hospitalid;
                if (patientModel.PatientId == 0 || patientModel.PatientId == null)
                {
                    patientModel.Mode = "A";
                    patientModel.PatientDoctorName = doctotdetails?.FullName;
                }
                else
                {
                    patientModel.PatientDoctorName = doctotdetails?.FullName;
                    patientModel.Mode = "E";
                }
                var result = await patientManager.SavePatient(patientModel);
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var patientidnumber = await patientManager.GetEditPatient(patientModel.PatientId ?? 0);
                        HttpPostedFileBase file = Request.Files[i];
                        int fileSize = file.ContentLength;
                        if (fileSize > 0)
                        {
                            FileInfo fileinfo = new FileInfo(file.FileName);
                            var folderName = patientidnumber.PatientIdNumber;
                            byte[] fileByteArray = new byte[fileSize];
                            file.InputStream.Read(fileByteArray, 0, fileSize);
                            string fileLocation = Path.Combine(Server.MapPath("~/Content/Files/PatientReports/" + folderName), fileinfo.Name);
                            if (!Directory.Exists(Path.Combine(Server.MapPath("~/Content/Files/PatientReports/" + folderName))))
                                Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Content/Files/PatientReports/" + folderName)));
                            file.SaveAs(fileLocation);

                            patientModel.FilePath = "File";
                            patientModel.Mode = "E";
                            await patientManager.SavePatient(patientModel);

                            var patientdocument = new PatientDocument 
                            {
                                PatientId = patientidnumber.PatientId,
                                FileName = fileinfo.Name,
                                FolderName = folderName,
                                PatientIdNumber = patientidnumber.PatientIdNumber,
                                DocumentCreatedBy= patientModel.PatientCreatedBy,
                                DocumentCreatedDate = DateTime.Now
                            };
                            await patientManager.SavePatientDocumnet(patientdocument);
                        }
                    }
                }
                //return Json(result);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> PatientStatus(long patientid, bool status)
        {
            try
            {
                IPatientManager patientManager = new PatientManager();
                var result = await patientManager.UpdatePatientStatus(patientid, status, Convert.ToInt64(Session["UserId"]));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditPatient(long patientid)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                IUserManager userManager = new UserManager();
                List<PlanModel> planlist = await ViewHelper.GetAllPlan();
                List<PatientModel> paientlist = await ViewHelper.GetAllPatients();
                List<Hospital> hospital = (await userManager.GetHospitals()).ToList();
                ViewBag.planlist = planlist;
                ViewBag.hospitallist = hospital;
                IPatientManager patientMagager = new PatientManager();
                var users = (await patientMagager.GetEditPatient(patientid));
                List<UserModel> doctorlist = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId == 3).ToList();
                ViewBag.doctorlist = doctorlist;
                return View(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditPatient(Model.PatientModel userModel)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                IPatientManager userManager = new PatientManager();
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var patientidnumber = await userManager.GetEditPatient(userModel.PatientId ?? 0);
                        HttpPostedFileBase file = Request.Files[i];
                        int fileSize = file.ContentLength;
                        if (fileSize > 0)
                        {
                            FileInfo fileinfo = new FileInfo(file.FileName);
                            var folderName = patientidnumber.PatientIdNumber; ;
                            byte[] fileByteArray = new byte[fileSize];
                            file.InputStream.Read(fileByteArray, 0, fileSize);
                            string fileLocation = Path.Combine(Server.MapPath("~/Content/Files/PatientReports/" + folderName), fileinfo.Name);
                            if (!Directory.Exists(Path.Combine(Server.MapPath("~/Content/Files/PatientReports/" + folderName))))
                                Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Content/Files/PatientReports/" + folderName)));
                            file.SaveAs(fileLocation);
                            userModel.FilePath = folderName;

                            var patientdocument = new PatientDocument
                            {
                                PatientId = patientidnumber.PatientId,
                                FileName = fileinfo.Name,
                                FolderName = folderName,
                                PatientIdNumber = patientidnumber.PatientIdNumber,
                                DocumentCreatedBy = userModel.PatientCreatedBy,
                                DocumentCreatedDate = DateTime.Now
                            };
                            await new Repository<PatientDocument>().Insert(patientdocument);
                        }
                    }
                }
              
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                userModel.PatientModiifedBy = Convert.ToInt64(Session["UserId"]);
                userModel.Mode = "E";
                userModel.Flag = "0";
                userModel.PatientHospitalId = hospitalid;
                await userManager.SavePatient(userModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }     

        [HttpPost]
        public async Task<ActionResult> DeletePatient(long patientid)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                IPatientManager patientManager = new PatientManager();
                var result = await patientManager.DeletePatient(patientid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> OPDIndex()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            DateTime? FromDate = null;
            DateTime? ToDate = null;
            long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
            try
            {
                var opdlist = (await ViewHelper.GetAllOPDConsoltation(FromDate, ToDate)).Where(x => x.HospitalId ==
                hospitalid).ToList();
                opdlist = opdlist.OrderByDescending(x => x.IDNumber).ToList();
                return View(opdlist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        //[HttpGet]
        //public async Task<ActionResult> NewOPD(long? opdconsutationid, string patientidnumber)
        //{
        //    return RedirectToAction("Login", "Home");
        //}
        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewOPD(long? opdconsutationid, string patientidnumber)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                IPatientManager patientManager = new PatientManager();
                IUserManager userManager = new UserManager();
                ITreatmentManager treatmentManager = new TreatmentManager();
                List<UserModel> doctorlist = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId == 3 && x.HospitalId == hospitalid).ToList();
                List<UserModel> therapistlist = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId != 1 && x.RoleId != 2 && x.HospitalId == hospitalid).ToList();
                List<UserModel> therapistlist1 = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId != 1 && x.RoleId != 2 && x.HospitalId == hospitalid).ToList();
                List<PatientModel> patientlist = (await ViewHelper.GetAllPatients()).Where(x => x.PatientHospitalId == hospitalid).ToList();
                List<TreatmentModel> treatmentlist = await ViewHelper.GetAllTreatment();
                List<MedicineModel> druglist = await ViewHelper.GetAllMedicine(null, null, hospitalid);
                //List<PatientPlanSubscriptionModel> planlist = await ViewHelper.GetAllPatientPlanSubscription();
                List<PlanModel> planlist = await ViewHelper.GetAllPlan();
                if(patientidnumber==null)
                {
                    patientidnumber = "";
                }

                var patientid = patientlist.Where(x => x.PatientIdNumber.Equals(patientidnumber + "")).ToList();
                long PID = 0;
                if(patientid.Count() != 0)
                {
                    PID = patientid.FirstOrDefault().PatientId ?? 0;
                }
                    
                List<PatientPlanSubscriptionModel> patientpackagelist = (await ViewHelper.GetAllPatientPlanSubscriptionDropdown()).Where(x => x.PatientId == PID).ToList();
               
                //List<PatientPlanSubscriptionModel> patientpackagelist = (await ViewHelper.GetAllPatientPlanSubscription()).Where(x => x.PatientId == 5).ToList();
                var opdservicelist = (await ViewHelper.GetAllOPDServices()).Where(x => x.OPDConsoltationId == opdconsutationid && x.HospitalId == hospitalid).ToList();
                var opdpreslist = (await ViewHelper.GetAllOPDPrescription()).Where(x => x.OPDConsultationId == opdconsutationid && x.HospitalId == hospitalid).ToList();
                ViewBag.doctorlist = doctorlist;
                ViewBag.patientlist = patientlist;
                ViewBag.treatmentlist = treatmentlist;
                ViewBag.druglist = druglist;
                ViewBag.therapistlist = therapistlist;
                var opdnumber = new OPDConsoltation();
                var opdconsolutation = new OPDConsoltation();
                var result = new Model.OPDConsoltationModel();
                result.PackageCount = patientpackagelist.Count();
                if (opdconsutationid != null)
                {
                    opdconsolutation = (await new Repository<OPDConsoltation>().GetById(opdconsutationid ?? 0));
                    result.ConsultationIDNumber = opdconsolutation?.ConsultationIDNumber;
                }
                else
                {
                    opdnumber = (await new Repository<OPDConsoltation>().GetAll()).Where(x => x.HospitalId == hospitalid)
                          .OrderByDescending(x => x.IDNumber).FirstOrDefault();
                    //var opdnumber11 = (await new Repository<OPDConsoltation>().GetAll()).Where(x => x.HospitalId == hospitalid).ToList();
                    result.ConsultationIDNumber = opdnumber == null ? "OPD-1" : "OPD-" + (opdnumber?.IDNumber + 1);
                }
                //-------------------------------------------------------
                result.HospitalId = hospitalid;
                result.ConsultationDate = opdconsolutation.ConsultationDate;
                result.DoctorName = opdconsolutation?.DoctorName;
                result.OPDConsultationId = opdconsolutation?.OPDConsultationId ?? 0;
                result.PatientGender = opdconsolutation?.Gender;
                result.ConsultationDate = opdconsolutation.ConsultationDate;
                result.PatientIDNumber = opdconsolutation?.PatientIDNumber;
                result.PatientAge = opdconsolutation?.Age;
                result.PatientId = opdconsolutation?.PatientId;
                result.PatientName = opdconsolutation?.PatientName;
                result.DoctorName = opdconsolutation?.DoctorName;
                result.MobileNo = opdconsolutation?.MobileNo;
                result.PatientHeight = opdconsolutation?.Height;
                result.PatientWeight = opdconsolutation?.Weight;
                result.PatientTemp = opdconsolutation?.Temp;
                result.PatientBP = opdconsolutation?.BP;
                result.PatientLMP = opdconsolutation?.LMP;
                result.PatientPulse = opdconsolutation?.Pulse;
                result.PatientSpO2 = opdconsolutation?.SpO2;
                result.PatientVisitType = opdconsolutation?.VisitType;
                result.TreatmentSplDiscount = opdconsolutation?.SplDiscount ?? 0;
                result.NetCharges = opdconsolutation?.NetCharges ?? 0;
                result.TreatmentPaymentMode = opdconsolutation?.PaymentMode;
                result.DoctorsNote = opdconsolutation?.DoctorsNote;
                result.NatureOfIllness = opdconsolutation?.DoctorsNote;
                //-------------------------------------------------------
                result.opdserviceslist = opdservicelist;
                result.opdprescriptionlist = opdpreslist;
                result.IDNumber = opdnumber == null ? 1 : opdnumber.IDNumber + 1;
                result.DoctorId = opdconsolutation.DoctorId;
                result.PlanId = opdconsolutation.PlanId;
                Session["opdconsoltationid"] = opdconsolutation?.OPDConsultationId;
                ViewBag.planlist = planlist;
                if (patientpackagelist == null)
                {
                    ViewBag.patientpackagelist = new List<PatientPlanSubscriptionModel>();
                }
                else
                {
                    ViewBag.patientpackagelist = patientpackagelist;
                }
                return View(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost] 
        public async Task<ActionResult> NewOPD(OPDConsoltationModel opdconsoltationmodel)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                List<PlanModel> planlist = await ViewHelper.GetAllPlan();
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                IPatientManager patientManager = new PatientManager();
                opdconsoltationmodel.OPDConsoltationCreatedBy = Convert.ToInt64(Session["UserId"]);
                opdconsoltationmodel.Mode = "A";
                opdconsoltationmodel.HospitalId = hospitalid;
                var result = await patientManager.SaveOPDConsoltation(opdconsoltationmodel);
                Session["opdconsoltationid"] = result.opdconsoltationid;
                ViewBag.planlist = planlist;
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditPlan(long planid)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                ISystemManager userManager = new SystemManager();
                var system = (await userManager.GetEditPlan(planid));
                return Json(system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost] 
        public async Task<ActionResult> UpdateTotalCharge(OPDConsoltationModel opdconsoltationmodel)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                IPatientManager patientManager = new PatientManager();
                var result = await patientManager.UpdateOPDTotalAmount(opdconsoltationmodel);
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> GetPackageDetails(long patientpackageid)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                ISystemManager systemManager = new SystemManager();
                var result = (await systemManager.GetEditPatientPlanSubscription(patientpackageid));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditOPD(long UserId)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                IUserManager userManager = new UserManager();
                List<Roles> role = (await userManager.GetRoles()).ToList();
                ViewBag.roletypes = role;

                var users = (await userManager.GetEditUser(UserId));
                return View(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditOPD(Model.UserModel userModel)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                long rolid = Convert.ToInt64(Session["RoleId"]);
                IUserManager userManager = new UserManager();
                userModel.UserModifiedBy = Convert.ToInt64(Session["UserId"]);
                userModel.Mode = "E";
                await userManager.SaveUser(userModel,rolid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        [Gzip]
        public async Task<ActionResult> GetPatientDetailById(string patientidnumber)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                IPatientManager patientManager = new PatientManager();
                PatientModel patientdetails = (await ViewHelper.GetAllPatients()).Where(x => x.PatientIdNumber == patientidnumber).FirstOrDefault();
                var result = new ResponseModel { Data = patientdetails ,Status = true};
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Gzip]
        public async Task<ActionResult> GetTreatmentById(long servicename)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                var treatmentdetails = (await new Repository<Treatment>().GetById(servicename));
                var result = new ResponseModel { Data = treatmentdetails, Status = true };
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveOPDService(OPDServicesModel opdservicemodel)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                IPatientManager patientManager = new PatientManager();
                opdservicemodel.OPDServicesCreatedBy = Convert.ToInt64(Session["UserId"]);
                opdservicemodel.Mode = "A";
                opdservicemodel.HospitalId = hospitalid;
                //opdservicemodel.OPDConsoltationId = Convert.ToInt64(Session["opdconsoltationid"]);
                 var result = await patientManager.SaveOPDService(opdservicemodel);
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveOPDPrescription(OPDPrescriptionModel opdprescriptionmodel)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                IPatientManager patientManager = new PatientManager();
                opdprescriptionmodel.OPDPrescriptionCreatedBy = Convert.ToInt64(Session["UserId"]);
                opdprescriptionmodel.HospitalId = hospitalid;
                if (opdprescriptionmodel.OPDPrescriptionId != null && opdprescriptionmodel.OPDPrescriptionId != 0)
                {
                    opdprescriptionmodel.Mode = "E";
                }
                else
                {
                    opdprescriptionmodel.Mode = "A";
                }
                opdprescriptionmodel.OPDConsultationId = Convert.ToInt64(Session["opdconsoltationid"]);
                var result = await patientManager.SaveOPDPrescription(opdprescriptionmodel);
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOPDService(long opdserviceid)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                IPatientManager patientManager = new PatientManager();
                var result = await patientManager.DeleteOPDService(opdserviceid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOPDPrescription(long opdprescriptionid)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                IPatientManager patientManager = new PatientManager();
                var result = await patientManager.DeleteOPDPrescription(opdprescriptionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditOPDService(long opdserviceid)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                IPatientManager patientManager = new PatientManager();
                var result = await patientManager.EditOPDService(opdserviceid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditOPDPrescription(long opdprescriptionid)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                IPatientManager patientManager = new PatientManager();
                var result = await patientManager.EditOPDPrescription(opdprescriptionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FileStreamResult DownloadPatient(long documentid)
        {
            IPatientManager patientManager = new PatientManager();
            var docuname =  patientManager.GetDocument(documentid);
            var filename = System.IO.Path.GetFileName(Server.MapPath("~/Content/Files/PatientReports/" + docuname.FolderName + "/" + docuname.FileName));
            var mimeType = "application/octet-stream";
            var stream = System.IO.File.OpenRead(Server.MapPath("~/Content/Files/PatientReports/" + docuname.FolderName + "/" + docuname.FileName));
            return new FileStreamResult(stream, mimeType) { FileDownloadName = filename };
        }

        [HttpGet]
        public async Task<ActionResult> PrintOPD(long? opdconsutationid)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                IPatientManager patientManager = new PatientManager();
                IUserManager userManager = new UserManager();
                ITreatmentManager treatmentManager = new TreatmentManager();
                List<UserModel> doctorlist = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId == 3 && x.HospitalId == hospitalid).ToList();
                List<UserModel> therapistlist = (await ViewHelper.GetAllUsers()).Where(x => x.DesignationId == 3 && x.HospitalId == hospitalid).ToList();
                List<PatientModel> patientlist = (await ViewHelper.GetAllPatients()).Where(x => x.PatientHospitalId == hospitalid).ToList();
                List<TreatmentModel> treatmentlist = await ViewHelper.GetAllTreatment();
                List<MedicineModel> druglist = await ViewHelper.GetAllMedicine(null, null, hospitalid);
                var opdservicelist = (await ViewHelper.GetAllOPDServices()).Where(x => x.OPDConsoltationId == opdconsutationid && x.HospitalId == hospitalid).ToList();
                var opdpreslist = (await ViewHelper.GetAllOPDPrescription()).Where(x => x.OPDConsultationId == opdconsutationid && x.HospitalId == hospitalid).ToList();
                ViewBag.doctorlist = doctorlist;
                ViewBag.patientlist = patientlist;
                ViewBag.treatmentlist = treatmentlist;
                ViewBag.druglist = druglist;
                ViewBag.therapistlist = therapistlist;
                var opdconsolutation = await new Repository<OPDConsoltation>().GetById(opdconsutationid ?? 0);
                var result = new Model.OPDConsoltationModel();
                //-------------------------------------------------------
                result.HospitalId = hospitalid;
                result.ConsultationDate = opdconsolutation.ConsultationDate;
                result.DoctorName = opdconsolutation?.DoctorName;
                result.OPDConsultationId = opdconsolutation?.OPDConsultationId ?? 0;
                result.PatientGender = opdconsolutation?.Gender;
                result.ConsultationDate = opdconsolutation.ConsultationDate;
                result.PatientIDNumber = opdconsolutation?.PatientIDNumber;
                result.PatientAge = opdconsolutation?.Age;
                result.PatientId = opdconsolutation?.PatientId;
                result.PatientName = opdconsolutation?.PatientName;
                result.DoctorName = opdconsolutation?.DoctorName;
                result.MobileNo = opdconsolutation?.MobileNo;
                result.PatientHeight = opdconsolutation?.Height;
                result.PatientWeight = opdconsolutation?.Weight;
                result.PatientTemp = opdconsolutation?.Temp;
                result.PatientBP = opdconsolutation?.BP;
                result.PatientLMP = opdconsolutation?.LMP;
                result.PatientPulse = opdconsolutation?.Pulse;
                result.PatientSpO2 = opdconsolutation?.SpO2;
                result.PatientVisitType = opdconsolutation?.VisitType;
                result.TreatmentSplDiscount = opdconsolutation?.SplDiscount ?? 0;
                result.NetCharges = opdconsolutation?.NetCharges ?? 0;
                result.TreatmentPaymentMode = opdconsolutation?.PaymentMode;
                result.DoctorsNote = opdconsolutation?.DoctorsNote;
                result.NatureOfIllness = opdconsolutation?.DoctorsNote;
                //-------------------------------------------------------
                result.opdserviceslist = opdservicelist;
                result.opdprescriptionlist = opdpreslist;
                result.IDNumber = opdconsolutation == null ? 1 : opdconsolutation.IDNumber + 1;
                result.ConsultationIDNumber = opdconsolutation == null ? "OPD-1" : "OPD-" + opdconsolutation?.IDNumber + 1.ToString();
                result.TreatmentTotalServicesCharges = opdconsolutation?.TotalCharges ?? 0;
                return View(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}