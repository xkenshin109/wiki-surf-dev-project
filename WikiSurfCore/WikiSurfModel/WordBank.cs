using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WikiSurfModel
{
    [Serializable]
    [DebuggerDisplay("Word: {Word} {WordBankId}")]
    public partial class WordBank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WordBankId { get; set; }
        public string Word { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WordIndex { get; set; }
        public bool Exclude { get; set; }
        public virtual ICollection<WordBankQueue> WordBankQueues { get; set; }
        public virtual ICollection<WordBankXref> WordBankXrefs { get; set; }
        public WordBank()
        {
            WordBankQueues = new List<WordBankQueue>();
        }

        public static WordBank AddWord(string word)
        {
            return new WordBank
            {
                Word = word
            };
        }
    }

    public class WordBankXref
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WordBankXrefId { get; set; }
        public Guid WordBankId { get; set; }
        public virtual WordBank WordBank { get; set; }
        public string Word { get; set; }
    }
}
