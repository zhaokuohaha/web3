using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web3.Domain.Entities
{
    public class Web_User
    {
        /// <summary>
        /// 用户id,自增字段
        /// </summary>
        [Key]
        [Display(Name ="用户名: ")]
        public int u_id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名: ")]
        [Required(ErrorMessage ="请输入用户名")]
        public string u_name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码: ")]
        [Required(ErrorMessage ="请输入用户密码:")]
        public string u_password { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别: ")]
        public string u_sex { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = "电话号码: ")]
        public string u_tel { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期: ")]
        public DateTime u_birth { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "爱好: ")]
        public string u_hoby { get; set; }
        /// <summary>
        /// 自我介绍
        /// </summary>
        [Display(Name = "自我介绍: ")]
        public string u_intro { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像: ")]
        public string u_image { get; set; }

		/// <summary>
		/// 图片地址
		/// </summary>
		public string u_imagedata { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱: ")]
        [DataType(DataType.EmailAddress,ErrorMessage ="请输入正确的邮箱地址")]
        public string u_email { get; set; }

    }
}
