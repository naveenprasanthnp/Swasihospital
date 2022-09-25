using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwasiHealthCare.Data.Entities
{
    [Table("Templates", Schema = "dbo")]
    public class Templates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string SMSContent { get; set; }
    }
}
