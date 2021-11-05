import xlrd

import json

def findobjinlist(objlist,name):
     for item in (objlist):
          if item.name == name:
               return item
     return None

def obj_dict(obj):
    return obj.__dict__

class objectjson:
     def __init__(self,name):
        self.name = name
        self.list = []
     def insertobj(newobjet):
         self.list.append(newobjet)
         
def exceltojson():
     myexcelworkbook =  xlrd.open_workbook('Extrait_TEMPO_de_082020_à_022021.xls')
     SheetNameList = myexcelworkbook.sheet_names()
    
     worksheet = myexcelworkbook.sheet_by_name(SheetNameList[0])
     num_rows = worksheet.nrows
     num_cells = worksheet.ncols
     print( 'num_rows, num_cells', num_rows, num_cells )
     curr_row = 0
     clientobjlist = []
     clientdictlist = []
     clientnamelist = []
                   
     while curr_row < num_rows:
          row = worksheet.row(curr_row)
          curr_row += 1
          if (row[4].value != "Clientèle"):
               continue
          #print row, len(row), row[0], row[1]
          #print( 'Row: ', curr_row )
          #print( row, len(row), row[0] )
          clientrow = row[5]
          missionrow = row[6]
          objetrow = row[7]
          etaprow = row[8]
             
          item1 = findobjinlist(clientobjlist, clientrow.value)
          if (item1 != None):
               pass
          else:
               newclient = objectjson(clientrow.value)
               clientobjlist.append(newclient)
               clientnamelist.append(clientrow.value)
               item1 = newclient
          item2 = findobjinlist(item1.list, missionrow.value)
          if (item2 != None):
                pass    
          else:
               newmission = objectjson(missionrow.value)
               item1.list.append(newmission)
               item2 = newmission


          item3 = findobjinlist(item2.list, objetrow.value)
          if (item3 != None):
               pass 
          else:
               newobjet = objectjson(objetrow.value)
               item2.list.append(newobjet)
               item3 = newobjet
          item4 = findobjinlist(item3.list, etaprow.value)
          if (item4 != None):
               pass
          else:
               newetap = objectjson(etaprow.value)
               item3.list.append(newetap)

     #print(clientnamelist)
     #for item in clientobjlist:
      #    clientdictlist.append(item.__dict__)
     jsonstr = json.dumps(clientobjlist, default=obj_dict)
     print(jsonstr)
     with open('data.txt', 'w') as outfile:
         json.dump(jsonstr, outfile)
#def addinobj(objclasse,objname):
    
    
exceltojson()


    
    
