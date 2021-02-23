using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WikiSurfModel.StoredProcedures
{
    public partial class S_New_Game_Words
    {
        public Guid WordBankId { get; set; }
        public string Word { get; set; }
        public long WordIndex { get; set; }
        public bool Exclude { get; set; }
        public S_New_Game_Words()
        {
            
        }

        public WordBank WordBank( ){
            return new WordBank
            {
                WordBankId = WordBankId,
                Word = Word,
                WordIndex = WordIndex,
                Exclude = Exclude
            };
        }
    }
}
