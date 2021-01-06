using ImcLabApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IMCLab.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PatientsController : ApiController
    {

        private AppDbContext db;
        public PatientsController()
        {
            db = new AppDbContext();
        }
        //// GET : /Api/Patients
        //public IEnumerable<Patients> GetPatients()
        //{
        //    return db.Patients.ToList();
        //}
        ////GET: Api/Patients?id = 1
        //public Patients GetPatientsbyId(int id)
        //{
        //    var patients = db.Patients.Where(e => e.Id == id).FirstOrDefault();

        //    if (patients == null)
        //        throw new HttpResponseException(HttpStatusCode.Forbidden);

        //    return patients;
        //}

        //GET: Api/Patients?medicalNumber=medicalNumber&NationalId=NationalId
        public Patients GetPatientsbyMedicalNumber(string medicalNumber, string NationalId)
        {
            var patients = db.Patients.Where(e => e.medicalNumber == medicalNumber && e.NationalID == NationalId ).FirstOrDefault();
             
            if (patients == null)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            
            return patients;
        }

        ////POST: Api/CreatePatients
        //[HttpPost]
        //public Patients CreatePatients(Patients p)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    db.Patients.Add(p);
        //    db.SaveChanges();

        //    return p;

        //}
        ////Put: Api/UpdatePatients/1
        //[HttpPut]
        //public void UpdatePatients(int id, Patients p)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    var PatientsInDb = db.Patients.SingleOrDefault(e => e.Id == id);

        //    if (PatientsInDb == null)
        //        throw new HttpResponseException(HttpStatusCode.Forbidden);

        //    PatientsInDb.Id = p.Id;
        //    PatientsInDb.userName = p.userName;
        //    PatientsInDb.phoneNumber = p.phoneNumber;

        //    db.SaveChanges();

        //}

        ////Put: Api/DeletePatients/1
        //[HttpDelete]
        //public void DeletePatients(int id)
        //{
        //    var PatientsInDb = db.Patients.SingleOrDefault(e => e.Id == id);

        //    if (PatientsInDb == null)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    db.Patients.Remove(PatientsInDb);
        //    db.SaveChanges();
        //}

    }
}
