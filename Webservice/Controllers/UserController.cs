using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Webservice.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult test()
        {
            User demo = new User();
            return Ok("It works bro");
        }

        [HttpPost]
        [ActionName("register")]
        public IHttpActionResult register([FromBody]User user)
        {
            //TODO
            return Ok();
        }
    }
}