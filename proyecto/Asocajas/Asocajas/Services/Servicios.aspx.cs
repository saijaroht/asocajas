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
        //public static string IsLogin()
        //{
        //    DataResult jsonData = new DataResult();
        //    try
        //    {
        //        jsonData.Message = HelperGeneral.User;
        //        jsonData.Ok = HelperGeneral.User == null ? false : true;
        //    }
        //    catch (Exception ex)
        //    {
        //        jsonData.Message = ex.ToString();
        //        jsonData.Ok = false;
        //    }
        //    return new JavaScriptSerializer().Serialize(jsonData);
        //}

        //[WebMethod]
        //public static string Login(string UserData)
        //{
        //    DataResult jsonData = new DataResult();
        //    try
        //    {
        //       HelperGeneral.User = UserData;
        //        jsonData.Message = UserData;
        //        jsonData.Ok = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        jsonData.Message = ex.ToString();
        //        jsonData.Ok = false;
        //    }
        //    return new JavaScriptSerializer().Serialize(jsonData);
        //}

        //[WebMethod]
        //public static string Logout()
        //{
        //    DataResult jsonData = new DataResult();
        //    try
        //    {
        //        HelperGeneral.CloseSession();
        //    }
        //    catch (Exception ex)
        //    {
        //        jsonData.Message = ex.ToString();
        //        jsonData.Ok = false;
        //    }
        //    return new JavaScriptSerializer().Serialize(jsonData);
        //}

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