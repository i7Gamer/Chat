using Database;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Webservice.Controllers
{
    public class UserController : BaseController
    {
        UserRepository userRepository = new UserRepository(provider);

        [HttpGet]
        [ActionName("getUser")]
        public IHttpActionResult getUser(string id)
        {
            using(IDbConnection connection = provider.getConnection())
            {
                User user;
                try
                {
                    user = userRepository.getUser(connection, id);
                }
                catch
                {
                    return BadRequest();
                }
                
                if(user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
        }

        [HttpPost]
        [ActionName("register")]
        public IHttpActionResult register(string username, string password, string firstName, string lastName)
        {
            using (IDbConnection connection = provider.getConnection())
            {
                User user;
                try
                {
                    user = userRepository.createUser(connection, username, password, firstName, lastName);
                }
                catch
                {
                    return BadRequest();
                }
                 return Ok(user);
            }
        }

        [HttpPost]
        [ActionName("login")]
        public IHttpActionResult login(string username, string password)
        {
            //TODO
            return Ok();
        }

        [HttpPost]
        [ActionName("logout")]
        public IHttpActionResult logout(string username)
        {
            //TODO
            return Ok();
        }
    }
}