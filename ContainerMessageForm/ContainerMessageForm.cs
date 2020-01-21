using ContainerMessageForm.Common;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace ContainerMessageForm
{
    public partial class ContainerMessageForm : Form
    {
        private delegate void ProgressBarShow(int current, int tottal);

        log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        LogContent logContent = new LogContent();

        SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction);

        public ContainerMessageForm()
        {
            InitializeComponent();
        }

        private void button_OpenFolder_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择文件所在文件夹";

            ///打开文件夹浏览器并选择文件夹
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                //获取选择的文件夹的路径
                this.textBox_Folder.Text = folderBrowserDialog.SelectedPath;
                this.textBox_Folder.Refresh();

                DirectoryInfo directoryInfo = new DirectoryInfo(folderBrowserDialog.SelectedPath.Trim());

                if (directoryInfo != null)
                {
                    //获取文件夹（不包含子目录）下的xml文件
                    FileInfo[] fileInfoArray = directoryInfo.GetFiles("*.xml");

                    //获取文件夹（不包含子目录）下的xml文件个数
                    this.textBox_totalFileCount.Text = fileInfoArray.Length.ToString();
                    this.textBox_totalFileCount.Refresh();
                }
            }
        }

        private void button_StartAnalyze_Click(object sender, EventArgs e)
        {
            DataCheckRules dataCheckRules = new DataCheckRules();

            if (string.IsNullOrEmpty(this.textBox_Folder.Text.Trim()))
            {
                MessageBox.Show("文件路径为空！");
                return;
            }

            if (this.textBox_totalFileCount.Text.Trim() == "0")
            {
                MessageBox.Show("当前目录下没有XML文件！");
                return;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(this.textBox_Folder.Text.Trim());

            if (directoryInfo != null)
            {
                //获取文件夹（不包含子目录）下的xml文件
                FileInfo[] fileInfoArray = directoryInfo.GetFiles("*.xml");

                if (fileInfoArray.Length > 0)
                {
                    //获取文件夹（不包含子目录）下的xml文件个数
                    this.textBox_totalFileCount.Text = fileInfoArray.Length.ToString();
                    this.textBox_totalFileCount.Refresh();
                }

                XmlDocument xmlDocument = new XmlDocument();

                CsaHead csaHead = new CsaHead();
                CsaData csaData = new CsaData();

                int currentNumber = 0;

                foreach (FileInfo fileInfo in fileInfoArray)
                {
                    Thread.Sleep(100);

                    string checkResult = string.Empty;

                    currentNumber++;

                    //获取xml文件
                    xmlDocument.Load(fileInfo.FullName);

                    #region 校验报文中Head节点的子节点数据
                    //获取Head节点及子节点数据
                    XmlNodeList xmlNodeListHead = xmlDocument.SelectNodes("ContaDeclareInfo/Head");
                    foreach (XmlElement xmlElement in xmlNodeListHead)
                    {
                        //校验Head节点的MsgId节点数据
                        csaHead.MsgId = xmlElement.GetElementsByTagName("MsgId")[0].InnerXml;
                        checkResult += dataCheckRules.CheckHeadMsgId(csaHead.MsgId);

                        //校验Head节点的MsgType节点数据
                        csaHead.MsgType = xmlElement.GetElementsByTagName("MsgType")[0].InnerXml;
                        checkResult += dataCheckRules.CheckHeadMsgType(csaHead.MsgType);

                        //校验Head节点的CustomsCode节点数据
                        csaHead.CustomsCode = xmlElement.GetElementsByTagName("CustomsCode")[0].InnerXml;
                        checkResult += dataCheckRules.CheckHeadCustomsCode(csaHead.CustomsCode);

                        //校验Head节点的SupvLoctCode节点数据
                        csaHead.SupvLoctCode = xmlElement.GetElementsByTagName("SupvLoctCode")[0].InnerXml;
                        checkResult += dataCheckRules.CheckHeadSupvLoctCode(csaHead.SupvLoctCode);

                        //校验Head节点的DeclDate节点数据
                        csaHead.DeclDate = xmlElement.GetElementsByTagName("DeclDate")[0].InnerXml;
                        checkResult += dataCheckRules.CheckHeadDeclDate(csaHead.DeclDate);

                        //校验Head节点的DeclareDataType节点数据
                        csaHead.DeclareDataType = xmlElement.GetElementsByTagName("DeclareDataType")[0].InnerXml;
                        checkResult += dataCheckRules.CheckHeadDeclareDataType(csaHead.DeclareDataType);

                        //校验Head节点的TotalMsgNo节点数据
                        csaHead.TotalMsgNo = xmlElement.GetElementsByTagName("TotalMsgNo")[0].InnerXml;
                        checkResult += dataCheckRules.CheckHeadTotalMsgNo(csaHead.TotalMsgNo, this.textBox_totalFileCount.Text);

                        //校验Head节点的CurMsgNo节点数据
                        csaHead.CurMsgNo = xmlElement.GetElementsByTagName("CurMsgNo")[0].InnerXml;
                        checkResult += dataCheckRules.CheckHeadCurMsgNo(csaHead.TotalMsgNo, csaHead.CurMsgNo);
                    }

                    if (string.IsNullOrEmpty(checkResult))
                    {
                        try
                        {
                            //报文中Head节点下子节点数据插入CsaHead表中
                            DataBaseOperate.InsertDataIntoCsaHead(sqlConnection, csaHead);
                        }
                        catch (Exception ex)
                        {
                            logContent.MsgId = csaHead.MsgId;
                            logContent.FilePath = fileInfo.FullName;
                            logContent.ErrorDescription = ex.Message;
                            log.Error(logContent);
                        }
                    }
                    else
                    {
                        logContent.MsgId = csaHead.MsgId;
                        logContent.FilePath = fileInfo.FullName;
                        logContent.ErrorDescription = checkResult;
                        log.Error(logContent);

                        checkResult = string.Empty;
                    }
                    #endregion

                    #region 校验报文中Data节点的子节点数据
                    XmlNodeList xmlNodeListData = xmlDocument.SelectNodes("ContaDeclareInfo/Declaration/Data");
                    foreach (XmlNode xmlNode in xmlNodeListData)
                    // foreach (XmlNode xmlNode in xmlDocument.SelectSingleNode("ContaDeclareInfo").SelectSingleNode("Declaration").SelectNodes("Data"))
                    {
                        #region 获取Data节点的子节点数据
                        csaData.ContaId = xmlNode.SelectSingleNode("ContaId").InnerText;
                        csaData.ContaTypeCode = xmlNode.SelectSingleNode("ContaTypeCode").InnerText;
                        csaData.Seat = xmlNode.SelectSingleNode("Seat").InnerText;
                        csaData.TradeMark = xmlNode.SelectSingleNode("TradeMark").InnerText;
                        csaData.IEFlag = xmlNode.SelectSingleNode("IEFlag").InnerText;
                        csaData.ContaMark = xmlNode.SelectSingleNode("ContaMark").InnerText;
                        csaData.LoadMark = xmlNode.SelectSingleNode("LoadMark").InnerText;
                        csaData.DangerMark = xmlNode.SelectSingleNode("DangerMark").InnerText;
                        csaData.EntranceDate = xmlNode.SelectSingleNode("EntranceDate").InnerText;
                        csaData.DeparttureDate = xmlNode.SelectSingleNode("DeparttureDate").InnerText;
                        csaData.WorkMark = xmlNode.SelectSingleNode("WorkMark").InnerText;
                        csaData.DataDealFlag = xmlNode.SelectSingleNode("DataDealFlag").InnerText;
                        csaData.BillNo = xmlNode.SelectSingleNode("BillNo").InnerText;
                        csaData.EntryId = xmlNode.SelectSingleNode("EntryId").InnerText;
                        csaData.PreNo = xmlNode.SelectSingleNode("PreNo").InnerText;
                        csaData.MtApplyBlNo = xmlNode.SelectSingleNode("MtApplyBlNo").InnerText;
                        csaData.Remark = xmlNode.SelectSingleNode("Remark").InnerText;
                        #endregion

                        #region 报文中Data节点的部分子节点数据校验
                        //校验报文中的Data节点下的ContaId节点数据
                        checkResult += dataCheckRules.CheckDataContaId(csaData.ContaId);

                        //校验报文中的Data节点下的ContaTypeCode节点数据
                        checkResult += dataCheckRules.CheckDataContaTypeCode(csaData.ContaTypeCode);

                        //校验报文中的Data节点下的Seat节点数据
                        checkResult += dataCheckRules.CheckDataSeat(csaData.Seat);

                        //校验报文中的Data节点下的TradeMark节点数据
                        checkResult += dataCheckRules.CheckDataTradeMark(csaData.TradeMark);

                        //校验报文中的Data节点下的IEFlag节点数据
                        checkResult += dataCheckRules.CheckDataIEFlag(csaData.TradeMark, csaData.IEFlag);

                        //校验报文中的Data节点下的LoadMark节点数据
                        checkResult += dataCheckRules.CheckDataLoadMark(csaData.LoadMark);

                        //校验报文中的Data节点下的DangerMark节点数据
                        checkResult += dataCheckRules.CheckDataDangerMark(csaData.DangerMark);

                        //校验报文中的Data节点下的EntranceDate节点数据
                        checkResult += dataCheckRules.CheckDataEntranceDate(csaData.EntranceDate);

                        //校验报文中的Data节点下的DataDealFlag节点数据
                        checkResult += dataCheckRules.CheckDataDataDealFlag(fileInfo.FullName, csaData.DataDealFlag, csaData.EntranceDate, csaData.DeparttureDate, csaData.ContaId);

                        //校验报文中的Data节点下的WorkMark节点数据
                        checkResult += dataCheckRules.CheckDataWorkMark(csaData.WorkMark);
                        #endregion

                        if (string.IsNullOrEmpty(checkResult))
                        {
                            try
                            {
                                DataBaseOperate.InsertDataIntoCsaData(sqlConnection, csaData, csaHead.MsgId);
                                sqlConnection.Close();
                            }
                            catch (Exception ex)
                            {
                                logContent.MsgId = csaHead.MsgId;
                                logContent.FilePath = fileInfo.FullName;
                                logContent.ContaId = csaData.ContaId;
                                logContent.ErrorDescription = ex.Message;
                                log.Error(logContent);
                            }
                        }
                        else
                        {
                            logContent.MsgId = csaHead.MsgId;
                            logContent.FilePath = fileInfo.FullName;
                            logContent.ContaId = csaData.ContaId;
                            logContent.ErrorDescription = checkResult;
                            log.Error(logContent);

                            checkResult = string.Empty;
                        }
                    }
                    #endregion
                    ShowPro(currentNumber, fileInfoArray.Length);
                }

                this.button_StartAnalyze.Enabled = false;
            }
        }

        private void button_GetAnalyzeResult_Click(object sender, EventArgs e)
        {
            string tableName = "LogInfo";
            DataGridViewBindData(tableName);
        }

        private void button_CleanAnalyzeResult_Click(object sender, EventArgs e)
        {
            DataBaseOperate.TruncateTable();

            string tableName = "LogInfo";
            DataGridViewBindData(tableName);

            this.button_StartAnalyze.Enabled = true;
            this.progressBar_DataDealProcess.Value = 0;
            this.label_DataDealProcess.Text = 0 + "% Processing...";
            this.label_DataDealProcess.Refresh();

        }

        private void DataGridViewBindData(string tableName)
        {
            DataSet ds = DataBaseOperate.GetLogInfo(tableName);

            if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
            {
                this.dataGridView_AnalyzeResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                this.dataGridView_AnalyzeResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }

            System.Data.DataTable table = new DataTable();
            System.Data.DataColumn column = new DataColumn();

            column.ColumnName = "序号";
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            table.Columns.Add(column);
            table.Merge(ds.Tables[0]);

            this.dataGridView_AnalyzeResult.DataSource = table;
            this.dataGridView_AnalyzeResult.Refresh();
        }

        private void dataGridView_AnalyzeResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //点击button按钮事件
            if (this.dataGridView_AnalyzeResult.Columns[e.ColumnIndex].Name == "button_SearchFileInIE" && e.RowIndex >= 0)
            {
                string filePath = Convert.ToString(this.dataGridView_AnalyzeResult.CurrentRow.Cells[4].Value);
                System.Diagnostics.Process.Start(@"IExplore.exe", filePath);
            }
        }

        private void ShowPro(int current, int total)
        {
            int percentageValue = 0;
            if (total != 0)
            {
                percentageValue = (current * 100 / total);
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new ProgressBarShow(ShowPro), current, total);
            }
            else
            {
                this.progressBar_DataDealProcess.Value = percentageValue;
                this.label_DataDealProcess.Text = percentageValue + "% Processing...";
                this.label_DataDealProcess.Refresh();
            }
        }

        private void button_DeriviveToExcel_Click(object sender, EventArgs e)
        {
            //获取数据
            DataTable dataTable = null;
            string tableName = "LogInfo";
            DataSet dataSet = DataBaseOperate.GetLogInfo(tableName);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                dataTable = dataSet.Tables[tableName];
            }

            CommFuction.DataGridViewToExcel(dataTable);
        }

        private void button_CreateExcel_Click(object sender, EventArgs e)
        {           
            #region 框架学习
            //HSSFWorkbook hSSFWorkbook = new HSSFWorkbook();
            //DocumentSummaryInformation documentSummaryInformation =PropertySetFactory.CreateDocumentSummaryInformation() ;
            //documentSummaryInformation.Company = "Deyou";

            //SummaryInformation summaryInformation = PropertySetFactory.CreateSummaryInformation();
            //summaryInformation.Author = "XMG";

            //hSSFWorkbook.DocumentSummaryInformation = documentSummaryInformation;
            //hSSFWorkbook.SummaryInformation = summaryInformation;

            //HSSFSheet hSSFSheet=(HSSFSheet) hSSFWorkbook.CreateSheet("Sheet1");

            //HSSFRow row1 = (HSSFRow)hSSFSheet.CreateRow(0);
            //row1.CreateCell(0).SetCellValue(1);

            //HSSFPatriarch hSSFPatriarch =(HSSFPatriarch) hSSFSheet.CreateDrawingPatriarch();
            //HSSFComment hSSFComment = hSSFPatriarch.CreateComment(new HSSFClientAnchor(0,0,0,0,1,2,4,4));
            //hSSFComment.String = new HSSFRichTextString("Hello World");
            //hSSFComment.Author = "XMG";

            //HSSFCell hSSFCell =(HSSFCell) hSSFSheet.CreateRow(1).CreateCell(1);
            //hSSFCell.CellComment = hSSFComment;

            //HSSFCell hSSFCellTime = (HSSFCell)hSSFSheet.CreateRow(2).CreateCell(1);
            //hSSFCellTime.SetCellValue(new DateTime(2020,01,17));
            //HSSFCellStyle hSSFCellStyle =(HSSFCellStyle) hSSFWorkbook.CreateCellStyle();
            //HSSFDataFormat hSSFDataFormat =(HSSFDataFormat) hSSFWorkbook.CreateDataFormat();
            //hSSFCellStyle.DataFormat = hSSFDataFormat.GetFormat("yyyy年MM月dd日");
            //hSSFCellTime.CellStyle = hSSFCellStyle;

            //HSSFCell hSSFCellMoney = (HSSFCell)hSSFSheet.CreateRow(3).CreateCell(1);
            //hSSFCellMoney.SetCellValue(2000);
            //HSSFCellStyle hSSFCellStyleMoney = (HSSFCellStyle)hSSFWorkbook.CreateCellStyle();
            //HSSFDataFormat hSSFDataFormatMoney = (HSSFDataFormat)hSSFWorkbook.CreateDataFormat();
            //hSSFCellStyleMoney.DataFormat = hSSFDataFormatMoney.GetFormat("￥#,##0");
            //hSSFCellMoney.CellStyle = hSSFCellStyleMoney;

            //HSSFCell hSSFCellRange = (HSSFCell)hSSFSheet.CreateRow(4).CreateCell(1);
            //hSSFCellRange.SetCellValue("Sales Report");
            //HSSFCellStyle hSSFCellStyleRange = (HSSFCellStyle)hSSFWorkbook.CreateCellStyle();
            //hSSFCellStyleRange.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            //HSSFFont hSSFFontRange = (HSSFFont)hSSFWorkbook.CreateFont();
            //hSSFFontRange.FontHeight = 20 * 20;
            //hSSFCellStyleRange.SetFont(hSSFFontRange);
            //hSSFCellRange.CellStyle = hSSFCellStyleRange;



            //hSSFSheet.Header.Center = "This is the PageHeader";
            //hSSFSheet.Footer.Left = "Copyright   Deyou";
            //hSSFSheet.Footer.Right = "created by XMG";           

            //FileStream fileStream = new FileStream(@"test.xls", FileMode.Create);
            //hSSFWorkbook.Write(fileStream);
            //fileStream.Close();
            //MessageBox.Show("Excel文件创建成功！");
            #endregion
        }
    }
}
