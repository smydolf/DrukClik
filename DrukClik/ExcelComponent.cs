using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace DrukClik
{
    public class ExcelComponent
    {
        private Application Excel { get; set; }
        private Workbook Wb { get; set; }
        private Worksheet ExcelSheet { get; set; }

        public ExcelComponent()
        {
            Excel = new Application();
        }
        public bool OpenConnection(string filePath)
        {
            try
            {

                Wb = Excel.Workbooks.Open(filePath);
                ExcelSheet = Wb.ActiveSheet;
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("Exception catch {0}", ex);
                return false;
            }
        }
        public Dictionary<string, List<string>> GetValueFromExcel()
        {
            Dictionary<string, List<string>> excelValue = new Dictionary<string, List<string>>();
            List<string> temporaryExcelData = new List<string>();
            int rowCount = 0;
            int columnCount = 0;

            Range firstRow = ExcelSheet.UsedRange.Rows[1];
            System.Array firstRowValue = (System.Array)firstRow.Cells.Value;

            Range firstColumn = ExcelSheet.UsedRange.Columns[1];
            System.Array firstColumnValue = (System.Array)firstColumn.Cells.Value;

            CountFilledRow(ref rowCount, firstColumnValue);
            CountFilledColumn(ref columnCount, firstRowValue);
            for (int i = 1; i <= columnCount; i++)
            {
                Range column = ExcelSheet.UsedRange.Columns[i];
                System.Array columnValue = (System.Array)column.Cells.Value;
                temporaryExcelData = GetValueFromExcel(rowCount, columnValue);
                String key = temporaryExcelData[0];
                temporaryExcelData.RemoveAt(0);
                excelValue.Add(key, temporaryExcelData);
            }
            Wb.Close();
            return excelValue;
        }
        private void CountFilledRow(ref int j, Array myvalues)
        {
            var range = ExcelSheet.UsedRange;
            int rowCount = range.Rows.Count;
            int columnCount = range.Columns.Count;

            for (int i = 1; i < rowCount; i++)
            {
                if (myvalues.GetValue(i, 1) == null) break;
                else
                    j++;
            }
        }
        private void CountFilledColumn(ref int j, Array myvalues)
        {
            var range = ExcelSheet.UsedRange;
            int columnCount = range.Columns.Count;
            for (int i = 1; i < columnCount; i++)
            {
                if (myvalues.GetValue(1, i) == null) break;
                else
                    j++;
            }
        }
        private List<string> GetValueFromExcel(int rowCount, Array myvalues)
        {
            List<string> strArray = new List<string>();
            for (int i = 1; i <= rowCount; i++)
                strArray.Add(myvalues.GetValue(i, 1) == null ? " " : myvalues.GetValue(i, 1).ToString());
            return strArray;
        }
    }
}
