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
    public class UserController : Controller
    {
        public UserController()
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
            long? UserId = Convert.ToInt64(Session["UserId"].ToString());
            long? RoleId = Convert.ToInt64(Session["RoleId"].ToString());
            try
            {
                var userList = new List<UserModel>();
                if (RoleId == 1)
                {
                    userList = (await ViewHelper.GetAllUsers()).ToList();
                }
                else
                {
                    long? HospitalId = Convert.ToInt64(Session["HospitalId"].ToString());
                    userList = (await ViewHelper.GetAllUsers()).Where(x => x.HospitalId == HospitalId
                    && x.RoleId != 2).ToList();
                }
                return View(userList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> DoctorIndex()
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
                var userList = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId == 2).ToList();
                return View(userList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewUser()
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
                long roleid = Convert.ToInt64(Session["RoleId"]);
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                IUserManager userManager = new UserManager();
                List<Roles> role = (await userManager.GetRoles()).Where(x => roleid == 2 ? (x.RoleId != 1 && x.RoleId !=2) : x.RoleId == 5).ToList();
                List<Hospital> hospital = new List<Hospital>(); 
                if(roleid == 1)
                {
                    hospital = (await userManager.GetHospitals()).ToList();
                }
                else
                {
                    hospital = (await userManager.GetHospitals()).Where(x => x.HospitalId == hospitalid).ToList();
                }
                List<DesignationModel> desigation = (await ViewHelper.GetAllDesignation()).Where(x => x.DesignationName != "Doctor").ToList();
                List<SystemViewModel> systemlist = (await ViewHelper.GetAllSystem()).ToList();
                ViewBag.roletypes = role;
                ViewBag.designation = desigation;
                ViewBag.hospitallist = hospital;
                ViewBag.systemlist = systemlist;
                ViewBag.roleid = roleid;
                return View(new Model.UserModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> NewUser(UserModel userModel)
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
                userModel.UserCreatedBy = Convert.ToInt64(Session["UserId"]);
                userModel.UserStatus = true;
                userModel.Mode = "A";
                var result = await userManager.SaveUser(userModel, rolid);
                return RedirectToAction("Index", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> NewDoctor()
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
                List<Roles> role = (await userManager.GetRoles()).Where(x => x.RoleId == 2).ToList();
                List<Hospital> hospital = (await userManager.GetHospitals()).ToList();
                ViewBag.roletypes = role;
                ViewBag.hospitallist = hospital;
                return View(new Model.UserModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> NewDoctor(UserModel userModel)
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
                userModel.UserCreatedBy = Convert.ToInt64(Session["UserId"]);
                userModel.UserStatus = true;
                userModel.Mode = "A";
                var result = await userManager.SaveUser(userModel,rolid);
                return RedirectToAction("Index", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditUser(long UserId)
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
                long roleid = Convert.ToInt64(Session["RoleId"]);
                IUserManager userManager = new UserManager();
                List<Roles> role = (await userManager.GetRoles()).Where(x => roleid == 2 ? (x.RoleId != 1 && x.RoleId != 2) : x.RoleId == 5).ToList();
                List<Hospital> hospital = (await userManager.GetHospitals()).Where(x=>x.HospitalId == hospitalid).ToList();
                List<DesignationModel> desigation = (await ViewHelper.GetAllDesignation()).Where(x => x.DesignationName != "Doctor").ToList();
                List<SystemViewModel> systemlist = (await ViewHelper.GetAllSystem()).ToList();
                ViewBag.roletypes = role;
                ViewBag.designation = desigation;
                ViewBag.hospitallist = hospital;
                ViewBag.systemlist = systemlist;
                long rolid = Convert.ToInt64(Session["RoleId"]);
                ViewBag.roleid = rolid;
                var users = (await userManager.GetEditUser(UserId));
                return View(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(Model.UserModel userModel)
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
                await userManager.SaveUser(userModel, rolid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Gzip]
        public async Task<ActionResult> EditDoctor(long UserId)
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
                List<Hospital> hospital = (await userManager.GetHospitals()).ToList();
                ViewBag.roletypes = role;
                ViewBag.hospitallist = hospital;
                var users = (await userManager.GetEditUser(UserId));
                return View(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditDoctor(Model.UserModel userModel)
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
                await userManager.SaveUser(userModel, rolid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(long userid)
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
                var result = await userManager.DeleteUser(userid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> UserStatus(long userid, bool status)
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
                var result = await userManager.UpdateUserStatus(userid, status, Convert.ToInt64(Session["UserId"]));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}