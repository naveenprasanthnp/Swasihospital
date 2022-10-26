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
    public class ReportController : Controller
    {
        [HttpGet]
        [Gzip]
        public async Task<ActionResult> PatientReport(FilterModel FilterModel)
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
                IUserManager userManager = new UserManager();
                List<Hospital> hospital = (await userManager.GetHospitals()).ToList();
                var patientlist = await ViewHelper.GetAllPatient(null,null, null, null);
                //var opdlist = await ViewHelper.GetAllPatient(FilterModel.PatientId,FilterModel.FromDate, FilterModel.ToDate, hospitalid);
                var opdlist = (await ViewHelper.GetAllPatient(FilterModel.PatientId, null, null, hospitalid)).ToList();
                if(FilterModel.FromDate != null && FilterModel.ToDate != null)
                {
                    var fdate = Convert.ToDateTime(FilterModel.FromDate).Date;
                    var tdate = Convert.ToDateTime(FilterModel.ToDate).Date;
                    opdlist = opdlist.Where(x => x.PatientDate.Date >= fdate && x.PatientDate.Date <= tdate).ToList();
                    ViewBag.fdate = fdate;
                    ViewBag.tdate = tdate;
                }
                ViewBag.patientlist = patientlist;
                ViewBag.hospitallist = hospital;
               
                return View(opdlist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> OPDReport(FilterModel FilterModel)
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
                IUserManager userManager = new UserManager();
                List<Hospital> hospital = (await userManager.GetHospitals()).ToList();
                IPatientManager patientManager = new PatientManager();
                var opdlist = await patientManager.GetOPDReport(null, null, hospitalid, FilterModel);
                //var patientlist = await ViewHelper.GetAllPatient(null, null);
                //var opdlist = await ViewHelper.GetAllOPDConsoltation(FilterModel.FromDate, FilterModel.ToDate);
                if (FilterModel.FromDate != null && FilterModel.ToDate != null)
                {
                    //var fdate1 = Convert.ToDateTime(FilterModel.FromDate).ToString("dd/MM/yyyy");
                    //var tdate1 = Convert.ToDateTime(FilterModel.ToDate).ToString("dd/MM/yyyy");
                    var fdate = Convert.ToDateTime(FilterModel.FromDate).Date;
                    var tdate = Convert.ToDateTime(FilterModel.ToDate).Date;

                    opdlist = opdlist.Where(x => x.ConsultationDate.Date >= fdate && x.ConsultationDate.Date <= tdate).ToList();
                    ViewBag.fdate = fdate;
                    ViewBag.tdate = tdate;
                }
                
                ViewBag.patientlist = opdlist;
                ViewBag.hospitallist = hospital;
                return View(opdlist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> MedicineReport(FilterModel FilterModel)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["RoleId"] == null || Session["RoleId"].ToString() == string.Empty)
            {
                return RedirectToAction("Login", "Home");
            }
            //long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
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
                var patientlist = await ViewHelper.GetAllPatient(null,null, null,null);
                //var medlist = await ViewHelper.GetAllMedicine(FilterModel.FromDate, FilterModel.ToDate, hospitalid);
                var medlist = await ViewHelper.GetAllPurchaseMedicine(null, null, hospitalid);
                var prelist = await medicinemanager.GetAllSalesReport(null, null, hospitalid);
                var result = new MedicineReportModel
                {
                    medicinelist = medlist,
                    salesreports = prelist.Where(x => x.Quantity != 0 && x.Rate != 0).ToList()
                };
                if (FilterModel.FromDate != null && FilterModel.ToDate != null)
                {
                    var fdate = Convert.ToDateTime(FilterModel.FromDate).Date;
                    var tdate = Convert.ToDateTime(FilterModel.ToDate).Date;
                    result.medicinelist = result.medicinelist.Where(x => x.PurchaseDate.Date >= fdate && x.PurchaseDate.Date <= tdate).ToList();
                    result.salesreports = result.salesreports.Where(x => x.Date.Date >= fdate && x.Date.Date <= tdate).ToList();
                    ViewBag.fdate = fdate;
                    ViewBag.tdate = tdate;
                }
                ViewBag.hospitallist = hospital;
                ViewBag.patientlist = patientlist;
                ViewBag.issales = FilterModel.IsSales;
                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> RevenueReport(FilterModel FilterModel)
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
                var medlist = await ViewHelper.GetAllMedicine(null,null, hospitalid);
                var medicincerevenuelist = await medicinemanager.GetAllMedicineReport(hospitalid ?? 0);
                var treatmentrevenuelist = await medicinemanager.GetAllTreatmentReport(hospitalid ?? 0);
                if (FilterModel.FromDate != null && FilterModel.ToDate != null)
                {
                    var fdate = Convert.ToDateTime(FilterModel.FromDate).Date;
                    var tdate = Convert.ToDateTime(FilterModel.ToDate).Date;
                    //var StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    //var EndDate = StartDate.AddMonths(1).AddDays(-1);
                    medicincerevenuelist = medicincerevenuelist.Where(x => x.Date >= fdate && x.Date <= tdate).ToList();
                    treatmentrevenuelist = treatmentrevenuelist.Where(x => x.Date >= fdate && x.Date <= tdate).ToList();
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
                        CompanyExpense = treatmentrevenuelist.Sum(x => x.CompanyExpense),
                        CompanyId = treatmentrevenuelist.First().CompanyId,
                        Date = DateTime.Now
                    };
                    treatmentrevenuelist.Add(fialresult);


                    var totalincome1 = medicincerevenuelist.Sum(x => x.Income) + medicincerevenuelist.Sum(x => x.OpeningBalance);
                    var totalreceipt1 = totalincome - medicincerevenuelist.Sum(x => x.PurchaseMedicine);
                    var fialresult1 = new MedicineRevenueReportModel
                    {
                        OpeningBalance = medicincerevenuelist.Sum(x => x.OpeningBalance),
                        Income = medicincerevenuelist.Sum(x => x.Income),
                        Receipt = medicincerevenuelist.Sum(x => x.Receipt),
                        Total = totalincome,
                        Profit = totalincome,
                        Date = DateTime.Now
                    };
                    medicincerevenuelist.Add(fialresult1);
                }
                else
                {
                    var StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    var EndDate = StartDate.AddMonths(1).AddDays(-1);
                    medicincerevenuelist = medicincerevenuelist.Where(x => x.Date >= StartDate &&
                    x.Date <= EndDate && x.HospitalId == hospitalid).ToList();
                    treatmentrevenuelist = treatmentrevenuelist.Where(x => x.Date >= StartDate &&
                     x.Date <= EndDate && x.CompanyId == hospitalid).ToList();
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
                        CompanyExpense = treatmentrevenuelist.Sum(x => x.CompanyExpense)
                    };
                    treatmentrevenuelist.Add(fialresult);

                    var totalincome1 = medicincerevenuelist.Sum(x => x.Income) + medicincerevenuelist.Sum(x => x.OpeningBalance);
                    var totalreceipt1 = totalincome1 - medicincerevenuelist.Sum(x => x.Receipt) - 
                        medicincerevenuelist.Sum(x => x.PurchaseMedicine);
                    var fialresult1 = new MedicineRevenueReportModel
                    {
                        OpeningBalance = medicincerevenuelist.Sum(x => x.OpeningBalance),
                        Income = medicincerevenuelist.Sum(x => x.Income),
                        Receipt = medicincerevenuelist.Sum(x => x.Receipt),
                        Total = totalincome1,
                        Profit = totalincome1,
                        Date = DateTime.Now
                    };
                    medicincerevenuelist.Add(fialresult1);
                }
                var result = new RevenueReportModel
                {
                    filterModel=FilterModel,
                    treatmentrevenue = treatmentrevenuelist,
                    medicinerevenue = medicincerevenuelist
                };
                //if (FilterModel.FromDate != null && FilterModel.ToDate != null)
                //{
                //    var fdate = Convert.ToDateTime(FilterModel.FromDate).Date;
                //    var tdate = Convert.ToDateTime(FilterModel.ToDate).Date;
                //    result.treatmentrevenue = result.treatmentrevenue.Where(x => x.Date.Date >= fdate && x.Date.Date <= tdate).ToList();
                //    result.medicinerevenue = result.medicinerevenue.Where(x => x.Date.Date >= fdate && x.Date.Date <= tdate).ToList();
                //    ViewBag.fdate = fdate;
                //    ViewBag.tdate = tdate;
                //}
                ViewBag.issales = FilterModel.IsSales;
                ViewBag.hospitallist = hospital;
                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> ShortRevenueReport(FilterModel FilterModel)
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
                var res = new List<TreatmentRevenueReportModel>();
                var res1 = new List<MedicineRevenueReportModel>();
                IMedicineManager medicinemanager = new MedicineManager();
                var trmtlist = await ViewHelper.GetAllTreatment();
                var medlist = await ViewHelper.GetAllMedicine(null, null, hospitalid);
                var treatmentrevenuelist = await medicinemanager.GetAllTreatmentReport(hospitalid ?? 0);
                var medicincerevenuelist = await medicinemanager.GetAllMedicineReport(hospitalid ?? 0);
                if (FilterModel.FromDate != null && FilterModel.ToDate != null)
                {
                    var fdate = Convert.ToDateTime(FilterModel.FromDate).Date;
                    var tdate = Convert.ToDateTime(FilterModel.ToDate).Date;
                    res = treatmentrevenuelist.GroupBy(l => l.Date)
                        .Select(cl => new TreatmentRevenueReportModel
                        {
                            Date = cl.First().Date,
                            OpeningBalance = cl.Sum(c => c.OpeningBalance),
                            Income = cl.Sum(c => c.Income),
                            Receipt = cl.Sum(c => c.Receipt),
                            Expense = cl.Sum(c => c.Expense),
                            Profit = cl.Sum(c => c.Profit),
                            CompanyId = cl.First().CompanyId
                        }).ToList();
                    res = res.Where(x => x.CompanyId == hospitalid).ToList();
                    res = res.Where(x => x.CompanyId == hospitalid).OrderBy(x => x.Date).ToList();
                    res = res.Where(x => x.Date.Date >= fdate && x.Date.Date <= tdate).OrderBy(x => x.Date).ToList();

                    var totalincome = res.Sum(x => x.Income) + res.Sum(x => x.OpeningBalance);
                    var totalexpense = res.Sum(x => x.Expense);
                    var totalreceipt = res.Sum(x => x.Receipt);
                    //res = res.Where(x => x.Date.Date >= fdate && x.Date.Date <= tdate).ToList();
                    var fialresult = new TreatmentRevenueReportModel
                    {
                        OpeningBalance = res.Sum(x => x.OpeningBalance),
                        Income = res.Sum(x => x.Income),
                        Receipt = res.Sum(x => x.Receipt),
                        Expense = res.Sum(x => x.Expense),
                        Total = totalincome,
                        Profit = totalincome - totalexpense - totalreceipt,
                        CompanyExpense = res.Sum(x => x.CompanyExpense),
                        Date = DateTime.Now
                    };
                    res.Add(fialresult);

                    //----------------------------------------
                    res1 = medicincerevenuelist.GroupBy(l => l.Date)
                        .Select(cl => new MedicineRevenueReportModel
                        {
                            Date = cl.First().Date,
                            OpeningBalance = cl.Sum(c => c.OpeningBalance),
                            Income = cl.Sum(c => c.Income),
                            Receipt = cl.Sum(c => c.Receipt),
                            PurchaseMedicine = cl.Sum(c => c.PurchaseMedicine),
                            Profit = cl.Sum(c => c.Profit),
                            HospitalId = cl.First().HospitalId
                        }).Where(x => x.HospitalId == hospitalid).OrderBy(x => x.Date).ToList();

                    res1 = res1.Where(x => x.Date.Date >= fdate && x.Date.Date <= tdate).ToList();

                    var totalincome1 = res1.Sum(x => x.Income) + res1.Sum(x => x.OpeningBalance);
                    var totalpurchasemedicine1 = res1.Sum(x => x.PurchaseMedicine);
                    var totalreceipt1 = res1.Sum(x => x.Receipt);
                    //res1 = res1.Where(x => x.Date.Date >= fdate && x.Date.Date <= tdate).ToList();
                    var fialresult1 = new MedicineRevenueReportModel
                    {
                        OpeningBalance = res1.Sum(x => x.OpeningBalance),
                        Income = res1.Sum(x => x.Income),
                        Receipt = res1.Sum(x => x.Receipt),
                        PurchaseMedicine = res1.Sum(x => x.PurchaseMedicine),
                        Total = totalincome1,
                        Profit = totalincome1 - totalpurchasemedicine1 - totalreceipt1,
                        Date = DateTime.Now
                    };
                    res1.Add(fialresult1);                   
                }
                else
                {
                    var StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    var EndDate = StartDate.AddMonths(1).AddDays(-1);
                    FilterModel.FromDate = StartDate.Date;
                    FilterModel.ToDate = EndDate.Date;
                    res = treatmentrevenuelist.GroupBy(l => l.Date)
                        .Select(cl => new TreatmentRevenueReportModel
                        {
                            Date = cl.First().Date,
                            OpeningBalance = cl.Sum(c => c.OpeningBalance),
                            Income = cl.Sum(c => c.Income),
                            Receipt = cl.Sum(c => c.Receipt),
                            Expense = cl.Sum(c => c.Expense),
                            Profit = cl.Sum(c => c.Profit),
                            CompanyId = cl.FirstOrDefault().CompanyId 
                        }).Where(x => x.Date.Date >= StartDate.Date && x.Date.Date <= EndDate.Date && x.CompanyId == hospitalid).OrderBy(x => x.Date).ToList();
                   

                    var totalincome = res.Where(x=>x.CompanyId == hospitalid).Sum(x => x.Income) + res.Sum(x => x.OpeningBalance);
                    var totalexpense = res.Where(x=>x.CompanyId == hospitalid).Sum(x => x.Expense);
                    var totalreceipt = res.Where(x=>x.CompanyId == hospitalid).Sum(x => x.Receipt);
                    var fialresult = new TreatmentRevenueReportModel
                    {
                        OpeningBalance = res.Sum(x => x.OpeningBalance),
                        Income = res.Sum(x => x.Income),
                        Receipt = res.Sum(x => x.Receipt),
                        Expense = res.Sum(x => x.Expense),
                        Total = totalincome,
                        Profit = totalincome - totalexpense - totalreceipt,
                        CompanyExpense = res.Sum(x => x.CompanyExpense),
                        Date = DateTime.Now
                    };
                    res.Add(fialresult);

                    //----------------------------------------------
                    res1 = medicincerevenuelist.GroupBy(l => l.Date)
                        .Select(cl => new MedicineRevenueReportModel
                        {     Date = cl.First().Date,
                            OpeningBalance = cl.Sum(c => c.OpeningBalance),
                            Income = cl.Sum(c => c.Income),
                            Receipt = cl.Sum(c => c.Receipt),
                            PurchaseMedicine = cl.Sum(c => c.PurchaseMedicine),
                            Profit = cl.Sum(c => c.Profit),
                            HospitalId = cl.FirstOrDefault().HospitalId,
                        }).Where(x => x.Date.Date >= StartDate.Date && x.Date.Date <= EndDate.Date && x.HospitalId == hospitalid).OrderBy(x => x.Date).ToList();

                    var totalincome1 = res1.Where(x=>x.HospitalId == hospitalid).Sum(x => x.Income) + res1.Sum(x => x.OpeningBalance);
                    var totalpurchasemedicine1 = res1.Where(x => x.HospitalId == hospitalid).Sum(x => x.PurchaseMedicine);
                    var totalreceipt1 = res1.Where(x => x.HospitalId == hospitalid).Sum(x => x.Receipt);
                    var fialresult1 = new MedicineRevenueReportModel
                    {
                        OpeningBalance = res1.Sum(x => x.OpeningBalance),
                        Income = res1.Sum(x => x.Income),
                        Receipt = res1.Sum(x => x.Receipt),
                        PurchaseMedicine = res1.Sum(x => x.PurchaseMedicine),
                        Total = totalincome1,
                        Profit = totalincome1 - totalpurchasemedicine1 - totalreceipt1,
                        Date = DateTime.Now
                    };
                    res1.Add(fialresult1);
                }
                var result = new RevenueReportModel
                {
                    filterModel = FilterModel,
                    treatmentrevenue = res,
                    medicinerevenue = res1
                };
                //if (FilterModel.FromDate != null && FilterModel.ToDate != null)
                //{
                //    var fdate = Convert.ToDateTime(FilterModel.FromDate).Date;
                //    var tdate = Convert.ToDateTime(FilterModel.ToDate).Date;
                //    result.treatmentrevenue = result.treatmentrevenue.Where(x => x.Date.Date >= fdate && x.Date.Date <= tdate).ToList();
                //    result.medicinerevenue = result.medicinerevenue.Where(x => x.Date.Date >= fdate && x.Date.Date <= tdate).ToList();
                //    ViewBag.fdate = fdate;
                //    ViewBag.tdate = tdate;
                //}
                ViewBag.issales = FilterModel.IsSales;
                ViewBag.hospitallist = hospital;
                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}