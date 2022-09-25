using SwasiHealthCare.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Data.EF.Core.DbContexts;

namespace SwasiHealthCare.Data.DbContext
{
    public class SwasiHealthCareDbConext : DbContextBase
    {
        public SwasiHealthCareDbConext()
          : base("SwasiHealthCareEntities")
        {
            this.SetCommandTimeOut(450);
            Database.SetInitializer<SwasiHealthCareDbConext>(null);
        }
        public void SetCommandTimeOut(int Timeout)
        {
            var objectContext = (this as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = Timeout;
        }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Entities.System> Systems { get; set; }
        public DbSet<Templates> Templates { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<OPDConsoltation> OPDConsoltation { get; set; }
        public DbSet<OPDPrescription> OPDPrescription { get; set; }
        public DbSet<OPDServices> OPDServices { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<PatientDocument> PatientDocuments { get; set; }
        public DbSet<AccessRights> AccessRights { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<SubMenu> SubMenu { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<PurchaseMedicine> PurchaseMedicines { get; set; }
        public DbSet<TreatmentOpeningBalance> TreatmentOpeningBalance { get; set; }
        public DbSet<MedicineOpeningBalance> MedicineOpeningBalance { get; set; }
        public DbSet<TreatmentReceipt> TreatmentReceipt { get; set; }
        public DbSet<MedicineReceipt> MedicineReceipt { get; set; }
        public DbSet<PatientPlanSubscription> PatientPlanSubscription { get; set; }
    }
}
