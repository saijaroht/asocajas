using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Asocajas;
using Asocajas.Utilities;
using System.Web.Script.Serialization;

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
            //password = Utility.TripleDES(password, true);
            var obj = this.objDb.Get().Where(o => o.Usuario == user).ToList();
            if (obj.Count() > 0)
            {
                var linqEmails = Utility.TripleDES(obj.FirstOrDefault().Password, false);
                if (password != linqEmails)
                {
                    obj = new List<RUsuario>();
                }
            }

            return Ok(obj);
        }

        public IHttpActionResult GetRUsuarioById(int idUsuario)
        {
            var obj = this.objDb.Get(o => o.IdUsuario == idUsuario).ToList();
            return Ok(obj);
        }

        public IHttpActionResult PostRUsuario(RUsuario rsuario)
        {
            if (this.objDb.Get().Where(o => o.Usuario == rsuario.Usuario).Count() == 0)
            {
                var randomPass = HelperGeneral.RandomPass();
                rsuario.Password = Utility.TripleDES(randomPass, true);
                //var decypt = Utility.TripleDES(rsuario.Password, false);
                var obj = this.objDb.Add(rsuario);
                bool enviaMail = HelperGeneral.SendMail(rsuario.Usuario, "Usuario creado", "<h1>Cambio de Contraseña</h1>en el siguente link podra realizar el cambio de contraseña");
                return CreatedAtRoute("DefaultApi", new { id = rsuario.IdUsuario }, rsuario);
            }
            else
            {
                DataResult result = new DataResult();
                result.Message = "El usuario ya existe";
                result.Ok = false;
                return Ok( );
            }

        }

        
    }
}