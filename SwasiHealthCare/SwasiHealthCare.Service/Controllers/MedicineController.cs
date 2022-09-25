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
    public class MedicineController : Controller
    {

        private static Random random;
        public MedicineController()
        {

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
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                var medicinelist = await ViewHelper.GetAllMedicine(null,null, hospitalid);
                return View(medicinelist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> PurchaseMedicineIndex()
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
                var medicinelist = await ViewHelper.GetAllPurchaseMedicine(null, null, hospitalid);
                medicinelist = medicinelist.OrderByDescending(x => x.PurchaseMedicineCreatedDate).ToList();
                return View(medicinelist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewMedicine()
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
                IMedicineManager medicineManager = new MedicineManager();
                return View(new MedicineModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewMedicine(MedicineModel medicineModel)
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
                IMedicineManager medicineManager = new MedicineManager();
                medicineModel.MedicineCreatedBy = Convert.ToInt64(Session["UserId"]);
                medicineModel.MedicineStatus = true;
                medicineModel.Mode = "A";
                medicineModel.HospitalId = hospitalid;
                var result = await medicineManager.AddNewMedicine(medicineModel);
                return RedirectToAction("Index", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditMedicine(long medicineid)
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
                IMedicineManager medicineManager = new MedicineManager();
                var medicine = (await medicineManager.GetEditMedicine(medicineid));
                return View(medicine);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> GetMedicineById(long medicineid)
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
                IMedicineManager medicineManager = new MedicineManager();
                var medicine = (await medicineManager.GetEditMedicine(medicineid));
                return Json(medicine,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditMedicine(MedicineModel medicineModel)
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
                IMedicineManager medicineManager = new MedicineManager();
                medicineModel.MedicineModifiedBy = Convert.ToInt64(Session["UserId"]);
                medicineModel.Mode = "E";
                await medicineManager.AddNewMedicine(medicineModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMedicine(long medicineid)
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
                IMedicineManager medicineManager = new MedicineManager();
                var result = await medicineManager.DeleteMedicine(medicineid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> MedicineStatus(long medicineid, bool status)
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
                IMedicineManager medicineManager = new MedicineManager();
                var result = await medicineManager.UpdateMedicineStatus(medicineid, status, Convert.ToInt64(Session["UserId"]));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> PurchaseNewMedicine()
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
                IMedicineManager medicineManager = new MedicineManager();
                var medicinelist = (await ViewHelper.GetAllMedicine(null, null, hospitalid)).ToList();
                ViewBag.medicinelist = medicinelist;
                return View(new PurchaseMedicineModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PurchaseNewMedicine(PurchaseMedicineModel medicineModel)
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
                IMedicineManager medicineManager = new MedicineManager();
                medicineModel.PurchaseMedicineCreatedBy = Convert.ToInt64(Session["UserId"]);
                medicineModel.Mode = "A";
                medicineModel.HospitalId = hospitalid;
                var result = await medicineManager.AddNewPurchaseMedicine(medicineModel);
                return RedirectToAction("PurchaseMedicineIndex", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> PurchaseEditMedicine(long purchasemedicineid)
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
                IMedicineManager medicineManager = new MedicineManager();
                var medicine = (await medicineManager.GetEditPurchaseMedicine(purchasemedicineid));
                var medicinelist = (await ViewHelper.GetAllMedicine(null, null, hospitalid)).ToList();
                ViewBag.medicinelist = medicinelist;
                return View(medicine);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> PurchaseEditMedicine(PurchaseMedicineModel medicineModel)
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
                IMedicineManager medicineManager = new MedicineManager();
                medicineModel.PurchaseMedicineModifiedBy = Convert.ToInt64(Session["UserId"]);
                medicineModel.Mode = "E";
                medicineModel.HospitalId = hospitalid;
                await medicineManager.AddNewPurchaseMedicine(medicineModel);
                return RedirectToAction("PurchaseMedicineIndex");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> PurchaseDeleteMedicine(long purchasemedicineid)
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
                IMedicineManager medicineManager = new MedicineManager();
                var result = await medicineManager.DeletePurchaseMedicine(purchasemedicineid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> IndexExpense()
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
                var medicinelist = await ViewHelper.GetAllExpense();
                medicinelist = medicinelist.OrderByDescending(x => x.ExpenseCreatedDate).Where(x => x.HospitalId == hospitalid).ToList();
                return View(medicinelist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> AllExpense()
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
                var medicinelist = await ViewHelper.GetAllExpense();
                medicinelist = medicinelist.OrderByDescending(x => x.ExpenseDate).ToList();
               //medicinelist = medicinelist.OrderByDescending(x => x.ExpenseCreatedDate).Where(x => x.HospitalId == hospitalid).ToList();
                return View(medicinelist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewExpense()
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
                IMedicineManager medicineManager = new MedicineManager();
                return View(new ExpenseModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewExpense(ExpenseModel  expenseModel)
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
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    int fileSize = file.ContentLength;
                    if (fileSize > 0)
                    {
                        Random random = new Random();
                        var fn = random.Next(0001, 1000);
                        //var fileName = Path.GetFileName(file.FileName);
                        FileInfo fileinfo = new FileInfo(file.FileName);
                        var fileName = "BC" + fn.ToString() + fileinfo.Extension;
                        byte[] fileByteArray = new byte[fileSize];
                        file.InputStream.Read(fileByteArray, 0, fileSize);
                        string fileLocation = Path.Combine(Server.MapPath("~/Content/Files/ExpenseBills"), fileName);
                        if (!Directory.Exists(Path.Combine(Server.MapPath("~/Content/Files"))))
                        Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Content/Files")));
                        file.SaveAs(fileLocation);
                        expenseModel.BillCopyFileName = fileName;
                    }
                }
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                IMedicineManager medicineManager = new MedicineManager();
                expenseModel.ExpenseCreatedBy = Convert.ToInt64(Session["UserId"]);
                expenseModel.ExpenseStatus = true;
                expenseModel.Mode = "A";
                expenseModel.HospitalId = hospitalid;
                var result = await medicineManager.AddNewExpense(expenseModel);
                return RedirectToAction("IndexExpense", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditExpense(long expenseid)
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
                IMedicineManager medicineManager = new MedicineManager();
                var medicine = (await medicineManager.GetEditExpense(expenseid));
                return View(medicine);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditExpense(ExpenseModel  expenseModel)
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
                IMedicineManager medicineManager = new MedicineManager();
                expenseModel.ExpenseModifiedBy = Convert.ToInt64(Session["UserId"]);
                expenseModel.Mode = "E";
                await medicineManager.AddNewExpense(expenseModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteExpense(long expenseid)
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
                IMedicineManager medicineManager = new MedicineManager();
                var result = await medicineManager.DeleteExpense(expenseid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ExpenseStatus(long expenseid, bool status)
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
                IMedicineManager medicineManager = new MedicineManager();
                var result = await medicineManager.UpdateExpenseStatus(expenseid, status, Convert.ToInt64(Session["UserId"]));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FileStreamResult DownloadFile(string path)
        {
            var filename = System.IO.Path.GetFileName(Server.MapPath("~/Content/Files/ExpenseBills/" + path));
            var mimeType = "application/octet-stream";
            var stream = System.IO.File.OpenRead(Server.MapPath("~/Content/Files/ExpenseBills/" + path));
            return new FileStreamResult(stream, mimeType) { FileDownloadName = filename };
        }
    }
}