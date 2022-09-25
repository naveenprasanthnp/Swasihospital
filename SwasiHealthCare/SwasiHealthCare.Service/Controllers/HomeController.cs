using SwasiHealthCare.BusinessManager;
using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.IBusinessManager;
using SwasiHealthCare.Model;
using SwasiHealthCare.Repository;
using SwasiHealthCare.Service.App_Start;
using SwasiHealthCare.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SwasiHealthCare.Service.FilterConfig;

namespace SwasiHealthCare.Service.Controllers
{
    public class HomeController : Controller
    {
        [SessionTimeout]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login");
            }

            try
            {
                var result = new DashboardModel();
                long roleid = Convert.ToInt64(Session["RoleId"]);
                long hospitalid = Convert.ToInt64(Session["HospitalId"]);
                long userid = Convert.ToInt64(Session["UserId"]);
                IUserManager userManager = new UserManager();
                //IPatientManager patientManager = new PatientManager();
                IHospitalManager hospitalManager = new HospitalManager();
                IPatientManager patientmanager = new PatientManager();
                if (roleid != 1)
                {
                    DateTime now = DateTime.Now;
                    var startDate = new DateTime(now.Year, now.Month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    var userdetails = (await userManager.GetEditUser(userid));
                    var userList = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId != 1).ToList();
                    List<PatientModel> paientlist = (await ViewHelper.GetAllPatients()).Where(x => x.PatientDate >= startDate && x.
                    PatientDate <= endDate).Where(x => x.PatientHospitalId == hospitalid).ToList();

                    var t = (await patientmanager.GetOPDServiceCount(hospitalid));
                    var opdcount = patientmanager.GetOPDCount(hospitalid, false);
                    result.totalpatients = paientlist.Count();
                    result.totalopd = opdcount;
                   
                    var purchasemedicine = (await ViewHelper.GetAllPurchaseMedicine(null, null, hospitalid));
                    var date = new FilterModel();
                    date.FromDate = startDate;
                    date.ToDate = endDate;
                    date.IsDashboard = true;
                    var revenu = (await RevenueReport(date));
                   
                    result.totalpurchasemedicine = purchasemedicine.Where(x => x.PurchaseDate >= startDate && x.PurchaseDate <= endDate).Sum(x => x.PurchaseCost);

                    result.totaltreatment = t.DashboardTreatmentCount ?? 0;
                    result.totalpatients = paientlist.Count();
                    result.totalopd = opdcount;
                    result.totaldrug = t.DashboardMedicineCount ?? 0;

                    result.totaltrmtrevenu = revenu.treatmentrevenue.Sum(x => x.Income);
                    result.totaldrugrevenue = revenu.medicinerevenue.Sum(x => x.Income);
                    date.IsDashboard = true;
                    revenu = await RevenueReportDay(date);
                    //decimal? companyexpense = revenu.treatmentrevenue.Sum(x => x.CompanyExpense);
                    result.totalexpense = revenu.treatmentrevenue.Sum(x => x.Expense) + revenu.medicinerevenue.Sum(x => x.Receipt);
                    var medicinelist1 = (await ViewHelper.GetAllExpense()).Where(x => x.ExpenseDate >= startDate
                    && x.ExpenseDate <= endDate && x.IsCompanyExpense).ToList();
                    decimal? expenseamtmonth = medicinelist1.Sum(x => x.BillAmount);
                    result.totalexpense = result.totalexpense + expenseamtmonth;

                    //Expense Today
                    var datetime1 = new FilterModel();
                    datetime1.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,23,59,59,999);
                    datetime1.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0); 
                    datetime1.IsDashboard = true;
                    var revenu1 = await RevenueReportDay(datetime1);

