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
    public class RMenuController : BaseController<RMenu>
    {
        public IHttpActionResult GetRMenu()
        {
            try
            {
                string Usuario = HelperGeneral.GetSession();
                using (BusinessBase<RUsuario> objRUsuario = new BusinessBase<RUsuario>())
                {
                    var IdRolUsuario = objRUsuario.Get(o => o.Usuario == Usuario).FirstOrDefault().IdRole;
                    var obj = this.objDb.Get(o => o.IdRole == IdRolUsuario).ToList();

                    return Ok(obj);
                }
            }
            catch (Exception ex)
            {
                return Ok(HelperGeneral.exceptionError(ex));
            }
        }
    }
}