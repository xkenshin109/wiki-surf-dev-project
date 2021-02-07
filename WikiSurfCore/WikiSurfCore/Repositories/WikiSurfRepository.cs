using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiSurfModel;
using WikiSurfCore.RabbitMQ;

namespace WikiSurfCore.Repositories
{
    public class WikiSurfRepository : IWikiSurfRepository
    {
        private WikiSurfContext _dbContext;
        public WikiSurfRepository(WikiSurfContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<WordBank> GetRandomWords(int count = 2)
        {
            var r = new Random();
            var iMin = (int)_dbContext.WordBanks.Min(x => x.WordIndex);
            var iMax = (int)_dbContext.WordBanks.Max(x => x.WordIndex);
            var firstIndex = r.Next(iMin, iMax);
            var lastIndex = r.Next(iMin, iMax);
            var test = _dbContext.WordBanks
                .Where(x => x.WordIndex == firstIndex || x.WordIndex == lastIndex);
            AddWordsToQueue(test.ToList());
            
            return test.ToList();
        }

        public void AddWordsToQueue(List<WordBank> words)
        {
            
            for (var i = 0; i < words.Count(); i++)
            {
                var wbId = words[i].WordBankId;
                var cached = _dbContext.WikiPages.FirstOrDefault(x => x.WordBankId == wbId);
                if (cached != null) continue;
                var wbq = new WordBankQueue
                {
                    CreatedDate = DateTime.Now,
                    IsProcessed = false,
                    WordBank = words[i]
                };
                _dbContext.WordBankQueues.Add(wbq);
                _dbContext.SaveChanges();
                RabbitHelper.Send(wbq); //Send items to RabbitMQ for processing
                _dbContext.Entry(wbq).Reload();

            }
           
            
        }

        public void AddWordToQueue(WordBank word)
        {
            this.AddWordsToQueue(new List<WordBank>(){word});
        }
        public WikiPage GetWikiPage(WordBank a_wordBank)
        {
            var wikiPage = _dbContext.WikiPages.FirstOrDefault(x => x.WordBankId == a_wordBank.WordBankId);
            return wikiPage;
        }

        public WikiPage GetWikiPage(Guid aWikiLinkId, string aWord)
        {
            var wikiLink =
                _dbContext.WikiLinks.FirstOrDefault((x => x.WikiLinksId == aWikiLinkId && x.Additional == aWord));
            if (wikiLink == null)
            {
                return null;
            }

            var wordBank = _dbContext.WordBanks.FirstOrDefault(x => x.Word == aWord);
            AddWordToQueue(wordBank);
            var wikiPage = _dbContext.WikiPages.FirstOrDefault(x => x.WordBankId == wordBank.WordBankId);
            return wikiPage;
        }
    }
}
