﻿using System;
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
                        foreach (var item in obj)
                        {
                            item.RCCF = objRCCF.Get(o => o.IdCcf == item.IdCcf).FirstOrDefault();
                            item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                        }
                    }
                }
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }
        public IHttpActionResult GetLTLogConsultasAniCcf(int idCcf)
        {
            try
            {
                var obj = this.objDb.Get().Where(o => o.IdCcf == idCcf).ToList();

                using (BusinessBase<RCCF> objRCCF = new BusinessBase<RCCF>())
                {
                    using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                    {
                        foreach (var item in obj)
                        {
                            item.RCCF = objRCCF.Get(o => o.IdCcf == item.IdCcf).FirstOrDefault();
                            item.RUsuario = objRUsuario.Get(o => o.IdUsuario == item.IdUsuario).FirstOrDefault();
                        }
                    }
                }
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }

        public IHttpActionResult PostLTLogConsultasAni(LTLogConsultasAni lTLogConsultasAni)
        {
            var obj = this.objDb.Add(lTLogConsultasAni);
            return CreatedAtRoute("DefaultApi", new { id = lTLogConsultasAni.IdConsulta }, lTLogConsultasAni);
        }
    }
}