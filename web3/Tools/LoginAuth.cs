using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace web3.Tools
{
	public class LoginAuth : AuthorizeAttribute
	{
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (httpContext.Session["username"] != null)
			{
				return true;
			}
			return false;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.HttpContext.Response.Redirect("/Home/Index");
		}
	}
}