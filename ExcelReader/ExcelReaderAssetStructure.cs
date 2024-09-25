using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SourceData.ObjectStructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReader
{
    public class ExcelReaderAssetStructure
    {
        public List<AssetStructureObject> Execute()
        {
            List<string> rowList = new List<string>();
            ISheet sheet;

            List<AssetStructureObject> result = new List<AssetStructureObject>();

            using (var stream = new FileStream(@"C:\Users\AndersMagnusNilsson\OneDrive - Å Energi AS\ERP\Anleggstruktur\Uleberg RDS pilot\Uleberg RDS Model_Rev02.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                int columnIndexObjectEBLId = 0;
                int columnIndexObjectName = 0;
                int columnIndexObjectTypeName = 0;

                int[]  columnIndexLevel = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };

                // Find column for given values
                // for (int j = 0; j < dtTable.Columns.Count; j++)
                for (int j = 0; j < headerRow.Count(); j++)
                {
                    //var column = dtTable.Columns[j];
                    // string? columnName = column.ToString();

                    string columnName = getCellValue(headerRow, j);

                    if (columnName.Length == 7 && columnName.Substring(0,5) == "Level")
                    {
                        int levelNo = int.Parse(columnName.Substring(6));
                        columnIndexLevel[levelNo] = j;
                    }

                    if (columnName.Equals("Property: IFS Kode: (Text)"))
                    {
                        columnIndexObjectEBLId = j;
                    }
                    else if (columnName.Equals("Property: Navn: (Text)"))
                    {
                        columnIndexObjectName = j;
                    }
                    else if (columnName.Equals("Property: Objekt Type: (Text)"))
                    {
                        columnIndexObjectTypeName = j;
                    }

                }

                AssetStructureObject[] actualLevelParent = new AssetStructureObject[columnIndexLevel.Length];

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;


                    string[] objectLevelValue = new string[columnIndexLevel.Length];
                    int currentLevel = -1;

                    for (int j = 0;j < row.Cells.Count(); j++)
                    {
                        for (int k = 0; k < columnIndexLevel.Length; k++)
                        {
                            if (j == columnIndexLevel[k])
                            {
                                string cellValue = getCellValue(row, columnIndexLevel[k]);
                                if (!string.IsNullOrEmpty(cellValue))
                                {
                                    objectLevelValue[k] = cellValue;
                                    if (k > currentLevel)
                                    {
                                        currentLevel = k;
                                    }
                                }
                            }
                        }
                    }


                    string objectEBLId = getCellValue(row, columnIndexObjectEBLId);
                    string objectName = getCellValue(row, columnIndexObjectName);
                    string objectObjectTypeName = getCellValue(row, columnIndexObjectTypeName);

                    if (objectEBLId == "12148")
                    {
                        Console.WriteLine($"{objectEBLId} - {objectName}");
                    }
                    if (objectObjectTypeName == "12148")
                    {
                        Console.WriteLine($"\t{objectEBLId} - {objectName}");
                    }

                    AssetStructureObject parentAso = null;
                    if (currentLevel > 0)
                    {
                        parentAso = actualLevelParent[currentLevel - 1];
                    }

                    AssetStructureObject aso = new AssetStructureObject(objectLevelValue[currentLevel], objectEBLId, objectName, objectObjectTypeName, parentAso);
                    if (parentAso != null)
                    {
                        parentAso.AddChild(aso);
                    }
                    result.Add(aso);
                    actualLevelParent[currentLevel] = aso;
                }

                return result;
            }
        }
        private string getCellValue(IRow row, int columnIndex)
        {
            ICell cell = row.GetCell(columnIndex);
            string result = string.Empty;
            if (cell != null)
            {
                result = cell.ToString();
            }
            return result;
        }

    }
}
