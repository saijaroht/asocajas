﻿using System;
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
using System.Web;

namespace Asocajas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RUsuarioController : BaseController<RUsuario>
    {
        private string CrearUsuario = "CREAR USUARIO";
        private string CambiarClave = "CAMBIA CLAVE";
        private string OlvidaClave= "OLVIDA CLAVE";
        private string BloqueaUsuario = "BLOQUEA USUARIO";
        private string DesbloqueaUsuario = "DESBLOQUEA USUARIO";


        public IHttpActionResult GetRUsuario()
        {
            try
            {
                if (HelperGeneral.IsUserLogin())
                {
                    //HelperGeneral.SaveFile();
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
                else
                    return Ok(HelperGeneral.resultsNull());
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }


        public IHttpActionResult GetRUsuarioByCCF()
        {
            try
            {
                if (HelperGeneral.IsUserLogin())
                {
                    //HelperGeneral.SaveFile();
                    var usuario = HelperGeneral.GetSession();
                    var CCFUserLogin = this.objDb.Get(o => o.Usuario == usuario).FirstOrDefault().IdCcf;
                    var obj = this.objDb.Get(o => o.IdCcf == CCFUserLogin).ToList();
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
                else
                    return Ok(HelperGeneral.resultsNull());
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult GetExistUser(string user, string password)
        {
            try
            {
                //if (HelperGeneral.IsUserLogin())
                //{
                    var obj = this.objDb.Get().Where(o => o.Usuario == user).ToList();
                    //HelperGeneral.SendMailsParams();
                    results result = new results();
                    if (obj.Count() > 0)
                    {
                        var linqEmails = Utility.TripleDES(obj.FirstOrDefault().Password, false);
                        RUsuario rusuario = new RUsuario();
                        rusuario = obj.FirstOrDefault();
                        rusuario.UsuarioLogueado = user;
                        var today = DateTime.Now.Date;

                        int resultadoFechas = DateTime.Compare(rusuario.Vigencia, today);

                        System.Configuration.AppSettingsReader settingsReader =
                                                       new AppSettingsReader();
                        var Cantidadintentos = Convert.ToInt32((string)settingsReader.GetValue("CantidadIntentos",
                                                                 typeof(String)));

                        if (Convert.ToInt32(rusuario.Estado) != (int)Estados.Activo)
                        {
                            switch (Convert.ToInt32(rusuario.Estado))
                            {
                                case (int)Estados.Inactivo:
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
                            CreateLogEventos(BloqueaUsuario, rusuario);
                            result.Message = "Su usuario se encuentra bloqueado, por favor contacte al administrador.";
                            result.Ok = false;
                        }
                        else if (resultadoFechas < 0)
                        {
                            rusuario.Estado = ((int)Estados.Inactivo).ToString();
                            UpdateTry(rusuario);
                            result.Message = "Su usuario ha caducado, por favor contacte al administrador.";
                            result.Ok = false;
                        }
                        else
                        {
                            if (password != linqEmails)
                            {
                                result.Message = "El usuario o contraseña no son correctos.";
                                result.Ok = false;
                                rusuario.Intentos = rusuario.Intentos == null ? 1 : rusuario.Intentos + 1;
                                UpdateTry(rusuario);
                            }
                            else
                            {
                                if (rusuario.CambioObligatorio)
                                {
                                    result.CambioObligatorio = true;
                                }
                                else
                                {
                                    result.CambioObligatorio = false;
                                }
                                result.Ok = true;
                                result.Message = rusuario.Nombre + " " + rusuario.Apellido;
                                //Utility.setSession(password);
                                rusuario.Intentos = 0;
                                UpdateTry(rusuario);
                                HelperGeneral.SetSession(rusuario.Usuario);
                            }
                        }
                    }
                    else
                    {
                        result.Message = "Su usuario no está registrado en el sistema.";
                        result.Ok = false;
                    }
                    return Ok(result);
                //}
                //else
                //    return Ok(HelperGeneral.resultsNull());
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult GetIsLogin()
        {
            try
            {
                if (HelperGeneral.IsUserLogin())
                {
                    results result = new results();
                    result.Ok = string.IsNullOrEmpty(HelperGeneral.GetSession()) ? false : true;
                    return Ok(result);
                } return Ok(HelperGeneral.resultsNull());
            }
            catch (Exception ex)
            {

                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult GetCloseSession()
        {
            try
            {
                results result = new results();
                HelperGeneral.CloseSession();
                result.Ok = true;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult GetExisteUser(string password)
        {
            try
            {
                string user = HelperGeneral.GetSession();
                var obj = this.objDb.Get().Where(o => o.Usuario == user).ToList();
                results result = new results();
                if (obj.Count() > 0)
                {
                    var linqEmails = Utility.TripleDES(obj.FirstOrDefault().Password, false);
                    if (password != linqEmails)
                    {
                        result.Message = "La contraseña actual no es correcta.";
                        result.Ok = false;
                    }
                    else
                    {
                        result.Message = "";
                        result.Ok = true;
                    }
                }
                else
                {
                    result.Message = "Su usuario no está registrado en el sistema.";
                    result.Ok = false;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public void UpdateTry(RUsuario rsuario)
        {
            try
            {
                var obj = this.objDb.AddUpdate(rsuario, "IdUsuario");
            }
            catch (Exception ex)
            {
                HelperGeneral.exceptionError(ex);
            }
        }

        public IHttpActionResult GetRUsuarioById(int idUsuario)
        {
            try
            {
                var obj = this.objDb.Get(o => o.IdUsuario == idUsuario).ToList();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult GetRUsuarioByMail()
        {
            try
            {
                string Mail = HelperGeneral.GetSession();
                var obj = this.objDb.Get(o => o.Usuario == Mail).FirstOrDefault();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult PostRUsuario(RUsuario rsuario)
        {
            try
            {
                if (this.objDb.Get().Where(o => o.Usuario == rsuario.Usuario).Count() == 0)
                {
                    var randomPass = HelperGeneral.RandomPass();
                    rsuario.Password = Utility.TripleDES(randomPass, true);
                    var decypt = Utility.TripleDES(rsuario.Password, false);
                    rsuario.CambioObligatorio = true;
                    var obj = this.objDb.Add(rsuario);
                    CreateLogEventos(CrearUsuario, rsuario);

                    this.IEnviarEmail(rsuario, randomPass, "");
                    return CreatedAtRoute("DefaultApi", new { id = rsuario.IdUsuario }, rsuario);
                }
                else
                {
                    results result = new results();
                    result.Message = "El usuario ya existe";
                    result.Ok = false;
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }


        public IHttpActionResult PutUpdatePassword(CambioPassword cambioPassword)
        {
            try
            {
                cambioPassword.Usuario = HelperGeneral.GetSession();
                var rsuario = this.objDb.Get(o => o.Usuario == cambioPassword.Usuario).FirstOrDefault();
                rsuario.CambioObligatorio = false;
                rsuario.UsuarioLogueado = HelperGeneral.GetSession();
                rsuario.Password = Utility.TripleDES(cambioPassword.Password, true);
                UpdateTry(rsuario);
                CreateLogEventos(CambiarClave, rsuario);

                return CreatedAtRoute("DefaultApi", new { id = rsuario.IdUsuario }, rsuario);
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult PutUpdateActivarBloquear(ActivarBloquear activarBloquear)
        {
            try
            {
                var rsuario = this.objDb.Get(activarBloquear.IdUsuario);
                rsuario.Estado = activarBloquear.Estado;
                rsuario.CambioObligatorio = true;
                var randomPass = HelperGeneral.RandomPass();
                rsuario.Password = Utility.TripleDES(randomPass, true);
                UpdateTry(rsuario);
                rsuario.UsuarioLogueado = HelperGeneral.GetSession();
                if (Convert.ToInt32(activarBloquear.Estado) == (int)Estados.Activo)
                {
                    CreateLogEventos(DesbloqueaUsuario, rsuario);
                    this.IEnviarEmail(rsuario, randomPass, "El administrador de Consulta ANI de Asocajas ha activado su usuario, antes de poder acceder al sistema, es imprescindible cambiar la contraseña. Para ello acceda a la siguiente dirección:","Usuario Activado");
                }
                else
                {
                    CreateLogEventos(BloqueaUsuario, rsuario);
                }

                return CreatedAtRoute("DefaultApi", new { id = rsuario.IdUsuario }, rsuario);
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult PutRecordarContrasena(RecuperarPassword recuperarPassword)
        {
            try
            {
                if (this.objDb.Get(o => o.Usuario == recuperarPassword.Usuario).Count() > 0)
                {
                    var rsuario = this.objDb.Get(o => o.Usuario == recuperarPassword.Usuario).FirstOrDefault();
                    rsuario.Estado = ((int)Estados.Activo).ToString();
                    rsuario.CambioObligatorio = true;
                    var randomPass = HelperGeneral.RandomPass();
                    rsuario.Password = Utility.TripleDES(randomPass, true);
                    UpdateTry(rsuario);
                    rsuario.UsuarioLogueado = HelperGeneral.GetSession();
                    CreateLogEventos(OlvidaClave, rsuario);

                    this.IEnviarEmail(rsuario, randomPass, "El administrador de Consulta ANI de Asocajas ha actualizado su contraseña, antes de poder acceder al sistema, es imprescindible cambiar la contraseña. Para ello acceda a la siguiente dirección:", "Recordar Contraseña");
                    return CreatedAtRoute("DefaultApi", new { id = rsuario.IdUsuario }, rsuario);
                }
                else {
                    results result = new results();
                    result.Message = "El usuario no existe";
                    result.Ok = false;
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult PutUpdateUser(UpdateUser updateUser)
        {
            try
            {
                var rsuario = this.objDb.Get(updateUser.IdUsuario);
                rsuario.Nombre = updateUser.Nombre;
                rsuario.Apellido = updateUser.Apellido;
                rsuario.Vigencia = updateUser.Vigencia;
                rsuario.IdRole = updateUser.IdRole;
                UpdateTry(rsuario);

                return CreatedAtRoute("DefaultApi", new { id = rsuario.IdUsuario }, rsuario);
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        private void CreateLogEventos(string Evento, RUsuario Usuario)
        {
            try
            {
                using (BusinessBase<LTLogEventos> objLTLogEventos = new BusinessBase<LTLogEventos>())
                {
                    string UserLogin = HelperGeneral.GetSession();
                    int? IdUsuarioMod = string.IsNullOrEmpty(UserLogin) ? null : (int?)this.objDb.Get(o => o.Usuario == UserLogin).FirstOrDefault().IdUsuario;
                    LTLogEventos NewLog = new LTLogEventos();
                    NewLog.FechaEvento = DateTime.Now;
                    NewLog.Evento = Evento;
                    NewLog.IdUsuario = Usuario.IdUsuario;
                    NewLog.idUserMod = IdUsuarioMod;
                    objLTLogEventos.Add(NewLog);
                }
            }

            catch (Exception ex)
            {
                HelperGeneral.exceptionError(ex);
            }
        }

        private void IEnviarEmail(RUsuario rsuario, string randomPass, string Mensaje, string AsuntoMensaje = null)
        {
            try
            {
                var url = Utility.GetLoginURL();
                string MensajeCorreo = "<table width='80%' border='0'>";
                MensajeCorreo += "<tbody>";
                MensajeCorreo += "                    <tr>";
                MensajeCorreo += "                        <td colspan='2'></td>";
                MensajeCorreo += "                    </tr>";
                MensajeCorreo += "                    <tr>";
                MensajeCorreo += "                        <td style='text-align: center;' > <img src='cid:HDIImage' class='img-responsive' /></td>";
                //MensajeCorreo += "                        <td > <img src=\"" + Utility.GetURLSite() + "/Scripts/Images/logo_asocajas.png" + "\" class='img-responsive' /></td>";
                MensajeCorreo += "                    </tr>";
                MensajeCorreo += "                    <tr>";
                MensajeCorreo += "                        <td > <h2 class='text-center '>Sistema Digital de Validación de Identidad (ANI)</h2></td>";
                MensajeCorreo += "                    </tr>";
                MensajeCorreo += "                    <tr>";
                MensajeCorreo += "                        <td >Usuario " + (string.IsNullOrEmpty(Mensaje) ? "creado" : "Actualizado") + "</td>";
                MensajeCorreo += "                    </tr>";
                MensajeCorreo += "                    <tr>";
                MensajeCorreo += "                        <td colspan='2'>";
                MensajeCorreo += "                        <p>" + (string.IsNullOrEmpty(Mensaje) ? "El administrador de Consulta ANI de Asocajas ha registrado un usuario para Usted, por favor acceda  al sistema por primera vez para activar la cuenta y acceda a la siguiente dirección:" : Mensaje) + "</p>";
                MensajeCorreo += "                        <a href='" + url + "'>" + url + "</a>";
                MensajeCorreo += "                        <br>";
                MensajeCorreo += "                        <p>Usuario: <strong>" + Convert.ToString(rsuario.Usuario) + "</strong><br>";
                MensajeCorreo += "                        Contraseña: <strong>" + randomPass + "</strong><br>";
                MensajeCorreo += "                        <br>";
                MensajeCorreo += "                       <p>Esta clave es temporal, cuando ingrese, se le solicitará  que  asigne  una nueva, la cual se requerirá con las siguientes caracteristicas: mínimo 8 caracteres, contener minúsculas y mayúsculas, mínimo un carácter especial, mínimo un número.</p>";
                MensajeCorreo += "                        <br>";
                MensajeCorreo += "                       <p>Este mensaje se ha generado de forma automática, por favor, no responda a esta dirección de correo.</p>";
                MensajeCorreo += "                        </td>";
                MensajeCorreo += "                    </tr>";
                MensajeCorreo += "                     <tr>";
                MensajeCorreo += "                       <td  colspan='2'></td>";
                MensajeCorreo += "                   </tr>";
                //MensajeCorreo += "                   <tr>";
                //MensajeCorreo += "                       <td colspan='2'>";
                //MensajeCorreo += "                       <p>Cordialmente,<br>";
                //MensajeCorreo += "                       <strong>Consultas ANI</strong></p>";
                //MensajeCorreo += "                       </td>";
                //MensajeCorreo += "                   </tr>";
                MensajeCorreo += "               </tbody>";
                MensajeCorreo += "           </table>";

                bool enviaMail = HelperGeneral.SendMail(rsuario.Usuario, (string.IsNullOrEmpty(AsuntoMensaje) ? "Usuario creado" : AsuntoMensaje), MensajeCorreo);
                //bool enviaMail = HelperGeneral.SendMail(rsuario.Usuario, "Usuario creado", "<h1>Usuario Creado</h1>en el siguente link podra realizar el cambio de contraseña");
            }
            catch (Exception ex)
            {
                HelperGeneral.exceptionError(ex);
            }
        }

    }
}