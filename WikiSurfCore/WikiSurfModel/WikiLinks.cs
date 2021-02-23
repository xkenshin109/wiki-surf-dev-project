using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiSurfModel.Utils;

namespace WikiSurfModel
{
    public class WikiLinks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WikiLinksId { get; set; }
        [ForeignKey("WikiPage")]
        public Guid WikiPageId { get; set; }
        public WikiPage WikiPage { get; set; }
        public string Ns { get; set; }
        public string Exists { get; set; }
        public string Additional { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime ImportDate { get; set; }
        [NotMapped]
        public string WordHtmlEncoded{
            get
            {
                return HtmlUtils.HtmlDecode(Additional);
            }

        }

        [NotMapped]
        public string WordHtmlDecoded
        {
            get
            {
                return HtmlUtils.HtmlDecode(WordHtmlEncoded);
            }
        }
    }
}
