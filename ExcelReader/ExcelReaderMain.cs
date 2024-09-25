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
    public class ExcelReaderMain
    {
        public Dictionary<string, AssetObjectBase> ExecuteAssets()
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;

            Dictionary<string, AssetObjectBase> result = new Dictionary<string, AssetObjectBase>();

            using (var stream = new FileStream(@"C:\work\VKSor_Anlegg_20240815.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    {
                        dtTable.Columns.Add(cell.ToString());
                    }
                }

                int columnIndexObjectId = 0;
                int columnIndexObjectDescription = 0;
                int columnIndexObjectBelongsToId = 0;

                // Find column for given values
                for (int j = 0; j < dtTable.Columns.Count; j++)
                {
                    var column = dtTable.Columns[j];

                    string? columnName = column.ToString();

                    if (columnName.Equals("Objekt ID"))
                    {
                        columnIndexObjectId = j;
                    }
                    else if (columnName.Equals("Objektbeskrivelse"))
                    {
                        columnIndexObjectDescription = j;
                    }
                    else if (columnName.Equals("Tilhører objekt"))
                    {
                        columnIndexObjectBelongsToId = j; 
                    }

                }

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                            {
                                rowList.Add(row.GetCell(j).ToString());
                            }
                        }
                    }
                    if (rowList.Count > 0)
                        dtTable.Rows.Add(rowList.ToArray());
                    rowList.Clear();

                    string objectId = getCellValue(row, columnIndexObjectId);
                    string objectDescription = getCellValue(row, columnIndexObjectDescription);
                    string objectBelongsToId = getCellValue(row, columnIndexObjectBelongsToId);

                    if (objectId == "12148")
                    {
                        Console.WriteLine($"{objectId} - {objectDescription}");
                    }
                    if (objectBelongsToId == "12148")
                    {
                        Console.WriteLine($"\t{objectId} - {objectDescription}");
                    }

                    result.Add(objectId, new AssetObjectBase(objectId, objectDescription, objectBelongsToId, IObject.SourceTypeValue.IFS9));
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
