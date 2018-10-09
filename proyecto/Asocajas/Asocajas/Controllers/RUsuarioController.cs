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
            using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
            {
                using (BusinessBase<RRole> objRRole = new BusinessBase<RRole>())
                {
                    foreach (var item in obj)
                    {
                        item.RCCF = objRCCF.Get(o => o.IdCcf == item.IdCcf).FirstOrDefault();
                        item.RRole = objRRole.Get(o => o.IdRole == item.IdRole).FirstOrDefault();
                    }
                }
            }
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
            var maquina = Utility.GetUserHostName();
            if (this.objDb.Get().Where(o => o.Usuario == rsuario.Usuario).Count() == 0)
            {
                var randomPass = HelperGeneral.RandomPass();
                rsuario.Password = Utility.TripleDES(randomPass, true);
                //var decypt = Utility.TripleDES(rsuario.Password, false);
                this.IEnviarEmail(rsuario, randomPass);
                var obj = this.objDb.Add(rsuario);
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

        private void IEnviarEmail(RUsuario rsuario, string randomPass)
        {

            string MensajeCorreo = "<table width='80%' border='0'>";
            MensajeCorreo += "<tbody>";
            MensajeCorreo += "                    <tr>";
            MensajeCorreo += "                        <td colspan='2'></td>";
            MensajeCorreo += "                    </tr>";
            MensajeCorreo += "                    <tr>";
            MensajeCorreo += "                        <td >Usuario creado</td>";
            MensajeCorreo += "                    </tr>";
            MensajeCorreo += "                    <tr>";
            MensajeCorreo += "                        <td colspan='2'>";
            MensajeCorreo += "                        <p>El administrador de Consulta ANI de Asocajas ha creado un usuario para Usted, antes depoder acceder al sistema por primera vez, es imprescindible activar la cuenta registrada. Para ello acceda a la siguiente dirección:</p>";
            MensajeCorreo += "                        <a href='http://localhost:25500/Pages/Login.aspx'>http://localhost:25500/Pages/Login.aspx</a>";
            MensajeCorreo += "                        <br>";
            MensajeCorreo += "                        <p>Usuario: <strong>" + Convert.ToString(rsuario.Usuario) + "</strong><br>";
            MensajeCorreo += "                        Contraseña: <strong>" + randomPass + "</strong><br>";
            MensajeCorreo += "                        <br>";
            MensajeCorreo += "                       <p>Esta clave es temporal, cuando interese debe asignar una nueva clave que debe cumplir con las siguientes condiciones, mínimo 8 caracteres, contener minúsculas y mayúsculas, mínimo un carácter especial, mínimo un número.</p>";
            MensajeCorreo += "                        </td>";
            MensajeCorreo += "                    </tr>";
            MensajeCorreo += "                     <tr>";
            MensajeCorreo += "                       <td  colspan='2'></td>";
            MensajeCorreo += "                   </tr>";
            MensajeCorreo += "                   <tr>";
            MensajeCorreo += "                       <td colspan='2'>";
            MensajeCorreo += "                       <p>Cordialmente,<br>";
            MensajeCorreo += "                       <strong>Consultas ANI</strong></p>";
            MensajeCorreo += "                       </td>";
            MensajeCorreo += "                   </tr>";
            MensajeCorreo += "               </tbody>";
            MensajeCorreo += "           </table>";

            bool enviaMail = HelperGeneral.SendMail(rsuario.Usuario, "Usuario creado", MensajeCorreo);
            //bool enviaMail = HelperGeneral.SendMail(rsuario.Usuario, "Usuario creado", "<h1>Usuario Creado</h1>en el siguente link podra realizar el cambio de contraseña");

        }
        
    }
}