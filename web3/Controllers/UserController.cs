using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web3.Domain.Concrete;
using web3.Domain.Entities;
namespace web3.Controllers
{
    public class UserController : Controller
    {
        EFDbContext db = new EFDbContext();
        public ActionResult Index()
        {
            string username = Convert.ToString(Session["u_name"]);
            ViewBag.uname = username;
            Web_User dbuser = db.Users.FirstOrDefault(m => m.u_name == username);
            return View(dbuser);
        }
    }
}