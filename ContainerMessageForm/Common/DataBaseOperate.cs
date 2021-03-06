﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerMessageForm.Common
{
    public abstract class DataBaseOperate
    {

        /// <summary>
        /// 获取系统参数表中的代码和释义
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetCodeAndExplanationByCategory(string category)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string sqlStr = string.Format(@"SELECT
    sc.SYS_CODE,
    sc.SYS_EXPLANATION
FROM
    SystemConfigure AS sc
WHERE
    sc.SYS_CATEGORY = '{0}' ", category);

            SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);

            while (sqlDataReader.Read())
            {
                string code = sqlDataReader[0].ToString();
                string explanation = sqlDataReader[1].ToString();
                dic.Add(code, explanation);
            }

            return dic;
        }

        /// <summary>
        /// 获取江苏所有关区代码及名称
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetNanjingCustomsInfo()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string sqlStr = string.Format(@"SELECT
	njc.CUSTOMS_CODE,
	njc.CUSTOMS_NAME 
FROM
	NanJingCustoms AS njc ");

            SqlDataReader sqlDataReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);

            while (sqlDataReader.Read())
            {
                string customsCode = sqlDataReader[0].ToString();
                string customsName = sqlDataReader[1].ToString();
                dic.Add(customsCode, customsName);
            }

            return dic;
        }

        public static void InsertDataIntoCsaHead(SqlConnection conn, CsaHead csaHead)
        {
            if (csaHead != null)
            {
                string insertSqlStr = string.Format(@"INSERT INTO CsaHead ( GUID, MSGID, MSGTYPE, CUSTOMSCODE, SUPVLOCTCODE, DECLDATE, DECLAREDATATYPE, TOTALMSGNO, CURMSGNO )
VALUES
	( NEWID ( ), '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' )", csaHead.MsgId, csaHead.MsgType, csaHead.CustomsCode, csaHead.SupvLoctCode, csaHead.DeclDate, csaHead.DeclareDataType, csaHead.TotalMsgNo, csaHead.CurMsgNo);

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, insertSqlStr);
            }
        }

        public static void InsertDataIntoCsaData(SqlConnection conn, CsaData csaData, string msgId)
        {
            if (csaData != null)
            {
                string insertSqlStr = string.Format(@"INSERT INTO CsaData (
GUID,
MSGID,
CONTAID,
CONTATYPECODE,
SEAT,
TRADEMARK,
IEFLAG,
CONTAMARK,
LOADMARK,
DANGERMARK,
ENTRANCEDATE,
DEPARTTUREDATE,
WORKMARK,
DATADEALFLAG,
BILLNO,
ENTRYID,
PRENO,
MTAPPLYBLNO,
REMARK 
)
VALUES
	( NEWID ( ),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}' ) ",
    msgId, csaData.ContaId, csaData.ContaTypeCode, csaData.Seat, csaData.TradeMark, csaData.IEFlag, csaData.ContaMark,
    csaData.LoadMark, csaData.DangerMark, csaData.EntranceDate, csaData.DeparttureDate, csaData.WorkMark, csaData.DataDealFlag,
    csaData.BillNo, csaData.EntryId, csaData.PreNo, csaData.MtApplyBlNo, csaData.Remark);

                SqlHelper.ExecuteNonQuery(conn, CommandType.Text, insertSqlStr);
            }
        }

        public static void TruncateTable()
        {
            string sqlStr = string.Format(@"TRUNCATE TABLE CsaHead;
TRUNCATE TABLE CsaData;
TRUNCATE TABLE LogInfo;");

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
        }

        public static DataSet GetLogInfo(string tableName)
        {
            string sqlStr = string.Format(@"SELECT
	li.MSG_ID AS '报文编码',
	li.CONTA_ID AS '箱号',
	li.FILE_PATH AS '文件路径',
	li.ERROR_DESCRIPTION AS '错误描述' 
FROM
	LogInfo li 
ORDER BY
	LOG_DATE");

            return SqlHelper.ExecuteSqlDataAdapter(SqlHelper.ConnectionStringLocalTransaction, sqlStr, tableName);
        }
    }
}
