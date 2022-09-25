using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class PatientDocumentModel
    {
        public long? DocumentId { get; set; }
        public long? PatientId { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string PatientIdNumber { get; set; }
        public long? DocumentCreatedBy { get; set; }
        public DateTime DocumentCreatedDate { get; set; }
    }
}
