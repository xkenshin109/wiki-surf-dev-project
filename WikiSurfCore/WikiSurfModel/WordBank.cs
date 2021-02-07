using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
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

        public WordBank()
        {
        }

        public static WordBank AddWord(string word)
        {
            return new WordBank
            {
                Word = word
            };
        }
    }
}
