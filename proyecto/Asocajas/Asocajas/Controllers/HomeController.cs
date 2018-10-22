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
                        //var usu =JsonDeserializer(input["search"]["value"]);
                        //var search = JObject.FromObject(input["search"]["value"]);
                        dataTableData.draw = (int)input["draw"];
                        List<LTLogEventos> list = new List<LTLogEventos>();
                        list = objLTLogEventos.PaginadorConsultas((int)input["start"], (int)input["length"]).ToList();

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