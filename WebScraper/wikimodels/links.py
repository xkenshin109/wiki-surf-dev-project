'''
Created on Feb 2, 2021

@author: Jeremy
'''
import python_sql
class Link:
    '''
    classdocs
    '''


    def __init__(self, params):
        '''
        Constructor
        '''
        self.wikiLinksId = None
        self.ns = params.get("ns")
        self.exists = params.get("exists")
        self.additional = params.get("*")
        
    def __repr__(self):
        return self.additional
    def __table__(self):
        return "WikiLinks"
    def storedProcedure(self, wikiPageId):
        dParams = dict()
        sProc = """
        EXEC S_INSERT_WIKI_LINK @WikiPageId = ?, @Ns = ?, @Exists = ?, @Additional = ?
        """
        dParams.setdefault("storedProcedure", sProc)
        
        params = (wikiPageId, self.ns, self.exists, self.additional)
        dParams.setdefault("params", params)
        return dParams
    def save(self, server, database, wikiPageId):
        db = python_sql.sqlConnect(server,database)
        id = db.execStoredProcedure(self.storedProcedure(wikiPageId))
        self.wikiLinksId = id
        