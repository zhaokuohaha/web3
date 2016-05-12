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
            return View(dbuser);
        }

        [ValidateInput(false)]
        public ActionResult Edit(Web_User user, FormCollection fc, HttpPostedFileBase image)
        {
			
			if (image != null)
			{
				user.u_image = image.ContentType;
				string path = Path.Combine(HttpContext.Server.MapPath("../Uploads"),image.FileName);
				image.SaveAs(path);
				user.u_imagedata = image.FileName;
			}
            ViewBag.user = user.u_name;
			string hobby = fc.GetValue("hobby").AttemptedValue;
			hobby = hobby.Replace(",false", "");

			Web_User u = efdb.Users.FirstOrDefault(m => m.u_name == user.u_name);
			if (u != null)
            {
                u.u_name = user.u_name;
                u.u_password = Tools.Tookit.md5(user.u_password);
                u.u_email = user.u_email;
                u.u_sex = user.u_sex;
                u.u_tel = user.u_tel;
                u.u_hoby = hobby;
                u.u_intro = fc["content"];
                u.u_birth = DateTime.Parse(fc["birthday"]);
				u.u_image = user.u_image;
				u.u_imagedata = user.u_imagedata;
            };
            efdb.SaveChanges();
            return View(u);
        }



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