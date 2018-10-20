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
    public class HomeController : BaseController<RUsuario>
    {
        private const int TOTAL_ROWS = 995;
        //private static readonly List<DataItem> _data = CreateData();

        public class DataItem
        {
            public string Nombres { get; set; }
            public string Apellido { get; set; }
            public string Usuario { get; set; }
        }

        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<RUsuario> data { get; set; }
        }


        // this ajax function is called by the client for each draw of the information on the page (i.e. when paging, ordering, searching, etc.). 
        public IHttpActionResult AjaxGetJsonData(object parameters)
        {
            DataTableData dataTableData = new DataTableData();
            var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);

            dataTableData.draw = (int)input["draw"];
            dataTableData.recordsTotal = TOTAL_ROWS;

            List<RUsuario> list = new List<RUsuario>();
            list = this.objDb.PaginadorConsultas((int)input["start"], (int)input["length"]).ToList();
            //list = HelperGeneral.PaginadorConsultasLTLogEventos((int)input["start"], (int)input["length"]);

            dataTableData.data = list;
            dataTableData.recordsFiltered = this.objDb.Get().Count();

            return Json(dataTableData);
        }
    }
}