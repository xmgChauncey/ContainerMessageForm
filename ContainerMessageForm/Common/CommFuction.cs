using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

            if(string.IsNullOrEmpty(checkedNumberString))
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
        public  static bool CheckSeat(string seat)
        {
            Regex regex = new Regex(@"^\d{2}/\d{3}/\d{3}/\d{2}$");
            if(string.IsNullOrEmpty(seat))
            {
                return false;
            }

            return regex.IsMatch(seat);
        }
    }
}
