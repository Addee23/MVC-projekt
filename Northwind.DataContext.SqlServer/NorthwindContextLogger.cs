using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace Northwind.DataContext.SqlServer
{
    /// <summary>
    /// Logger for NorthwindContext, ska logga till fil
    /// </summary>
    public class NorthwindContextLogger
    {
        public static void WriteLine(string message)
        {
            string path = Path.Combine(GetFolderPath(SpecialFolder.DesktopDirectory), "northwindlog.txt");

            StreamWriter textFile = File.AppendText(path);
            textFile.WriteLine(message);
            textFile.Close();
        }
        
    }
}
