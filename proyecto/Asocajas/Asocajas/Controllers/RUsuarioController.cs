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
using System.Configuration;

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
            var obj = this.objDb.Get().Where(o => o.Usuario == user).ToList();
            results result = new results();
            if (obj.Count() > 0)
            {
                var linqEmails = Utility.TripleDES(obj.FirstOrDefault().Password, false);
                RUsuario rusuario = new RUsuario();
                rusuario = obj.FirstOrDefault();
                int resultadoFechas = DateTime.Compare(rusuario.Vigencia, new DateTime());

                System.Configuration.AppSettingsReader settingsReader =
                                               new AppSettingsReader();
                var Cantidadintentos = Convert.ToInt32((string)settingsReader.GetValue("CantidadIntentos",
                                                         typeof(String)));

                if (Convert.ToInt32(rusuario.Estado) != (int)Estados.Activo)
                {
                    switch (Convert.ToInt32(rusuario.Estado))
                    {
                        case (int)Estados.InActivo:
                            result.Message = "Su usuario se encuentra inactivo, por favor contacte al administrador.";
                            break;
                        case (int)Estados.Bloqueado:
                            result.Message = "Su usuario se encuentra bloqueado, por favor contacte al administrador.";
                            break;
                    }
                    result.Ok = false;
                }
                else if (rusuario.Intentos >= Cantidadintentos)
                {
                    rusuario.Estado = ((int)Estados.Bloqueado).ToString();
                    UpdateTry(rusuario);
                    result.Message = "Su usuario se encuentra bloqueado, por favor contacte al administrador.";
                    result.Ok = false;
                }
                else if (resultadoFechas < 0)
                {
                    result.Message = "Su usuario ha caducado, por favor contacte al administrador.";
                    result.Ok = false;
                }
                else
                {
                    if (password != linqEmails)
                    {
                        result.Message = "Contraseña Incorrecta";
                        result.Ok = false;
                        rusuario.Intentos = rusuario.Intentos == null ? 1 : rusuario.Intentos + 1;
                        UpdateTry(rusuario);
                    }
                    else
                    {
                        result.Message = "";
                        result.Ok = true;
                        rusuario.Intentos = 0;
                        UpdateTry(rusuario);
                    }
                }
            }
            else
            {
                result.Message = "Su usuario no está registrado en el sistema.";
                result.Ok = false;
            }
            return Ok(result);
        }

        public void UpdateTry(RUsuario rsuario)
        {
            var obj = this.objDb.AddUpdate(rsuario, "IdUsuario");

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
                var decypt = Utility.TripleDES(rsuario.Password, false);
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
            var url = Utility.GetLoginURL();
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
            MensajeCorreo += "                        <a href='" + url + "'>" + url + "</a>";
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