using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Asocajas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LTLogEventosController : BaseController<LTLogEventos>
    {
        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<LTLogEventos> data { get; set; }
        }

        public IHttpActionResult GetLTLogEventos(object parameters)
        {
            DataTableData dataTableData = new DataTableData();
            var input = JObject.FromObject(JObject.FromObject(parameters)["parameters"]);

            dataTableData.draw = (int)input["draw"];

            //list = HelperGeneral.PaginadorConsultasLTLogEventos((int)input["start"], (int)input["length"]);

            dataTableData.data = this.objDb.PaginadorConsultas((int)input["start"], (int)input["length"], "").ToList();
            dataTableData.recordsFiltered = this.objDb.Get().Count();
            dataTableData.recordsTotal = dataTableData.recordsFiltered;

            return Json(dataTableData);
        }

        public IHttpActionResult PostLTLogEventos(LTLogEventos lTLogEventos)
        {
            var obj = this.objDb.Add(lTLogEventos);
            return CreatedAtRoute("DefaultApi", new { id = lTLogEventos.IdLogEvento }, lTLogEventos);
        }   
    }
}