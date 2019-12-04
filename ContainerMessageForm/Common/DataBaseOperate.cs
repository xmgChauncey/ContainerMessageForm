using System;
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

        public static void InsertDataIntoCsaHead(CsaHead csaHead)
        {
            if (csaHead != null)
            {
                string insertSqlStr = string.Format(@"INSERT INTO CsaHead ( GUID, MSGID, MSGTYPE, CUSTOMSCODE, SUPVLOCTCODE, DECLDATE, DECLAREDATATYPE, TOTALMSGNO, CURMSGNO )
VALUES
	( NEWID ( ), '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}' )", csaHead.MsgId, csaHead.MsgType, csaHead.CustomsCode, csaHead.SupvLoctCode,csaHead.DeclDate,csaHead.DeclareDataType, csaHead.TotalMsgNo, csaHead.CurMsgNo);

                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, insertSqlStr);
            }
        }
    }
}
