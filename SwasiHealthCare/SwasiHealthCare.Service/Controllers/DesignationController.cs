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
    public class DesignationController : Controller
    {
        public DesignationController()
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
                var designationlist = await ViewHelper.GetAllDesignation();
                return View(designationlist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost] 
        public async Task<ActionResult> NewDesignation(DesignationModel designationModel)
        {
            try
            {
                IDesignationManager designationManager = new DesignationManager();
                designationModel.DesignationCreatedBy = Convert.ToInt64(Session["UserId"]);
                designationModel.DesignationStatus = true;
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                if (designationModel.DesignationId != null)
                {
                    designationModel.HospitalId = hospitalid;
                    designationModel.Mode = "E";
                }
                else
                {
                    
                    designationModel.HospitalId = hospitalid;
                    designationModel.Mode = "A";
                }
                var result = await designationManager.AddNewDesignation(designationModel);
                return Json(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditDesignation(long designationid)
        {
            try
            {
                IDesignationManager designationManager = new DesignationManager();
                var designation = (await designationManager.GetEditDesignation(designationid));
                return View(designation);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditDesignation(DesignationModel designationModel)
        {
            try
            {
                IDesignationManager designationManager = new DesignationManager();
                designationModel.DesignationModifiedBy = Convert.ToInt64(Session["UserId"]);
                designationModel.Mode = "E";
                await designationManager.AddNewDesignation(designationModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDesignation(long designationid)
        {
            try
            {
                IDesignationManager designationManager = new DesignationManager();
                var result = await designationManager.DeleteDesignation(designationid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DesignationStatus(long designationid, bool status)
        {
            try
            {
                IDesignationManager designationManager = new DesignationManager();
                var result = await designationManager.UpdateDesignationStatus(designationid, status, Convert.ToInt64(Session["UserId"]));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}