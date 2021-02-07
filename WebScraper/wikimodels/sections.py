'''
Created on Feb 2, 2021

@author: Jeremy
'''
import python_sql
class Section:
    '''
    classdocs
    '''


    def __init__(self, params):
        '''
        Constructor
        '''
        self.toclevel = params.get("toclevel")
        self.level = params.get("level")
        self.line = params.get("line")
        self.number = params.get("number")
        self.index = params.get("index")
        self.fromtitle = params.get("fromtitle")
        self.byteoffset = params.get("byteoffset")
        self.anchor = params.get("anchor")
        
    def __repr__(self):
        return self.fromtitle + " => " + self.line + "Index: " + self.index
    def __table__(self):
        return "WikiSections"
    def storedProcedure(self, wikiPageId):
        dParams = dict()
        storedProcedure = """
            EXEC S_INSERT_WIKI_SECTION @WikiPageId = ?, @ToCLevel = ?, @Level = ?, @Line = ?, @Number = ?, @Index = ?, @FromTitle = ?, @ByteOffset = ?, @Anchor = ?
        """
        dParams.setdefault("storedProcedure", storedProcedure)
        params = (wikiPageId, self.toclevel, self.level, self.line, self.number, self.index, self.fromtitle, self.byteoffset, self.anchor)
        dParams.setdefault("params",params)
        return dParams
    def save(self, server, database, wikiPageId):
        db = python_sql.sqlConnect(server,database)
        id = db.execStoredProcedure(self.storedProcedure(wikiPageId))
        self.wikiLinksId = id