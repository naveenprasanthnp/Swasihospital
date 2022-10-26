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
    public class TeleComController : Controller
    {
        // GET: TeleCom
        public TeleComController()
        {

        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewTeleComPatient()
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
                List<PatientModel> patientlist = (await ViewHelper.GetAllPatients()).Where(x => x.PatientHospitalId == hospitalid).ToList();
                ViewBag.patientlist = patientlist;
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Gzip]
        public async Task<ActionResult> NewTeleComPatient(TeleComPatient teleComPatient)
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}