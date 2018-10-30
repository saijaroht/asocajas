using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Asocajas.Services
{
    public partial class Servicios : System.Web.UI.Page
    {
        protected static string ReCaptcha_Secret = "6LcBs3QUAAAAADLSosYj3Dz91_iYPz2YLSDDOotG";
        //private const string SESSION_VAR = "User";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Método para la Validacion del captcha
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            try
            {
                string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
                return (new WebClient()).DownloadString(url);
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(ex);
            }
        }

        //[WebMethod]
        //public static string GetDataUser(object parameters)
        //{
        //    DataResult jsonData = new DataResult();
        //    DataTableParameters DatosReturn = new DataTableParameters();
        //    try
        //    {
        //        jsonData.Message = User;
        //        jsonData.Ok = User == null ? false : true;
        //        List<RUsuario> data = HelperGeneral.PaginadorConsultasLTLogEventos(1,10);

        //        DatosReturn.data = new List<List<string>>();

        //        foreach (var item in data)
        //        {
        //            List<string> campos = new List<string>();
        //            campos.Add("");
        //            campos.Add(item.Nombre);
        //            campos.Add(item.Apellido);
        //            campos.Add(item.Nombre);
        //            campos.Add("");
        //            campos.Add("");
        //            campos.Add("");
        //            campos.Add("");
        //            campos.Add("");
        //            DatosReturn.data.Add(campos);
        //        }
        //        DatosReturn.recordsTotal = 2;
        //        DatosReturn.recordsTotal = data.Count();
        //        DatosReturn.recordsFiltered = data.Count();
        //    }
        //    catch (Exception ex)
        //    {
        //        jsonData.Message = ex.ToString();
        //        jsonData.Ok = false;
        //    }
        //    return Json(DatosReturn);
        //}

        [WebMethod]
        public static string IsLogin()
        {
            DataResult jsonData = new DataResult();
            try
            {
                jsonData.Message = HelperGeneral.User;
                jsonData.Ok = HelperGeneral.User == null ? false : true;
            }
            catch (Exception ex)
            {
                jsonData.Message = ex.ToString();
                jsonData.Ok = false;
            }
            return new JavaScriptSerializer().Serialize(jsonData);
        }

        [WebMethod]
        public static string Login(string UserData)
        {
            DataResult jsonData = new DataResult();
            try
            {
               HelperGeneral.User = UserData;
                jsonData.Message = UserData;
                jsonData.Ok = true;
            }
            catch (Exception ex)
            {
                jsonData.Message = ex.ToString();
                jsonData.Ok = false;
            }
            return new JavaScriptSerializer().Serialize(jsonData);
        }

        [WebMethod]
        public static string Logout()
        {
            DataResult jsonData = new DataResult();
            try
            {
                HelperGeneral.CloseSession();
            }
            catch (Exception ex)
            {
                jsonData.Message = ex.ToString();
                jsonData.Ok = false;
            }
            return new JavaScriptSerializer().Serialize(jsonData);
        }

        #region Session var

        //public static string User
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[SESSION_VAR] == null)
        //            HttpContext.Current.Session[SESSION_VAR] = null;
        //        return (string)HttpContext.Current.Session[SESSION_VAR];
        //    }
        //    set { HttpContext.Current.Session[SESSION_VAR] = value; }
        //}
        #endregion
    }
}