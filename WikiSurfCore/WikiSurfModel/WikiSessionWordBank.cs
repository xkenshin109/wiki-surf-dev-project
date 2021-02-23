using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiSurfModel
{
    /// <summary>
    /// <see cref="WikiSessionWordBank"></see>
    /// <see cref="WikiSession"/> one to many relationship <see cref="WordBank"/>
    /// </summary>
    public class WikiSessionWordBank
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("WikiSession")]
        public Guid WikiSessionId { get; set; }
        public virtual WikiSession WikiSession { get; set; }
        [ForeignKey("WordBank")]
        public Guid WordBankId { get; set; }
        public virtual WordBank WordBank { get; set; }
    }
}
