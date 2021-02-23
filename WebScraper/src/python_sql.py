'''
Created on Jan 30, 2021
Py Module: python.sql
Python class connection to retrieve datasets from database

@author: Jeremy
'''
import pyodbc    
import uuid
class sqlConnect:
    def __init__(self, server, database):
        self.server = server
        self.database = database
    def retrieveDataset(self, stmt):
        conn = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=' + self.server + ';Database='+ self.database +';Trusted_Connection=yes;')
        cursor = conn.cursor()

        cursor.execute(stmt)
        
        rows = []
        for row in cursor:
            rows.append(row)
        return rows
    def insertRecordToDb(self, stmt):
        conn = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=' + self.server + ';Database='+ self.database +';Trusted_Connection=yes;')
        cursor = conn.cursor()
        cursor.execute(stmt)

        conn.commit()
        for row in cursor:
            print(1)
        return id 
    def updateRecordToDb(self, stmt):
        conn = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=' + self.server + ';Database='+ self.database +';Trusted_Connection=yes;')
        cursor = conn.cursor()
        cursor.execute(stmt)
        conn.commit()       
        
    def execStoredProcedure(self,d):
        conn = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=' + self.server + ';Database='+ self.database +';Trusted_Connection=yes;')
        cursor = conn.cursor()
        params = d.get("params")
        sProc = d.get("storedProcedure")
        cursor.execute(sProc,params)
        row = cursor.fetchval()
        conn.commit()
        return row
    def execStoredProcedureRow(self,d):
        conn = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=' + self.server + ';Database='+ self.database +';Trusted_Connection=yes;')
        cursor = conn.cursor()
        params = d.get("params")
        sProc = d.get("storedProcedure")
        cursor.execute(sProc,params)
        row = cursor.fetchall()
        conn.commit()
        return row