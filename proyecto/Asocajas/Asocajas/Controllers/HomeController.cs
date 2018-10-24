using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        }
        public static int TotalRowsLogEventos { get; set; }
        public static List<LTLogEventos> listLTLogEventos { get; set; }
        public static string strSearch { get; set; }

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
                        list = objLTLogEventos.PaginadorConsultas((int)input["start"], (int)input["length"], where).ToList();
                        //list = objLTLogEventos.Get(o => (!string.IsNullOrEmpty(IdUsuariostr) ? o.IdUsuario == IdUsuario : true)
                        //    && (!string.IsNullOrEmpty(IdCCFstr) ? IdsUsersCCF.Contains(o.IdUsuario) : true) &&
                        //    ((!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr)) ? o.FechaEvento >= FechaInicial && o.FechaEvento <= FechaFinal : true)
                        //    ).Skip((int)input["start"]).Take((int)input["length"]).ToList();

                        //if (strSearch != input["search"]["value"].ToString())
                        //{
                        //    TotalRowsLogEventos = objLTLogEventos.Get().Where(o => (!string.IsNullOrEmpty(IdUsuariostr) ? o.IdUsuario == IdUsuario : true)
                        //        && (!string.IsNullOrEmpty(IdCCFstr) ? IdsUsersCCF.Contains(o.IdUsuario) : true) &&
                        //        ((!string.IsNullOrEmpty(FechaInicialstr) && !string.IsNullOrEmpty(FechaFinalstr)) ? o.FechaEvento >= FechaInicial && o.FechaEvento <= FechaFinal : true)
                        //        ).Count();
                        //    strSearch = input["search"]["value"].ToString();

                        //TotalRowsLogEventos = objLTLogEventos.CountPaginadorConsultas(where);
                        //}

                        dataTableData.draw = (int)input["draw"];
                        dataTableData.recordsFiltered = objLTLogEventos.CountPaginadorConsultas(where);
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
        }
        public static int TotalRowsConsultasAni { get; set; }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public IHttpActionResult AjaxGetJsonDataLTLogConsultasAni(object parameters)
        {
            using (BusinessBase<LTLogConsultasAni> objLTLogConsultasAni = new BusinessBase<LTLogConsultasAni>())
            {
                using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                {
                    using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
                    {
                        DataTableDataLogConsultasAni dataTableData = new DataTableDataLogConsultasAni();
                        var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);
                        JsonDeserializer js = new JsonDeserializer(input["search"]["value"].ToString());
                        dataTableData.draw = (int)input["draw"];
                        List<LTLogConsultasAni> list = new List<LTLogConsultasAni>();
                        list = objLTLogConsultasAni.PaginadorConsultas((int)input["start"], (int)input["length"], "").ToList();

                        foreach (var item in list)
                        {
                            item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                            item.RUsuario.RCCF = objRCCF.Get(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                        }

                        dataTableData.data = list;
                        if (TotalRowsConsultasAni == 0)
                        {
                            TotalRowsConsultasAni = objLTLogConsultasAni.Get().Count();
                        }
                        dataTableData.recordsFiltered = TotalRowsConsultasAni;
                        dataTableData.recordsTotal = dataTableData.recordsFiltered;

                        return Json(dataTableData);
                    }
                }
            }
        }
        #endregion
    }
}