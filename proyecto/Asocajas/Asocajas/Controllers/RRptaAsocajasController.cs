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
    public class RRptaAsocajasController : BaseController<RRptaAsocajas>
    {
        public IHttpActionResult GetRptaAsocajas()
        {
            var obj = this.objDb.Get().ToList();
            return Ok(obj);
        }

        public IHttpActionResult PostRRptaAsocajas(RRptaAsocajas rRptaAsocajas)
        {
            var obj = this.objDb.Add(rRptaAsocajas);
            return CreatedAtRoute("DefaultApi", new { id = rRptaAsocajas.IdRptaAsocajas }, rRptaAsocajas);
        }
        public IHttpActionResult GetRRptaAsocajasId(string id)
        {
            try
            {
                var obj = this.objDb.Get(o => o.IdRptaAsocajas == id).ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }
    }
}