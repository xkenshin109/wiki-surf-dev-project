'''
Created on Jan 30, 2021
Py Module: python.sql
Python class connection to retrieve datasets from database

@author: Jeremy
'''
import pyodbc    
class sqlConnect:
    def __init__(self, server, database):
        self.server = server
        self.database = database
    def retrieveDataset(self, tableName):
        conn = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=SERVERPC\SQLEXPRESS;Database=ChieveItDb;Trusted_Connection=yes;')
        cursor = conn.cursor()

        cursor.execute('SELECT * FROM %s.[dbo].%s' % (self.database, tableName))
        
        for row in cursor:
            print(row)
        