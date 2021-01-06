using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ImcLabApp.Models;
namespace ImcLabApp.Controllers.Api
{
    public class RequestsController : ApiController
    {
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IEnumerable<Requests>  Requests()
        {
            var requests = db.Requests.ToList();
            return requests;
        }
        
        [HttpPost]
        public Requests Requests(Requests requests)
        {
            if (requests == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            db.Requests.Add(requests);
            db.SaveChanges();
            return(requests);
        }
    }
}
