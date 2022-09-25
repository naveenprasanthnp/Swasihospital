using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Model
{
    public class NotesModel
    {
        public long NotesId { get; set; }
        public string Description { get; set; }
        public long? NotesCreatedBy { get; set; }
        public DateTime? NotesCreatedDate { get; set; }
        public string NotesCreatedByName { get; set; }
        public long? LoginUserId { get; set; }
    }
}
