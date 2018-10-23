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

                        var IdCCF = js.GetString("search.IdCCF");
                        var IdUsuario = js.GetString("search.IdUsuario");
                        var FechaInicial = js.GetString("search.FechaInicial");
                        var FechaFinal = js.GetString("search.FechaFinal");
                        dataTableData.draw = (int)input["draw"];

                        if (!string.IsNullOrEmpty(IdCCF) || !string.IsNullOrEmpty(IdUsuario) || !string.IsNullOrEmpty(FechaInicial) || !string.IsNullOrEmpty(FechaFinal))
                        {
                            if (strSearch == input["search"]["value"].ToString())
                            {
                                list = listLTLogEventos.Skip((int)input["start"]).Take((int)input["length"]).ToList();
                            }
                            else
                            {
                                strSearch = input["search"]["value"].ToString();
                                
                                list = objLTLogEventos.Get().ToList();
                                if (!string.IsNullOrEmpty(IdUsuario))
                                {
                                    list = list.Where(o => o.IdUsuario == Convert.ToInt32(IdUsuario)).ToList();
                                }
                                else if (!string.IsNullOrEmpty(IdCCF))
                                {
                                    //long[] IdsUsersCCF;
                                    var IdsUsersCCF = objRUsuario.Get(o => o.IdCcf == Convert.ToInt32(IdCCF)).Select(o => Convert.ToInt32(o.IdUsuario)).ToList();
                                    list = list.Where(o => IdsUsersCCF.Contains(o.IdUsuario)).ToList();
                                }
                                if (!string.IsNullOrEmpty(FechaInicial) && !string.IsNullOrEmpty(FechaFinal))
                                {
                                    list = list.Where(o => o.FechaEvento >= Convert.ToDateTime(FechaInicial) && o.FechaEvento <= Convert.ToDateTime(FechaFinal)).ToList();
                                }
                                listLTLogEventos = list;
                                TotalRowsLogEventos = list.Count();
                                list = list.Skip((int)input["start"]).Take((int)input["length"]).ToList();
                            }
                        }
                        else
                        {
                            TotalRowsLogEventos = 0;
                            list = objLTLogEventos.Get().Skip((int)input["start"]).Take((int)input["length"]).ToList();
                        }

                        //list = objLTLogEventos.PaginadorConsultas((int)input["start"], (int)input["length"]).ToList();

                        foreach (var item in list)
                        {
                            item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                            item.RUsuario.RCCF = objRCCF.Get(o => o.IdCcf == item.RUsuario.IdCcf).FirstOrDefault();
                        }
                        //list = HelperGeneral.PaginadorConsultasLTLogEventos((int)input["start"], (int)input["length"]);

                        dataTableData.data = list;
                        if (TotalRowsLogEventos == 0)
                        {
                            TotalRowsLogEventos = objLTLogEventos.Get().Count();
                        }
                        dataTableData.recordsFiltered = TotalRowsLogEventos;
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
                        list = objLTLogConsultasAni.PaginadorConsultas((int)input["start"], (int)input["length"]).ToList();

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