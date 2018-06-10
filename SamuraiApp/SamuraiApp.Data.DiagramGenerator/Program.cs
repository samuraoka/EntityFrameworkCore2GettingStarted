using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;

namespace SamuraiApp.Data.DiagramGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // SqlConnection.ConnectionString Property
            // https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnection.connectionstring(v=vs.110).aspx
            var context = new SamuraiContext("connectionString.json");

            // Microsoft.EntityFrameworkCore
            // https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/
            // Install-Package -Id Microsoft.EntityFrameworkCore -ProjectName SamuraiApp.Data.DiagramGenerator
            //
            // Add reference to the ..\packages\ErikEJ.EntityFrameworkCore.DgmlBuilder.1.0.10\lib\netstandard2.0\ErikEJ.EntityFrameworkCore.DgmlBuilder.dll
            File.WriteAllText(
                Directory.GetCurrentDirectory() + "\\Entities.dgml",
                context.AsDgml(), Encoding.UTF8);
        }
    }
}
