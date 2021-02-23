'''
Created on Feb 3, 2021

@author: Jeremy
'''
import python_sql 
class WordBankQueue:
    def __init__(self, params):
        self.tableName = "WordBankQueues"
        self.wordBankQueueId = params.get("WordBankQueueId")
        self.wordBankId = params.get("WordBankId")
        self.isProcessed = params.get("IsProcessed")
        self.createdDate = params.get("CreatedDate")
        self.processedDate = params.get("ProcessedDate")
        self.word = params.get("Word")
    def updateQueueRow(self, row):
        self.wordBankQueueId = row[0][0]
        self.wordBankId = row[0][1]
        self.isProcessed = row[0][2]
        self.processedDate = row[0][3]
        self.createdDate = row[0][4]
        self.exclude = row[0][5]
        self.wordIndex = row[0][6]
    def setSession(self, sessionId):
        self.sessionId = sessionId
    def updateRow(self, row):
        self.wordBankId = row[0][0]
        self.word = row[0][1]
    def getJsonObject(self):
        d = {
            "WordBankQueueId" : self.wordBankQueueId,
            "WordBankId": self.wordBankId,
            "IsProcessed": self.isProcessed,
            "CreatedDate": self.createdDate,
            "ProcessedDate": self.processedDate,
            "WordBank": {
                "Word": self.word,
                "WordBankId": self.wordBankId,
                "Exclude": self.exclude,
                "WordIndex": self.wordIndex
                }
            }
        return d
    @staticmethod
    def getItemsFromQueueStmt():
        return """
            /****** Script for SelectTopNRows command from SSMS  ******/
            SELECT WordBankQueues.[WordBankQueueId]
                  ,WordBankQueues.[WordBankId]
                  ,WordBankQueues.[IsProcessed]
                  ,WordBankQueues.[CreatedDate]
                  ,WordBankQueues.[ProcessedDate]
                  ,WordBanks.Word    
              FROM [WikiSurf].[dbo].[WordBankQueues]
              inner join WordBanks on WordBanks.WordBankId = WordBankQueues.WordBankId
              where IsProcessed = 0 and ProcessedDate is null
          """
    def processCompleteStmt(self, server, database):
        conn = python_sql.sqlConnect(server,database)
        dParams = dict()
        sProc = '''EXEC S_WORD_BANK_QUEUE_COMPLETE @WordBankQueueId = ? '''
        dParams.setdefault("storedProcedure", sProc)
        params = (self.wordBankQueueId)
        dParams.setdefault("params",params)
        row = conn.execStoredProcedureRow(dParams)
        self.updateQueueRow(row)
    def cancelWord(self, server, database, wikiSessionId):
        conn = python_sql.sqlConnect(server,database)
        dParams = dict()
        params = (self.wordBankQueueId, wikiSessionId)
        dParams.setdefault('params', params)
        sProc = '''EXEC S_EXCLUDE_WORD_BY_WORD_BANK_QUEUE @WordBankQueueId = ?, @SessionId = ?'''
        dParams.setdefault('storedProcedure',sProc)
        newWord = conn.execStoredProcedureRow(dParams)
        self.updateRow(newWord)
        