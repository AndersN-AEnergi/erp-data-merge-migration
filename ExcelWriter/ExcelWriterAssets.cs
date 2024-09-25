using S = SourceData.ObjectStructure;
using T = TargetData.ObjectStructure;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using Org.BouncyCastle.Utilities;
using System.Xml.Linq;

namespace ExcelWriter
{
    public class ExcelWriterAssets : ExcelWriterBase
    {
        public ExcelWriterAssets(string filePath) : base(filePath)
        {

        }

        public void Execute(IEnumerable<T.IAssetObject> topLevelAssets, string filePath)
        {
            string filePathNameLocations = getFilePath("FunctionalLocation");
            using (var fsLocations = new FileStream(filePathNameLocations, FileMode.Create, FileAccess.Write))
            {
                string filePathNameAssets = getFilePath("Asset");
                using (var fsAssets = new FileStream(filePathNameAssets, FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbookLocations = initializeFile(fsLocations, new string[] {
                        "FUNCTIONALLOCATIONID",
                        "FUNCTIONALLOCATIONLIFECYCLESTATEID",
                        "FUNCTIONALLOCATIONTYPEID",
                        "NAME",
                        "PARENTFUNCTIONALLOCATIONID"
                    });

                    IWorkbook workbookAssets = initializeFile(fsAssets, new string[] {
                        "MAINTENANCEASSETID",
                        "FUNCTIONALLOCATIONID",
                        "MAINTENANCEASSETLIFECYCLESTATEID", 
                        "MAINTENANCEASSETTYPEID",
                        "NAME",
                        "PARENTMAINTENANCEASSETID"
                    });

                    foreach (T.IAssetObject assetObject in topLevelAssets)
                    {
                        processAssetObject(assetObject, workbookLocations.GetSheetAt(0), workbookAssets.GetSheetAt(0));
                    }

                    finalizeFile(fsLocations, workbookLocations);
                    finalizeFile(fsAssets, workbookAssets);
                }
            }
        }

        private void processAssetObject(T.IAssetObject assetObject, ISheet sheetLocations, ISheet sheetAssets)
        {
            if (assetObject.ElementType == T.IAssetObject.D365ElementTypeValue.FunctionalLocation)
            {
                writeLine(sheetLocations, new string[] 
                {
                    assetObject.Id,
                    "Aktiv",
                    assetObject.LocationType.ToString(),
                    assetObject.Description,
                    (assetObject.Parent != null ? assetObject.Parent.Id : "")
                });
            }
            else if (assetObject.ElementType == T.IAssetObject.D365ElementTypeValue.Asset)
            {
                if (assetObject.AssetType != T.IAssetObject.AssetTypeValue.Udefinert)
                {
                    writeLine(sheetAssets, new string[]
                    {
                        assetObject.Id,
                        (assetObject.Parent != null && assetObject.Parent.ElementType == T.IAssetObject.D365ElementTypeValue.FunctionalLocation ? assetObject.Parent.Id : ""),
                        "Drift",
                        assetObject.AssetType.ToString(),
                        assetObject.Description,
                        (assetObject.Parent != null && assetObject.Parent.ElementType == T.IAssetObject.D365ElementTypeValue.Asset ? assetObject.Parent.Id : "")
                    });
                }            
            }
            foreach (var aoc in assetObject.Children) 
            { 
                processAssetObject(aoc, sheetLocations, sheetAssets); 
            }
        }
    }
}
