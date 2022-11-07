#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#endregion

namespace RAB_Sission01_Challenge___FizzBuzz
{
    [Transaction(TransactionMode.Manual)]
    public class Command01Challenge : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // variables 
           
            XYZ myPoint = new XYZ(10,10,0);
            XYZ myNextPoint = new XYZ();

            // Filtered Element Colectors
            FilteredElementCollector colletor = new FilteredElementCollector(doc);
            colletor.OfClass(typeof(TextNoteType));

           
            Transaction t = new Transaction(doc);
            t.Start("Create text note");


            XYZ offset = new XYZ(0,3,0);
            XYZ newPoint = myPoint;

            for (int i = 1; i <=100; i++)
            {
                var result = Fizzbuzz(i);
                //total = total + 1;
                newPoint = newPoint.Add(offset);
                TextNote myTextNote = TextNote.Create(doc, doc.ActiveView.Id, newPoint, result,
                    colletor.FirstElementId());
            }

            t.Commit();

            return Result.Succeeded;
        }
        private static string Fizzbuzz(int i)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                return "BIZZFUZZ";
            }

            else if (i % 5 == 0)
            {
                return "FUZZ";
            }

            else if (i % 3 == 0)
            {
                return "BIZZ";
            }
            else
            {
                return  i.ToString();
            }
        }
    }
}
