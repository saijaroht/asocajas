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
    public class RCCFController : BaseController<RCCF>
    {
        public IHttpActionResult GetRCCF()
        {
            var obj = this.objDb.Get().ToList();
            return Ok(obj);
        }

        public IHttpActionResult GetRCCFById(int idCcf)
        {
            var obj = this.objDb.Get(o => o.IdCcf == idCcf).ToList();
            return Ok(obj);
        }

        public IHttpActionResult PostRCCF(RCCF rCCF)
        {
            var obj = this.objDb.Add(rCCF);
            return CreatedAtRoute("DefaultApi", new { id = rCCF.IdCcf }, rCCF);
        }   
    }
}