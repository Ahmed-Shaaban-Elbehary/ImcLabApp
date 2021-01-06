using ImcLabApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace ImcLabApp.Controllers
{
    [HandleError]
    public class LoginController : Controller
    {
        AppDbContext db = new AppDbContext();

        // GET: Login
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Users users)
        {

            var userInDb = db.Users.Where(u => u.UserName == users.UserName && u.Password == users.Password).FirstOrDefault();
            
            if (userInDb != null)
            {
                var userDept = userInDb.Departments;
                if (userDept == "إشعة")
                {
                    Session["uId"] = userInDb.Id;
                    Session["uName"] = userInDb.UserName;
                    return RedirectToAction("Index", "Radios");
                }
                else if (userDept == "أورام")
                {
                    Session["uId"] = userInDb.Id;
                    Session["uName"] = userInDb.UserName;
                    return RedirectToAction("Index", "Tumors");
                }
                else if (userDept == "معمل")
                {
                    Session["uId"] = userInDb.Id;
                    Session["uName"] = userInDb.UserName;
                    return RedirectToAction("Index", "Labs");
                }
                else if (userDept == "مدير")
                {
                    Session["uId"] = userInDb.Id;
                    Session["uName"] = userInDb.UserName;
                    return RedirectToAction("Index", "adminPanel");
                }
                else
                {
                    ViewBag.errorMessage = "هذا المستخدم غير موجود";
                    return View("login", users);
                }
            }
            else
            {
                ViewBag.errorMessage = "إسم المستخدم او الرقم السري خطأ";
                return View("login", users);
            }
        }

        public ActionResult logout()
        {
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("login");
        }

    }
}