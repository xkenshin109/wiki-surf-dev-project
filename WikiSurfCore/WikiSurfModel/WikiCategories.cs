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
    public class WikiCategories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WikiCategoriesId { get; set; }
        [ForeignKey("WikiPage")]
        public Guid WikiPageId { get; set; }
        public WikiPage WikiPage { get; set; }
        public string SortKey { get; set; }
        public string Hidden { get; set; }
        public string Additional { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime ImportDate { get; set; }
    }
}
