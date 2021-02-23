using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WikiSurfModel;
using WikiSurfCore.RabbitMQ;
using WikiSurfModel.StoredProcedures;

namespace WikiSurfCore.Repositories
{
    public class WikiSurfRepository : IWikiSurfRepository
    {
        private WikiSurfContext _dbContext;
        public WikiSurfRepository(WikiSurfContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WikiSession GetNewSession()
        {
            var newSession = new WikiSession
            {
                PlayerName = "Test",
                CreatedDate = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                IsActive = true
            };
            _dbContext.WikiSessions.Add(newSession);
            _dbContext.SaveChanges();
            return newSession;
        }
        public List<WordBank> GetRandomWords(WikiSession aWikiSession, int count = 2)
        {
            var test = GetNewWords(aWikiSession);
            //var test = _dbContext.WordBanks
            //    //.Where(x => x.WordIndex == firstIndex || x.WordIndex == lastIndex)
            //    //.ToList()
            //    .Where(x => x.Word == "Endotherium" || x.Word == "Academy of Allied Health & Science")
            //    .ToList();

            _dbContext.Entry(aWikiSession).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return AddWordsToQueue(test.ToList(), aWikiSession);
        }

        public List<WordBank> AddWordsToQueue(List<WordBank> words, WikiSession aSession)
        {
            for (var i = 0; i < words.Count(); i++)
            {
                var wbId = words[i].WordBankId;
                var cached = _dbContext.WikiPages.FirstOrDefault(x => x.WordBankId == wbId);
                if (cached != null) continue;
                var wordBank = _dbContext.WordBanks.FirstOrDefault(x => x.WordBankId == wbId);
                var wbq = new WordBankQueue
                {
                    CreatedDate = DateTime.Now,
                    IsProcessed = false,
                    WordBankId = wbId,
                    WordBank = wordBank
                };
                
                _dbContext.WordBankQueues.Add(wbq);
                _dbContext.SaveChanges();
                _dbContext.Entry(wbq).Reload();
                
                wbq = RabbitHelper.Send(wbq, aSession); //Send items to RabbitMQ for processing
                
                words[i] = wbq.WordBank;
                words[i].WordBankQueues.Add(wbq);
            }

            return words;
        }

        public void AddWordToQueue(WordBank word, WikiSession aWikiSession)
        {
            this.AddWordsToQueue(new List<WordBank>(){word}, aWikiSession);
        }
        public WikiPage GetWikiPage(WordBank a_wordBank)
        {
            var wikiPage = _dbContext.WikiPages.FirstOrDefault(x => x.WordBankId == a_wordBank.WordBankId);
            return wikiPage;
        }

        public WikiPage GetWikiPage(Guid aWikiLinkId, string aWord, WikiSession aWikiSession)
        {
            var wikiLink =
                _dbContext.WikiLinks.FirstOrDefault((x => x.WikiLinksId == aWikiLinkId && x.Additional == aWord));
            if (wikiLink == null)
            {
                return null;
            }

            var wordBank = _dbContext.WordBanks.FirstOrDefault(x => x.Word == aWord);
            AddWordToQueue(wordBank, aWikiSession);
            var wikiPage = _dbContext.WikiPages.FirstOrDefault(x => x.WordBankId == wordBank.WordBankId);
            aWikiSession.CurrentWordBankId = wordBank.WordBankId;
            _dbContext.Entry(aWikiSession).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return wikiPage;
        }

        public List<WordBank> GetNewWords(WikiSession session)
        {
            var sessionIdParamter = new SqlParameter("@WikiSessionId",session.WikiSessionId);
            var result = _dbContext
                .Database
                .SqlQuery<S_New_Game_Words>("EXEC S_NEW_GAME_WORDS @WikiSessionId", sessionIdParamter)
                .ToList();
            var res = new List<WordBank>();
            foreach(var a in result)
            {
                res.Add(a.WordBank());
            }
            return res;
        }

        public WikiSession GetExistingSession(Guid aSessionId)
        {

            var session = _dbContext.WikiSessions.FirstOrDefault(x => x.WikiSessionId == aSessionId);

            return session;
        }

        public WikiSession IncreaseClick(WikiSession session)
        {
            var dbEntry = _dbContext.WikiSessions.First(s=>s.WikiSessionId == session.WikiSessionId);
            dbEntry.TotalClicks += 1;
            _dbContext.Entry(dbEntry).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return dbEntry;
        }
    }
}
