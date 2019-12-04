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
        Dictionary<string, string> contaTypeCOdeDic = DataBaseOperate.GetCodeAndExplanationByCategory("ContaTypeCode");

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
                checkResult = "发送的报文类型不正确";
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
                checkResult += "发送报文的海关代码非南京关区。";
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
                checkResult += "报文中的作业场所代码未备案。";
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
                checkResult += "报文中报文发送时间的格式不对。" + ex.Message;
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
                checkResult += "报文中的申报数据类型不是全量或者增量。";
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
                    checkResult += "发送的报文中TotalMsgNo字段值包含非数字字符";
                }
                else
                {
                    //发送的报文数量校验
                    if (!totalMsgNo.Equals(filesCount))
                    {
                        checkResult += "报文发送的数量少于总数。";
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
                    checkResult += "发送的报文中CurMsgNo字段值包含非数字字符";
                }
                else
                {
                    //发送的报文数量校验
                    try
                    {
                        if (!(int.Parse(curMsgNo) > 0 && int.Parse(curMsgNo) <= int.Parse(totalMsgNo)))
                        {
                            checkResult += "发送报文中的CurMsgNo填写不正确。";
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
                if(!CommFuction.CheckIsNumberAndLetter(contaId))
                {
                    checkResult += "该集装箱的ContaId中包含非数字和字母的字符";
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
            if(!string.IsNullOrEmpty(contaTypeCode))
            {
                checkResult += "该集装箱的的ContaTypeCode节点值为空";
            }
            else
            {
                if(!contaTypeCOdeDic.Keys.Contains(contaTypeCode))
                {
                    checkResult += "该集装箱的的ContaTypeCode节点值不是标准的集装箱尺寸";
                }
            }

            return checkResult;
        }
        #endregion
    }
}
