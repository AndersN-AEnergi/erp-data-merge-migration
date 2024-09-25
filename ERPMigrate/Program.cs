using ExcelReader;
using Processors;
using SourceData.ObjectStructure;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("*****************************************************************************************");
            Console.WriteLine("*                                                                                       *");
            Console.WriteLine("*    Datamigrering IFS -> D365                                                          *");
            Console.WriteLine("*                                                                                       *");
            Console.WriteLine("*****************************************************************************************");
            Console.WriteLine();
            Console.WriteLine("Parameter 1, Datatype:");
            Console.WriteLine("1. Anleggsstruktur, IFS9 fra fil");
            Console.WriteLine("2. Anleggsstruktur ombygd, Excel med struktur, IFS9 fra fil");
            Console.WriteLine("Parameter 2, Testprefix: Verdi eller tom om skarp kjøring.");
            Console.WriteLine("Parameter 3, Anonymiser: Y/N.");
            Console.WriteLine("");
        }
        else 
        {
            bool isTest = false;
            bool isAnonymize = false;
            string testPrefix = null;

            if (args.Length > 1)
            {
                if (!string.IsNullOrEmpty(args[1]))
                {
                    isTest = true;
                    testPrefix = args[1];
                }
            }

            if (args.Length > 2)
            {
                if (!string.IsNullOrEmpty(args[2]))
                {
                    isAnonymize = true;
                }
            }

            if (args[0] == "1" ||
                args[0] == "2")
            {
                ExcelReaderMain reader = new ExcelReaderMain();
                Dictionary<string, AssetObjectBase> assets = reader.ExecuteAssets();

                List<AssetStructureObject> assetStructure = null;

                if (args[0] == "2")
                {
                    ExcelReaderAssetStructure excelReaderAssetStructure = new ExcelReaderAssetStructure();
                    assetStructure = excelReaderAssetStructure.Execute();
                }

                MainProcessor mainProcessor = new MainProcessor();
                mainProcessor.Execute(args, assets, assetStructure, isTest, testPrefix, isAnonymize);
            }
            if (args[0] == "3")
            {

            }
        }
    }
}