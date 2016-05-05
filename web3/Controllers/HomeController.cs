using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web3.Domain.Entities;
using web3.Domain.Concrete;
using web3.Tools;

namespace web3.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 全局模型上下文对象
        /// </summary>
        EFDbContext efdb = new EFDbContext();
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 检查用户是否存在,  将来用ajax异步调用
        /// </summary>
        /// <returns></returns>
        public string CheckLogin()
        {
            return "用户名不存在";
        }

        /// <summary>
        /// 用户列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            IQueryable<Web_User> user = efdb.Users;
            return View(user);
        }

        [HttpGet]
        public ViewResult Register() { return View(); }

        [HttpPost]
        public ActionResult Register(Web_User user)
        {
            //if(efdb.Users.FirstOrDefault(m => m.u_name == user.u_name) != null)
            //{

            //}
            try
            {
                efdb.Users.Add(new Web_User
                {
                    u_name = user.u_name,
                    u_password = Tools.Tookit.md5(user.u_password),
                    u_email = user.u_email
                });
                efdb.SaveChanges();
                ViewBag.info = "注册成功";
            }
            catch
            {
                ViewBag.info = "注册失败, 请重新确认您的注册信息";
            }
            return PartialView("doRegister");
        }

        [HttpPost]
        public ActionResult Login(Web_User user, FormCollection fc)
        {
            string code = fc["validatecode"];
            if (Session["ValidateCode"].ToString() != code)
            {
                TempData["valid"] = "验证码错误";
                return Redirect("/Home/Index");
            }

            string username = user.u_name;
            string password = Tools.Tookit.md5(user.u_password);
            Web_User findUser =  efdb.Users.FirstOrDefault(m => m.u_name == username && m.u_password == password);
            if (findUser == null)
            {
                TempData["info"] = "登录失败";
                TempData["u_name"] = username;
                TempData["u_password"] = password;
                return Redirect("/Home/Index");
            }
            else
            {
                Session.Add("u_name", username);
            }
            return RedirectToAction("Index", "User");
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        /// <summary>
        /// 检查用户师傅存在
        /// </summary>
        /// <returns></returns>
        public string checkUser(string data)
        {
            Web_User user = efdb.Users.FirstOrDefault(m => m.u_name == data);
            if (user != null)
                return "true";
            return "false";
        }
    }
}