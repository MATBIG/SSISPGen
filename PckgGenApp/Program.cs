using PckgGenApp.Contoso;
using static System.Console;

namespace PckgGenAppMain
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoader c1 = new ContosoLoader();
            c1.Start(@"C:\Users\tomek\Desktop\DCDemoSSIS\TesterDC.ispac");

            WriteLine("\n\n\n<---- success ---->");
            ReadKey();
        }
    }
}

