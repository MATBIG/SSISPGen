using PckgGenApp.Contoso;
using static System.Console;

namespace PckgGenAppMain
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoader c1 = new ContosoLoader();
            c1.Start(@"C:\Users\kostytom\Desktop\PackageGen\Test.ispac");

            WriteLine("\n\n\n<---- success ---->");
            ReadKey();
        }
    }
}

//  todo
//  -   nazewnictwo posprzątać w Lib
//  -   Lookup
//  -   poczytać o ezApi https://blogs.msdn.microsoft.com/mattm/2008/12/30/ezapi-alternative-package-creation-api/
//  -   http://blogs.solidq.com/en/businessanalytics/generating-ssis-packages-programmatically-part/
//  -   https://social.msdn.microsoft.com/Forums/sqlserver/en-US/d66131aa-5d67-46f2-9cc5-3649bfb95e31/programmatically-create-merge-join-does-not-join-on-all-sortkeys?forum=sqlintegrationservices
//  -   http://www.sqlservercentral.com/articles/Integration+Services+(SSIS)/64572/
//  -   https://github.com/samskolli/Pegasus/tree/master/Pegasus.DtsWrapper/PipelineComponents
//  -   http://joshrobi.blogspot.com/2012/04/developing-microsoft-ssis-packages.html
//  -   http://binodmahto.blogspot.com/2014/07/create-ssis-package-programatically.html
//  -   http://blogs.msdn.com/b/mattm/archive/2008/12/30/samples-for-creating-ssis-packages-programmatically.aspx
//  -   Microsoft SQL Server 2012 Integration Services: An Expert Cookbook

