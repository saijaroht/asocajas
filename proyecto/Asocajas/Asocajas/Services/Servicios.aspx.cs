using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
        public static string IsLogin()
        {
            DataResult jsonData = new DataResult();
            try
            {
                jsonData.Message = User;
                jsonData.Ok = User == null ? false : true;
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
                //DownloadFile();
                User = UserData;
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
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.RemoveAll();
            }
            catch (Exception ex)
            {
                jsonData.Message = ex.ToString();
                jsonData.Ok = false;
            }
            return new JavaScriptSerializer().Serialize(jsonData);
        }

        #region Session var

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
        #endregion


        public static void DownloadFile()
        {
            //// This is an array of strings right?
            //string[,] DataToExportToCSV = new string[,] { { "2000", "2" }, { "2001", "4" } };

            //// Use a StringBuilder to accumulate your output
            //StringBuilder sb = new StringBuilder("Date;C1\r\n");
            //for (int i = 0; i <= array.GetUpperBound(0); i++)
            //{
            //    for (int j = 0; j <= array.GetUpperBound(1); j++)
            //    {
            //        sb.Append((j == 0 ? "" : ";") + array[i, j]);
            //    }
            //    sb.AppendLine();
            //}

            //// Write everything with a single command 
            //File.WriteAllText(@"C:\\FilesCSV\\fileadress.csv", sb.ToString());

        }
    }
}