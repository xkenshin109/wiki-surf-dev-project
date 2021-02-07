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
    public class WikiPage
    {
        private string _htlmFormatted;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WikiPageId { get; set; }
        [ForeignKey("WordBank")]
        public Guid WordBankId { get; set; }
        public virtual WordBank WordBank { get; set; }
        public long PageId { get; set; }
        public long RevId { get; set; }
        public string Title { get; set; }
        public string DisplayTitle { get; set; }
        public string PlainTextContent { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime ImportDate { get; set; }
        public virtual ICollection<WikiLanguages> Languages { get; set; }
        public virtual ICollection<WikiCategories> Categories { get; set; }
        public virtual ICollection<WikiLinks> Links { get; set; }
        public virtual ICollection<WikiSections> Sections { get; set; }
        public virtual ICollection<WikiProperties> Properties { get; set; }
        public WikiPage()
        {
            Languages = new List<WikiLanguages>();
            Categories = new List<WikiCategories>();
            Links = new List<WikiLinks>();
            Sections = new List<WikiSections>();
            Properties = new List<WikiProperties>();
        }
        [NotMapped]
        public string HtlmFormatted { get
            {
                if (string.IsNullOrEmpty(_htlmFormatted))
                {
                    _htlmFormatted = PlainTextContent;
                    foreach (WikiLinks link in Links)
                    {
                        if (_htlmFormatted.Contains((link.Additional)))
                        {
                            _htlmFormatted = _htlmFormatted.Replace(link.Additional, $"<a href=\"javascript:userClicked('{link.Additional}','{link.WikiLinksId}')\"><span>{HtmlUtils.HtmlEncode(link.Additional)}</span></a>");
                        }
                        
                    }
                }

                return _htlmFormatted;
            } }
    }
}
