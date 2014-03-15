using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aspnet5Demo.Controllers
{
    [Authorize]
    public class EventsMvcController : Controller
    {
        [Route("events")]
        [Route("events/index")]
        public ActionResult Index()
        {
            return View();
        }
	}
}