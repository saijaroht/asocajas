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
    public class LTLogEventosController : BaseController<LTLogEventos>
    {
        public IHttpActionResult GetLTLogEventos()
        {
            var obj = this.objDb.Get().ToList();
            return Ok(obj);
        }

        public IHttpActionResult PostLTLogEventos(LTLogEventos lTLogEventos)
        {
            var obj = this.objDb.Add(lTLogEventos);
            return CreatedAtRoute("DefaultApi", new { id = lTLogEventos.IdLogEvento }, lTLogEventos);
        }   
    }
}