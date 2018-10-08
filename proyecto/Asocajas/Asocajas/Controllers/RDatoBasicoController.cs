using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Asocajas.Controllers
{
    public class RDatoBasicoController : BaseController<RDatoBasico>
    {
        public IHttpActionResult GetRTipoDatoBasico(long idTipoDatoBasico)
        {
            var obj = this.objDb.Get(o => o.Activo && o.IdTipoDatoBasico == idTipoDatoBasico).ToList();
            return Ok(obj);
        }
        public IHttpActionResult GetRTipoDatoBasico2(long idTipoDatoBasico, string code)
        {
            var obj = this.objDb.Get(o => o.Activo && o.IdTipoDatoBasico == idTipoDatoBasico && o.Code == code).FirstOrDefault();
            return Ok(obj);
        }
        
    }
}