                    result.totalexpenseday = revenu1.treatmentrevenue.Sum(x => x.Expense) + revenu1.medicinerevenue.Sum(x => x.Receipt);
                    result.totalpurchasemedicineday = purchasemedicine.Where(x => x.PurchaseDate >= datetime1.FromDate && x.PurchaseDate <= datetime1.ToDate).Sum(x => x.PurchaseCost);
                    decimal? opdserviceamt = (await patientmanager.GetAllOPDServicePrescription(datetime1.FromDate, datetime1.ToDate,hospitalid, true));
                    decimal? opdprescamt = (await patientmanager.GetAllOPDServicePrescription(datetime1.FromDate, datetime1.ToDate,hospitalid, false));
                    result.totaltrmtrevenuday = opdserviceamt;
                    result.totaldrugrevenueday = opdprescamt;
                    var medicinelist = (await ViewHelper.GetAllExpense()).Where(x => x.ExpenseDate >= datetime1.FromDate 
                    && x.ExpenseDate <= datetime1.ToDate && x.IsCompanyExpense).ToList();
                    decimal? expenseamt = medicinelist.Sum(x => x.BillAmount);
                    result.totalexpenseday = result.totalexpenseday + expenseamt;

                }
                else
                {
                    
                    DateTime now = DateTime.Now;
                    var startDate = new DateTime(now.Year, now.Month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    
                    var userdetails = (await userManager.GetEditUser(userid));
                    var userList = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId != 1).ToList();
                    List<PatientModel> paientlist = (await ViewHelper.GetAllPatients()).Where(x=>x.PatientCreatedDate >= startDate && x.
                    PatientCreatedDate <= endDate).Where(x=>x.PatientHospitalId == hospitalid).ToList();
                    IMedicineManager medicinemanager = new MedicineManager();
                    List<MedicineModel> medicinelist = (await ViewHelper.GetAllMedicine(null,null,null)).ToList();
                    var treatmentlist =( await ViewHelper.GetAllTreatment()).Where(x => x.TreatmentCreatedDate >= startDate && x.
                    TreatmentCreatedDate <= endDate).ToList(); ;
                    var opdcount = patientmanager.GetOPDCount(hospitalid, false);
                    var purchasemedicine= (await ViewHelper.GetAllPurchaseMedicine(null,null, hospitalid));
                    var date = new FilterModel();
                    date.FromDate = startDate;
                    date.ToDate = endDate;
                    date.IsDashboard = true;
                    var revenu = await RevenueReport(date);
                    var t = (await patientmanager.GetOPDServiceCount(hospitalid));
                    //result.totaldoctors = userList.Where(x => x.RoleId == 3).Count();
                    //result.totalstaffs = userList.Where(x => x.RoleId != 3).Count();
                    result.totalpurchasemedicine = purchasemedicine.Where(x => x.PurchaseDate >= startDate && x.PurchaseDate <= endDate).Sum(x => x.PurchaseCost);
                    //result.totaltreatment = treatmentlist.Count();
                    result.totaltreatment = t.DashboardTreatmentCount ?? 0;
                    result.totalpatients = paientlist.Count();
                    result.totalopd = opdcount;
                    //result.totaldrug = medicinelist.Where(x=> x.MedicineCreatedDate >= startDate && x.MedicineCreatedDate <= endDate).Count();
                    result.totaldrug = t.DashboardMedicineCount ?? 0;
                    result.totaltrmtrevenu = revenu.treatmentrevenue.Sum(x => x.Income);
                    result.totaldrugrevenue = revenu.medicinerevenue.Sum(x => x.Income);

                    date.IsDashboard = false;
                    revenu = await RevenueReportDay(date);
                    result.totalexpense = revenu.treatmentrevenue/*.Where(x => x.IsCompanyExpense != false)*/.Sum(x => x.Expense) +
                      revenu.medicinerevenue.Sum(x => x.Receipt);

                    result.totalexpense = revenu.treatmentrevenue.Sum(x => x.Expense) + revenu.medicinerevenue.Sum(x => x.Receipt);
                    var medicinelist1 = (await ViewHelper.GetAllExpense()).Where(x => x.ExpenseDate >= startDate
                    && x.ExpenseDate <= endDate && x.IsCompanyExpense).ToList();
                    decimal? expenseamtmonth = medicinelist1.Sum(x => x.BillAmount);
                    result.totalexpense = result.totalexpense + expenseamtmonth;

                    var datetime1 = new FilterModel();
                    datetime1.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, 999);
                    datetime1.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
                    datetime1.IsDashboard = true;
                    var revenu1 = await RevenueReportDay(datetime1);
                    //result.totaltrmtrevenuday = revenu1.treatmentrevenue.Sum(x => x.Income);
                    //result.totaldrugrevenueday = revenu1.medicinerevenue.Sum(x => x.Income);
                    result.totalexpenseday = revenu1.treatmentrevenue/*.Where(x => x.IsCompanyExpense != false)*/.Sum(x => x.Expense) + revenu.medicinerevenue.Sum(x => x.Receipt);
                    result.totalpurchasemedicineday = purchasemedicine.Where(x => x.PurchaseDate >= datetime1.FromDate && x.PurchaseDate <= datetime1.ToDate).Sum(x => x.PurchaseCost);
                    decimal? opdserviceamt = await patientmanager.GetAllOPDServicePrescription(datetime1.FromDate, datetime1.ToDate, hospitalid, true);
                    decimal? opdprescamt = await patientmanager.GetAllOPDServicePrescription(datetime1.FromDate, datetime1.ToDate, hospitalid, false);

                    result.totaltrmtrevenuday = opdserviceamt;
                    result.totaldrugrevenueday = opdprescamt;

                    //Company Expense
                    var revenu2 = await RevenueReportDay(datetime1);
                    var medicinelist11 = (await ViewHelper.GetAllExpense()).Where(x => x.ExpenseDate >= datetime1.FromDate
                    && x.ExpenseDate <= datetime1.ToDate && x.IsCompanyExpense).ToList();
                    decimal? expenseamt = medicinelist11.Sum(x => x.BillAmount);
                    result.totalexpenseday = result.totalexpenseday + expenseamt;
                }
                return View(result);
            }
            catch (Exception ex)
            {
                throw ex;
              // return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<RevenueReportModel> RevenueReport(FilterModel FilterModel)
        {
            
            try
            {
                long? roleid = Convert.ToInt64(Session["RoleId"]);
                IUserManager userManager = new UserManager();
                List<Hospital> hospital = (await userManager.GetHospitals()).ToList();
                long? hospitalid = null;
                if (roleid == 1)
                {
                    hospitalid = FilterModel.HospitalId;
                }
                else
                {
                    hospitalid = Convert.ToInt64(Session["HospitalId"]);
                }
                IMedicineManager medicinemanager = new MedicineManager();

                //var trmtlist = await ViewHelper.GetAllTreatment();
                //var medlist = await ViewHelper.GetAllMedicine(null, null, hospitalid);

                var medicincerevenuelist = await medicinemanager.GetAllMedicineReport(hospitalid ?? 0);
                var treatmentrevenuelist = await medicinemanager.GetAllTreatmentReport(hospitalid ?? 0);
               
                if (FilterModel.FromDate != null && FilterModel.ToDate != null)
                {
                    var StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    var EndDate = StartDate.AddMonths(1).AddDays(-1);
                    
                    medicincerevenuelist = medicincerevenuelist.Where(x => x.Date >= FilterModel.FromDate &&
                    x.Date <= FilterModel.ToDate).Where(x=>x.HospitalId == hospitalid).ToList();
                    
                    treatmentrevenuelist = treatmentrevenuelist.Where(x => x.Date >= FilterModel.FromDate &&
                    x.Date <= FilterModel.ToDate).Where(x => x.CompanyId == hospitalid).ToList();
                    
                    FilterModel.FromDate = StartDate;
                    FilterModel.ToDate = EndDate;
                    
                    var totalincome = treatmentrevenuelist.Sum(x => x.Income) + treatmentrevenuelist.Sum(x => x.OpeningBalance);
                    var totalexpense = treatmentrevenuelist.Sum(x => x.Expense);
                    var totalreceipt = medicincerevenuelist.Sum(x => x.Receipt);
                    
                    var fialresult = new TreatmentRevenueReportModel
                    {
                        OpeningBalance = treatmentrevenuelist.Sum(x => x.OpeningBalance),
                        Income = treatmentrevenuelist.Sum(x => x.Income),
                        Receipt = treatmentrevenuelist.Sum(x => x.Receipt),
                        Expense = treatmentrevenuelist.Sum(x => x.Expense),
                        Total = totalincome,
                        Profit = totalincome - totalexpense - totalreceipt,
                        CompanyExpense = treatmentrevenuelist.Sum(x => x.CompanyExpense),
                        CompanyId = hospitalid
                    };
                    if (FilterModel.IsDashboard == false || FilterModel.IsDashboard == null)
                    {
                        treatmentrevenuelist.Add(fialresult);
                    }
                    var totalincome1 = medicincerevenuelist.Sum(x => x.Income) + medicincerevenuelist.Sum(x => x.OpeningBalance);
                    var totalreceipt1 = totalincome - medicincerevenuelist.Sum(x => x.PurchaseMedicine);
                    var fialresult1 = new MedicineRevenueReportModel
                    {
                        OpeningBalance = medicincerevenuelist.Sum(x => x.OpeningBalance),
                        Income = medicincerevenuelist.Sum(x => x.Income),
                        Receipt = medicincerevenuelist.Sum(x => x.Receipt),
                        Total = totalincome,
                        Profit = totalincome,
                        HospitalId = hospitalid
                    };
                    if (FilterModel.IsDashboard == false || FilterModel.IsDashboard == null)
                    {
                        medicincerevenuelist.Add(fialresult1);
                    }
                }
                var result = new RevenueReportModel
                {
                    filterModel = FilterModel,
                    treatmentrevenue = treatmentrevenuelist,
                    medicinerevenue = medicincerevenuelist
                };
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<RevenueReportModel> RevenueReportDay(FilterModel FilterModel)
        {

            try
            {
                long? roleid = Convert.ToInt64(Session["RoleId"]);
                IUserManager userManager = new UserManager();
                List<Hospital> hospital = (await userManager.GetHospitals()).ToList();
                long? hospitalid = null;
                if (roleid == 1)
                {
                    hospitalid = FilterModel.HospitalId;
                }
                else
                {
                    hospitalid = Convert.ToInt64(Session["HospitalId"]);
                }
                IMedicineManager medicinemanager = new MedicineManager();
                var trmtlist = await ViewHelper.GetAllTreatment();
                var medlist = await ViewHelper.GetAllMedicine(null, null, hospitalid);
                var medicincerevenuelist = await medicinemanager.GetAllMedicineReport(hospitalid ?? 0);
                var treatmentrevenuelist = await medicinemanager.GetAllTreatmentReport(hospitalid ?? 0);
                if (FilterModel.FromDate != null && FilterModel.ToDate != null)
                {
                    
                    medicincerevenuelist = medicincerevenuelist.Where(x => x.Date >= FilterModel.FromDate &&
                    x.Date <= FilterModel.ToDate).ToList();
                    treatmentrevenuelist = treatmentrevenuelist.Where(x => x.Date >= FilterModel.FromDate &&
                    x.Date <= FilterModel.ToDate).ToList();
                    //FilterModel.FromDate = StartDate;
                    //FilterModel.ToDate = EndDate;
                    var totalincome = treatmentrevenuelist.Sum(x => x.Income) + treatmentrevenuelist.Sum(x => x.OpeningBalance);
                    var totalexpense = treatmentrevenuelist.Sum(x => x.Expense);
                    var totalreceipt = medicincerevenuelist.Sum(x => x.Receipt);
                    var fialresult = new TreatmentRevenueReportModel
                    {
                        OpeningBalance = treatmentrevenuelist.Sum(x => x.OpeningBalance),
                        Income = treatmentrevenuelist.Sum(x => x.Income),
                        Receipt = treatmentrevenuelist.Sum(x => x.Receipt),
                        Expense = treatmentrevenuelist.Sum(x => x.Expense),
                        Total = totalincome,
                        Profit = totalincome - totalexpense - totalreceipt,
                        CompanyExpense = treatmentrevenuelist.Sum(x => x.CompanyExpense)
                    };
                    if (FilterModel.IsDashboard == false || FilterModel.IsDashboard == null)
                    {
                        treatmentrevenuelist.Add(fialresult);
                    }
                    var totalincome1 = medicincerevenuelist.Sum(x => x.Income) + medicincerevenuelist.Sum(x => x.OpeningBalance);
                    var totalreceipt1 = totalincome - medicincerevenuelist.Sum(x => x.PurchaseMedicine);
                    var fialresult1 = new MedicineRevenueReportModel
                    {
                        OpeningBalance = medicincerevenuelist.Sum(x => x.OpeningBalance),
                        Income = medicincerevenuelist.Sum(x => x.Income),
                        Receipt = medicincerevenuelist.Sum(x => x.Receipt),
                        Total = totalincome,
                        Profit = totalincome
                    };
                    if (FilterModel.IsDashboard == false || FilterModel.IsDashboard == null)
                    {
                        medicincerevenuelist.Add(fialresult1);
                    }
                }
                var result = new RevenueReportModel
                {
                    filterModel = FilterModel,
                    treatmentrevenue = treatmentrevenuelist,
                    medicinerevenue = medicincerevenuelist
                };
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        //[Gzip]
        public ActionResult SideMenu()
        {
            try
            {
                if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }
                IUserManager usermanager = new UserManager();
                long? userid = Convert.ToInt64(Session["UserId"]);
                var usedetails = usermanager.GetLoginUserDetails(userid ?? 0);
                var SideMenuModel = new SideMenuModel { LoginUserName = usedetails.Data.FirstName + " " + usedetails.Data.LastName, RoleId = usedetails.Data.UserRoleId,ProfilePath = usedetails.Data.ProfilePath };
                return PartialView("_SideMenu", SideMenuModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        //[Gzip]
        public async Task<ActionResult> PatientOPDDetails()
        {
            try
            {
                if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }

                IUserManager usermanager = new UserManager();
                long? userid = Convert.ToInt64(Session["UserId"]);
                long hospitalid = Convert.ToInt64(Session["HospitalId"]);
                var usedetails = usermanager.GetLoginUserDetails(userid ?? 0);
                var opdlist = await ViewHelper.GetAllOPDCommonSearch(null, null, hospitalid);
                return PartialView("_PatientOPDDetails", opdlist);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        //[Gzip]
        public async Task<ActionResult> Notes()
        {
            try
            {
                if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }
                IUserManager usermanager = new UserManager();
                long? userid = Convert.ToInt64(Session["UserId"]);
                long? roleid = Convert.ToInt64(Session["RoleId"]);
                List<NotesModel> noteslist = (await ViewHelper.GetAllNotesList()).ToList();
                var notes = new NotesModel();
                ViewBag.userid = userid;
                return PartialView("_Notes", noteslist);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        //[Gzip]
        public async Task<ActionResult> Notes(NotesModel notesmodel)
        {
            try
            {
                if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }
                IUserManager usermanager = new UserManager();
                long? userid = Convert.ToInt64(Session["UserId"]);
                long? roleid = Convert.ToInt64(Session["RoleId"]);
                notesmodel.NotesCreatedBy = userid;
                var result = await usermanager.AddNewNotes(notesmodel);
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        //[Gzip]
        public ActionResult TopMenuName(bool? isfirstlogin)
        {
            try
            {
                if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }
                IUserManager usermanager = new UserManager();
                var userid = Convert.ToInt64(Session["UserId"]);
                var usedetails = usermanager.GetLoginUserDetails(userid);
                var SideMenuModel = new SideMenuModel { LoginUserName = usedetails.Data.FirstName + " " + usedetails.Data.LastName, IsFirstLogin = isfirstlogin ?? false };
                return Json(SideMenuModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        //[Gzip]
        public ActionResult TopRightCard()
        {
            try
            {
                if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
                {
                    return RedirectToAction("Login", "Home");
                }
                long userid = Convert.ToInt64(Session["UserId"]);
                long hospitalid = Convert.ToInt64(Session["HospitalId"]);
                IUserManager userManager = new UserManager();
                var usedetails = userManager.GetLoginUserDetails(userid);

                //if(usedetails.Data )
                IPatientManager patientmanager = new PatientManager();
                //var t = (patientmanager.GetOPDServiceCount(hospitalid));
                var patientcount = patientmanager.GetPatientsCountHospital(usedetails.Data.HospitalId ?? 0);
                var opdcount = patientmanager.GetOPDCount(usedetails.Data.HospitalId,true);
                var TopRightCard = new TopRightCardModel
                {
                    patientcount = patientcount,
                    opdmonthcount = opdcount,
                    revenuecurrentmonth = 0
                };
                return PartialView("_TopRightCard", TopRightCard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            try
            {
                IUserManager userManager = new UserManager();
                var result = await userManager.UserSignIn(loginModel.UserName, loginModel.Password, "");
                if (result.Status)
                {
                    Session["UserId"] = result.UserProfile.UserId;
                    Session["RoleId"] = result.UserProfile.UserRoleId;
                    Session["UserEmail"] = result.UserProfile.UserEmail;
                    Session["AccessRights"] = result.AccessRights;
                    if (Convert.ToInt64(Session["RoleId"].ToString()) != 1)
                    {
                        Session["HospitalId"] = result.UserProfile.HospitalId;
                    }
                    return RedirectToAction("Index", "Home");
                    //return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //return Json(result, JsonRequestBehavior.AllowGet);
                    ViewBag.ErrorMsg = result.ErrorMessage;
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(LoginModel loginModel)
        {
            try
            {
                IUserManager userManager = new UserManager();
                var result = await userManager.ForgetPassword(loginModel.UserName);
                if (result.Status)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            try
            {
                IUserManager userManager = new UserManager();
                var result = await userManager.ChangePassword(changePasswordModel.UserName, changePasswordModel.NewPassword);
                if (result.Status)
                {
                    ViewBag.Message = result.SuccessMessage;
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return View();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var restunmodel = new ChangePasswordModel
            {
                UserId = Convert.ToInt64(System.Web.HttpContext.Current.Session["UserId"]),
                UserName = System.Web.HttpContext.Current.Session["UserEmail"].ToString()
            };

            return View(restunmodel);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            Session["UserId"] = null;
            return RedirectToAction("Login", "Home");
        }

        public void CreateSuperAdmin()
        {
            try
            {
                var firstuser = new UserModel();
                IUserManager userManager = new UserManager();
                firstuser.UserStatus = true;
                firstuser.Mode = "A";
                firstuser.FirstName = "SuperAdmin";
                firstuser.UserEmail = "superadmin@swasicure.com";
                firstuser.UserPassword = "12345";
                firstuser.HospitalId = 0;
                var result =  userManager.CreateSuperAdmin(firstuser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> RemoveNotes(long NotesId)
        {
            try
            {
                IUserManager usermanager = new UserManager();
                var result = await usermanager.DeleteNotes(NotesId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}