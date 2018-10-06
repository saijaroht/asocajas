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
    public class RRoleController : BaseController<RRole>
    {
        public IHttpActionResult GetRRole()
        {
            var obj = this.objDb.Get().ToList();
            return Ok(obj);
        }

        public IHttpActionResult PostRRole(RRole rRole)
        {
            var obj = this.objDb.Add(rRole);
            return CreatedAtRoute("DefaultApi", new { id = rRole.IdRole }, rRole);
        }   
    }
}