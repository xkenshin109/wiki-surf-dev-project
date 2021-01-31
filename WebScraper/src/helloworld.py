from python_sql import sqlConnect

c = sqlConnect('SERVERPC\SQLEXPRESS', 'ChieveItDb')
c.retrieveDataset('Accounts')