using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelWriter
{
    public abstract class ExcelWriterBase
    {
        private string filePath;
        protected ExcelWriterBase(string filePath)
        {
            this.filePath = filePath;
        }

        protected IWorkbook initializeFile(FileStream fileStream, string[] columnNames)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet("Sheet1");

            List<String> columns = new List<string>();
            IRow row = excelSheet.CreateRow(0);
            int columnIndex = 0;

            foreach (string columnName in columnNames)
            {
                columns.Add(columnName);
                row.CreateCell(columnIndex).SetCellValue(columnName);
                columnIndex++;
            }

            return workbook;
        }

        protected void writeLine(ISheet sheetLocations, string[] columnValues)
        {
            IRow row = sheetLocations.CreateRow(sheetLocations.LastRowNum + 1);
            int cellIndex = 0;
            foreach (String col in columnValues)
            {
                row.CreateCell(cellIndex).SetCellValue(col);
                cellIndex++;
            }
        }

        protected void finalizeFile(FileStream fileStream, IWorkbook workbook)
        {
            workbook.Write(fileStream);
        }

        protected string getFilePath(string fileName)
        {
            return filePath + fileName + ".xlsx";
        }
    }
}
