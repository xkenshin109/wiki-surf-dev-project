'''
Created on Feb 2, 2021

@author: Jeremy
'''

class Language:
    '''
    classdocs
    '''


    def __init__(self, params):
        '''
        Constructor
        '''
        self.lang = params.get("lang")
        self.url = params.get("url")
        self.langname = params.get("langname")
        self.autonym = params.get("autonym")
        self.add = params.get("*")
        
    def __repr__(self):
        return self.lang + " - " + self.langname
    def __table__(self):
        return "WikiLanguages"