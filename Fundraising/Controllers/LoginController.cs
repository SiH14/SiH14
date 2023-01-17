using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fundraising.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;

namespace Fundraising.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly FundraisingDbContext _context;

		public LoginController(FundraisingDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public string login(LoginPost value)
		{
			var user = (from a in _context.Users
						where a.UserEmail == value.UserEmail
						&& a.UserPassword == value.UserPassword
						select a).SingleOrDefault();

			if (user == null)
			{
				return "帳號密碼錯誤";
			}
			else
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserEmail),
					new Claim("userID", user.UserId.ToString()),
					//new Claim("userPhoto", user.UserPhoto)
				               // new Claim(ClaimTypes.Role, "Administrator")
				            };

				var authProperties = new AuthenticationProperties
				{



					IsPersistent = true,
					// Whether the authentication session is persisted across 
					// multiple requests. When used with cookies, controls
					// whether the cookie's lifetime is absolute (matching the
					// lifetime of the authentication ticket) or session-based.

					//IssuedUtc = <DateTimeOffset>,
					// The time at which the authentication ticket was issued.

					//RedirectUri = <string>
					// The full path or absolute URI to be used as an http 
					// redirect response value.
				};

				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

			}
			return user.UserId.ToString();
		}
		public class LoginPost
		{
			public string UserEmail { get; set; }
			public string UserPassword { get; set; }
		}

		[HttpGet("getuserid")]
		public IActionResult getuserID()
		{
			var Claim = HttpContext.User.Claims.ToList();
			var userID = Claim.Where(a => a.Type == "userID").First().Value;
			return Content(userID);
		}

		[HttpGet("getuserphoto/{id}")]
		public ActionResult<IEnumerable<dynamic>> getuserPhoto(string id)
		{
			var user = _context.Users.Where(x => x.UserId == Convert.ToInt32(id));
				return user.ToList();
		}

		[HttpDelete]
		public string logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return "已登出";
		}
		[HttpGet("NoLogin")]
		public string noLogin()
		{
			return "未登入";
		}
	}
}
