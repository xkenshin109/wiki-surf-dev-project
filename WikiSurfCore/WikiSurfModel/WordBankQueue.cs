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
    [Serializable]
    public partial class WordBankQueue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WordBankQueueId { get; set; }
        [ForeignKey("WordBank")]
        public Guid WordBankId { get; set; }
        public virtual WordBank WordBank { get; set; }
        public bool IsProcessed { get; set; }
        [DefaultValue("getutcdate()")]
        public DateTime CreatedDate { get; set; }

        public DateTime? ProcessedDate { get; set; }

    }
}
