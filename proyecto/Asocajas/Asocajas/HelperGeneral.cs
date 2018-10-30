using Asocajas.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Web.Mvc;
using System.Net.Http;

namespace Asocajas
{
    public class HelperGeneral
    {

        /// <summary>
        /// 
        /// </summary>
        public static string MachineInfo { get; set; }

        public static string RandomPass()
        {
            int longitud = 8;
            Guid miGuid = Guid.NewGuid();
            string token = Convert.ToBase64String(miGuid.ToByteArray());
            token = token.Replace("=", "").Replace("+", "");
            return token.Substring(0, longitud);
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",
                                                             typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey",
                                                         typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static bool SendMail(string Para, string asunto, string html)
        {
            try
            {
                System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();

                SmtpClient clientDetails = new SmtpClient();
                clientDetails.Port = Convert.ToInt32((string)settingsReader.GetValue("Port", typeof(String)));
                clientDetails.Host = (string)settingsReader.GetValue("Host", typeof(String));
                clientDetails.EnableSsl = true;
                clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientDetails.UseDefaultCredentials = false;
                clientDetails.Credentials = new NetworkCredential((string)settingsReader.GetValue("UserMail", typeof(String)),
                    (string)settingsReader.GetValue("PasswordMail", typeof(String)));

                //Detalle Mensaje
                MailMessage mailDetails = new MailMessage();
                mailDetails.From = new MailAddress((string)settingsReader.GetValue("UserMail", typeof(String)));
                mailDetails.To.Add(Para);
                mailDetails.Subject = asunto;
                mailDetails.IsBodyHtml = true;
                mailDetails.Body = html;

                clientDetails.Send(mailDetails);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static results exceptionError(Exception ex)
        {
            results result = new results();
            result.Message = "El usuario ya existe";
            result.Ok = false;
            using (BusinessBase<LTLogApp> objLTLogApp = new BusinessBase<LTLogApp>())
            {
                LTLogApp newLog = new LTLogApp();

                newLog.Type = "Log";
                newLog.GUID = Guid.NewGuid().ToString();
                newLog.AppName = ex.Source;
                newLog.WebServiceName = ((System.Reflection.MemberInfo)(((System.Reflection.MemberInfo)(ex.TargetSite)).DeclaringType)).Name;
                newLog.MethodName = "First";
                newLog.MethodNameUI = ((System.Reflection.MemberInfo)(ex.TargetSite)).Name;
                newLog.UserName = null;
                newLog.UserMachineInfo = Utility.GetUserMachineInfo(MachineInfo);
                newLog.ServerIP = Utility.GetServerIP();
               // newLog.Data = ((System.NullReferenceException)(ex)).ToString();
                newLog.ErrorMessage = ex.Message;
                newLog.Source =ex.Source;
                newLog.Method = ((System.Reflection.MemberInfo)(ex.TargetSite)).Name;
                newLog.ErrorType = null;
                newLog.Trace = null;
                newLog.CreationDate = DateTime.Now;
                objLTLogApp.Add(newLog);
            }
            return result;
        }

        public static void SendMailsParams()
        {
            SmtpClient clientDetails = new SmtpClient();
            clientDetails.Port = 587;
            clientDetails.Host = "smtp.office365.com";
            clientDetails.EnableSsl = true;
            clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
            clientDetails.UseDefaultCredentials = false;
            clientDetails.Credentials = new NetworkCredential("enviosautomaticos@asocajas.org.co", "Colombia2017");

            //Detalle Mensaje
            MailMessage mailDetails = new MailMessage();
            mailDetails.From = new MailAddress("enviosautomaticos@asocajas.org.co");
            mailDetails.To.Add("norbey9212@gmail.com");
            //para multiples destinatarios
            //mailDetails.To.Add("another recipient email address");
            //for copia oculta
            //mailDetails.Bcc.Add("bcc email address")
            mailDetails.Subject = "Asunto del mensaje de correo";
            mailDetails.IsBodyHtml = false;
            mailDetails.Body = "Texto del correo";

            clientDetails.Send(mailDetails);
        }

        #region Paginadores

        public static List<RUsuario> PaginadorConsultasLTLogEventos(int limiteInferior, int limiteSuperior)
        {
            try
            {
                List<RUsuario> exec = new List<RUsuario>();
                using (var ctx = new AsocajasBDEntities())
                {
                    var LimiteInferior = new SqlParameter
                    {
                        ParameterName = "LimiteInferior",
                        Value = limiteInferior
                    };

                    var LimiteSuperior = new SqlParameter
                    {
                        ParameterName = "LimiteSuperior",
                        Value = limiteSuperior
                    };

                    var Tabla = new SqlParameter
                    {
                        ParameterName = "Tabla",
                        Value = "RUsuario"
                    };

                    var IdTable = new SqlParameter
                    {
                        ParameterName = "IdTable",
                        Value = "IdUsuario"
                    };
                    exec = ctx.Database.SqlQuery<RUsuario>("exec PaginadorConsultas @LimiteInferior,@LimiteSuperior,@Tabla,@IdTable ", LimiteInferior, LimiteSuperior, Tabla, IdTable).ToList<RUsuario>();

                    //var EXEC = ctx.INSERTSOLicitud(IdSolicitudAntigua, IdSolicitudNueva);
                }
                return exec;
            }
            catch (Exception ex)
            {
                List<RUsuario> exec = new List<RUsuario>();
                return exec;
            }
        }


        #endregion

        #region Session var
        private const string SESSION_VAR = "User";

        public static string User
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_VAR] == null)
                    HttpContext.Current.Session[SESSION_VAR] = null;
                return (string)HttpContext.Current.Session[SESSION_VAR];
            }
            set { HttpContext.Current.Session[SESSION_VAR] = value; }
        }

        public static void CloseSession()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.RemoveAll();
        }
        #endregion

        public static void SaveFile()
        {
            string filename = "C:\\ArchivosPlanos\\prueba1.csv";
            //int index = 1;
            //int process = 5;
            using (StreamWriter sw = new StreamWriter(new FileStream(filename, FileMode.Create), Encoding.UTF8))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("titulo1, Titulo2, Titulo3");
                sb.AppendLine("parametro11, parametro21, parametro31");
                sb.AppendLine("parametro12, parametro22, parametro32");
                sb.AppendLine("parametro13, parametro23, parametro33");
                sb.AppendLine("parametro14, parametro24, parametro34");


                sw.Write(sb.ToString());

            }
            byte[] file;
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }
            TReporte NewReport = new TReporte();
            NewReport.Excel = file;
            using (BusinessBase<TReporte> objTReporte = new BusinessBase<TReporte>())
            {
                objTReporte.Add(NewReport);
            }
            HttpResponseMessage getFile = new HttpResponseMessage();
            getFile = GetFile(file);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public static HttpResponseMessage GetFile(byte[] file)
        {

            //crear en html <a target="_self" href="/api/download/1">Download File</a>

            HttpResponseMessage result = null;
            var localFilePath = string.Empty;// HttpContext.Current.Server.MapPath("~/timetable.jpg");


            var fileName = "MiDocumento.csv";
            //localFilePath = Path.Combine(Path.GetTempPath(), fileName);
            //File.WriteAllBytes(localFilePath, current.ArchivoCifrado);
            //if (!File.Exists(localFilePath))
            //{
            //    throw new Exception("El Archivo especificado no existe " + localFilePath);
            //}

            result = new HttpResponseMessage();
            //result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            //result.Content = new StringContent(file);
            result.Content = new ByteArrayContent(file);
            result.Content.Headers.Add("x-filename", fileName);

            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = fileName;


            return result;

        }
    }
}