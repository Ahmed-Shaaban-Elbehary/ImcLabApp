 using ImcLabApp.Models.BackUpSystemModels;
using ImcLabApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ImcLabApp.Controllers.BackUpSystem
{
    public class Lab_BackUpController : Controller
    {
        AppDbContext db = new AppDbContext();
        // GET: Lab_BackUp

        public ActionResult getBackUpTests()
        {
            return View();
        }

        public JsonResult Get_Backup()
        {

            var data = db.tb_LabsBackUps.ToList();

            //IQueryable<tb_labsBackUp> BackUpSearch = db.tb_LabsBackUps;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    BackUpSearch = db.tb_LabsBackUps.Where(b => b.PatientMedicalNumber.Contains(searchString) || b.PatientName.Contains(searchString));
            //}
            //ViewBag.error = TempData["error"];
            //ViewBag.success = TempData["success"];
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Deleter(int fileId, string FileName)
        {

            var fileInDb = db.tb_LabsBackUps.Where(t => t.Id == fileId).FirstOrDefault();
            var FileOfPatient = db.Labs.Where(t => t.Name == FileName).FirstOrDefault();

            string filePath = Server.MapPath("~/Uploaded/");
            string fullPath = Path.Combine(filePath + FileName);

            if (System.IO.File.Exists(fullPath))
            {
                if(FileOfPatient == null)
                {
                    System.IO.File.Delete(fullPath);
                    db.tb_LabsBackUps.Remove(fileInDb);
                    db.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("existInPatientTable", JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Restor(int fileId, string FileName)
        {
            #pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (fileId != null)
            #pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            {
                var fileNameInTests = db.Labs.Where(t => t.Name == FileName).FirstOrDefault();
                if (fileNameInTests == null)
                {
                    var fileInDb = db.tb_LabsBackUps.Where(t => t.Id == fileId).FirstOrDefault();
                    var testName = fileInDb.testName;
                    var fileName = fileInDb.Name;
                    var fileURL = fileInDb.urlName;
                    var dateTime = fileInDb.dateWasAdded;
                    var patientId = fileInDb.patients_Id;
                    Labs labs = new Labs();
                    labs.testName = testName;
                    labs.Name = fileName;
                    labs.urlName = fileURL;
                    labs.Datetime = dateTime;
                    labs.patients_Id = patientId;
                    db.Labs.Add(labs);
                    db.SaveChanges();

                    return Json("done",JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("exist",JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json("error",JsonRequestBehavior.AllowGet);
            }


        }
    }
}