using ImcLabApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ImcLabApp.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegisterationPatientsController : ApiController
    {


        private AppDbContext db;
        public RegisterationPatientsController()
        {
            db = new AppDbContext();
        }

        [HttpGet]
        public IEnumerable<PatientsRegisteration> getRegisterPatients()
        {
            return db.PatientsRegisterations.ToList();
        }

        [HttpPost]
        public PatientsRegisteration RegistPatients(PatientsRegisteration RP)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            db.PatientsRegisterations.Add(RP);
            db.SaveChanges();
            return RP;

        }



        //Put: Api/UpdatePatients/1
        [HttpPut]
        public void UpdatePatients(int id, PatientsRegisteration p)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var PatientsInDb = db.PatientsRegisterations.SingleOrDefault(e => e.Id == id);

            if (PatientsInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            PatientsInDb.UserName = p.UserName;
            PatientsInDb.Password = p.Password;
            db.SaveChanges();

        }



    }
}
