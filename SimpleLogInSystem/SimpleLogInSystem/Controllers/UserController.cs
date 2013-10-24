using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult LogIn(SimpleLogInSystem.Models.UserModel user)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(SimpleLogInSystem.Models.UserModel user)
        {
            return View();
        }

        private bool IsValid(string email, string password)
        {          
            var Crypto = new SimpleCrypto.PBKDF2();
            bool IsValid = false;

            using(var db = new MainDbEntitiesContext)
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
