using Newtonsoft.Json;
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
    public class AccessRightController : Controller
    {
        // GET: AccessRight
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Gzip]
        public async Task<ActionResult> CreateRights(long? userid,bool? dropdowncall)
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
                var menulist = await ViewHelper.GetAllMenu();
                var result = new List<RightsManagementModel>();
                // var rightslist = (await ViewHelper.GetAllAccessRights(userid)).ToList();
                foreach (var item in menulist)
                {
                    var rightslist = (await ViewHelper.GetAllAccessRights(userid)).Where(x => x.MenuId == item.MenuId).FirstOrDefault();
                    var list = new RightsManagementModel
                    {
                        MenuId = item.MenuId,
                        MenuName = item.MenuName,
                        IsCreate = rightslist?.IsCreate ?? false,
                        IsView = rightslist?.IsView ?? false,
                        IsDelete = rightslist?.IsDelete ?? false,
                        IsEdit = rightslist?.IsEdit ?? false,
                        DesignationId = rightslist?.DesignationId ?? 0,
                        AccessRightsId = rightslist?.AccessRightsId ?? 0
                    };
                    result.Add(list);
                }
                var userlist = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId != 1 && x.RoleId != 2
                && x.HospitalId == hospitalid).ToList();
                ViewBag.userlist = userlist;
                if(dropdowncall == false || dropdowncall ==null)
                {
                    //if(userid != null && dropdowncall != null)
                    //{
                    //    ViewBag.successmessage = "Update successfully";
                    //}
                    return View(result);
                }
                else
                {
                    return Json(userid, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [Gzip]
        public async Task<ActionResult> CreateRights(string docsToDelete,long? staffid)
        {
            try
            {
                long? hospitalid = Convert.ToInt64(Session["HospitalId"]);
                string[] arrayDocs = docsToDelete.Split(',');
                string[] arrayDocs1 = arrayDocs.Skip(4).ToArray();
                string[] arrayDocs2= arrayDocs1.Take(arrayDocs1.Count() - 1).ToArray();
                var list = new List<AccessRightsModel>();
                var d = new AccessRightsModel();
                int i = 1;
                foreach (string docId in arrayDocs2)
                {
                    if (i <= 4)
                    {
                        var data = docId.Split('-');
                        var len = data[0].Trim();
                        var l = len.Length;
                        var s = "";
                        if (l != 1 && l != 2 && l!=3)
                        {
                             s = data[0].Remove(data[0].Trim().Length - 5);
                             data[0] = s;
                        }
                        else
                        {
                            s = data[0];
                        }
                        d.MenuId = Convert.ToInt64(data[0]);
                        d.UserId = staffid ?? 0;
                        if(i==1)
                        {
                            d.IsCreate = Convert.ToBoolean(data[1]);
                        }
                        if (i == 2)
                        {
                            d.IsView = Convert.ToBoolean(data[1]);
                        }
                        if (i == 3)
                        {
                            d.IsEdit = Convert.ToBoolean(data[1]);
                        }
                        if (i == 4)
                        {
                            d.IsDelete = Convert.ToBoolean(data[1]);
                            
                            list.Add(d);
                            d = new AccessRightsModel();
                        }
                        if (i == 4)
                        {
                            i = 1;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
                IAccessRightManager accessright = new AccessRightManager();
                var userlist = (await ViewHelper.GetAllUsers()).Where(x => x.RoleId != 1 && x.RoleId != 2
                && x.HospitalId == hospitalid).ToList();
                ViewBag.userlist = userlist;
                var result = await accessright.AddAccessRights(list, hospitalid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return View(new RightsManagementModel());
            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }
}