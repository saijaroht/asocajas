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
    public class LTLogConsultasAniController : BaseController<LTLogConsultasAni>
    {
        public IHttpActionResult GetLTLogConsultasAni()
        {
            try
            {
                var obj = this.objDb.Get().ToList();
                using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
                {
                    using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                    {
                        using (BusinessBase<RRptaRnec> objRRptaRnec = new BusinessBase<RRptaRnec>())
                        {
                            using (BusinessBase<RRptaAsocajas> objRRptaAsocajas = new BusinessBase<RRptaAsocajas>())
                            {
                                foreach (var item in obj)
                                {
                                    item.RCCF = objRCCF.Get(o => o.IdCcf == item.IdCcf).FirstOrDefault();
                                    item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                                    item.RRptaRnec = objRRptaRnec.Get(o => o.IdRptaRnec == item.IdRptaRnec).FirstOrDefault();
                                    item.RRptaAsocajas = objRRptaAsocajas.Get(o => o.IdRptaAsocajas == item.IdRptaAsocajas).FirstOrDefault();
                                }
                            }
                        }
                    }
                }
                return Ok(obj);
            }
            catch (Exception)
            {
                return Ok(HelperGeneral.exceptionError());
            }
        }

        public IHttpActionResult PostLTLogConsultasAni(LTLogConsultasAni lTLogConsultasAni)
        {
            var obj = this.objDb.Add(lTLogConsultasAni);
            return CreatedAtRoute("DefaultApi", new { id = lTLogConsultasAni.IdConsulta }, lTLogConsultasAni);
        }
    }
}