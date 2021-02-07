using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiSurfModel;

namespace WikiSurfCore.Repositories
{
    public interface IWikiSurfRepository
    {
        List<WordBank> GetRandomWords(int count = 2);

        void AddWordsToQueue(List<WordBank> words);

        WikiPage GetWikiPage(WordBank a_wordBank);

        WikiPage GetWikiPage(Guid aWikiLinkId, string aWord);
    }
}
