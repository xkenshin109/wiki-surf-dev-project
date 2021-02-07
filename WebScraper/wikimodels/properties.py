'''
Created on Feb 2, 2021

@author: Jeremy
'''

class Property:
    '''
    classdocs
    '''


    def __init__(self, params):
        '''
        Constructor
        '''
        self.name = params.get("name")
        self.additional = params.get("*")
        
    def __repr__(self):
        return self.name + " - " + self.additional
    def __table__(self):
        return "WikiProperties"