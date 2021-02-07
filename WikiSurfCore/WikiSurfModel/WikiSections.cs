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
    public class WikiSections
    {
        [NotMapped]
        private DateTime _importDate;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WikiSectionsId { get; set; }
        [ForeignKey("WikiPage")]
        public Guid WikiPageId { get; set; }
        public WikiPage WikiPage { get; set; }
        public int ToCLevel { get; set; }
        public string Level { get; set; }
        public string Line { get; set; }
        public string Number { get; set; }
        public string Index { get; set; }
        public string FromTitle { get; set; }
        public int ByteOffset { get; set; }
        public string Anchor { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime ImportDate { get; set; }
    }
}
