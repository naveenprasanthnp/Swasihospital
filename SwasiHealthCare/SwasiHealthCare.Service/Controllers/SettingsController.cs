using SwasiHealthCare.BusinessManager;
using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.IBusinessManager;
using SwasiHealthCare.Model;
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
    public class SettingsController : Controller
    {
        public SettingsController()
        {

        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> Tile()
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
                long? userid = Convert.ToInt64(Session["UserId"]);
                long? roleid = Convert.ToInt64(Session["RoleId"]);
                var result = new TilesModel();
                
                IUserManager userManager = new UserManager();
                var rolelist = (await userManager.GetRoles()).ToList();

                var systemlist = (await ViewHelper.GetAllSystem()).Where(x => x.HospitalId == hospitalid).ToList();
                result.systemcount = systemlist.Count();

                if (roleid != 1)
                {
                    var userlist = (await ViewHelper.GetAllUsers()).Where(x => x.UserId != userid &&
                     x.HospitalId == hospitalid).ToList();
                    result.staffcount = userlist.Count();
                }
                else
                {
                    var userlist = (await ViewHelper.GetAllUsers()).Where(x => x.UserId != userid ).ToList();
                    result.staffcount = userlist.Count();
                }

                var medicinelist = await ViewHelper.GetAllMedicine(null,null, hospitalid);
                result.medicinescount = medicinelist.Count();

                var treatmentlist = await ViewHelper.GetAllTreatment();
                result.treatmentcount = treatmentlist.Count();

                var hospitallist = await ViewHelper.GetAllHospital();
                result.hospitalcount = hospitallist.Count();

                var designationlist = await ViewHelper.GetAllDesignation();
                result.designationcount = designationlist.Count();

                result.rileid = Convert.ToInt64(Session["RoleId"]);
                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

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

            try
            {
                var systemlist = await ViewHelper.GetAllSystem();
                return View(systemlist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewSystem()
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
                return View(new Model.SystemViewModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]      
        public async Task<ActionResult> NewSystem(SystemViewModel sysmodel)
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
                ISystemManager systemManager = new SystemManager();
                sysmodel.SystemCreatedBy = Convert.ToInt64(Session["UserId"]);
                sysmodel.SystemStatus = true;
                sysmodel.Mode = "A";
                sysmodel.HospitalId = hospitalid;
                 var result = await systemManager.AddNewSystem(sysmodel);
                return RedirectToAction("Index", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditSystem(long systemid)
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
                var system = (await userManager.GetEditSystem(systemid));
                return View(system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditSystem(Model.SystemViewModel sysModel)
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
                sysModel.SystemModifiedBy = Convert.ToInt64(Session["UserId"]);
                sysModel.Mode = "E";
                await systemManager.AddNewSystem(sysModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSystem(long systemid)
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
                var result = await systemManager.DeleteSystem(systemid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SystemStatus(long systemid, bool status)
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
                var result = await systemManager.UpdateSystemStatus(systemid, status, Convert.ToInt64(Session["UserId"]));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> IndexPlan()
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
                var planlist = await ViewHelper.GetAllPlan();
                return View(planlist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewPlan()
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
                List<TreatmentModel> treatmentlist = await ViewHelper.GetAllTreatment();
                ViewBag.treatmentlist = treatmentlist;
                ISystemManager systemManager = new SystemManager();
                return View(new Model.PlanModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewPlan(PlanModel planmodel)
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
                planmodel.PlanCreatedBy = Convert.ToInt64(Session["UserId"]);
                planmodel.PlanStatus = true;
                planmodel.Mode = "A";
                var result = await systemManager.AddNewPlan(planmodel);
                return RedirectToAction("IndexPlan", result);
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
                List<TreatmentModel> treatmentlist = await ViewHelper.GetAllTreatment();
                ViewBag.treatmentlist = treatmentlist;
                return View(system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditPlan(Model.PlanModel planModel)
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
                planModel.PlanModifiedBy = Convert.ToInt64(Session["UserId"]);
                planModel.Mode = "E";
                await systemManager.AddNewPlan(planModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeletePlan(long planid)
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
                var result = await systemManager.DeletePlan(planid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> PlanStatus(long planid, bool status)
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
                var result = await systemManager.UpdatePlanStatus(planid, status, Convert.ToInt64(Session["UserId"]));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> IndexSubscription()
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
                 var planlist = await ViewHelper.GetAllPatientPlan();
                  return View(planlist);
                //return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewSubscription()
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
                ISystemManager systemManager = new SystemManager();
                List<PatientModel> patientlist = (await ViewHelper.GetAllPatients()).Where(x => x.PatientHospitalId == hospitalid).ToList();
                List<PlanModel> planlist = await ViewHelper.GetAllPlan();
                ViewBag.patientlist = patientlist;
                ViewBag.planlist = planlist;
                return View(new Model.PatientPlanSubscriptionModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewSubscription(PatientPlanSubscriptionModel patientplansubscriptionmodel)
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
                patientplansubscriptionmodel.PatientPlanSubscriptionCreatedBy = Convert.ToInt64(Session["UserId"]);
                patientplansubscriptionmodel.PatientPlanSubscriptionStatus = true;
                patientplansubscriptionmodel.Mode = "A";
                var result = await systemManager.AddPatientPlanSubscription
                    (patientplansubscriptionmodel);
                return RedirectToAction("NewSubscription", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditSubscription(long patientplansubscriptionid)
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
                long? hospitalid = Convert.ToInt64(Session["HospitalId"].ToString());
                ISystemManager userManager = new SystemManager();
                var system = (await userManager.GetEditPatientPlanSubscription(patientplansubscriptionid));
                List<PatientModel> patientlist = (await ViewHelper.GetAllPatients()).Where(x => x.PatientHospitalId == hospitalid).ToList();
                List<PlanModel> planlist = await ViewHelper.GetAllPlan();
                ViewBag.patientlist = patientlist;
                ViewBag.planlist = planlist;
                return View(system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditSubscription(PatientPlanSubscriptionModel patientplansubscriptionmodel)
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
                patientplansubscriptionmodel.PatientPlanSubscriptionModifiedBy = Convert.ToInt64(Session["UserId"]);
                patientplansubscriptionmodel.Mode = "E";
                await systemManager.AddPatientPlanSubscription(patientplansubscriptionmodel);
                return RedirectToAction("IndexSubscription");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSubscription(long patientplansubscriptionid)
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
                var result = await systemManager.DeletePatientPlanSubscription(patientplansubscriptionid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SubscriptionStatus(long patientplansubscriptionid, bool status)
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
                var result = await systemManager.UpdatePatientPlanSubscriptionStatus(patientplansubscriptionid, status, Convert.ToInt64(Session["UserId"]));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> MyProfile()
        {
            long userid = Convert.ToInt64(Session["UserId"]);
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
                var users = (await userManager.GetEditUser(userid));
                return View(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MyProfile(UserModel usermodel)
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
                var users = (await userManager.GetEditUser(usermodel.UserId ?? 0));
                long rolid = Convert.ToInt64(Session["RoleId"]);
               
                users.UserModifiedBy = Convert.ToInt64(Session["UserId"]);
                users.Mode = "E";
               
                if (usermodel.NewPassword != null)
                {
                    users.UserPassword = usermodel.NewPassword;
                    usermodel.IsPasswordChange = true;
                }
                users.UserMobile = usermodel.UserMobile;
                users.UserEmail = usermodel.UserEmail;
                await userManager.SaveUser(users, rolid);
                return RedirectToAction("MyProfile", "Settings");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SendConfirmCode(HttpPostedFileBase[] httpPostedFileBase, long? userid)
        {
            if (Request.Files.Count > 0)
            {
                UserModel usermodel = new UserModel();
                HttpPostedFileBase file = Request.Files[0];
                int fileSize = file.ContentLength;
                if (fileSize > 0)
                {
                    Random random = new Random();
                    var fn = random.Next(0001, 1000);
                    FileInfo fileinfo = new FileInfo(file.FileName);
                    var fileName = "PP" + fn.ToString() + fileinfo.Extension;
                    byte[] fileByteArray = new byte[fileSize];
                    file.InputStream.Read(fileByteArray, 0, fileSize);
                    string fileLocation = Path.Combine(Server.MapPath("~/Content/Files/ProfilePicture"), fileName);
                    if (!Directory.Exists(Path.Combine(Server.MapPath("~/Content/Files"))))
                        Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Content/Files")));
                    file.SaveAs(fileLocation);
                    usermodel.ProfilePath = fileName;
                    usermodel.UserId = userid;
                    IUserManager userManager = new UserManager();
                    await userManager.UpdateProfilePicture(usermodel);
                }
                else
                {

                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> IndexOpeningBalance()
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
                var result = new TmtMedOBModel
                {
                    TreatmentOpeningBalanceModels = await ViewHelper.GetAllTmtOB(),
                    MedicineOpeningBalanceModels = await ViewHelper.GetAllMedOB(),
                    TreatmentReceiptModels = await ViewHelper.GetAllTmtReceipt(),
                    MedicineReceiptModels = await ViewHelper.GetAllMedReceipt()
                };
                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> OpeningBalance()
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
                return View(new Model.OBAndReceiptModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OpeningBalance(OBAndReceiptModel obandr)
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
                var tmtopeningbalancemodel = new TreatmentOpeningBalanceModel();
                var mdnopeningbalancemodel = new MedicineOpeningBalanceModel();

                tmtopeningbalancemodel.Amount = obandr.TreatmentOpeningBalanceModel.Amount;
                tmtopeningbalancemodel.Date = obandr.TreatmentOpeningBalanceModel.Date;
                tmtopeningbalancemodel.CreatedBy = Convert.ToInt64(Session["UserId"]);
                tmtopeningbalancemodel.Mode = "A";

                mdnopeningbalancemodel.Amount = obandr.MedicineOpeningBalanceModel.Amount;
                mdnopeningbalancemodel.Date = obandr.MedicineOpeningBalanceModel.Date;
                mdnopeningbalancemodel.CreatedBy = Convert.ToInt64(Session["UserId"]);
                mdnopeningbalancemodel.Mode = "A";
                 
                if (tmtopeningbalancemodel.HospitalId == null ||
                   mdnopeningbalancemodel.HospitalId == null)
                {
                    mdnopeningbalancemodel.HospitalId = Convert.ToInt64(Session["HospitalId"]);
                    tmtopeningbalancemodel.HospitalId = Convert.ToInt64(Session["HospitalId"]);
                }
                if(tmtopeningbalancemodel.Amount != null && tmtopeningbalancemodel.Date.ToString() != "01/01/0001")
                {
                    var resulttmt = await systemManager.AddTreatmentOpeningBalance(tmtopeningbalancemodel);
                }
                if (mdnopeningbalancemodel.Amount != null && mdnopeningbalancemodel.Date.ToString() != "01/01/0001")
                {
                    var resultmed = await systemManager.AddMedicineOpeningBalance(mdnopeningbalancemodel);
                }
                return RedirectToAction("IndexOpeningBalance");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditTreatmentOpeningBalance(long OpeningBalanceId)
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
                var system = (await userManager.GetEditTreatmentOpeningBalance(OpeningBalanceId));
                return View(system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditOpeningBalance(OBAndReceiptModel planModel)
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
                planModel.MedicineOpeningBalanceModel.Mode = "E";
                planModel.TreatmentOpeningBalanceModel.Mode = "E";
                if (planModel.MedicineOpeningBalanceModel.HospitalId == null ||
                    planModel.TreatmentOpeningBalanceModel.HospitalId == null)
                {
                    planModel.TreatmentOpeningBalanceModel.HospitalId = Convert.ToInt64(Session["HospitalId"]);
                    planModel.MedicineOpeningBalanceModel.HospitalId = Convert.ToInt64(Session["HospitalId"]);
                }
                var resulttmt = await systemManager.AddTreatmentOpeningBalance(planModel.TreatmentOpeningBalanceModel);
                var resultmed = await systemManager.AddMedicineOpeningBalance(planModel.MedicineOpeningBalanceModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOpeningBalance(long openingbalance,bool ismed)
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
                var result = await systemManager.DeleteOpeningBalance(openingbalance, ismed);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> Receipt()
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
                return View(new Model.OBAndReceiptModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Receipt(OBAndReceiptModel obandr)
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
                var tmtreceiptmodel = new TreatmentReceiptModel();
                var mdnreceiptmodel = new MedicineReceiptModel();

                tmtreceiptmodel.ReceiptDate = obandr.TreatmentReceiptModel.ReceiptDate;
                tmtreceiptmodel.ReceiptAmount = obandr.TreatmentReceiptModel.ReceiptAmount;
                tmtreceiptmodel.ReceiptCreatedBy = Convert.ToInt64(Session["UserId"]);
                tmtreceiptmodel.Mode = "A";

                mdnreceiptmodel.ReceiptDate = obandr.MedicineReceiptModel.ReceiptDate;
                mdnreceiptmodel.ReceiptAmount = obandr.MedicineReceiptModel.ReceiptAmount;
                mdnreceiptmodel.ReceiptCreatedBy = Convert.ToInt64(Session["UserId"]);
                mdnreceiptmodel.Mode = "A";

                if (mdnreceiptmodel.ReceiptHospitalId == null ||
                   tmtreceiptmodel.ReceiptHospitalId == null)
                {
                    tmtreceiptmodel.ReceiptHospitalId = Convert.ToInt64(Session["HospitalId"]);
                    mdnreceiptmodel.ReceiptHospitalId = Convert.ToInt64(Session["HospitalId"]);
                }

                if (tmtreceiptmodel.ReceiptAmount != null && tmtreceiptmodel.ReceiptDate.ToString() != "01/01/0001")
                {
                    var resulttmt = await systemManager.AddTreatmentReceipt(tmtreceiptmodel);
                }
                if (mdnreceiptmodel.ReceiptAmount != null && mdnreceiptmodel.ReceiptDate.ToString() != "01/01/0001")
                {
                    var resultmed = await systemManager.AddMedicineReceipt(mdnreceiptmodel);
                }
                return RedirectToAction("IndexPlan");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditTreatmentReceipt(long ReceiptId)
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
                var system = (await userManager.GetEditTreatmentOpeningBalance(ReceiptId));
                return View(system);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditReceipt(OBAndReceiptModel planModel)
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
                planModel.MedicineOpeningBalanceModel.Mode = "E";
                planModel.TreatmentOpeningBalanceModel.Mode = "E";
                if (planModel.MedicineOpeningBalanceModel.HospitalId == null ||
                    planModel.TreatmentOpeningBalanceModel.HospitalId == null)
                {
                    planModel.TreatmentOpeningBalanceModel.HospitalId = Convert.ToInt64(Session["HospitalId"]);
                    planModel.MedicineOpeningBalanceModel.HospitalId = Convert.ToInt64(Session["HospitalId"]);
                }
                var resulttmt = await systemManager.AddTreatmentOpeningBalance(planModel.TreatmentOpeningBalanceModel);
                var resultmed = await systemManager.AddMedicineOpeningBalance(planModel.MedicineOpeningBalanceModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteReceipt(long openingbalance,bool ismed)
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
                var result = await systemManager.DeleteReceipt(openingbalance, ismed);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}