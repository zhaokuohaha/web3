using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
namespace web3.Tools
{
    public class Tookit
    {
        /// <summary>
        /// 生成MD5
        /// </summary>
        /// <param name="strPwd"></param>
        /// <returns></returns>
        public static string md5(string strPwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(strPwd);//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        public static string getimageRandom()
        {
            Random ro = new Random(unchecked((int)DateTime.Now.Ticks));
            String imageRandom = ro.Next().ToString();
            return imageRandom;
        }
    }
}