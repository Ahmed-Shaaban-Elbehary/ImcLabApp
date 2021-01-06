﻿using ImcLabApp.Models;
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
    public class RadiosController : Controller
    {
        AppDbContext db = new AppDbContext();
        // GET: Radios
        public ActionResult Index()
        {
            return View();
        }//--------------------------------------//
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
            var patient = db.Patients.Where(p => p.Id.Equals(id)).FirstOrDefault<Patients>();
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
            var results = db.Radios.Where(l => l.patients_Id == id).ToList();
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        //Complete Base File Controllers.
        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        public ActionResult uploadFile(HttpPostedFileBase upFile, int id, Radios r)
        {
            if (upFile != null && r.testName != null)
            {
                if (upFile.ContentType == "application/pdf")
                {
                    Radios radio = new Radios();
                    radio.patients_Id = id;

                    var info = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
                    DateTimeOffset localServerTime = DateTimeOffset.Now;
                    DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);
                    var dateTime = localTime.ToString("dd/MM/yyyy hh:mm tt");

                    string UniquefileName = Guid.NewGuid() + Path.GetFileName(upFile.FileName);
                    string filePath = Server.MapPath("~/Uploaded/");
                    string actualPath = Path.Combine(filePath + UniquefileName);
                    upFile.SaveAs(actualPath);
                    radio.Name = UniquefileName;
                    radio.urlName = actualPath;
                    radio.testName = r.testName;
                    radio.Datetime = dateTime;

                    db.Radios.Add(radio);
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
                ViewData["uploadErrorMessage"] = "من فضلك إختر الملف - أسم التحليل";
                TempData["error"] = ViewData["uploadErrorMessage"];
                return RedirectToAction("patientProfile/" + id);

            }
        }
        public FileResult Reader(string FileName)
        {
            string filePath = Server.MapPath("~/Uploaded/");

            string actualPath = Path.Combine(filePath + FileName);

            var fileStream = new FileStream(actualPath, FileMode.Open, FileAccess.Read);

            var fsResult = new FileStreamResult(fileStream, "application/pdf");

            return fsResult;
        }
        public ActionResult Downloader(string FileName)
        {
            string filePath = Server.MapPath("~/Uploaded/");

            string fullPath = Path.Combine(filePath + FileName);

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileName);

        }
        public ActionResult Deleter( int fileId, string FileName)
        {

            var fileInDb = db.Radios.Where(t => t.Id == fileId).FirstOrDefault();
            var fileInBackUpTests = db.tb_RadiosBackUps.Where(b => b.Name == FileName).FirstOrDefault();
            string filePath = Server.MapPath("~/Uploaded/");
            string fullPath = Path.Combine(filePath + FileName);

            //----------------------------------------------------------------------------
            var info = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);
            //----------------------------------------------------------------------------

            if (System.IO.File.Exists(fullPath))
            {
                if (fileInBackUpTests != null)
                {
                    db.Radios.Remove(fileInDb);
                    db.SaveChanges();

                    return Json("تم الحذف بنجاح", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    tb_radiosBackUp tb_RadiosBackUp = new tb_radiosBackUp();

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
                    tb_RadiosBackUp.testName = TestName;
                    tb_RadiosBackUp.Name = fileName;
                    tb_RadiosBackUp.urlName = fileUrl;
                    tb_RadiosBackUp.dateWasAdded = AddedDate;
                    tb_RadiosBackUp.PatientName = patientName;
                    tb_RadiosBackUp.PatientMedicalNumber = patientMedicalNumber;
                    tb_RadiosBackUp.UserName = userName;
                    tb_RadiosBackUp.Datetime = localTime.ToString("dd/MM/yyyy hh:mm tt");
                    tb_RadiosBackUp.patients_Id = patientId;

                    db.tb_RadiosBackUps.Add(tb_RadiosBackUp);
                    db.Radios.Remove(fileInDb);
                    db.SaveChanges();
                }

            }
            else
            {
                return Json("هذا الملف غير متاح", JsonRequestBehavior.AllowGet);
            }

            return Json("تم الحذف بنجاح", JsonRequestBehavior.AllowGet);

        }

        public ActionResult Requests()
        {
            return View();
        }
        public JsonResult Getlb_Requests()
        {
            var data = db.Requests.Where(e => e.rd_Request == true).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRequest(int id)
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
            return Json(req, JsonRequestBehavior.AllowGet);
        }

    }
}