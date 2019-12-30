using ContainerMessageForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerMessageForm
{
    public class DataCheckRules
    {

        Dictionary<string, string> supvLoctInfo = DataBaseOperate.GetCodeAndExplanationByCategory("SupvLoctCode");
        Dictionary<string, string> msgTypeDic = DataBaseOperate.GetCodeAndExplanationByCategory("MsgType");
        Dictionary<string, string> customsCodeDic = DataBaseOperate.GetNanjingCustomsInfo();
        Dictionary<string, string> declareDataTypeDic = DataBaseOperate.GetCodeAndExplanationByCategory("DeclareDataType");
        Dictionary<string, string> contaTypeCodeDic = DataBaseOperate.GetCodeAndExplanationByCategory("ContaTypeCode");
        Dictionary<string, string> tradeMarkDic = DataBaseOperate.GetCodeAndExplanationByCategory("TradeMark");
        Dictionary<string, string> iEFlagDic = DataBaseOperate.GetCodeAndExplanationByCategory("IEFlag");
        Dictionary<string, string> loadMarkDic = DataBaseOperate.GetCodeAndExplanationByCategory("LoadMark");
        Dictionary<string, string> workMarkDic = DataBaseOperate.GetCodeAndExplanationByCategory("WorkMark");
        Dictionary<string, string> dataDealFlagDic = DataBaseOperate.GetCodeAndExplanationByCategory("DataDealFlag");
        Dictionary<string, string> dangerMarkDic = DataBaseOperate.GetCodeAndExplanationByCategory("DangerMark");

        #region 报文中Head节点的子节点校验
        /// <summary>
        /// 校验报文的Head节点下的MsgId节点
        /// </summary>
        /// <param name="msgId"></param>
        public string CheckHeadMsgId(string msgId)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(msgId))
            {
                checkResult += "发送的报文中MsgId字段值为空";
            }

            //长度校验
            if (msgId.Length != 30)
            {
                checkResult += "报文的MsgId的长度不是30个字符。";
            }

            #region 校验MsgId的组成是否规范
            //业务类型（2位）＋作业场所（场地）代码＋
            //四位年＋两位月＋两位日＋两位小时（24小时制）＋两位分钟＋两位秒＋三位毫秒。
            //业务类型默认值： 01集装箱堆场数据。
            string businessTypeString = msgId.Substring(0, 2);
            string supvLoctCodeString = msgId.Substring(2, 11);
            string createTimeString = msgId.Substring(13, msgId.Length - 13);

            if (businessTypeString != "01")
            {
                checkResult += "发送的报文不是监管作业场所集装箱堆场数据申报报文。";
            }

            if (!supvLoctInfo.Keys.Contains(supvLoctCodeString))
            {
                checkResult += "报文编码中的作业场所代码未备案。";
            }

            try
            {
                DateTime.ParseExact(createTimeString, "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                checkResult += "报文编码中的时间格式不对。" + ex.Message;
            }
            #endregion

            return checkResult;
        }

        /// <summary>
        /// 校验报文的Head节点下的MsgType节点
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string CheckHeadMsgType(string msgType)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(msgType))
            {
                checkResult += "发送的报文中MsgType字段值为空";
            }

            if (!msgTypeDic.Keys.Contains(msgType))
            {
                checkResult = "发送的报文类型不正确：" + msgType + "。";
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文的Head节点下的CustomsCode节点
        /// </summary>
        /// <param name="customsCode"></param>
        /// <returns></returns>
        public string CheckHeadCustomsCode(string customsCode)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(customsCode))
            {
                checkResult += "发送的报文中CustomsCode字段值为空";
            }

            if (!customsCodeDic.Keys.Contains(customsCode))
            {
                checkResult += "发送报文的海关代码非南京关区：" + customsCode + "。";
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文的Head节点下的SupvLocCode节点
        /// </summary>
        /// <param name="supvLocCode"></param>
        /// <returns></returns>
        public string CheckHeadSupvLoctCode(string supvLoctCode)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(supvLoctCode))
            {
                checkResult += "发送的报文中SupvLocCode字段值为空";
            }

            if (!supvLoctInfo.Keys.Contains(supvLoctCode))
            {
                checkResult += "报文中的作业场所代码未备案：" + supvLoctCode + "。";
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文的Head节点下的DeclDate节点
        /// </summary>
        /// <param name="declDate"></param>
        /// <returns></returns>
        public string CheckHeadDeclDate(string declDate)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(declDate))
            {
                checkResult += "发送的报文中DeclDate字段值为空";
            }

            try
            {
                DateTime.ParseExact(declDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                checkResult += "报文中报文发送时间的格式不对：" + declDate + "。" + ex.Message + "。";
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文的Head节点下的DeclareDataType节点
        /// </summary>
        /// <param name="declareDataType"></param>
        /// <returns></returns>
        public string CheckHeadDeclareDataType(string declareDataType)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(declareDataType))
            {
                checkResult += "发送的报文中DeclareDataType字段值为空";
            }

            if (!declareDataTypeDic.Keys.Contains(declareDataType))
            {
                checkResult += "报文中的申报数据类型不是全量或者增量：" + declareDataType + "。";
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文的Head节点下的TotalMsgNo节点
        /// </summary>
        /// <param name="totalMsgNo"></param>
        /// <param name="filesCount"></param>
        /// <returns></returns>
        public string CheckHeadTotalMsgNo(string totalMsgNo, string filesCount)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(totalMsgNo))
            {
                checkResult += "发送的报文中TotalMsgNo字段值为空";
            }
            else
            {
                //数字组成校验
                if (!CommFuction.CheckIsNumber(totalMsgNo))
                {
                    checkResult += "发送的报文中TotalMsgNo字段值包含非数字字符：" + totalMsgNo + "。";
                }
                else
                {
                    //发送的报文数量校验
                    if (!totalMsgNo.Equals(filesCount))
                    {
                        checkResult += "报文发送的数量少于总数：" + totalMsgNo + "。";
                    }
                }
            }

            return checkResult;
        }

        /// <summary>
        /// 校验报文的Head节点下的CurMsgNo节点
        /// </summary>
        /// <param name="totalMsgNo"></param>
        /// <param name="curMsgNo"></param>
        /// <returns></returns>
        public string CheckHeadCurMsgNo(string totalMsgNo, string curMsgNo)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(curMsgNo))
            {
                checkResult += "发送的报文中CurMsgNo字段值为空";
            }
            else
            {
                //数字组成校验
                if (!CommFuction.CheckIsNumber(curMsgNo))
                {
                    checkResult += "发送的报文中CurMsgNo字段值包含非数字字符：" + curMsgNo + "。";
                }
                else
                {
                    //发送的报文数量校验
                    try
                    {
                        if (!(int.Parse(curMsgNo) > 0 && int.Parse(curMsgNo) <= int.Parse(totalMsgNo)))
                        {
                            checkResult += "发送报文中的CurMsgNo填写不正确：" + curMsgNo + "。";
                        }
                    }
                    catch (Exception ex)
                    {
                        checkResult += "发送报文中的CurMsgNo校验失败。" + ex.Message;
                    }
                }
            }

            return checkResult;
        }
        #endregion

        #region 报文中的Data节点的部分子节点校验

        /// <summary>
        /// 校验报文中Data节点下ContaId节点数据
        /// </summary>
        /// <param name="contaId"></param>
        /// <returns></returns>
        public string CheckDataContaId(string contaId)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(contaId))
            {
                checkResult += "该集装箱的的ContaId节点值为空";
            }
            else
            {
                if (!CommFuction.CheckIsNumberAndLetter(contaId))
                {
                    checkResult += "该集装箱的ContaId中包含非数字和字母的字符：" + contaId + "。";
                }
            }

            return checkResult;
        }

        /// <summary>
        /// 校验报文中Data节点下ContaTypeCode节点数据
        /// </summary>
        /// <param name="contaTypeCode"></param>
        /// <returns></returns>
        public string CheckDataContaTypeCode(string contaTypeCode)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(contaTypeCode))
            {
                checkResult += "该集装箱的的ContaTypeCode节点值为空";
            }
            else
            {
                if (!contaTypeCodeDic.Keys.Contains(contaTypeCode))
                {
                    checkResult += "该集装箱的的ContaTypeCode节点值不是标准的集装箱尺寸：" + contaTypeCode + "。";
                }
            }

            return checkResult;
        }

        /// <summary>
        /// 校验报文中Data节点下Seat节点数据
        /// </summary>
        /// <param name="seat"></param>
        /// <returns></returns>
        public string CheckDataSeat(string seat)
        {
            string checkResult = string.Empty;

            if (string.IsNullOrEmpty(seat))
            {
                checkResult += "该集装箱的的Seat节点值为空";
            }
            else
            {
                if (!CommFuction.CheckSeat(seat))
                {
                    checkResult += "该集装箱的的Seat节点值组成结构应该为‘xx/xxx/xxx/xx’，其中x标识0~9A~Z中的数字：" + seat + "。";
                }
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文中Data节点下TradeMark节点数据
        /// </summary>
        /// <param name="tradeMark"></param>
        /// <returns></returns>
        public string CheckDataTradeMark(string tradeMark)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(tradeMark))
            {
                checkResult += "该集装箱的的TradeMark节点值为空";
            }
            else
            {
                if (!tradeMarkDic.Keys.Contains(tradeMark))
                {
                    checkResult += "该集装箱的的TradeMark节点值不是D、I或者O：" + tradeMark + "。";
                }
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文中Data节点下IEFlag节点数据
        /// </summary>
        /// <param name="tradeMark"></param>
        /// <param name="iEFlag"></param>
        /// <returns></returns>
        public string CheckDataIEFlag(string tradeMark, string iEFlag)
        {
            string checkResult = string.Empty;
            if (tradeMark.Equals("I"))
            {
                if (string.IsNullOrEmpty(iEFlag))
                {
                    checkResult += "当集装箱为外贸时，进出口标识不能为空。";
                }
            }

            if (!string.IsNullOrEmpty(iEFlag))
            {
                if (!iEFlagDic.Keys.Contains(iEFlag))
                {
                    checkResult += "该集装箱的的IEFlag节点值不是I或者E：" + iEFlag + "。";
                }
            }
            return checkResult;
        }

        /// <summary>
        ///  校验报文中Data节点下LoadMark节点数据
        /// </summary>
        /// <param name="loadMark"></param>
        /// <returns></returns>
        public string CheckDataLoadMark(string loadMark)
        {
            string checkResult = string.Empty;
            if (string.IsNullOrEmpty(loadMark))
            {
                checkResult += "该集装箱的的LoadMark节点值为空。";
            }
            else
            {
                if (!loadMarkDic.Keys.Contains(loadMark))
                {
                    checkResult += "该集装箱的的LoadMark节点值不是E或者F：" + loadMark + "。";
                }
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文中Data节点下DangerMark节点数据
        /// </summary>
        /// <param name="dangerMark"></param>
        /// <returns></returns>
        public string CheckDataDangerMark(string dangerMark)
        {
            string checkResult = string.Empty;
            if (!string.IsNullOrEmpty(dangerMark))
            {
                if (!dangerMarkDic.Keys.Contains(dangerMark))
                {
                    checkResult += "该集装箱的的DangerMark节点值不是0或者W：" + dangerMark + "。";
                }
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文中Data节点下WorkMark节点数据
        /// </summary>
        /// <param name="workMark"></param>
        /// <returns></returns>
        public string CheckDataWorkMark(string workMark)
        {
            string checkResult = string.Empty;
            if (string.IsNullOrEmpty(workMark))
            {
                checkResult += "该集装箱的的WorkMark节点值为空。";
            }
            else
            {
                if (!workMarkDic.Keys.Contains(workMark))
                {
                    checkResult += "该集装箱的的WorkMark节点值不是A、B、C、D、E或者F：" + workMark + "。";
                }
            }
            return checkResult;
        }

        /// <summary>
        ///  校验报文中Data节点下EntranceDate节点数据
        /// </summary>
        /// <param name="entranceDate"></param>
        /// <returns></returns>
        public string CheckDataEntranceDate(string entranceDate)
        {
            string checkResult = string.Empty;

            //是否为空校验
            if (string.IsNullOrEmpty(entranceDate))
            {
                checkResult += "发送的报文中EntranceDate字段值为空";
            }
            try
            {
                DateTime.ParseExact(entranceDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                checkResult += "集装箱进场时间的格式不对：" + entranceDate + "。" + ex.Message + "。";
            }
            return checkResult;
        }

        /// <summary>
        /// 校验报文中Data节点下DataDealFlag、EntranceDate和DeparttureDate节点数据
        /// </summary>
        /// <param name="dataDealFlag"></param>
        /// <param name="entranceDate"></param>
        /// <param name="departtureDate"></param>
        /// <returns></returns>
        public string CheckDataDataDealFlag(string filePath, string dataDealFlag, string entranceDate, string departtureDate, string contaId)
        {
            string checkResult = string.Empty;
            if (string.IsNullOrEmpty(dataDealFlag))
            {
                checkResult += "该集装箱的的DataDealFlag节点值为空。";
            }
            else
            {
                if (!dataDealFlagDic.Keys.Contains(dataDealFlag))
                {
                    checkResult += "该集装箱的的DataDealFlag节点值不是A、M或者D：" + dataDealFlag + "。";
                }
                else
                {
                    switch (dataDealFlag)
                    {
                        case "D":
                            if (string.IsNullOrEmpty(departtureDate))
                            {
                                checkResult += "当该集装箱的的DataDealFlag为D时，出场时间不能为空。";
                            }
                            break;
                        default:
                            if (!string.IsNullOrEmpty(departtureDate))
                            {
                                checkResult += "当该集装箱的的DataDealFlag为A或者M时，该集装箱的的DeparttureDate节点值应该为空。";
                            }
                            break;
                    }
                }
            }
            return checkResult;
        }
        #endregion
    }
}
