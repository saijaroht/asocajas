using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Asocajas;
using HelperGeneral;

namespace Asocajas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RUsuarioController : BaseController<RUsuario>
    {
        public IHttpActionResult GetRUsuario()
        {
            var obj = this.objDb.Get().ToList();
            return Ok(obj);
        }


        public IHttpActionResult GetExistUser(string user, string password)
        {
            password = HelperGeneral.Encrypt(password, true);
            var obj = this.objDb.Get(o => o.Usuario == user && o.Password == password).ToList();
            return Ok(obj);
        }

        public IHttpActionResult GetRUsuarioById(int idUsuario)
        {
            var obj = this.objDb.Get(o => o.IdUsuario == idUsuario).ToList();
            return Ok(obj);
        }

        public IHttpActionResult PostRUsuario(RUsuario rsuario)
        {
            rsuario.Password = HelperGeneral.Encrypt(HelperGeneral.RandomPass(), true);
            var obj = this.objDb.Add(rsuario);
            return CreatedAtRoute("DefaultApi", new { id = rsuario.IdUsuario }, rsuario);
        }
    }
}