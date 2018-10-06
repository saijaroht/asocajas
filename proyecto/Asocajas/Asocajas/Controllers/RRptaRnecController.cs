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
    public class RRptaRnecController : BaseController<RRptaRnec>
    {
        public IHttpActionResult GetRRptaRnec()
        {
            var obj = this.objDb.Get().ToList();
            return Ok(obj);
        }

        public IHttpActionResult PostRRptaRnec(RRptaRnec rRptaRnec)
        {
            var obj = this.objDb.Add(rRptaRnec);
            return CreatedAtRoute("DefaultApi", new { id = rRptaRnec.IdRptaRnec }, rRptaRnec);
        }
    }
}