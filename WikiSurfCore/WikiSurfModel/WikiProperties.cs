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
    public class WikiProperties
    {
        [Key]
        public Guid WikiPropertiesId { get; set; }
        [ForeignKey("WikiPage")]
        public Guid WikiPageId { get; set; }
        public WikiPage WikiPage { get; set; }
        public string Name { get; set; }
        public string Additional { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime ImportDate { get; set; }
    }
}
