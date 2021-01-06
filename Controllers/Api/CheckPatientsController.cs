using ImcLabApp.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ImcLabApp.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CheckPatientsController : ApiController
    {
        private AppDbContext db;
        public CheckPatientsController()
        {
            db = new AppDbContext();
        }

        [HttpPost]
        public int patientsLogin(PatientsRegisteration p)
        {
            var patients = db.PatientsRegisterations.Where(e => e.UserName.Equals(p.UserName) && e.Password.Equals(p.Password)).FirstOrDefault();

            if (patients != null)
            {
                var userIdDB = patients.Id;
                return (userIdDB);
            }
            else
            {
                return (0);
            }
        }
    }
}
