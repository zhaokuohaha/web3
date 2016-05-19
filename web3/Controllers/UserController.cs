using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web3.Domain.Concrete;
using web3.Domain.Entities;
using web3.Tools;
namespace web3.Controllers
{
	[LoginAuth]
    public class UserController : Controller
    {
        EFDbContext efdb = new EFDbContext();
        /// <summary>
        /// 首页---编辑用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string username = Convert.ToString(Session["u_name"]);
            ViewBag.uname = username;
            Web_User dbuser = efdb.Users.FirstOrDefault(m => m.u_name == username);
			string[] hobbies = dbuser.u_hoby.Split(',');
			//ViewBag.hobbies = hobbies;
			//foreach(string item in hobbies)
			//{
			//	ViewBag[item] = 1;
			//}
			//if (dbuser.u_hoby.Contains("sport"))
			//	ViewBag.sport = 1;
			//if (dbuser.u_hoby.Contains("music"))
			//	ViewBag.music = 1;
			//if (dbuser.u_hoby.Contains("book"))
			//	ViewBag.book = 1;
			return View(dbuser);
        }
		public ActionResult Password()
		{
			string uname = Session["u_name"].ToString();
			var user = efdb.Users.FirstOrDefault(u => u.u_name == uname);
			return View(user);
		}
		public string checkPassword(string username, string password)
		{
			string mdpsw = Tookit.md5(password);
			if (efdb.Users.FirstOrDefault(u => u.u_name == username && u.u_password == mdpsw) == null)
				return "false";
			return "true";
		}
		public ActionResult changePassword(Web_User u)
		{
			string psw = Tookit.md5(u.u_password);
			var user = efdb.Users.FirstOrDefault(m => m.u_name == u.u_name);
			user.u_password = psw;
			try {
				efdb.SaveChanges();
				ViewBag.info = "修改成功!";
				return View();
			}
			catch (Exception ex) {
				ViewBag.info = "修改失败: " + ex.Message;
				return View();
			}
		}
		[ValidateInput(false)]
        public ActionResult Edit(Web_User user, FormCollection fc, HttpPostedFileBase image)
        {

			Web_User u = efdb.Users.FirstOrDefault(m => m.u_name == user.u_name);
			if (image != null)
			{
				user.u_image = image.ContentType;
				string path = Path.Combine(HttpContext.Server.MapPath("../Uploads"),image.FileName);
				string thmbPath = Path.Combine(HttpContext.Server.MapPath("../Thumbnail"), image.FileName);
				image.SaveAs(path);
				Tookit.MakeThumbnail(path, thmbPath, 100, 100, "HW");
				user.u_imagedata = image.FileName;
			}
			else
			{
				user.u_image = u.u_image;
				user.u_imagedata = u.u_imagedata;
			}
            ViewBag.user = user.u_name;
			ViewBag.password = user.u_password;
			string hobby = fc.GetValue("hobby").AttemptedValue;
			hobby = hobby.Replace(",false", "");

			if (u != null)
            {
                u.u_name = user.u_name;
                u.u_email = user.u_email;
                u.u_sex = user.u_sex;
                u.u_tel = user.u_tel;
                u.u_hoby = hobby;
                u.u_intro = fc["content"];
                u.u_birth = DateTime.Parse(fc["birthday"]);
				u.u_image = user.u_image;
				u.u_imagedata = user.u_imagedata;
				u.u_addr_sheng = fc["prov"];
				u.u_addr_shi = fc["city"];
				u.u_addr_xian = fc["dist"];
            };
            efdb.SaveChanges();
            return View(u);
        }


		/// <summary>
		/// 获取相片信息
		/// </summary>
		/// <param name="u_id"></param>
		/// <returns></returns>
		public FilePathResult GetIamge(int u_id)
		{
			Web_User user = efdb.Users.FirstOrDefault(p => p.u_id == u_id);
			if (user != null)
			{
				string path = Path.Combine(HttpContext.Server.MapPath("../Uploads"), user.u_imagedata);
				TempData["path"] = path;
				FilePathResult f = new FilePathResult(path, user.u_image);
				return f;
			}
			else
			{
				return null;
			}
		}
	}
}