using SwasiHealthCare.BusinessManager;
using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.IBusinessManager;
using SwasiHealthCare.Model;
using SwasiHealthCare.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static SwasiHealthCare.Service.FilterConfig;
namespace SwasiHealthCare.Service.Controllers
{
    public class HospitalController : Controller
    {
        public HospitalController()
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
                var hospitallist = await ViewHelper.GetAllHospital();
                return View(hospitallist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewHospital()
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
                IHospitalManager hospitalManager = new HospitalManager();
                return View(new Model.HospitalModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> NewHospital(HospitalModel hospitalModel)
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
                IHospitalManager hospitalManager = new HospitalManager();
                hospitalModel.HospitalCreatedBy = Convert.ToInt64(Session["UserId"]);
                hospitalModel.HospitalStatus = true;
                hospitalModel.Mode = "A";
                var result = await hospitalManager.AddHospital(hospitalModel);
                return RedirectToAction("Index", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditHospital(long hospitalid)
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
                IHospitalManager hospitalManager = new HospitalManager();
                var hospital = (await hospitalManager.GetEditHospital(hospitalid));
                return View(hospital);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditHospital(Model.HospitalModel hospitalModel)
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
                IHospitalManager hospitalManager = new HospitalManager();
                hospitalModel.HospitalModifiedBy = Convert.ToInt64(Session["UserId"]);
                hospitalModel.Mode = "E";
                await hospitalManager.AddHospital(hospitalModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteHospital(long hospitalid)
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
                IHospitalManager hospitalManager = new HospitalManager();
                var result = await hospitalManager.DeleteHospital(hospitalid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> HospitalStatus(long hospitalid, bool status)
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
                IHospitalManager hospitalManager = new HospitalManager();
                var result = await hospitalManager.UpdateHospitalStatus(hospitalid, status, Convert.ToInt64(Session["UserId"]));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}