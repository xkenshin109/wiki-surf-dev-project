'''
Created on Feb 2, 2021

@author: Jeremy
'''

class Category:
    '''
    classdocs
    '''

    def __init__(self, params):
        '''
        Constructor
        '''
        self.sortKey = params.get("sortkey")
        self.hidden = params.get("hidden")
        self.additional = params.get("*")
        
    def __repr__(self):
        return self.sortKey + " - " + self.additional
    def __table__(self):
        return "WikiCategories"