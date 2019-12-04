using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerMessageForm
{
    public class CsaHead
    {
        private string msgId;

        /// <summary>
        /// 报文编号
        /// </summary>
        public string MsgId
        {
            get { return msgId; }
            set { msgId = value; }
        }
        private string msgType;

        /// <summary>
        /// 报文类型
        /// </summary>
        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }
        private string customsCode;

        /// <summary>
        /// 海关代码
        /// </summary>
        public string CustomsCode
        {
            get { return customsCode; }
            set { customsCode = value; }
        }
        private string supvLoctCode;

        /// <summary>
        /// 监管场所代码
        /// </summary>
        public string SupvLoctCode
        {
            get { return supvLoctCode; }
            set { supvLoctCode = value; }
        }
        private string declDate;

        /// <summary>
        /// 申报时间
        /// </summary>
        public string DeclDate
        {
            get { return declDate; }
            set { declDate = value; }
        }
        private string declareDataType;

        /// <summary>
        /// 申报数据类型 全量或者增量
        /// </summary>
        public string DeclareDataType
        {
            get { return declareDataType; }
            set { declareDataType = value; }
        }
        private string totalMsgNo;

        /// <summary>
        /// 报文总数
        /// </summary>
        public string TotalMsgNo
        {
            get { return totalMsgNo; }
            set { totalMsgNo = value; }
        }
        private string curMsgNo;

        /// <summary>
        /// 当前报文序号
        /// </summary>
        public string CurMsgNo
        {
            get { return curMsgNo; }
            set { curMsgNo = value; }
        }
    }
}
