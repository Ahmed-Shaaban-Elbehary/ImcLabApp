using ImcLabApp.Models;
using ImcLabApp.Models.BackUpSystemModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImcLabApp.Controllers
{
    public class LabsController : Controller
    {
        AppDbContext db = new AppDbContext();

        // GET: UserHome
        public ActionResult Index()
        {
            return View();
        }

        //--------------------------------------//
        //CRUD OPERATIONS USING ASP.NET MVC AND DATATABLE OF PATIENTS.
        //-------------------------------------//

        // GET ALL PATIENTS.
        public JsonResult GetPatients()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(new { data = db.Patients.ToList() }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Patients());
            }
            else
            {
                return View(db.Patients.Where(p => p.Id.Equals(id)).FirstOrDefault<Patients>());
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(int id, Patients p)
        {

            if (p.Id == 0)
            {
                var patientInDB = db.Patients.Where(m => m.medicalNumber == p.medicalNumber).FirstOrDefault();
                if (patientInDB == null)
                {
                    db.Patients.Add(p);
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
                var patientInDB = db.Patients.Where(m => m.medicalNumber == p.medicalNumber).FirstOrDefault();
                if (patientInDB == null)
                {
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "تم التعديل" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = "هذ المريض موجود بالفعل" }, JsonRequestBehavior.AllowGet);
                }

            }

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var patient = db.Patients.Where(p => p.Id.Equals(id)).FirstOrDefault();
            db.Patients.Remove(patient);
            db.SaveChanges();
            return Json(new { success = true, message = "تم المسح" }, JsonRequestBehavior.AllowGet);
        }
        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //----------------------------------------Patient Profile-------------------------------------------
        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

        // GET: patientsProfile
        public ActionResult patientProfile(int? id)
        {
            var patients = db.Patients.Where(e => e.Id == id).FirstOrDefault();

            ViewBag.error = TempData["error"];
            ViewBag.success = TempData["success"];
            ViewBag.patientId = patients.Id;
            return View();
        }

        public JsonResult getPatientById(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var patient = db.Patients.Where(p => p.Id.Equals(id)).FirstOrDefault<Patients>();
            return Json(patient, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPatientResult(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var results = db.Labs.Where(l => l.patients_Id == id).ToList();
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //Complete Base File Controllers.
        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        public ActionResult uploadFile(HttpPostedFileBase upFile, int id, Labs lab)
        {
            if (upFile != null && lab.testName != null)
            {
                if (upFile.ContentType == "application/pdf")
                {
                    Labs labs = new Labs();
                    labs.patients_Id = id;

                    var info = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                    DateTimeOffset localServerTime = DateTimeOffset.Now;
                    DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);

                    var dateTime = localTime.ToString("dd/MM/yyyy hh:mm tt");

                    string UniquefileName = Guid.NewGuid() + Path.GetFileName(upFile.FileName);
                    string filePath = Server.MapPath("~/Uploaded/");
                    string actualPath = Path.Combine(filePath + UniquefileName);
                    upFile.SaveAs(actualPath);
                    labs.Name = UniquefileName;
                    labs.urlName = actualPath;
                    labs.testName = lab.testName;
                    labs.Datetime = dateTime;

                    db.Labs.Add(labs);
                    db.SaveChanges();
                    ViewData["uploadSuccessMessage"] = "تم الرفع بنجاح";
                    TempData["success"] = ViewData["uploadSuccessMessage"];
                    return RedirectToAction("patientProfile/" + id);
                }
                else
                {
                    ViewData["errorTypeMessage"] = "هذا الملف ليس pdf";
                    TempData["error"] = ViewData["errorTypeMessage"];
                    return RedirectToAction("patientProfile/" + id);
                }

            }
            else
            {
                ViewData["uploadErrorMessage"] = "من فضلك إختر الملف أو ادخل أسم التحليل";
                TempData["error"] = ViewData["uploadErrorMessage"];
                return RedirectToAction("patientProfile/" + id);

            }
        }
        public FileResult Reader(string FileName)
        {
            string filePath = Server.MapPath("~/Uploaded/");

            string actualPath = Path.Combine(filePath + FileName);

            FileStream fileStream = new FileStream(actualPath, FileMode.Open, FileAccess.Read);

            FileStreamResult fsResult = new FileStreamResult(fileStream, "application/pdf");

            return fsResult;
        }
        public ActionResult Downloader(string FileName)
        {
            string filePath = Server.MapPath("~/Uploaded/");

            string fullPath = Path.Combine(filePath + FileName);

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);

        }
        public JsonResult Deleter( int fileId, string FileName)
        {

            var fileInDb = db.Labs.Where(t => t.Id == fileId).FirstOrDefault();

            var fileInBackUpTests = db.tb_LabsBackUps.Where(b => b.Name == FileName).FirstOrDefault();
            string filePath = Server.MapPath("~/Uploaded/");
            string fullPath = Path.Combine(filePath + FileName);

            //----------------------------------------------------------------------------
            var info = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);
            //----------------------------------------------------------------------------

            if (System.IO.File.Exists(path: fullPath))
            {
                if (fileInBackUpTests != null)
                {
                    db.Labs.Remove(fileInDb);
                    db.SaveChanges();
                    return Json("تم الحذف بنجاح", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tb_labsBackUp tb_LabsBackUp = new tb_labsBackUp();

                    var TestName = fileInDb.testName;
                    var fileName = fileInDb.Name;
                    var fileUrl = fileInDb.urlName;
                    var AddedDate = fileInDb.Datetime;
                    var patientId = fileInDb.patients_Id;

                    //get patient from database.
                    var patients = db.Patients.Where(p => p.Id == patientId).FirstOrDefault();
                    var patientName = patients.userName;
                    var patientMedicalNumber = patients.medicalNumber;
                    var userName = Session["uName"].ToString();

                    //copying data from tests table to backup table.
                    tb_LabsBackUp.testName = TestName;
                    tb_LabsBackUp.Name = fileName;
                    tb_LabsBackUp.urlName = fileUrl;
                    tb_LabsBackUp.dateWasAdded = AddedDate;
                    tb_LabsBackUp.PatientName = patientName;
                    tb_LabsBackUp.PatientMedicalNumber = patientMedicalNumber;
                    tb_LabsBackUp.UserName = userName;
                    tb_LabsBackUp.Datetime = localTime.ToString("dd/MM/yyyy hh:mm tt");
                    tb_LabsBackUp.patients_Id = patientId;

                    db.tb_LabsBackUps.Add(tb_LabsBackUp);
                    db.Labs.Remove(fileInDb);
                    db.SaveChanges();
                }

            }
            else
            {
                return Json("هذا الملف غير متاح",JsonRequestBehavior.AllowGet);
            }

            return Json("تم الحذف بنجاح", JsonRequestBehavior.AllowGet);

        }

        public ActionResult Requests()
        {
            return View();
        }
        public JsonResult Getlb_Requests()
        {
            var data = db.Requests.Where(e => e.lb_Request == true).ToList();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRequest (int id)
        {
            var requestsInDb = db.Requests.Where(e => e.Id == id).FirstOrDefault();
            
            string req;

            if (requestsInDb != null)
            {
                //----------------------------------------------------------------------------
                var info = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                DateTimeOffset localServerTime = DateTimeOffset.Now;
                DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);
                //----------------------------------------------------------------------------

                    var medicalNumber = requestsInDb.medicalNumber;
                    var nationalId = requestsInDb.nationalId;
                    var dateTime = requestsInDb.dateTime;
                    var RemovedDate = localTime.ToString("dd/MM/yyyy hh:mm tt");
                    var userName = Session["uName"].ToString();
                    var lbType = requestsInDb.lb_Request;
                    var rdType = requestsInDb.rd_Request;
                    var trType = requestsInDb.tr_Request;
                    
                    RequestesBackUp requestsBackUp = new RequestesBackUp();

                    requestsBackUp.medicalNumber = medicalNumber;
                    requestsBackUp.nationalId = nationalId;
                    requestsBackUp.dateTime = dateTime;
                    requestsBackUp.RemovedDate = RemovedDate;
                    requestsBackUp.UserName = userName;
                    requestsBackUp.lb_Request = lbType;
                    requestsBackUp.rd_Request = rdType;
                    requestsBackUp.tr_Request = trType;

                    db.RequestsBackUps.Add(requestsBackUp);
                    db.SaveChanges();

                    db.Requests.Remove(requestsInDb);
                    db.SaveChanges();
                    req = "تم الحذف";
            }
            else
            {
                req = "ليس موجود";
            }
            return Json(req , JsonRequestBehavior.AllowGet);
        }


    }
}
