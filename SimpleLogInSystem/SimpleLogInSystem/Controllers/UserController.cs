using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleLogInSystem.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.email, user.password))
                {
                    FormsAuthentication.SetAuthCookie(user.email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect.");
                       
                }
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbEntitiesContext())
                {
                    var crypto = new SimpleCrypto.PBKDF2();

                    var encrpPass = crypto.Compute(user.password);

                    var sysUser = db.SystemUsers.Create();

                    sysUser.Email = user.email;
                    sysUser.Password = encrpPass;
                    sysUser.PasswordSalt = crypto.Salt;
                    sysUser.UserId = Guid.NewGuid();

                    db.SystemUsers.Add(sysUser);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");

                }
            }
            else
            {
                ModelState.AddModelError("", "Login Data is incorrect");
            }
            return View(user);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        private bool IsValid(string email, string password)
        {          
            var Crypto = new SimpleCrypto.PBKDF2();
            bool IsValid = false;

            using(var db = new MainDbEntitiesContext())
            {
                var user = db.SystemUsers.FirstOrDefault(u => u.Email == email);

                if(user != null)
                {
                    if(user.Password == Crypto.Compute(password, user.PasswordSalt))
                    {
                        IsValid = true;
                    }
                }
            }


            return IsValid;
        }
    }
}
