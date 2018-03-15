using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Webservice.Controllers
{
    public class BaseController : ApiController
    {
        protected static ConnectionProvider provider = new ConnectionProvider();
    }
}