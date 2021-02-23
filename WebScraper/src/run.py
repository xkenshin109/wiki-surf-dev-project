
from wikimodels import pages, wordBankQueue
import requests
import json
import python_sql
class WikiProcess:
    
    def __init__(self):
        self.config = {}
        self.processing = False
        with open('./config.json') as f:
            self.config = json.load(f)
    def setDatabase(self, db):
        self.config.set("database",db)
    def getDatabase(self):
        return self.config.get("database")
    def setServer(self, server):
        self.config.set("server", server)
    def getServer(self):
        return self.config.get("server")
    
    def run(self,queueItem, sessionId):
        """
            Workflow to retrieving Wiki Data from API
            1) While Loop(Continuous)
                - Read records from Database's Query Table
                - If records exists; perform API calls and map results to models
                - Save Model to DB
        """
        try:
            if(isinstance(queueItem,wordBankQueue.WordBankQueue)):
                self.queuedItem = queueItem
            else:
                self.queuedItem = wordBankQueue.WordBankQueue(queueItem)
            self.queuedItem.setSession(sessionId)
            self.getPageDetails(self.queuedItem)        
        except:
            self.queuedItem.cancelWord(self.getServer(), self.getDatabase(), sessionId)
            print(" [-] Word has been cancelled")
            print(" [x] New Word WordBankId: %s WordBankQueueId: %s" % (self.queuedItem.wordBankId, self.queuedItem.wordBankQueueId))          
            self.run(self.queuedItem, sessionId)
        return self.queuedItem.getJsonObject()
    def getItemsFromWordQueue(self):
        res = python_sql.sqlConnect(self.getServer(),self.getDatabase())
        return res.retrieveDataset(wordBankQueue.WordBankQueue.getItemsFromQueueStmt())     
    def getConnectionToDb(self):
        return python_sql.sqlConnect(self.getServer(), self.getDatabase())
    def getPageDetails(self, queueItem):
        S = requests.Session()
        
        URL = "https://en.wikipedia.org/w/api.php"
        
        PARAMS = {
            "action": "parse", 
            "page": queueItem.word,
            "format": "json",
            "redirects": True,
        }
        print("Word: " + queueItem.word + " processing...")
        R = S.get(url=URL, params=PARAMS)
        DATA = R.json()
        page = pages.Page(DATA.get("parse"))
        self.getPageContent(queueItem.word, page)
        page.saveModelToDb(self.getServer(), self.getDatabase(),queueItem.wordBankId)
        print("      saved to db...")
        queueItem.processCompleteStmt(self.getServer(), self.getDatabase())
        print("      word bank queue process complete...")
        S.close() 
    def getPageContent(self, word, page):
        S = requests.Session()
        
        URL = "https://en.wikipedia.org/w/api.php"
        
        PARAMS = {
            "action": "query", 
            #"page": word,
            "format": "json",
            "redirects": True,
            "prop":"extracts",
            "exsentences":500,
            "exlimit":1,
            "titles": word,
            "explaintext": True
        }
        print("      getting page content...")
        R = S.get(url=URL, params=PARAMS)
        DATA = R.json()
        datas = DATA.get("query")
        page.loadContent(datas.get("pages"))
        S.close()         
        