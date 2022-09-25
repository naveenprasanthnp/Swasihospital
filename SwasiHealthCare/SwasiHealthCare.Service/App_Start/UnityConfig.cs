using SwasiHealthCare.BusinessManager;
using SwasiHealthCare.IBusinessManager;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace SwasiHealthCare.Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUserManager, UserManager>();
            container.RegisterType<IRoleManager, RoleManager>();
            container.RegisterType<IPatientManager, PatientManager>();
            container.RegisterType<ISystemManager, SystemManager>();
            container.RegisterType<IHospitalManager, HospitalManager>();
            container.RegisterType<ITreatmentManager, TreatmentManager>();
            container.RegisterType<IMedicineManager, MedicineManager>();
            container.RegisterType<IDesignationManager, DesignationManager>();
            container.RegisterType<IReportManager, ReportManager>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}