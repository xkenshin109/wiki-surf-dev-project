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
        conn.execStoredProcedure(dParams)