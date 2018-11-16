using Asocajas.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Asocajas.ConsultaCedulasPrueba;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace Asocajas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : ApiController, System.Web.SessionState.IRequiresSessionState
    {
        #region LTLogEventos
        public class DataTableDataLogEventos
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<LTLogEventos> data { get; set; }
            public string DownloadStr { get; set; }
        }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public IHttpActionResult AjaxGetJsonDataLTLogEventos(object parameters)
        {
            using (BusinessBase<LTLogEventos> objLTLogEventos = new BusinessBase<LTLogEventos>())
            {
                using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                {
                    using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
                    {
                        DataTableDataLogEventos dataTableData = new DataTableDataLogEventos();
                        var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);
                        JsonDeserializer js = new JsonDeserializer(input["search"]["value"].ToString());
                        List<LTLogEventos> list = new List<LTLogEventos>();
                        //var timestamp = Number(Request.QueryString('ts'));

                        var IdCCFstr = js.GetString("search.IdCCF");
                        var IdUsuariostr = js.GetString("search.IdUsuario");
                        var FechaInicialstr = js.GetString("search.FechaInicial");
                        var FechaFinalstr = js.GetString("search.FechaFinal");

                        int? IdUsuario = !string.IsNullOrEmpty(IdUsuariostr) ? (int?)Convert.ToInt32(IdUsuariostr) : null;
                        int? IdCCF = !string.IsNullOrEmpty(IdCCFstr) ? (int?)Convert.ToInt64(IdCCFstr) : null;
                        DateTime? FechaInicial = !string.IsNullOrEmpty(FechaInicialstr) ? (DateTime?)Convert.ToDateTime(FechaInicialstr) : null;
                        DateTime? FechaFinal = !string.IsNullOrEmpty(FechaFinalstr) ? (DateTime?)Convert.ToDateTime(FechaFinalstr).AddDays(1) : null;

                        FechaFinalstr = !string.IsNullOrEmpty(FechaFinalstr) ? Convert.ToDateTime(FechaFinal).Year.ToString() + "-" + Convert.ToDateTime(FechaFinal).Month.ToString() + "-" + Convert.ToDateTime(FechaFinal).Day.ToString() : "";
                        string where = "";
                        if ((!string.IsNullOrEmpty(IdUsuariostr)) || (!string.IsNullOrEmpty(IdCCFstr)) || (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr)))
                        {
                            where += "where ";
                            if (!string.IsNullOrEmpty(IdUsuariostr))
                            {
                                where += "IdUsuario = " + IdUsuario.ToString();
                            }
                            else if (!string.IsNullOrEmpty(IdCCFstr))
                            {
                                var IdsUsersCCF = objRUsuario.Get(o => o.IdCcf == IdCCF).Select(o => o.IdUsuario).ToList();
                                where += "CHARINDEX(CAST(IdUsuario AS VARCHAR),'";
                                foreach (var item in IdsUsersCCF)
                                {
                                    where += item + ";";
                                }
                                where += "')<>0";
                            }
                            if (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr))
                            {
                                if ((!string.IsNullOrEmpty(IdUsuariostr)) || (!string.IsNullOrEmpty(IdCCFstr)))
                                {
                                    where += " and ";
                                }
                                where += "FechaEvento BETWEEN '" + FechaInicialstr + "' AND '" + FechaFinalstr + "'";
                            }
                        }
                        list = (int)input["draw"] == 1 ? new List<LTLogEventos>() : objLTLogEventos.PaginadorConsultas((int)input["start"], (int)input["length"], where).ToList();

                        #region CSVFile

                        var download = Convert.ToBoolean(input["download"]);
                        if (download)
                        {
                            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("CCF, Usuario, Evento, FechaEvento\n");
                            var datosExport = objLTLogEventos.PaginadorConsultas(0, (int)settingsReader.GetValue("CountExportCSV", typeof(int)), where).ToList();
                            var allobjRUsuario = objRUsuario.Get().ToList();
                            var allobjRCCF = objRCCF.Get().ToList();
                            foreach (var item in datosExport)
                            {
                                item.RUsuario = allobjRUsuario.Where(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                item.RUsuario.RCCF = allobjRCCF.Where(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                                sb.AppendLine(item.RUsuario.RCCF.Nombre + "," + item.RUsuario.Nombres + "," + item.Evento + "," + item.FechaEvento);
                            }

                            //var csvExport = datosExport.Select(o => new { o.RUsuario.Nombres, o.Evento, o.FechaEvento }).ToList();
                            //sb.AppendLine(string.Join(",", csvExport));
                            //sb.AppendLine("parametro11, parametro21, parametro31");
                            //sb.AppendLine("parametro12, parametro22, parametro32");
                            //sb.AppendLine("parametro13, parametro23, parametro33");
                            //sb.AppendLine("parametro14, parametro24, parametro34");
                            dataTableData.DownloadStr = sb.ToString();
                        }

                        #endregion

                        dataTableData.draw = (int)input["draw"];
                        dataTableData.recordsFiltered = (int)input["draw"] == 1 ? 0 : objLTLogEventos.CountPaginadorConsultas(where);
                        foreach (var item in list)
                        {
                            item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                            item.RUsuario.RCCF = objRCCF.Get(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                        }

                        dataTableData.data = list;
                        dataTableData.recordsTotal = dataTableData.recordsFiltered;

                        return Json(dataTableData);
                    }
                }
            }
        }
        #endregion

        #region LTLogConsultasAni
        public class DataTableDataLogConsultasAni
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<LTLogConsultasAni> data { get; set; }
            public string DownloadStr { get; set; }
        }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public IHttpActionResult AjaxGetJsonDataLTLogConsultasAni(object parameters)
        {
            using (BusinessBase<LTLogConsultasAni> objLTLogConsultasAni = new BusinessBase<LTLogConsultasAni>())
            {
                using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                {
                    using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
                    {
                        using (BusinessBase<RRptaAsocajas> objRRptaAsocajas = new BusinessBase<RRptaAsocajas>())
                        {
                            using (BusinessBase<ROrigen> objROrigen = new BusinessBase<ROrigen>())
                            {
                                DataTableDataLogConsultasAni dataTableData = new DataTableDataLogConsultasAni();
                                var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);
                                JsonDeserializer js = new JsonDeserializer(input["search"]["value"].ToString());
                                List<LTLogConsultasAni> list = new List<LTLogConsultasAni>();
                                //var timestamp = Number(Request.QueryString('ts'));

                                var IdCCFstr = js.GetString("search.IdCCF");
                                var IdUsuariostr = js.GetString("search.IdUsuario");
                                var FechaInicialstr = js.GetString("search.FechaInicial");
                                var FechaFinalstr = js.GetString("search.FechaFinal");

                                int? IdUsuario = !string.IsNullOrEmpty(IdUsuariostr) ? (int?)Convert.ToInt32(IdUsuariostr) : null;
                                int? IdCCF = !string.IsNullOrEmpty(IdCCFstr) ? (int?)Convert.ToInt64(IdCCFstr) : null;
                                DateTime? FechaInicial = !string.IsNullOrEmpty(FechaInicialstr) ? (DateTime?)Convert.ToDateTime(FechaInicialstr) : null;
                                DateTime? FechaFinal = !string.IsNullOrEmpty(FechaFinalstr) ? (DateTime?)Convert.ToDateTime(FechaFinalstr).AddDays(1) : null;

                                FechaFinalstr = !string.IsNullOrEmpty(FechaFinalstr) ? Convert.ToDateTime(FechaFinal).Year.ToString() + "-" + Convert.ToDateTime(FechaFinal).Month.ToString() + "-" + Convert.ToDateTime(FechaFinal).Day.ToString() : "";
                                string where = "";
                                if ((!string.IsNullOrEmpty(IdUsuariostr)) || (!string.IsNullOrEmpty(IdCCFstr)) || (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr)))
                                {
                                    where += "where ";
                                    if (!string.IsNullOrEmpty(IdUsuariostr))
                                    {
                                        where += "IdUsuario = " + IdUsuario.ToString();
                                    }
                                    else if (!string.IsNullOrEmpty(IdCCFstr))
                                    {
                                        var IdsUsersCCF = objRUsuario.Get(o => o.IdCcf == IdCCF).Select(o => o.IdUsuario).ToList();
                                        where += "CHARINDEX(CAST(IdUsuario AS VARCHAR),'";
                                        foreach (var item in IdsUsersCCF)
                                        {
                                            where += item + ";";
                                        }
                                        where += "')<>0";
                                    }
                                    if (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr))
                                    {
                                        if ((!string.IsNullOrEmpty(IdUsuariostr)) || (!string.IsNullOrEmpty(IdCCFstr)))
                                        {
                                            where += " and ";
                                        }
                                        where += "FechaConsulta BETWEEN '" + FechaInicialstr + "' AND '" + FechaFinalstr + "'";
                                    }
                                }
                                list = (int)input["draw"] == 1 ? new List<LTLogConsultasAni>() : objLTLogConsultasAni.PaginadorConsultas((int)input["start"], (int)input["length"], where).ToList();

                                #region CSVFile

                                var download = Convert.ToBoolean(input["download"]);
                                if (download)
                                {
                                    System.Configuration.AppSettingsReader settingsReader =
                                                        new AppSettingsReader();

                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendLine("CCF, Usuario, Id., Documento, Vía, Estado, Fecha Inicia, Fecha Fin, Id Rnec, Desc Rnec");
                                    var datosExport = objLTLogConsultasAni.PaginadorConsultas(0, (int)settingsReader.GetValue("CountExportCSV", typeof(int)), where).ToList();
                                    var allobjRUsuario = objRUsuario.Get().ToList();
                                    var allobjRCCF = objRCCF.Get().ToList();
                                    var allobjROrigen = objROrigen.Get().ToList();
                                    var allobjRRptaAsocajas = objRRptaAsocajas.Get().ToList();
                                    foreach (var item in datosExport)
                                    {
                                        item.RUsuario = allobjRUsuario.Where(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                        item.RUsuario.RCCF = allobjRCCF.Where(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                                        item.ROrigen = allobjROrigen.Where(o => o.IdOrigen == item.IdOrigen).FirstOrDefault();
                                        var rRptaAsocajas = objRRptaAsocajas.Get(o => o.IdRptaAsocajas == item.IdRptaAsocajas).ToList();
                                        item.RRptaAsocajas = rRptaAsocajas.Count() > 0 ? rRptaAsocajas.FirstOrDefault() : new RRptaAsocajas();
                                        sb.AppendLine(
                                            item.RUsuario.RCCF.Nombre + "," + 
                                            item.RUsuario.Nombres + "," +
                                            item.IdConsulta + "," +
                                            item.Nuip + "," +
                                            item.ROrigen.OrigenConsulta + "," +
                                            item.RRptaAsocajas.RptaAsocajas + "," +
                                            item.FechaInicia + "," +
                                            item.FechaFin + "," +
                                            item.ControlRNEC + "," +
                                            item.DescrRNEC);
                                    }
                                    dataTableData.DownloadStr = sb.ToString();
                                }

                                #endregion

                                dataTableData.draw = (int)input["draw"];
                                dataTableData.recordsFiltered = (int)input["draw"] == 1 ? 0 : objLTLogConsultasAni.CountPaginadorConsultas(where);
                                foreach (var item in list)
                                {
                                    item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                    item.RUsuario.RCCF = objRCCF.Get(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                                    item.ROrigen = objROrigen.Get(o => o.IdOrigen == item.IdOrigen).FirstOrDefault();
                                    var rRptaAsocajas = objRRptaAsocajas.Get(o => o.IdRptaAsocajas == item.IdRptaAsocajas).ToList();
                                    item.RRptaAsocajas = rRptaAsocajas.Count() > 0 ? rRptaAsocajas.FirstOrDefault() : new RRptaAsocajas();
                                }

                                dataTableData.data = list;
                                dataTableData.recordsTotal = dataTableData.recordsFiltered;

                                return Json(dataTableData);
                            }
                        }
                    }
                }
            }
        }

        public class DataTableDataLogConsultasAniByCCF
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<LTLogConsultasAni> data { get; set; }
            public string DownloadStr { get; set; }
        }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public IHttpActionResult AjaxGetJsonDataLTLogConsultasAniByCCF(object parameters)
        {
            using (BusinessBase<LTLogConsultasAni> objLTLogConsultasAni = new BusinessBase<LTLogConsultasAni>())
            {
                using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                {
                    using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
                    {
                        using (BusinessBase<RRptaAsocajas> objRRptaAsocajas = new BusinessBase<RRptaAsocajas>())
                        {
                            using (BusinessBase<ROrigen> objROrigen = new BusinessBase<ROrigen>())
                            {
                                DataTableDataLogConsultasAniByCCF dataTableData = new DataTableDataLogConsultasAniByCCF();
                                var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);
                                JsonDeserializer js = new JsonDeserializer(input["search"]["value"].ToString());
                                List<LTLogConsultasAni> list = new List<LTLogConsultasAni>();
                                //var timestamp = Number(Request.QueryString('ts'));

                                var IdCCFstr = js.GetString("search.IdCCF");
                                var IdUsuariostr = js.GetString("search.IdUsuario");
                                var FechaInicialstr = js.GetString("search.FechaInicial");
                                var FechaFinalstr = js.GetString("search.FechaFinal");

                                var Usuario = HelperGeneral.GetSession();
                                IdCCFstr = objRUsuario.Get(o => o.Usuario == Usuario).FirstOrDefault().IdCcf.ToString();

                                int? IdUsuario = !string.IsNullOrEmpty(IdUsuariostr) ? (int?)Convert.ToInt32(IdUsuariostr) : null;
                                int? IdCCF = !string.IsNullOrEmpty(IdCCFstr) ? (int?)Convert.ToInt64(IdCCFstr) : null;
                                DateTime? FechaInicial = !string.IsNullOrEmpty(FechaInicialstr) ? (DateTime?)Convert.ToDateTime(FechaInicialstr) : null;
                                DateTime? FechaFinal = !string.IsNullOrEmpty(FechaFinalstr) ? (DateTime?)Convert.ToDateTime(FechaFinalstr).AddDays(1) : null;

                                FechaFinalstr = !string.IsNullOrEmpty(FechaFinalstr) ? Convert.ToDateTime(FechaFinal).Year.ToString() + "-" + Convert.ToDateTime(FechaFinal).Month.ToString() + "-" + Convert.ToDateTime(FechaFinal).Day.ToString() : "";
                                string where = "";
                                if ((!string.IsNullOrEmpty(IdUsuariostr)) || (!string.IsNullOrEmpty(IdCCFstr)) || (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr)))
                                {
                                    where += "where ";
                                    if (!string.IsNullOrEmpty(IdUsuariostr))
                                    {
                                        where += "IdUsuario = " + IdUsuario.ToString();
                                    }
                                    else if (!string.IsNullOrEmpty(IdCCFstr))
                                    {
                                        var IdsUsersCCF = objRUsuario.Get(o => o.IdCcf == IdCCF).Select(o => o.IdUsuario).ToList();
                                        where += "CHARINDEX(CAST(IdUsuario AS VARCHAR),'";
                                        foreach (var item in IdsUsersCCF)
                                        {
                                            where += item + ";";
                                        }
                                        where += "')<>0";
                                    }
                                    if (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr))
                                    {
                                        if ((!string.IsNullOrEmpty(IdUsuariostr)) || (!string.IsNullOrEmpty(IdCCFstr)))
                                        {
                                            where += " and ";
                                        }
                                        where += "FechaConsulta BETWEEN '" + FechaInicialstr + "' AND '" + FechaFinalstr + "'";
                                    }
                                }
                                list = (int)input["draw"] == 1 ? new List<LTLogConsultasAni>() : objLTLogConsultasAni.PaginadorConsultas((int)input["start"], (int)input["length"], where).ToList();

                                #region CSVFile

                                var download = Convert.ToBoolean(input["download"]);
                                if (download)
                                {
                                    System.Configuration.AppSettingsReader settingsReader =
                                                        new AppSettingsReader();

                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendLine("CCF, Usuario, Id., Documento, Vía, Estado, Fecha Inicia, Fecha Fin, Id Rnec, Desc Rnec");
                                    var datosExport = objLTLogConsultasAni.PaginadorConsultas(0, (int)settingsReader.GetValue("CountExportCSV", typeof(int)), where).ToList();
                                    var allobjRUsuario = objRUsuario.Get().ToList();
                                    var allobjRCCF = objRCCF.Get().ToList();
                                    var allobjROrigen = objROrigen.Get().ToList();
                                    var allobjRRptaAsocajas = objRRptaAsocajas.Get().ToList();
                                    foreach (var item in datosExport)
                                    {
                                        item.RUsuario = allobjRUsuario.Where(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                        item.RUsuario.RCCF = allobjRCCF.Where(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                                        item.ROrigen = allobjROrigen.Where(o => o.IdOrigen == item.IdOrigen).FirstOrDefault();
                                        var rRptaAsocajas = objRRptaAsocajas.Get(o => o.IdRptaAsocajas == item.IdRptaAsocajas).ToList();
                                        item.RRptaAsocajas = rRptaAsocajas.Count() > 0 ? rRptaAsocajas.FirstOrDefault() : new RRptaAsocajas();
                                        sb.AppendLine(
                                            item.RUsuario.RCCF.Nombre + "," +
                                            item.RUsuario.Nombres + "," +
                                            item.IdConsulta + "," +
                                            item.Nuip + "," +
                                            item.ROrigen.OrigenConsulta + "," +
                                            item.RRptaAsocajas.RptaAsocajas + "," +
                                            item.FechaInicia + "," +
                                            item.FechaFin + "," +
                                            item.ControlRNEC + "," +
                                            item.DescrRNEC);
                                    }
                                    dataTableData.DownloadStr = sb.ToString();
                                }

                                #endregion

                                dataTableData.draw = (int)input["draw"];
                                dataTableData.recordsFiltered = (int)input["draw"] == 1 ? 0 : objLTLogConsultasAni.CountPaginadorConsultas(where);
                                foreach (var item in list)
                                {
                                    item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                    item.RUsuario.RCCF = objRCCF.Get(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                                    item.ROrigen = objROrigen.Get(o => o.IdOrigen == item.IdOrigen).FirstOrDefault();
                                    var rRptaAsocajas = objRRptaAsocajas.Get(o => o.IdRptaAsocajas == item.IdRptaAsocajas).ToList();
                                    item.RRptaAsocajas = rRptaAsocajas.Count() > 0 ? rRptaAsocajas.FirstOrDefault() : new RRptaAsocajas();
                                }

                                dataTableData.data = list;
                                dataTableData.recordsTotal = dataTableData.recordsFiltered;

                                return Json(dataTableData);
                            }
                        }
                    }
                }
            }
        }


        public class DataTableDataLogConsultasProcesadas
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<LTLogConsultasProcesadas> data { get; set; }
            public string DownloadStr { get; set; }
        }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public IHttpActionResult AjaxGetJsonDataLTLogConsultasProcesadas(object parameters)
        {
            using (BusinessBase<LTLogConsultasProcesadas> objLTLogConsultasAni = new BusinessBase<LTLogConsultasProcesadas>())
            {
                using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                {
                    DataTableDataLogConsultasProcesadas dataTableData = new DataTableDataLogConsultasProcesadas();
                    var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);
                    JsonDeserializer js = new JsonDeserializer(input["search"]["value"].ToString());
                    List<LTLogConsultasProcesadas> list = new List<LTLogConsultasProcesadas>();
                    //var timestamp = Number(Request.QueryString('ts'));

                    var IdCCFstr = js.GetString("search.IdCCF");
                    var IdUsuariostr = js.GetString("search.IdUsuario");
                    var FechaInicialstr = js.GetString("search.FechaInicial");
                    var FechaFinalstr = js.GetString("search.FechaFinal");
                    bool esIgual = false;
                    if (FechaInicialstr == FechaFinalstr)
                    {
                        esIgual = true;
                    }

                    int? IdUsuario = !string.IsNullOrEmpty(IdUsuariostr) ? (int?)Convert.ToInt32(IdUsuariostr) : null;
                    int? IdCCF = !string.IsNullOrEmpty(IdCCFstr) ? (int?)Convert.ToInt64(IdCCFstr) : null;
                    DateTime? FechaInicial = !string.IsNullOrEmpty(FechaInicialstr) ? (DateTime?)Convert.ToDateTime(FechaInicialstr) : null;
                    DateTime? FechaFinal = !string.IsNullOrEmpty(FechaFinalstr) ? (DateTime?)Convert.ToDateTime(FechaFinalstr).AddDays(1) : null;

                    FechaFinalstr = !string.IsNullOrEmpty(FechaFinalstr) ? Convert.ToDateTime(FechaFinal).Year.ToString() + "-" + Convert.ToDateTime(FechaFinal).Month.ToString() + "-" + Convert.ToDateTime(FechaFinal).Day.ToString() : "";
                    string where = "";
                    if ((!string.IsNullOrEmpty(IdUsuariostr)) || (!string.IsNullOrEmpty(IdCCFstr)) || (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr)))
                    {
                        where += "where ";
                        if (!string.IsNullOrEmpty(IdUsuariostr))
                        {
                            where += "IdUsuario = " + IdUsuario.ToString();
                        }
                        else if (!string.IsNullOrEmpty(IdCCFstr))
                        {
                            var IdsUsersCCF = objRUsuario.Get(o => o.IdCcf == IdCCF).Select(o => o.IdUsuario).ToList();
                            where += "CHARINDEX(CAST(IdUsuario AS VARCHAR),'";
                            foreach (var item in IdsUsersCCF)
                            {
                                where += item + ";";
                            }
                            where += "')<>0";
                        }
                        if (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr))
                        {
                            if ((!string.IsNullOrEmpty(IdUsuariostr)) || (!string.IsNullOrEmpty(IdCCFstr)))
                            {
                                where += " and ";
                            }
                            where += "FechaConsulta BETWEEN '" + FechaInicialstr + "' AND '" + FechaFinalstr + "'";
                        }
                        else
                            esIgual = false;

                    }else
                        esIgual = false;
                    list = (int)input["draw"] == 1 ? new List<LTLogConsultasProcesadas>() : objLTLogConsultasAni.PaginadorConsultasProceadas((int)input["start"], (int)input["length"], where, esIgual).ToList();

                    #region CSVFile

                    var download = Convert.ToBoolean(input["download"]);
                    if (download)
                    {
                        System.Configuration.AppSettingsReader settingsReader =
                                            new AppSettingsReader();

                        StringBuilder sb = new StringBuilder();
                        if (esIgual)
                            sb.AppendLine("Hora, Cantidad");
                        else
                            sb.AppendLine("Fecha, Cantidad");

                        var datosExport = objLTLogConsultasAni.PaginadorConsultasProceadas(0, (int)settingsReader.GetValue("CountExportCSV", typeof(int)), where, esIgual).ToList();

                        foreach (var item in datosExport)
                        {
                            if (esIgual)
                                sb.AppendLine(
                                item.Lapso+ "," +
                                item.Cantidad);
                        else
                                sb.AppendLine(
                                item.Fecha + "," +
                                item.Cantidad);
                        }
                        dataTableData.DownloadStr = sb.ToString();
                    }
                    #endregion

                    dataTableData.draw = (int)input["draw"];
                    dataTableData.recordsFiltered = (int)input["draw"] == 1 ? 0 : objLTLogConsultasAni.CountPaginadorConsultasProcesadas(where, esIgual);

                    dataTableData.data = list;
                    dataTableData.recordsTotal = dataTableData.recordsFiltered;

                    return Json(dataTableData);

                }
            }
        }


        public class DataTableDataLogConsultasTiempoRespuesta
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<LTLogConsultasAni> data { get; set; }
            public string DownloadStr { get; set; }
        }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public IHttpActionResult AjaxGetJsonDataLTLogConsultasTiempoRes(object parameters)
        {
            using (BusinessBase<LTLogConsultasAni> objLTLogConsultasAni = new BusinessBase<LTLogConsultasAni>())
            {
                using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                {
                    using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
                    {
                        using (BusinessBase<RRptaAsocajas> objRRptaAsocajas = new BusinessBase<RRptaAsocajas>())
                        {
                            using (BusinessBase<ROrigen> objROrigen = new BusinessBase<ROrigen>())
                            {
                                DataTableDataLogConsultasTiempoRespuesta dataTableData = new DataTableDataLogConsultasTiempoRespuesta();
                                var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);
                                JsonDeserializer js = new JsonDeserializer(input["search"]["value"].ToString());
                                List<LTLogConsultasAni> list = new List<LTLogConsultasAni>();
                                //var timestamp = Number(Request.QueryString('ts'));

                                var Duracionstr = js.GetString("search.Duracion");
                                var FechaInicialstr = js.GetString("search.FechaInicial");
                                var FechaFinalstr = js.GetString("search.FechaFinal");

                                DateTime? FechaInicial = !string.IsNullOrEmpty(FechaInicialstr) ? (DateTime?)Convert.ToDateTime(FechaInicialstr) : null;
                                DateTime? FechaFinal = !string.IsNullOrEmpty(FechaFinalstr) ? (DateTime?)Convert.ToDateTime(FechaFinalstr).AddDays(1) : null;

                                FechaFinalstr = !string.IsNullOrEmpty(FechaFinalstr) ? Convert.ToDateTime(FechaFinal).Year.ToString() + "-" + Convert.ToDateTime(FechaFinal).Month.ToString() + "-" + Convert.ToDateTime(FechaFinal).Day.ToString() : "";
                                string where = "";
                                if ((!string.IsNullOrEmpty(Duracionstr)) || (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr)))
                                {
                                    where += "where ";
                                    if (!string.IsNullOrEmpty(Duracionstr))
                                    {
                                        where += "Duracion >= " + Duracionstr;
                                    }
                                   
                                    if (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr))
                                    {
                                        if ((!string.IsNullOrEmpty(Duracionstr)))
                                        {
                                            where += " and ";
                                        }
                                        where += "FechaConsulta BETWEEN '" + FechaInicialstr + "' AND '" + FechaFinalstr + "'";
                                    }
                                }
                                list = (int)input["draw"] == 1 ? new List<LTLogConsultasAni>() : objLTLogConsultasAni.PaginadorConsultas((int)input["start"], (int)input["length"], where).ToList();

                                #region CSVFile

                                var download = Convert.ToBoolean(input["download"]);
                                if (download)
                                {
                                    System.Configuration.AppSettingsReader settingsReader =
                                                        new AppSettingsReader();

                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendLine("Duracion, CCF, Usuario, Id., Vía, Estado, Fecha Inicia, Fecha Fin, Id Rnec, Desc Rnec");
                                    var datosExport = objLTLogConsultasAni.PaginadorConsultas(0, (int)settingsReader.GetValue("CountExportCSV", typeof(int)), where).ToList();
                                    var allobjRUsuario = objRUsuario.Get().ToList();
                                    var allobjRCCF = objRCCF.Get().ToList();
                                    var allobjROrigen = objROrigen.Get().ToList();
                                    var allobjRRptaAsocajas = objRRptaAsocajas.Get().ToList();
                                    foreach (var item in datosExport)
                                    {
                                        item.RUsuario = allobjRUsuario.Where(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                        item.RUsuario.RCCF = allobjRCCF.Where(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                                        item.ROrigen = allobjROrigen.Where(o => o.IdOrigen == item.IdOrigen).FirstOrDefault();
                                        var rRptaAsocajas = objRRptaAsocajas.Get(o => o.IdRptaAsocajas == item.IdRptaAsocajas).ToList();
                                        item.RRptaAsocajas = rRptaAsocajas.Count() > 0 ? rRptaAsocajas.FirstOrDefault() : new RRptaAsocajas();
                                        sb.AppendLine(
                                            item.Duracion + "," +
                                            item.RUsuario.RCCF.Nombre + "," +
                                            item.RUsuario.Nombres + "," +
                                            item.IdConsulta + "," +
                                            item.ROrigen.OrigenConsulta + "," +
                                            item.RRptaAsocajas.RptaAsocajas + "," +
                                            item.FechaInicia + "," +
                                            item.FechaFin + "," +
                                            item.ControlRNEC + "," +
                                            item.DescrRNEC);
                                    }
                                    dataTableData.DownloadStr = sb.ToString();
                                }

                                #endregion

                                dataTableData.draw = (int)input["draw"];
                                dataTableData.recordsFiltered = (int)input["draw"] == 1 ? 0 : objLTLogConsultasAni.CountPaginadorConsultas(where);
                                foreach (var item in list)
                                {
                                    item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                    item.RUsuario.RCCF = objRCCF.Get(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                                    item.ROrigen = objROrigen.Get(o => o.IdOrigen == item.IdOrigen).FirstOrDefault();
                                    var rRptaAsocajas = objRRptaAsocajas.Get(o => o.IdRptaAsocajas == item.IdRptaAsocajas).ToList();
                                    item.RRptaAsocajas = rRptaAsocajas.Count() > 0 ? rRptaAsocajas.FirstOrDefault() : new RRptaAsocajas();
                                }

                                dataTableData.data = list;
                                dataTableData.recordsTotal = dataTableData.recordsFiltered;

                                return Json(dataTableData);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region LTLogApp
        public class DataTableDataLTLogApp
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<LTLogApp> data { get; set; }
            public string DownloadStr { get; set; }
        }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public IHttpActionResult AjaxGetJsonDataLTLogApp(object parameters)
        {
            using (BusinessBase<LTLogApp> objLTLogApp = new BusinessBase<LTLogApp>())
            {

                DataTableDataLTLogApp dataTableData = new DataTableDataLTLogApp();
                var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);
                JsonDeserializer js = new JsonDeserializer(input["search"]["value"].ToString());
                List<LTLogApp> list = new List<LTLogApp>();
                //var timestamp = Number(Request.QueryString('ts'));

                var FechaInicialstr = js.GetString("search.FechaInicial");
                var FechaFinalstr = js.GetString("search.FechaFinal");

                DateTime? FechaInicial = !string.IsNullOrEmpty(FechaInicialstr) ? (DateTime?)Convert.ToDateTime(FechaInicialstr) : null;
                DateTime? FechaFinal = !string.IsNullOrEmpty(FechaFinalstr) ? (DateTime?)Convert.ToDateTime(FechaFinalstr).AddDays(1) : null;

                FechaFinalstr = !string.IsNullOrEmpty(FechaFinalstr) ? Convert.ToDateTime(FechaFinal).Year.ToString() + "-" + Convert.ToDateTime(FechaFinal).Month.ToString() + "-" + Convert.ToDateTime(FechaFinal).Day.ToString() : "";
                string where = "";
                if (!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr))
                {
                    where += "where CreationDate BETWEEN '" + FechaInicialstr + "' AND '" + FechaFinalstr + "'";
                }
                list = (int)input["draw"] == 1 ? new List<LTLogApp>() : objLTLogApp.PaginadorConsultas((int)input["start"], (int)input["length"], where).ToList();

                #region CSVFile

                var download = Convert.ToBoolean(input["download"]);
                if (download)
                {
                    System.Configuration.AppSettingsReader settingsReader =
                                        new AppSettingsReader();

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("ID, FECHA, CLASE, METODO, DETALLE");
                    var datosExport = objLTLogApp.PaginadorConsultas(0, (int)settingsReader.GetValue("CountExportCSV", typeof(int)), where).ToList();
                    foreach (var item in datosExport)
                    {
                        sb.AppendLine(
                            item.IdLogApp + "," +
                            item.CreationDate + "," +
                            item.WebServiceName + "," +
                            item.MethodNameUI + "," +
                            item.Data);
                    }
                    dataTableData.DownloadStr = sb.ToString();
                }

                #endregion

                dataTableData.draw = (int)input["draw"];
                dataTableData.recordsFiltered = (int)input["draw"] == 1 ? 0 : objLTLogApp.CountPaginadorConsultas(where);


                dataTableData.data = list;
                dataTableData.recordsTotal = dataTableData.recordsFiltered;

                return Json(dataTableData);
            }

        }
        #endregion

        #region Login

        public IHttpActionResult Login(string UserData)
        {
            results jsonData = new results();
            try
            {


                //HttpContextBase context = null;
                //var httpContext = context as HttpContextBase;
                //HttpContext.Current.Session["username"] = "sa";
                HelperGeneral.User = UserData;
                jsonData.Message = UserData;
                jsonData.Ok = true;
            }
            catch (Exception ex)
            {
                jsonData.Message = ex.ToString();
                jsonData.Ok = false;
            }
            return Ok(jsonData);
        }
        #endregion

        #region ConsultasWSDL
        public IHttpActionResult GetCedula(string Cedula)
        {
            var today = DateTime.Now;
            System.Configuration.AppSettingsReader settingsReader =
                                new AppSettingsReader();
            using (ServiceSoapClient consultaCedulasPrueba = new ServiceSoapClient())
            {

                ConsultaCedulasPrueba.UserAuth UserAuth = new ConsultaCedulasPrueba.UserAuth();
                UserAuth.usuario = (string)settingsReader.GetValue("UserSoap", typeof(string));
                UserAuth.contrasena = (string)settingsReader.GetValue("PassSoap", typeof(string));
                UserAuth.ip = Utility.GetServerIP();
                var retornaC = consultaCedulasPrueba.consultarCedulas(UserAuth, Cedula);
                var datereturnSOAP = DateTime.Now;
                PutLTLogConsultasAni(today, datereturnSOAP, Cedula, retornaC, ((int)Origen.INDIVIDUAL).ToString());
                //int resultadoFechas = DateTime.Compare(today, datereturnSOAP);
                return Json(retornaC);
            }
        }


        public IHttpActionResult PostListCedulas(ConsultaDocumentos Cedulas)
        {
            System.Configuration.AppSettingsReader settingsReader =
                                new AppSettingsReader();
            using (ServiceSoapClient consultaCedulasPrueba = new ServiceSoapClient())
            {
                List<Usuario> RetornaCedulas = new List<Usuario>();
                ConsultaCedulasPrueba.UserAuth UserAuth = new ConsultaCedulasPrueba.UserAuth();
                UserAuth.usuario = (string)settingsReader.GetValue("UserSoap", typeof(string));
                UserAuth.contrasena = (string)settingsReader.GetValue("PassSoap", typeof(string));
                UserAuth.ip = Utility.GetServerIP();

                foreach (var Cedula in Cedulas.Cedulas)
                {
                    var today = DateTime.Now;
                    var retornaC = consultaCedulasPrueba.consultarCedulas(UserAuth, Cedula);
                    RetornaCedulas.Add(retornaC);
                    var datereturnSOAP = DateTime.Now;
                    PutLTLogConsultasAni(today, datereturnSOAP, Cedula, retornaC, ((int)Origen.MASIVO).ToString());
                }
                return Json(RetornaCedulas);
            }
        }

        public void PutLTLogConsultasAni(DateTime today, DateTime datereturnSOAP, string Cedula, Usuario consultaCedulasPrueba, string Origen)
        {
            using (BusinessBase<LTLogConsultasAni> objLTLogConsultasAni = new BusinessBase<LTLogConsultasAni>())
            {

                using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                {
                    using (BusinessBase<RRptaAsocajas> objRRptaAsocajas = new BusinessBase<RRptaAsocajas>())
                    {

                        var localIP = Utility.GetServerIP();
                        var sMacAddress = Utility.GetMacMachine();
                        var idUsu = HelperGeneral.GetSession();
                        var ResRnec="000";
                        var RusuarioData = objRUsuario.Get(o => o.Usuario == idUsu).FirstOrDefault();
                        var RRptaAsocajasData = objRRptaAsocajas.Get(o => o.IdRptaAsocajas == ResRnec).FirstOrDefault();
                        TimeSpan diferenciaFecha = datereturnSOAP - today;
                        var Duracion = diferenciaFecha.Milliseconds;
                        LTLogConsultasAni lTLogConsultasAni = new LTLogConsultasAni();
                        lTLogConsultasAni.FechaConsulta = today;
                        lTLogConsultasAni.Nuip = Cedula;
                        lTLogConsultasAni.IdOrigen = Origen; //((int)Origen.INDIVIDUAL).ToString();
                        lTLogConsultasAni.Mac = sMacAddress;
                        lTLogConsultasAni.Ip = localIP;
                        lTLogConsultasAni.FechaInicia = today;
                        lTLogConsultasAni.FechaFin = datereturnSOAP;
                        lTLogConsultasAni.Duracion = Duracion;
                        lTLogConsultasAni.IdCcf = RusuarioData.IdCcf;
                        lTLogConsultasAni.IdUsuario = RusuarioData.IdUsuario;
                        lTLogConsultasAni.IdRptaRnec = consultaCedulasPrueba.estadoConsulta.codError;
                        lTLogConsultasAni.IdRptaAsocajas = RRptaAsocajasData.IdRptaAsocajas;
                        lTLogConsultasAni.ControlRNEC = consultaCedulasPrueba.estadoConsulta.numeroControl;
                        lTLogConsultasAni.DescrRNEC = consultaCedulasPrueba.estadoConsulta.descripcionError;
                        lTLogConsultasAni.CdgoRNEC = consultaCedulasPrueba.datosCedulas.codError;
                        objLTLogConsultasAni.Add(lTLogConsultasAni);
                    }
                }



            }
        }
        #endregion
    }
}