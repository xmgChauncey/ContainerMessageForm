using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContainerMessageForm.Common
{
    public abstract class CommFuction
    {
        /// <summary>
        /// 校验字符串只由正整数组成
        /// </summary>
        /// <param name="checkedNumberString"></param>
        /// <returns></returns>
        public static bool CheckIsNumber(string checkedNumberString)
        {
            Regex regex = new Regex(@"^[0-9]*$");

            if (string.IsNullOrEmpty(checkedNumberString))
            {
                return false;
            }
            return regex.IsMatch(checkedNumberString);
        }

        /// <summary>
        /// 验证由数字和26个英文字母组成的字符串
        /// </summary>
        /// <param name="checkedString"></param>
        /// <returns></returns>
        public static bool CheckIsNumberAndLetter(string checkedString)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9]+$");

            if (string.IsNullOrEmpty(checkedString))
            {
                return false;
            }
            return regex.IsMatch(checkedString);
        }

        /// <summary>
        /// 验证箱位的组成00/000/000/00
        /// </summary>
        /// <param name="seat"></param>
        /// <returns></returns>
        public static bool CheckSeat(string seat)
        {
            Regex regex = new Regex(@"^[A-Z0-9]{2}/[A-Z0-9]{3}/[A-Z0-9]{3}/[A-Z0-9]{2}$");
            //Regex regex = new Regex(@"^[0-9]{2}/[0-9]{3}/[0-9]{3}/[0-9]{2}$");
            if (string.IsNullOrEmpty(seat))
            {
                return false;
            }

            return regex.IsMatch(seat);
        }

        #region DataGridView导出到Excel
        /// <summary>
        /// 导出DataGridView中的数据到Excel文件
        /// </summary>
        /// <param name="dgv"></param>
        public static void DataGridViewToExcel(DataGridView dgv)
        {

            #region 验证可操作性
            //申明保存对话框
            SaveFileDialog dlg = new SaveFileDialog();
            //文件后缀列表
            dlg.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            //默认路径是系统当前路径
            dlg.InitialDirectory = Directory.GetCurrentDirectory();
            //打开保存对话框
            if (dlg.ShowDialog() == DialogResult.Cancel) return;
            //返回文件路径
            string fileNameString = dlg.FileName;
            //验证strFileName是否为空或值无效
            if (fileNameString.Trim() == " ")
            { return; }
            //定义表格内数据的行数和列数
            int rowscount = dgv.Rows.Count;
            int colscount = dgv.Columns.Count;

            //行数必须大于0
            if (rowscount <= 0)
            {
                MessageBox.Show("没有数据可供保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //列数必须大于0
            if (colscount <= 0)
            {
                MessageBox.Show("没有数据可供保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //行数不可以大于65536
            if (rowscount > 65536)
            {
                MessageBox.Show("数据记录数太多(最多不能超过65536条)，不能保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //列数不可以大于255
            if (colscount > 255)
            {
                MessageBox.Show("数据记录行数太多，不能保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //验证以fileNameString命名的文件是否存在，如果存在删除它
            FileInfo file = new FileInfo(fileNameString);
            if (file.Exists)
            {
                try
                {
                    file.Delete();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "删除失败 ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            #endregion

            #region 数据导入Excel表
            Microsoft.Office.Interop.Excel.Application objExcel = null;
            Microsoft.Office.Interop.Excel.Workbook objWorkbook = null;
            Microsoft.Office.Interop.Excel.Worksheet objsheet = null;
            try
            {
                //申明对象
                objExcel = new Microsoft.Office.Interop.Excel.Application();
                objWorkbook = objExcel.Workbooks.Add(Missing.Value);
                objsheet = (Microsoft.Office.Interop.Excel.Worksheet)objWorkbook.ActiveSheet;

                //设置EXCEL不可见
                objExcel.Visible = false;

                //向Excel中写入表格的表头
                int displayColumnsCount = 1;
                for (int i = 1; i <= dgv.ColumnCount - 1; i++)
                {
                    if (dgv.Columns[i].Visible == true)
                    {
                        objExcel.Cells[1, displayColumnsCount] = dgv.Columns[i].HeaderText.Trim();
                        displayColumnsCount++;
                    }
                }

                //向Excel中逐行逐列写入表格中的数据
                for (int row = 0; row <= dgv.RowCount - 1; row++)
                {

                    displayColumnsCount = 1;
                    for (int col = 0; col < colscount; col++)
                    {
                        if (dgv.Columns[col].Visible == true)
                        {
                            try
                            {
                                objExcel.Cells[row + 2, displayColumnsCount] = @"'" + dgv.Rows[row].Cells[col].Value.ToString().Trim();
                                displayColumnsCount++;
                            }
                            catch (Exception)
                            {

                            }

                        }
                    }
                }

                //保存文件
                objWorkbook.SaveAs(fileNameString, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "警告 ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            finally
            {
                //关闭Excel应用
                if (objWorkbook != null) objWorkbook.Close(Missing.Value, Missing.Value, Missing.Value);
                if (objExcel.Workbooks != null) objExcel.Workbooks.Close();
                if (objExcel != null) objExcel.Quit();

                objsheet = null;
                objWorkbook = null;
                objExcel = null;
            }
            MessageBox.Show(fileNameString + "\n\n导出完毕!", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void DataGridViewToExcel(DataTable dataTable)
        {

            #region 创建保存的Excel文件
            //申明保存对话框
            SaveFileDialog dlg = new SaveFileDialog();
            //文件后缀列表
            dlg.Filter = "Excel(*.xls)|*.xls";
            //默认路径是系统当前路径
            dlg.InitialDirectory = Directory.GetCurrentDirectory();
            //打开保存对话框
            if (dlg.ShowDialog() == DialogResult.Cancel) return;
            //返回文件路径
            string fileNameString = dlg.FileName;
            //验证strFileName是否为空或值无效
            if (fileNameString.Trim() == " ")
            { return; }
            #endregion

            //Excel操作对象
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            FileStream fileStream = null;

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();

                    int starRow = 0;
                    int endRow = 65535;

                    //行数
                    int rowCount = dataTable.Rows.Count;
                    //列数
                    int columnCount = dataTable.Columns.Count;

                    //Excel文件中的sheet数量
                    int sheetNum = dataTable.Rows.Count / 65535;
                    for (int m = 0; m <= sheetNum; m++)
                    {
                        //Excel表中创建"sheet"
                        sheet = workbook.CreateSheet("sheet"+m);

                        //设置列头
                        row = sheet.CreateRow(0);
                        for (int c = 0; c < columnCount; c++)
                        {
                            cell = row.CreateCell(c);
                            cell.SetCellValue(dataTable.Columns[c].ColumnName);
                        }

                        starRow = m * 65535;

                        if (m==sheetNum)
                        {
                            endRow = rowCount - starRow;
                        }

                        //设置每行每列数据
                        for (int i = 0; i < endRow; i++)
                        {
                            //第二行开始写入数据
                            row = sheet.CreateRow(i + 1);
                            for (int j = 0; j < columnCount; j++)
                            {
                                cell = row.CreateCell(j);
                                cell.SetCellValue(dataTable.Rows[starRow+ i][j].ToString());
                            }
                        }
                    }
                    using (fileStream = new FileStream(fileNameString, FileMode.Create))
                    {
                        workbook.Write(fileStream);
                        fileStream.Close();
                        MessageBox.Show(fileNameString + "\n\n导出完毕!", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("没有数据！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误："+ex.Message);
            }
        }
        #endregion
    }


    #endregion
}
