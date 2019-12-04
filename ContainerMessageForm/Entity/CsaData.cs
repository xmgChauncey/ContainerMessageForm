using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerMessageForm
{
    public class CsaData
    {
        private string contaId;

        /// <summary>
        /// 箱号
        /// </summary>
        public string ContaId
        {
            get { return contaId; }
            set { contaId = value; }
        }
        private string contaTypeCode;

        /// <summary>
        /// 集装箱尺寸
        /// </summary>
        public string ContaTypeCode
        {
            get { return contaTypeCode; }
            set { contaTypeCode = value; }
        }
        private string seat;

        /// <summary>
        /// 箱位
        /// </summary>
        public string Seat
        {
            get { return seat; }
            set { seat = value; }
        }
        private string tradeMark;

        /// <summary>
        /// 内外贸标识
        /// </summary>
        public string TradeMark
        {
            get { return tradeMark; }
            set { tradeMark = value; }
        }
        private string iEFlag;

        /// <summary>
        /// 进出口标识
        /// </summary>
        public string IEFlag
        {
            get { return iEFlag; }
            set { iEFlag = value; }
        }
        private string contaMark;

        /// <summary>
        /// 拼箱状态
        /// </summary>
        public string ContaMark
        {
            get { return contaMark; }
            set { contaMark = value; }
        }
        private string loadMark;

        /// <summary>
        /// 装载状态
        /// </summary>
        public string LoadMark
        {
            get { return loadMark; }
            set { loadMark = value; }
        }
        private string dangerMark;

        /// <summary>
        /// 危品柜状态
        /// </summary>
        public string DangerMark
        {
            get { return dangerMark; }
            set { dangerMark = value; }
        }
        private string entranceDate;

        /// <summary>
        /// 进场时间
        /// </summary>
        public string EntranceDate
        {
            get { return entranceDate; }
            set { entranceDate = value; }
        }
        private string departtureDate;

        /// <summary>
        /// 离场时间
        /// </summary>
        public string DeparttureDate
        {
            get { return departtureDate; }
            set { departtureDate = value; }
        }
        private string workMark;

        /// <summary>
        /// 当前状态
        /// </summary>
        public string WorkMark
        {
            get { return workMark; }
            set { workMark = value; }
        }
        private string dataDealFlag;

        /// <summary>
        /// 数据处理标识
        /// </summary>
        public string DataDealFlag
        {
            get { return dataDealFlag; }
            set { dataDealFlag = value; }
        }
        private string billNo;

        /// <summary>
        /// 提单号
        /// </summary>
        public string BillNo
        {
            get { return billNo; }
            set { billNo = value; }
        }
        private string entryId;

        /// <summary>
        /// 报关单号
        /// </summary>
        public string EntryId
        {
            get { return entryId; }
            set { entryId = value; }
        }
        private string preNo;

        /// <summary>
        /// 转关单号
        /// </summary>
        public string PreNo
        {
            get { return preNo; }
            set { preNo = value; }
        }
        private string mtApplyBlNo;

        /// <summary>
        /// 多式联运单号
        /// </summary>
        public string MtApplyBlNo
        {
            get { return mtApplyBlNo; }
            set { mtApplyBlNo = value; }
        }
        private string remark;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
