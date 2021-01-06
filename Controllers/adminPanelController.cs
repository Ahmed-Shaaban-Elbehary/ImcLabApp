using ImcLabApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ImcLabApp.Controllers
{
    [HandleError]
    public class adminPanelController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: adminPanel
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPatient()
        {
            return Json(db.Users.ToList(),JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Users());
            }
            else
            {
                return View(db.Users.Where(p => p.Id.Equals(id)).FirstOrDefault<Users>());
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(int id, Users u)
        {

            if (u.Id == 0)
            {
                var UserInDb = db.Users.Where(m => m.UserName == u.UserName).FirstOrDefault();
                if (UserInDb == null)
                {
                    db.Users.Add(u);
                    db.SaveChanges();
                    return Json(new { success = true, message = "تم الإضافة" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = "هذ المريض موجود بالفعل" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                var UserInDb = db.Users.Where(m => m.UserName == u.UserName).FirstOrDefault();
                if (UserInDb == null)
                {
                    db.Entry(u).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "تم التعديل" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = "هذ المريض موجود بالفعل" }, JsonRequestBehavior.AllowGet);
                }

            }

        }

        // GET: adminPanel/Delete/5
        public ActionResult Delete(int? id)
        {
            var UserInDb = db.Users.Where(e => e.Id == id).FirstOrDefault();
            if(UserInDb != null)
            {
                db.Users.Remove(UserInDb);
                db.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Patients()
        {
            return View();
        }
        public ActionResult DeletePatient(int id)
        {
            var patient = db.Patients.Where(p => p.Id.Equals(id)).FirstOrDefault();
            db.Patients.Remove(patient);
            db.SaveChanges();
            return Json(new { success = true, message = "تم المسح" }, JsonRequestBehavior.AllowGet);
        }
    }
}
