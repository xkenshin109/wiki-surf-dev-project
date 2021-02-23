using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiSurfModel
{
    /// <summary>
    /// <see cref="WikiSession"/>
    /// Tracks a session for a player
    /// Used to store client side data for WikiSurf
    /// </summary>
    [DebuggerDisplay("SessionId: {WikiSessionId}")]
    public class WikiSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WikiSessionId { get; set; }
        public string PlayerName { get; set; }
        public int TotalClicks { get; set; }
        public int TotalWarnings { get; set; }
        [DefaultValue(1)]
        public bool IsActive { get; set; }
        public Guid? CurrentWordBankId { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime UpdateDateTime { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<WikiSessionWordBank> WikiSessionWordBanks { get; set; }

        public WikiSession()
        {
            WikiSessionWordBanks = new List<WikiSessionWordBank>();
        }


    }
}
