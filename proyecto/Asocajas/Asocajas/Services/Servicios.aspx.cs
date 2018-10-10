using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Asocajas.Services
{
    public partial class Servicios : System.Web.UI.Page
    {
        protected static string ReCaptcha_Secret = "6Le1FXQUAAAAAIeQ9MATkXnoQSG7-EXj7Rz6XjJZ";
        private const string SESSION_VAR = "User";

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
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }

        [WebMethod]
        public string IsLogin()
        {
            DataResult jsonData = new DataResult();
            try
            {
                jsonData.Message = this.User;
                jsonData.Ok = this.User == null ? false : true;
            }
            catch (Exception ex)
            {
                jsonData.Message = ex.ToString();
                jsonData.Ok = false;
            }
            return new JavaScriptSerializer().Serialize(jsonData);
        }

        [WebMethod]
        public string Login(string User)
        {
            DataResult jsonData = new DataResult();
            try
            {
                this.User = User;
                jsonData.Message = User;
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
        public string Logout()
        {
            DataResult jsonData = new DataResult();
            try
            {
                this.User = User;
                jsonData.Message = User;
                jsonData.Ok = true;
            }
            catch (Exception ex)
            {
                jsonData.Message = ex.ToString();
                jsonData.Ok = false;
            }
            return new JavaScriptSerializer().Serialize(jsonData);
        }

        #region Session var

        string User
        {
            get
            {
                if (Session[SESSION_VAR] == null)
                    Session[SESSION_VAR] = null;
                return (string)Session[SESSION_VAR];
            }
            set { Session[SESSION_VAR] = value; }
        }
        #endregion
    }
}