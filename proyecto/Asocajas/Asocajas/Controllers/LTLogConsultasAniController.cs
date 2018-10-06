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
    public class LTLogConsultasAniController : BaseController<LTLogConsultasAni>
    {
        public IHttpActionResult GetLTLogConsultasAni()
        {
            var obj = this.objDb.Get().ToList();
            return Ok(obj);
        }

        public IHttpActionResult PostLTLogConsultasAni(LTLogConsultasAni lTLogConsultasAni)
        {
            var obj = this.objDb.Add(lTLogConsultasAni);
            return CreatedAtRoute("DefaultApi", new { id = lTLogConsultasAni.IdConsulta }, lTLogConsultasAni);
        }
    }
}