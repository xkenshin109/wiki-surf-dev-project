'''
Created on Feb 2, 2021

@author: Jeremy
'''
from wikimodels import languages, categories, links, properties, sections
import python_sql
class Page:
    '''
    classdocs
    '''
    

    def __init__(self, args):
        '''
        Constructor
        '''
        self.extract = None
        self.title = args.get("title")
        self.pageId = args.get("pageid")
        self.revId = args.get("revid")
        self.displaytitle = args.get("displaytitle")
        self.langs = []
        for x in args.get("langlinks"):
            self.langs.append(languages.Language(x))
            
        self.categories = []
        for x in args.get("categories"):
            self.categories.append(categories.Category(x))
        self.links = []
        for x in args.get("links"):
            if(x.get("ns") == 0):
                self.links.append(links.Link(x))
        self.sections = []
        for x in args.get("sections"):
            self.sections.append(sections.Section(x))

        self.properties = []
        for x in args.get("properties"):
            self.properties.append(properties.Property(x))
            
    def saveModelToDb(self, server, database, wordBankId):
        sql = python_sql.sqlConnect(server, database)
        id = sql.execStoredProcedure(self.storedProcedure_Insert(wordBankId))
        self.wikiPageId = id       
        self.wordBankId = wordBankId
        #Save Links To DB
        for link in self.links:
            link.save(server, database, self.wikiPageId)
        for section in self.sections:
            section.save(server, database, self.wikiPageId)
        return
    def __repr__(self):
        return "Word: " + self.displaytitle
    def __table__(self):
        return "WikiPages"    
    def storedProcedure_Insert(self, wordBankId):
        d = dict()
        storedProcedure = """
        EXEC S_INSERT_WIKI_PAGE @WordBankId = ?, @PageId = ?, @RevId = ?, @Title = ?, @DisplayTitle = ?, @PlainTextContent = ?
        """
        params = (wordBankId, self.pageId, self.revId, self.title, self.displaytitle, self.extract)
        d.setdefault("storedProcedure", storedProcedure)
        d.setdefault("params",params)
        return d
    def loadContent(self, data):
        contentData = data.get(self.pageId.__str__())
        self.extract = contentData.get("extract")