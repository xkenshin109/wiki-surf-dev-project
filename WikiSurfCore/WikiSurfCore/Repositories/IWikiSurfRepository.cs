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
        WikiSession GetNewSession();

        List<WordBank> GetRandomWords(WikiSession aWikiSession, int count = 2);

        List<WordBank> AddWordsToQueue(List<WordBank> words, WikiSession aWikiSession);

        WikiPage GetWikiPage(WordBank a_wordBank);

        WikiPage GetWikiPage(Guid aWikiLinkId, string aWord, WikiSession aWikiSession);

        List<WordBank> GetNewWords(WikiSession session);

        WikiSession GetExistingSession(Guid aSessionId);

        WikiSession IncreaseClick(WikiSession session);
    }
}
