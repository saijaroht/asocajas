using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Asocajas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LTLogAppController : BaseController<LTLogApp>
    {

        public IHttpActionResult GetLTLogApp()
        {
            var obj = this.objDb.Get().ToList();
            return Ok(obj);
        }

        public IHttpActionResult PostLTLogApp(LTLogApp lTLogApp)
        {
            var obj = this.objDb.Add(lTLogApp);
            return CreatedAtRoute("DefaultApi", new { id = lTLogApp.IdLogApp }, lTLogApp);
        }

    }
}