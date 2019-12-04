using ContainerMessageForm.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ContainerMessageForm
{
    public partial class ContainerMessageForm : Form
    {
        log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        LogContent logContent = new LogContent();

        public ContainerMessageForm()
        {
            InitializeComponent();
        }

        private void button_OpenFolder_Click(object sender, EventArgs e)
        {
            DataCheckRules dataCheckRules = new DataCheckRules();

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

                DirectoryInfo directoryInfo = new DirectoryInfo(folderBrowserDialog.SelectedPath);

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

                    foreach (FileInfo fileInfo in fileInfoArray)
                    {
                        string checkResult = string.Empty;

                        //获取xml文件
                        xmlDocument.Load(fileInfo.FullName);

                        #region 校验报文中Head节点的子节点数据
                        //获取Head节点及子节点数据
                        XmlNodeList xmlNodeListHead = xmlDocument.SelectNodes("/ContaDeclareInfo/Head");
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
                                DataBaseOperate.InsertDataIntoCsaHead(csaHead);
                            }
                            catch(Exception ex)
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
                        }
                        #endregion

                        #region 校验报文中Data节点的子节点数据
                        XmlNodeList xmlNodeListData = xmlDocument.SelectNodes("/ContaDeclareInfo/Declaration/Data");
                        foreach (XmlElement xmlElement in xmlNodeListData)
                        {
                            #region 获取Data节点的子节点数据
                            csaData.ContaId = xmlElement.GetElementsByTagName("ContaId")[0].InnerXml;
                            csaData.ContaTypeCode = xmlElement.GetElementsByTagName("ContaTypeCode")[0].InnerXml;
                            csaData.Seat = xmlElement.GetElementsByTagName("Seat")[0].InnerXml;
                            csaData.TradeMark = xmlElement.GetElementsByTagName("TradeMark")[0].InnerXml;
                            csaData.IEFlag = xmlElement.GetElementsByTagName("IEFlag")[0].InnerXml;
                            csaData.ContaMark = xmlElement.GetElementsByTagName("ContaMark")[0].InnerXml;
                            csaData.LoadMark = xmlElement.GetElementsByTagName("LoadMark")[0].InnerXml;
                            csaData.DangerMark = xmlElement.GetElementsByTagName("DangerMark")[0].InnerXml;
                            csaData.EntranceDate = xmlElement.GetElementsByTagName("EntranceDate")[0].InnerXml;
                            csaData.DeparttureDate = xmlElement.GetElementsByTagName("DeparttureDate")[0].InnerXml;
                            csaData.WorkMark = xmlElement.GetElementsByTagName("WorkMark")[0].InnerXml;
                            csaData.DataDealFlag = xmlElement.GetElementsByTagName("DataDealFlag")[0].InnerXml;
                            csaData.BillNo = xmlElement.GetElementsByTagName("BillNo")[0].InnerXml;
                            csaData.EntryId = xmlElement.GetElementsByTagName("EntryId")[0].InnerXml;
                            csaData.PreNo = xmlElement.GetElementsByTagName("PreNo")[0].InnerXml;
                            csaData.MtApplyBlNo = xmlElement.GetElementsByTagName("MtApplyBlNo")[0].InnerXml;
                            csaData.Remark = xmlElement.GetElementsByTagName("Remark")[0].InnerXml;
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
                            checkResult += dataCheckRules.CheckDataIEFlag(csaData.TradeMark,csaData.IEFlag);

                            //校验报文中的Data节点下的LoadMark节点数据
                            checkResult += dataCheckRules.CheckDataLoadMark(csaData.LoadMark);

                            //校验报文中的Data节点下的DataDealFlag节点数据
                            checkResult += dataCheckRules.CheckDataDataDealFlag(csaData.DataDealFlag, csaData.EntranceDate, csaData.DeparttureDate);

                            //校验报文中的Data节点下的WorkMark节点数据
                            checkResult += dataCheckRules.CheckDataWorkMark(csaData.WorkMark);
                            #endregion

                            if(string.IsNullOrEmpty(checkResult))
                            {
                                try
                                {
                                    DataBaseOperate.InsertDataIntoCsaData(csaData, csaHead.MsgId);
                                }
                                catch(Exception ex)
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
                            }
                        }

                        #endregion
                    }
                }
            }

        }
    }
}
