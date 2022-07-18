using System.Text.RegularExpressions;

namespace Common
{
    public class IsValueUtil
    {
        /// <summary>
        /// 手机校验
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^[1]+[3,5,8]+\d{9}");
        }

        /// <summary>
        /// 邮箱校验
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?");
        }
    }
}
