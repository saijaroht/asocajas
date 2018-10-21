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
        private const int TOTAL_ROWS = 995;
        //private static readonly List<DataItem> _data = CreateData();

        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<LTLogEventos> data { get; set; }
        }
        public static int TotalRows { get; set; }

        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public IHttpActionResult AjaxGetJsonDataLTLogEventos(object parameters)
        {
            using (BusinessBase<LTLogEventos> objLTLogEventos = new BusinessBase<LTLogEventos>())
            {
                using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                {
                    using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
                    {
                        DataTableData dataTableData = new DataTableData();
                        var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);
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
                        if (TotalRows == 0)
                        {
                            TotalRows = objLTLogEventos.Get().Count();
                        }
                        dataTableData.recordsFiltered = TotalRows;
                        dataTableData.recordsTotal = dataTableData.recordsFiltered;

                        return Json(dataTableData);
                    }
                }
            }
        }
    }
}