using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiSurfModel
{
    public class WikiLanguages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WikiLanguagesId { get; set; }
        [ForeignKey("WikiPage")]
        public Guid WikiPageId { get; set; }
        public WikiPage WikiPage { get; set; }
        public string Language { get; set; }
        public string Url { get; set; }
        public string LanguageName { get; set; }
        public string Autonym { get; set; }
        public string Add { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime ImportDate { get; set; }
    }
}
