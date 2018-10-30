
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Asocajas.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool EsPrueba
        {
            get
            {
                return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EsPrueba"]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool ValidarHorario
        {
            get
            {
                return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ValidarHorario"]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool ValidarMac
        {
            get
            {
                return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ValidarMac"]);
            }
        }
        public static Stopwatch GetStopwatch()
        {

            Stopwatch obj = new Stopwatch();
            obj.Start();
            return obj;
        }
        public static string GetElapsed(this Stopwatch stopwatch, string proccess = null, bool dispose = true)
        {
            string tracer = string.Empty;
            stopwatch.Stop();
            //, string.Format("[{0}:{1}:{2}]", this.Elapsed.Hours, this.Elapsed.Minutes, this.Elapsed.Seconds)
            tracer = string.IsNullOrEmpty(proccess) ?
                string.Format("Elapsed:[{2}]ElapsedMilliseconds:[{3}]{0}",
                Environment.NewLine,
                string.Empty,
                stopwatch.Elapsed.ToString(),
                stopwatch.ElapsedMilliseconds)
                : string.Format("Ejecucion de {1}:{0}Elapsed:[{2}]ElapsedMilliseconds:[{3}]{0}",
                Environment.NewLine,
                proccess,
                stopwatch.Elapsed.ToString(),
                stopwatch.ElapsedMilliseconds);
            if (dispose)
                stopwatch = null;

            return tracer;
        }


        public static object ConvertExtraData(object extraData = null)
        {
            object extraObject = null;
            if (extraData != null && !string.IsNullOrEmpty(extraData.ToString()))
            {
                if (extraData.ToString().StartsWith("["))
                    extraObject = Utility.DeserializeObjectFromString<List<Dictionary<string, object>>>(extraData.ToString());
                else
                    extraObject = Utility.DeserializeObjectFromString<Dictionary<string, object>>(extraData.ToString());
            }
            return extraObject;
        }
        public static Type GetType(string typeName, string assemblyPath)
        {
            Type type = null;
            if (!string.IsNullOrEmpty(assemblyPath))
            {
                assemblyPath = assemblyPath.Replace("#", ":");
                var assembly = Assembly.LoadFile(assemblyPath);
                type = assembly.GetType(typeName);
                return type;
            }
            type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }

        /// <summary>
        /// Especifica el nombre de la aplicacion que contien los servicios
        /// Autor:Marlon Granda-Fecha:25/07/2016-Mail:mar_gran2003@yahoo.com.ar
        /// Empresa:BEXT
        /// </summary>
        public static string NombreAppServicios
        {
            get
            {
                return ConfigurationManager.AppSettings["NombreAppServicios"];
            }
        }

        public static string GetEntityType(object obj, bool lower = false)
        {
            if (obj is Dictionary<string, object>)
            {
                var dir = (obj as Dictionary<string, object>);
                var type = (dir["EntityName"] == null) ? dir["TableName"].ToString() : dir["EntityName"].ToString();
                return (lower) ? type.ToLower() : type;
            }
            else
            {
                return obj.GetType().Name.ToLower();
            }
        }
        public static object GetValueFromValue(object obj, string value, object extraObject,
            Func<object, string, object, object> function = null)
        {
            object valueProperty = value;

            if (value.StartsWith("prop(") || value.StartsWith("prop["))
            {
                var otherProperty = value.Replace("prop(", string.Empty).Replace(")", string.Empty)
                    .Replace("prop[", string.Empty).Replace("]", string.Empty);
                valueProperty = Utility.GetValueFromObject(obj, otherProperty, extraObject);
            }
            else if (value.StartsWith("fun(") || value.StartsWith("fun["))
            {
                var otherProperty = value.Replace("fun(", string.Empty).Replace(")", string.Empty)
                    .Replace("fun[", string.Empty).Replace("]", string.Empty);
                valueProperty = Utility.GetValueFromObject(obj, otherProperty, extraObject);
            }
            return valueProperty;
        }
        public static void SetValueProperty(object entity, PropertyInfo propi, object value)
        {
            if (propi == null)
                return;
            Type ptype = Nullable.GetUnderlyingType(propi.PropertyType) ?? propi.PropertyType;
            propi.SetValue(entity, Convert.ChangeType(value, ptype), null);
        }
        public static object GetValueFromObject(object obj, string property, object extraObject)
        {
            object valueProperty = null;

            valueProperty = Utility.GetValueFromObject(obj, property); //(obj is Int32)?obj:obj.GetType().GetProperty(valueToValidate.Property).GetValue(obj);
            if (extraObject != null && valueProperty == null)
                valueProperty = Utility.GetValueFromObject(extraObject, property);

            return valueProperty;
        }
        public static object GetValueFromObject(object obj, string property)
        {
            object valueProperty = null;
            if (obj == null)
                return valueProperty;
            if (obj is string)
                return valueProperty;
            if (obj is Dictionary<string, object>)
            {
                valueProperty = (obj as Dictionary<string, object>).FirstOrDefault(o => o.Key == property);
            }
            else if (obj is List<Dictionary<string, object>>)
            {
                foreach (var dir in (obj as List<Dictionary<string, object>>))
                {
                    if (dir.Any(o => o.Key == property))
                    {
                        valueProperty = dir.FirstOrDefault(o => o.Key == property);
                        return valueProperty;
                    }
                }
            }
            else
            {
                if (obj is Int32)
                    valueProperty = obj;
                else
                {
                    var prop = obj.GetType().GetProperty(property);
                    if (prop != null)
                        valueProperty = prop.GetValue(obj);
                }
            }
            return valueProperty;
        }
        public static string SerializeObjectFromObject(object obj, bool withFormatting = false)
        {


            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                if (withFormatting)
                {
                    writer.Formatting = Formatting.Indented;
                    //writer.WriteWhitespace(Environment.NewLine);
                }
                writer.QuoteChar = '\'';
                writer.QuoteName = false;
                JsonSerializer ser = new JsonSerializer();
                ser.Serialize(writer, obj);
            }
            return sb.ToString();

        }

        public static T DeserializeObjectFromAppSettings<T>(string name)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(ConfigurationManager.AppSettings[name]);
        }
        public static T DeserializeObjectFromString<T>(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }
        public static T DeserializeObject<T>(object data)
        {

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.FromObject(data);
            T obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jObject.ToString());
            return obj;
        }
        public static T DeserializeObject2<T>(this object data)
        {

            var jObject = data.ToString();
            T obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jObject);
            return obj;
        }

        public static string ToJson(object data)
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return result;
        }
        public static List<Dictionary<string, object>> DataTableToListDictionary(this DataTable table)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {

                    dict[col.ColumnName] = (row[col] == DBNull.Value || row[col] == null) ? string.Empty : row[col];
                }
                list.Add(dict);
            }
            return list;
        }
        public static string GetServerIP()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

                foreach (IPAddress address in ipHostInfo.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                        return address.ToString();
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MachineInfo"></param>
        /// <returns></returns>
        public static string GetUserMachineInfo(string MachineInfo = null, bool IsNameMachine = false)
        {
            if (System.Web.HttpContext.Current == null)
                return string.IsNullOrEmpty(MachineInfo) ? "CONETXT NULO" : MachineInfo;
            if (System.Web.HttpContext.Current.Request == null)
            {
                return string.IsNullOrEmpty(MachineInfo) ? "REQUEST NULO" : MachineInfo;
            }
            return System.Web.HttpContext.Current.Request.GetUserMachineInfo(IsNameMachine);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="MachineInfo"></param>
        /// <returns></returns>
        public static string GetUserMachineInfo(this HttpRequest request, bool IsNameMachine = false, string MachineInfo = null)
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            if (IsNameMachine)
            {
                var hostmi = host.HostName;
                return hostmi.ToString();
            }
            else
            {
                var hostmi = host
                    .AddressList
                    .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                return hostmi.ToString();
            }

            //if (request == null)
            //{
            //    return string.IsNullOrEmpty(MachineInfo) ? "REQUEST NULO" : MachineInfo;
            //}
            //string userMachine = string.Empty;
            //var REMOTE_ADDR = request.ServerVariables["REMOTE_ADDR"];
            //var REMOTE_HOST = request.ServerVariables["REMOTE_HOST"];
            //var REMOTE_PORT = request.ServerVariables["REMOTE_PORT"];
            //if (REMOTE_ADDR == REMOTE_HOST)
            //    userMachine = REMOTE_HOST + (string.IsNullOrEmpty(REMOTE_PORT) ? string.Empty : ":" + REMOTE_PORT);
            //else
            //    userMachine = REMOTE_HOST + "|" + REMOTE_ADDR + (string.IsNullOrEmpty(REMOTE_PORT) ? string.Empty : ":" + REMOTE_PORT);

            //return userMachine;
        }
        public static string GetMachineName()
        {
            if (System.Web.HttpContext.Current == null)
                return "CONETEXT NULO";
            return System.Web.HttpContext.Current.Server.MachineName;
        }
        public static string GetUserIP()
        {
            if (System.Web.HttpContext.Current == null)
                return "CONETEXT NULO";
            return System.Web.HttpContext.Current.Request.GetUserIP();
        }
        public static string GetUserIP(this HttpRequest request)
        {
            if (request == null)
                return "REQUEST NULO";
            string userMachine = string.Empty;
            var REMOTE_ADDR = request.ServerVariables["REMOTE_ADDR"];
            return REMOTE_ADDR;
        }

        public static string GetUserHostName()
        {
            if (System.Web.HttpContext.Current == null)
                return "CONETXT NULO";
            return System.Web.HttpContext.Current.Request.GetUserHostName();
        }
        public static string GetUserHostName(this HttpRequest request)
        {
            if (request == null)
                return "REQUEST NULO";
            string userMachine = string.Empty;
            var REMOTE_ADDR = request.UserHostName;

            return REMOTE_ADDR;
        }


        public static string GetLoginURL()
        {
            if (System.Web.HttpContext.Current == null)
                return "CONETXT NULO";
            return System.Web.HttpContext.Current.Request.GetUserURL();
        }
        public static string GetUserURL(this HttpRequest request)
        {
            if (request == null)
                return "REQUEST NULO";
            System.Configuration.AppSettingsReader settingsReader =
                                               new AppSettingsReader();

            var URL = request.Url.Scheme + "://" + request.Url.Authority +
    request.ApplicationPath.TrimEnd('/') + "/"+

            (string)settingsReader.GetValue("LoginUrl",
                                                         typeof(String));
            return URL;
        }

        public static string PathLog
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("~/Log/");
            }
        }
        public static string PathSolicitudes
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("~/Solicitudes/");
            }
        }
        public static string PathImagenesProductos
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("~/ImagenesProductos/");
            }
        }
        public static string NamePathImagenesProductos()
        {
            return "/ImagenesProductos/";

        }
        public static List<T> ExecuteSpToListOfT<T>(this DbContext context, string spName, Dictionary<string, object> parameters)
        {
            List<SqlParameter> _parameters = new List<SqlParameter>();
            string sql = spName + " ";
            foreach (KeyValuePair<string, object> pair in parameters)
            {
                _parameters.Add(new SqlParameter((pair.Key.ToLower().StartsWith("@") ? pair.Key : "@" + pair.Key), pair.Value));
                sql += (pair.Key.ToLower().StartsWith("@") ? pair.Key : "@" + pair.Key) + ",";
            }
            sql = sql.TrimEnd(',');
            sql = sql.Trim();
            return context.Database.SqlQuery<T>(sql, _parameters.ToArray()).ToList();
        }

        public static DataTable ExecuteSpToDataTable(this DbContext context, string spName, Dictionary<string, object> parameters)
        {
            List<SqlParameter> _parameters = new List<SqlParameter>();
            foreach (KeyValuePair<string, object> pair in parameters)
            {
                _parameters.Add(new SqlParameter((pair.Key.ToLower().StartsWith("@") ? pair.Key : "@" + pair.Key), pair.Value));
            }
            return context.ExecuteSpToDataTable(spName, _parameters);
        }

        public static DataTable ExecuteSpToDataTable(this DbContext context, string spName, params SqlParameter[] parameters)
        {
            return context.ExecuteSpToDataTable(spName, parameters.ToList());
        }

        public static DataTable ExecuteSpToDataTable(this DbContext context, string spName, List<SqlParameter> parameters)
        {
            DataTable dtt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                SqlCommand c = new SqlCommand();
                c.CommandText = spName;
                c.CommandType = CommandType.StoredProcedure;
                c.Connection = (context.Database.Connection as SqlConnection);
                c.Parameters.AddRange(parameters.ToArray());
                adapter.SelectCommand = c;
                adapter.Fill(dtt);

            }
            return dtt;
        }
        public static DataSet ExecuteSpToDataSet(this DbContext context, string spName, List<SqlParameter> parameters)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                SqlCommand c = new SqlCommand();
                c.CommandText = spName;
                c.CommandType = CommandType.StoredProcedure;
                c.Connection = (context.Database.Connection as SqlConnection);
                c.Parameters.AddRange(parameters.ToArray());
                adapter.SelectCommand = c;
                adapter.Fill(ds);

            }
            return ds;
        }
        public static DataTable ExecuteQueryToDataTable(this DbContext context, string query)
        {
            DataTable dtt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                SqlCommand c = new SqlCommand();
                c.CommandText = query;
                c.CommandType = CommandType.Text;
                c.Connection = (context.Database.Connection as SqlConnection);

                adapter.SelectCommand = c;
                adapter.Fill(dtt);

            }
            return dtt;
        }

        public static DataSet ExecuteQueryToDataSet(this DbContext context, string query)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                SqlCommand c = new SqlCommand();
                c.CommandText = query;
                c.CommandType = CommandType.Text;
                c.Connection = (context.Database.Connection as SqlConnection);

                adapter.SelectCommand = c;
                adapter.Fill(ds);

            }
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ExecuteQueryReaderToListDictionary(this DbContext context, string query)
        {
            if ((context.Database.Connection as SqlConnection).State != ConnectionState.Open)
                (context.Database.Connection as SqlConnection).Open();
            using (SqlCommand command = new SqlCommand(query, (context.Database.Connection as SqlConnection)))
            {
                command.CommandType = CommandType.Text;

                using (var objDataReader = command.ExecuteReader())
                    return MapBuilderListDictionary(objDataReader);

            }

        }
        public static List<Dictionary<string, object>> ExecuteSpReaderToListDictionary(this DbContext context, string spName, Dictionary<string, object> parameters)
        {
            List<SqlParameter> _parameters = new List<SqlParameter>();
            foreach (KeyValuePair<string, object> pair in parameters)
            {
                _parameters.Add(new SqlParameter((pair.Key.ToLower().StartsWith("@") ? pair.Key : "@" + pair.Key), pair.Value));
            }
            return context.ExecuteSpReaderToListDictionary(spName, _parameters);
        }
        public static List<Dictionary<string, object>> ExecuteSpReaderToListDictionary(this DbContext context, string spName, List<SqlParameter> parameters)
        {
            if ((context.Database.Connection as SqlConnection).State != ConnectionState.Open)
                (context.Database.Connection as SqlConnection).Open();
            using (SqlCommand command = new SqlCommand(spName, (context.Database.Connection as SqlConnection)))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters.ToArray());
                using (var objDataReader = command.ExecuteReader())
                    return MapBuilderListDictionary(objDataReader);

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDataReader"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> MapBuilderListDictionary(SqlDataReader objDataReader)
        {


            List<Dictionary<string, object>> lstEntidades = new List<Dictionary<string, object>>();
            int count = 1;
            string identityField = string.Empty;

            while (objDataReader.Read())
            {
                Dictionary<string, object> entidad = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(identityField))
                    entidad.Add(identityField, count);

                for (int i = 0; i < objDataReader.FieldCount; i++)
                {
                    string name = objDataReader.GetName(i);
                    var value = objDataReader.GetValue(i);
                    var dataType = objDataReader.GetFieldType(i);
                    value = (value == DBNull.Value || value == null) ? string.Empty : value;
                    entidad.Add(name, value);

                }

                lstEntidades.Add(entidad);
                //if (!string.IsNullOrEmpty(identityField) && filterIdentityField == count)
                //    return lstEntidades;
                //filterIdentityField

                count++;
            }

            return lstEntidades;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ExecuteMultiQuery(this DbContext context, string query)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            var ds = context.ExecuteQueryToDataSet(query);
            int count = 0;
            foreach (DataTable dataTable in ds.Tables)
            {
                List<Dictionary<string, object>> lst = dataTable.DataTableToListDictionary();

                result.Add(count.ToString(), lst);
                count++;

            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ExecuteMultiQuerySp(this DbContext context, string spName, List<SqlParameter> parameters)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            var ds = context.ExecuteSpToDataSet(spName, parameters);
            int count = 0;
            foreach (DataTable dataTable in ds.Tables)
            {
                List<Dictionary<string, object>> lst = dataTable.DataTableToListDictionary();

                result.Add(count.ToString(), lst);
                count++;

            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ExecuteQueryToListDictonary(this DbContext context, string query)
        {
            return Utility.DataTableToListDictionary(context.ExecuteQueryToDataTable(query));
        }
        public static List<Dictionary<string, object>> ExecuteSpToListDictonary(this DbContext context, string spName, Dictionary<string, object> parameters)
        {
            return Utility.DataTableToListDictionary(context.ExecuteSpToDataTable(spName, parameters));
        }

        public static List<Dictionary<string, object>> ExecuteSpToListDictonary(this DbContext context, string spName, List<SqlParameter> parameters)
        {
            return Utility.DataTableToListDictionary(context.ExecuteSpToDataTable(spName, parameters));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteSpToRowsCount(this DbContext context, string spName, List<SqlParameter> parameters)
        {
            return context.ExecuteSpToDataTable(spName, parameters).Rows.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteSpToRowsCount(this DbContext context, string spName, params SqlParameter[] parameters)
        {
            return context.ExecuteSpToDataTable(spName, parameters.ToList()).Rows.Count;
        }


        public static T Clone<T>(this T source) where T : class
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
            //var obj = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            //using (var stream = new System.IO.MemoryStream())
            //{
            //    obj.WriteObject(stream, source);
            //    stream.Seek(0, System.IO.SeekOrigin.Begin);
            //    return (T)obj.ReadObject(stream);
            //}
        }


        //static string encryptionKey = "_Bext#Security$Token";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public static string TripleDES(string token, bool encrypt)
        {
            try
            {
                TripleDESCryptoServiceProvider odes = new TripleDESCryptoServiceProvider();
                Encoding encoder = Encoding.ASCII;
                MD5CryptoServiceProvider ohashAlgo = new MD5CryptoServiceProvider();
                System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
                string encryptionKey = (string)settingsReader.GetValue("SecurityKey",
                                                         typeof(String));
                odes.Key = ohashAlgo.ComputeHash(encoder.GetBytes(encryptionKey));
                odes.Mode = CipherMode.ECB;
                if (encrypt)
                {
                    ICryptoTransform desEncrypt = odes.CreateEncryptor();
                    ASCIIEncoding oASCIIEncoding = new ASCIIEncoding();
                    Byte[] bytbuff = ASCIIEncoding.ASCII.GetBytes(token);
                    return Convert.ToBase64String(desEncrypt.TransformFinalBlock(bytbuff, 0, bytbuff.Length));
                }
                else
                {
                    ICryptoTransform desDecrypt = odes.CreateDecryptor();
                    Byte[] bytbuff = Convert.FromBase64String(token);
                    return ASCIIEncoding.ASCII.GetString(desDecrypt.TransformFinalBlock(bytbuff, 0, bytbuff.Length));
                }
            }
            catch (Exception)
            {
                throw new Exception("Invalid encryption / decryption");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ConvertToBase64(this Stream stream)
        {
            Byte[] inArray = new Byte[(int)stream.Length];
            Char[] outArray = new Char[(int)(stream.Length * 1.34)];
            stream.Read(inArray, 0, (int)stream.Length);
            Convert.ToBase64CharArray(inArray, 0, inArray.Length, outArray, 0);
            var str = Convert.ToBase64String(inArray);
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="toAdd"></param>
        public static void AddDictionary(this Dictionary<string, object> obj, Dictionary<string, object> toAdd)
        {
            foreach (KeyValuePair<string, object> item in toAdd)
                obj.Add(item.Key, item.Value);
        }

        

        //public static IEnumerable<System.Reflection.PropertyInfo> GetPropiedadesPlanas<T>(this T entidad, List<string> lstColumnasExclucion = null, string _namespace = null) where T : class
        //{
        //    return Utility.GetPropiedadesPlanas(entidad, lstColumnasExclucion, _namespace);
        //}

        public static IEnumerable<System.Reflection.PropertyInfo> GetPropiedadesPlanas<T>(this T entidad,
            List<string> lstColumnasExclucion = null,
            string _namespace = null) where T : class
        {
            string strNamespace = typeof(T).Namespace;
            if (lstColumnasExclucion == null)
                lstColumnasExclucion = new List<string>();
            IEnumerable<System.Reflection.PropertyInfo> properties = new List<System.Reflection.PropertyInfo>();

            if (entidad is Type)
            {
                Type tipo = entidad as Type;
                strNamespace = tipo.Namespace;
                if (!string.IsNullOrEmpty(_namespace))
                    strNamespace = _namespace;
                properties = tipo.GetProperties(System.Reflection.BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
                    .Where(p => !p.PropertyType.Name.Contains("EntityCollection`1") &&
                      !p.PropertyType.Name.Contains("EntityReference`1") &&
                      !p.PropertyType.Name.Contains("ICollection`1") &&
                      !p.PropertyType.Name.Contains("IEnumerable`1") &&
                      !p.PropertyType.Name.Contains("IList`1") &&
                      !p.PropertyType.Name.Contains("List`1") &&
                      p.PropertyType.Name != p.Name &&
                      p.PropertyType.Namespace != strNamespace && !lstColumnasExclucion.Exists(o => o == p.Name));
            }
            else
            {
                properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
                    .Where(p => !p.PropertyType.Name.Contains("EntityCollection`1") &&
                      !p.PropertyType.Name.Contains("EntityReference`1") &&
                      !p.PropertyType.Name.Contains("ICollection`1") &&
                      !p.PropertyType.Name.Contains("IEnumerable`1") &&
                      !p.PropertyType.Name.Contains("IList`1") &&
                      !p.PropertyType.Name.Contains("List`1") &&
                      p.PropertyType.Name != p.Name &&
                      p.PropertyType.Namespace != strNamespace && !lstColumnasExclucion.Exists(o => o == p.Name));
            }

            var listas = properties.ToList();
            return properties;
        }
        public static T SetValor<T>(T entidad, string propiedad, object valor)
        {
            PropertyInfo propiedadActualAsignacion = entidad.GetType().GetProperty(propiedad);
            if (propiedadActualAsignacion != null)
            {
                Type ptype = Nullable.GetUnderlyingType(propiedadActualAsignacion.PropertyType) ?? propiedadActualAsignacion.PropertyType;
                if (valor != null)
                    propiedadActualAsignacion.SetValue(entidad, Convert.ChangeType(valor, ptype), null);
            }
            return entidad;
        }


        public static object ConvertirEntidades<T>(object entidadParaAsignacion, T entidadFuente,
            Dictionary<string, object> lstValoresAdicinales,
            bool soloLasPlanas = true)
        {
            try
            {
                if (lstValoresAdicinales == null)
                    lstValoresAdicinales = new Dictionary<string, object>();
                if (entidadParaAsignacion == null)
                    entidadParaAsignacion = Activator.CreateInstance(entidadParaAsignacion.GetType());
                //var propiedades =entidadFuente.GetPropiedadesPlanas(
                if (entidadFuente != null)
                {
                    var propiedades = entidadFuente.GetType().GetPropiedadesPlanas(null, typeof(T).Namespace);
                    if (propiedades.Count() == 0)
                        propiedades = typeof(T).GetPropiedadesPlanas(null, typeof(T).Namespace);
                    foreach (PropertyInfo propiedadActualFunete in propiedades)
                    {
                        try
                        {
                            object valor = propiedadActualFunete.GetValue(entidadFuente, null);
                            SetValor(entidadParaAsignacion, propiedadActualFunete.Name, valor);
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                    }
                }
                try
                {
                    foreach (KeyValuePair<string, object> pair in lstValoresAdicinales)
                    {
                        SetValor(entidadParaAsignacion, pair.Key, pair.Value);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                return entidadParaAsignacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}