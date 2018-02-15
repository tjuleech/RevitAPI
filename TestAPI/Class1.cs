using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;

using System.Windows.Markup;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using System.Collections;
using System.Linq;

namespace TestAPI
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class Class1:IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ///获取全部模型实例（包括自带族和自建族）
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;

            ElementClassFilter instanceFitler = new ElementClassFilter(typeof(FamilyInstance));
            ElementClassFilter hostFilter = new ElementClassFilter(typeof(HostObject));

            LogicalOrFilter andFilter = new LogicalOrFilter(instanceFitler, hostFilter);

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.WherePasses(andFilter);


            ElementCategoryFilter elementCategoryFilter;
            String a = "";
            ElementClassFilter testClassfilter;
            BuiltInCategory enumCategory;
            Type ty;
            int c = 0;


            foreach (Element e in collector)
            {

                ///取得类别内参名的方法并用其进行过滤
                Category category = e.Category;
                enumCategory = (BuiltInCategory)category.Id.IntegerValue;
                elementCategoryFilter = new ElementCategoryFilter(enumCategory);

                ///取得类型内参名的方法并用其进行过滤
                ty = e.GetType();
                testClassfilter = new ElementClassFilter(ty);

                ///过滤
                collector.WherePasses(elementCategoryFilter);


                foreach (Element f in collector)
                    c++;
                

                TaskDialog.Show("count", "sss  "+c.ToString());
                break;
            }

            return Result.Succeeded;
        }
    }
}
