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
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Asocajas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : ApiController
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
                                    where += "FechaEvento BETWEEN '" + FechaInicialstr + "' AND '" + FechaFinalstr + "'";
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
                                sb.AppendLine("CCF, Usuario, Evento, FechaEvento\n");
                                var datosExport = objLTLogConsultasAni.PaginadorConsultas(0, (int)settingsReader.GetValue("CountExportCSV", typeof(int)), where).ToList();
                                var allobjRUsuario = objRUsuario.Get().ToList();
                                var allobjRCCF = objRCCF.Get().ToList();
                                var allobjROrigen = objROrigen.Get().ToList();
                                foreach (var item in datosExport)
                                {
                                    item.RUsuario = allobjRUsuario.Where(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                    item.RUsuario.RCCF = allobjRCCF.Where(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                                    item.ROrigen = allobjROrigen.Where(o => o.IdOrigen == item.IdOrigen).FirstOrDefault();
                                    sb.AppendLine(item.RUsuario.RCCF.Nombre + "," + item.RUsuario.Nombres + "," + item.IdConsulta + "," + item.ROrigen.OrigenConsulta);
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
                            dataTableData.recordsFiltered = (int)input["draw"] == 1 ? 0 : objLTLogConsultasAni.CountPaginadorConsultas(where);
                            foreach (var item in list)
                            {
                                item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                item.RUsuario.RCCF = objRCCF.Get(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                                item.ROrigen = objROrigen.Get(o => o.IdOrigen == item.IdOrigen).FirstOrDefault();
                            }

                            dataTableData.data = list;
                            dataTableData.recordsTotal = dataTableData.recordsFiltered;

                            return Json(dataTableData);
                        }
                    }
                }
            }
        }
        #endregion

        //#region LTLogConsultasAni
        //public class DataTableDataLogConsultasAni
        //{
        //    public int draw { get; set; }
        //    public int recordsTotal { get; set; }
        //    public int recordsFiltered { get; set; }
        //    public List<LTLogConsultasAni> data { get; set; }
        //}
        //public static int TotalRowsConsultasAni { get; set; }

        //// this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        //public IHttpActionResult AjaxGetJsonDataLTLogConsultasAni(object parameters)
        //{
        //    using (BusinessBase<LTLogConsultasAni> objLTLogConsultasAni = new BusinessBase<LTLogConsultasAni>())
        //    {
        //        using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
        //        {
        //            using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
        //            {
        //                DataTableDataLogConsultasAni dataTableData = new DataTableDataLogConsultasAni();
        //                var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);
        //                JsonDeserializer js = new JsonDeserializer(input["search"]["value"].ToString());
        //                dataTableData.draw = (int)input["draw"];
        //                List<LTLogConsultasAni> list = new List<LTLogConsultasAni>();
        //                list = objLTLogConsultasAni.PaginadorConsultas((int)input["start"], (int)input["length"], "").ToList();

        //                foreach (var item in list)
        //                {
        //                    item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
        //                    item.RUsuario.RCCF = objRCCF.Get(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
        //                }

        //                dataTableData.data = list;
        //                if (TotalRowsConsultasAni == 0)
        //                {
        //                    TotalRowsConsultasAni = objLTLogConsultasAni.Get().Count();
        //                }
        //                dataTableData.recordsFiltered = TotalRowsConsultasAni;
        //                dataTableData.recordsTotal = dataTableData.recordsFiltered;

        //                return Json(dataTableData);
        //            }
        //        }
        //    }
        //}
        //#endregion

        #region Descarga Documentos

        //public IHttpActionResult GetFile123()
        //{
        //    using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        IEnumerable<string> columnNames = objRUsuario.Get()
        //                .Select(property => property.FechaCreacion);

        //        sb.AppendLine(string.Join(",", columnNames));
                

        //        //foreach (var row in objLTLogEventos.Get().ToList())
        //        //{
        //        IEnumerable<string> fields = objRUsuario.Get().Select(field => field.ToString());
        //        sb.AppendLine(string.Join(",", fields));
        //        //}
        //        return Json(sb);
        //    }
        //    //string filename = "C:\\ArchivosPlanos\\prueba1.csv";
        //    ////int index = 1;
        //    ////int process = 5;

        //    //using (StreamWriter sw = new StreamWriter(new FileStream(filename, FileMode.Create), Encoding.UTF8))
        //    //{
        //    //    StringBuilder sb = new StringBuilder();
        //    //    sb.AppendLine("titulo1, Titulo2, Titulo3");
        //    //    sb.AppendLine("parametro11, parametro21, parametro31");
        //    //    sb.AppendLine("parametro12, parametro22, parametro32");
        //    //    sb.AppendLine("parametro13, parametro23, parametro33");
        //    //    sb.AppendLine("parametro14, parametro24, parametro34");


        //    //    sw.Write(sb.ToString());

        //    //    return Json(sw);
        //    //}
        //}
        #endregion
    }
}