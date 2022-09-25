using SwasiHealthCare.Data.Entities;
using SwasiHealthCare.Helper;
using SwasiHealthCare.IBusinessManager;
using SwasiHealthCare.IRepository;
using SwasiHealthCare.Model;
using SwasiHealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.BusinessManager
{
    public class ReportManager : IReportManager
    {
        private IRepository<Patient> PatientRepository;

        public ReportManager()
        {
            this.PatientRepository = new Repository<Patient>();
        }
    }
}
