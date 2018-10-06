using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Asocajas;
using Supertype;

namespace Asocajas.Controllers
{
    public class BaseController<TEntity> : ApiController where TEntity : EntityBase
    {
        protected BusinessBase<TEntity> objDb = null;
        public BaseController()
        {
            objDb = new BusinessBase<TEntity>();

        }


        protected virtual Int64 GetPkValue(TEntity obj)
        {
            var prop = typeof(TEntity).GetProperty(obj.PkName);
            var value = Convert.ToInt64(prop.GetValue(obj));
            return value;
        }
        // GET api/Entidad
        public virtual IQueryable<TEntity> Get()
        {
            return objDb.Get().AsQueryable();
        }

        // GET api/Entidad/5
        //[ResponseType(typeof(TEntity))]
        public virtual IHttpActionResult Get(long id)
        {
            TEntity obj = objDb.Get(id);
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        // PUT api/Entidad/5
        public virtual IHttpActionResult Put(long id, TEntity obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != GetPkValue(obj))
            {
                return BadRequest();
            }

            //db.Entry(obj).State = EntityState.Modified;

            try
            {
                this.objDb.Update(obj);
                //db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Entidad
        //[ResponseType(typeof(TEntity))]
        public virtual IHttpActionResult Post(TEntity obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.obj.Add(obj);
            //db.SaveChanges();
            objDb.Add(obj);

            return CreatedAtRoute("DefaultApi", new { id = this.GetPkValue(obj) }, obj);
        }

        public virtual IHttpActionResult PostPut(TEntity obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.obj.Add(obj);
            //db.SaveChanges();
            objDb.AddUpdate(obj, obj.PkName);

            return CreatedAtRoute("DefaultApi", new { id = this.GetPkValue(obj) }, obj);
        }

        // DELETE api/Entidad/5
        // [ResponseType(typeof(TEntity))]
        public virtual IHttpActionResult Delete(long id)
        {

            objDb.Delete(id);
            //obj obj = db.obj.Find(id);
            //if (obj == null)
            //{
            //    return NotFound();
            //}

            //db.obj.Remove(obj);
            //db.SaveChanges();

            return Ok(); //Ok(obj);
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                if (this.objDb != null)
                {
                    this.objDb.Dispose();
                    this.objDb = null;
                }
                //this.o.Dispose();
            }
            base.Dispose(disposing);
        }

        protected virtual bool Exists(long id)
        {
            return (this.objDb.Get(id) != null);
            //return db.obj.Count(e => e.IdEntidad == id) > 0;
        }
    }
}