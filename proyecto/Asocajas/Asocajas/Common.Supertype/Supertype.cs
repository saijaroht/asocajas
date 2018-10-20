using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Supertype.Extensions;
using Asocajas.Common.Supertype;
using Asocajas.Utilities;
using Asocajas;
using System.Data.SqlClient;

namespace Supertype
{
    public enum CrudActions
    {
        None = 0,
        Insert,
        UpdateStatus,
        Update,
        Delete,
    }
    /// <summary>
    /// Layer Supertype Pattern
    /// </summary>
    public class SuperType<TEntity> : IDisposable where TEntity : class
    {

        /// <summary>
        /// 
        /// </summary>
        public string MachineInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected DbContext context = null;

        DbSet<TEntity> objectSet = null;

        //protected CurrentContextInfo currentContextInfo = null;
        /// <summary>
        /// 
        /// </summary>
        public DbSet<TEntity> ObjectSet
        {
            get { return objectSet; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool SetAduditFields { get; set; }
        /// <summary>
        /// Creates Context
        /// </summary>
        public DbContext Context
        {
            get
            {
                return context;
            }
            set
            {
                CreateContext(value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="setAduditFields"></param>
        protected virtual void CreateContext(DbContext context,
          bool setAduditFields = true)
        {
            this.SetAduditFields = setAduditFields;
            this.context = context;
            objectSet = context.Set<TEntity>();
            //this.currentContextInfo = currentContextInfo;
            //if (this.currentContextInfo == null)
            //    this.currentContextInfo = UtilityLayer.CurrentContextInfo.CurrentContextGeneralInfo;
        }
        /// <summary>
        /// Instances a SuperType object 
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="currentContextInfo">currentContextInfo</param>
        /// <param name="setAduditFields">if set Audit Fields</param>
        public SuperType(DbContext context,
            bool setAduditFields = true)
        {
            CreateContext(context);
        }

        /// <summary>
        /// Instances a SuperType object 
        /// </summary>
        public SuperType()
        {

        }

        /// <summary>
        /// Adds a record
        /// </summary>
        /// <param name="entity">Domain entity</param>
        public virtual TEntity Add(TEntity entity)
        {
            if (this.SetAduditFields)
                SetAuditingValues(entity, CrudActions.Insert);
            objectSet.Add(entity);
            context.SaveChanges();
            return entity;
        }


        /// <summary>
        /// Updates a record
        /// </summary>
        /// <param name="entity">Data entity</param>
        public virtual TEntity Update(TEntity entity, string user = null, bool find = true)
        {
            if (this.SetAduditFields)
                SetAuditingValues(entity, CrudActions.Update);

            if (find)
            {
                TEntity _entity = this.Find((entity as EntityBase).PkValue);
                //context.Entry<TEntity>(_entity).State = EntityState.Modified;
                //(entity as ICamposAuditoria).FechaCreacion = (_entity as ICamposAuditoria).FechaCreacion;
                //(entity as ICamposAuditoria).UsuarioCreacion = (_entity as ICamposAuditoria).UsuarioCreacion;
                //(entity as ICamposAuditoria).MaquinaCreacion = (_entity as ICamposAuditoria).MaquinaCreacion;
            }

            context.Entry<TEntity>(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;

        }

        /// <summary>
        /// add or Updates a record
        /// </summary>
        /// <param name="entity">data entity</param>
        /// <param name="pkName">pk property name</param>
        /// <param name="withFind">withFind</param>
        /// <returns></returns>
        public virtual TEntity AddUpdate(TEntity entity, string pkName, bool withFind = false)
        {
            var propPk = typeof(TEntity).GetProperty(pkName);
            if (withFind)
            {
                TEntity entityTemp = this.Get(Convert.ToInt64(propPk.GetValue(entity)));
                if (entityTemp == default(TEntity))
                    Add(entity);
                else
                {
                    context.Entry<TEntity>(entityTemp).State = EntityState.Detached;
                    //Update(entityTemp);
                    Update(entity);
                }
            }
            else
            {
                if (Convert.ToInt64(propPk.GetValue(entity)) == 0)
                    Add(entity);
                else
                    Update(entity);
            }
            return entity;

        }


        /// <summary>
        /// Deletes a records
        /// </summary>
        /// <param name="entity">Data entity</param>
        public virtual void Delete(TEntity entity)
        {
            context.Entry<TEntity>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }
        /// <summary>
        ///  Deletes a records
        /// </summary>
        /// <param name="id">id Register</param>
        public virtual void Delete(Int64 id)
        {
            var entity = Get(id);
            Delete(entity);
        }
        /// <summary>
        /// Gets a Domain entity by the specified id
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>Domain Entity</returns>
        public virtual TEntity Get(long id)
        {
            return objectSet.Find(id);
        }

        /// <summary>
        /// Gets an Enumerable Domain entities
        /// </summary>
        /// <returns>Domain entities</returns>
        public virtual IEnumerable<TEntity> Get()
        {
            IEnumerable<TEntity> list = new List<TEntity>();
            var query = from item in context.Set<TEntity>()
                        select item;

            return query;
        }

        /// <summary>
        /// Gets an Enumerable Domain entities by using the specified predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>Enumerable Domain entities</returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return this.Get();
            else
            {
                var query = from item in context.Set<TEntity>().Where<TEntity>(predicate)
                            select item;

                return query;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(int skip, int take,
            Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, Int64>> orderBy = null, bool isDesc = true)
        {
            IQueryable<TEntity> query = null;
            if (predicate == null)
            {
                if (orderBy == null)
                    query = from item in context.Set<TEntity>().Skip(skip).Take(take)
                            select item;
                else
                {
                    if (isDesc)
                        query = from item in context.Set<TEntity>().OrderByDescending(orderBy).Skip(skip).Take(take)
                                select item;
                    else
                        query = from item in context.Set<TEntity>().OrderBy(orderBy).Skip(skip).Take(take)
                                select item;
                }
            }
            else
            {
                if (orderBy == null)
                    query = from item in context.Set<TEntity>().Where(predicate).Skip(skip).Take(take)
                            select item;
                else
                {
                    if (isDesc)
                        query = from item in context.Set<TEntity>().Where(predicate).OrderByDescending(orderBy).Skip(skip).Take(take)
                                select item;
                    else
                        query = from item in context.Set<TEntity>().Where(predicate).OrderBy(orderBy).Skip(skip).Take(take)
                                select item;
                }
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(int skip, int take,
          string predicate = null, string orderBy = null)
        {
            IQueryable<TEntity> query = null;
            if (string.IsNullOrEmpty(predicate))
            {
                if (string.IsNullOrEmpty(orderBy))
                    query = from item in context.Set<TEntity>().Skip(skip).Take(take)
                            select item;
                else
                    query = from item in context.Set<TEntity>().OrderBy(orderBy).Skip(skip).Take(take)
                            select item;
            }
            else
            {
                if (orderBy == null)
                    query = from item in context.Set<TEntity>().Where(predicate).Skip(skip).Take(take)
                            select item;
                else
                    query = from item in context.Set<TEntity>().Where(predicate).OrderBy(orderBy).Skip(skip).Take(take)
                            select item;

            }

            return query;
        }
        #region Dispose
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

                if (this.context != null)
                {
                    this.context.Dispose();
                    this.context = null;
                }


                if (this.objectSet != null)
                {
                    this.objectSet = null;
                }

                //if (this.Entidad != null)
                //{
                //    GC.SuppressFinalize(this.Entidad);
                //    this.Entidad = null;
                //}


            }
        }

        #endregion

        /// <summary>
        /// Metodo paara mapear valor a una propiedad
        /// </summary>
        /// <param name="entity">entidad</param>
        /// <param name="propertyName">nombre propiedad</param>
        /// <param name="valueToSet">valor</param>
        /// <returns></returns>
        public virtual TEntity SetValue(TEntity entity, string propertyName, object valueToSet)
        {
            Type type = typeof(TEntity);
            if (type.Name.ToLower() == "object")
                type = entity.GetType();
            PropertyInfo currentProperty = type.GetProperty(propertyName);
            if (currentProperty != null)
            {
                Type ptype = Nullable.GetUnderlyingType(currentProperty.PropertyType) ?? currentProperty.PropertyType;
                if (valueToSet != null)
                    currentProperty.SetValue(entity, Convert.ChangeType(valueToSet, ptype), null);
            }
            return entity;
        }

        /// <summary>
        /// Metodo para asignar campos de auditoria
        /// </summary>
        /// <param name="entity">entidad</param>
        /// <param name="crudAction">Accion de Crud a ejecutar</param>
        /// <returns></returns>
        public virtual TEntity SetAuditingValues(TEntity entity, CrudActions crudAction, string user = null)
        {
            Type type = null;
            if (typeof(TEntity).Name != "Object")
                type = typeof(TEntity);
            else
                type = entity.GetType();

            switch (crudAction)
            {
                case CrudActions.Insert:
                    if (entity is ICamposAuditoria)
                    {
                        (entity as ICamposAuditoria).FechaCreacion = DateTime.Now;
                        (entity as ICamposAuditoria).MaquinaCreacion = Utility.GetUserMachineInfo(MachineInfo);
                        if (!string.IsNullOrEmpty(user))
                            (entity as ICamposAuditoria).UsuarioCreacion = user;
                        else
                            (entity as ICamposAuditoria).UsuarioCreacion = Utility.GetUserMachineInfo(MachineInfo, true);
                    }
                    //SetValue(entity, "FechaCreacion", DateTime.Now);
                    //SetValue(entity, "UsuarioCreacion", currentContextInfo.IdUser + "|" + currentContextInfo.User);
                    //SetValue(entity, "MaquinaCreacion", System.Web.HttpContext.Current.Request.GetUserMachineInfo());
                    break;
                case CrudActions.Update:
                    if (entity is ICamposAuditoria)
                    {
                        (entity as ICamposAuditoria).FechaActualizacion = DateTime.Now;
                        (entity as ICamposAuditoria).MaquinaActualizacion = Utility.GetUserMachineInfo(MachineInfo);
                        if (!string.IsNullOrEmpty(user))
                            (entity as ICamposAuditoria).UsuarioActualizacion = user;
                        else
                            (entity as ICamposAuditoria).UsuarioActualizacion = Utility.GetUserMachineInfo(MachineInfo, true);

                    }
                    //SetValue(entity, "FechaActualizacion", DateTime.Now);
                    ////SetValue(entity, "UsuarioActualizacion", currentContextInfo.IdUser + "|" + currentContextInfo.User);
                    //SetValue(entity, "MaquinaActualizacion", System.Web.HttpContext.Current.Request.GetUserMachineInfo());
                    break;
            }
            //switch (crudAction)
            //{
            //    case CrudActions.Insert:
            //        SetValue(entity, "FechaCreacion", DateTime.Now);
            //        SetValue(entity, "UsuarioCreacion", currentContextInfo.IdUser + "|" + currentContextInfo.User);
            //        SetValue(entity, "MaquinaCreacion", currentContextInfo.UserMachine);
            //        break;
            //    case CrudActions.Update:
            //        SetValue(entity, "FechaActualizacion", DateTime.Now);
            //        SetValue(entity, "UsuarioActualizacion", currentContextInfo.IdUser + "|" + currentContextInfo.User);
            //        SetValue(entity, "MaquinaActualizacion", currentContextInfo.UserMachine);
            //        break;
            //}
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            return this.ObjectSet.Find(keyValues);

        }


        #region paginador

        public virtual IEnumerable<TEntity> PaginadorConsultas(int limiteInferior, int limiteSuperior)
        {
            var propPk = typeof(TEntity);

            IEnumerable<TEntity> exec = new List<TEntity>();
            using (var ctx = new AsocajasBDEntities())
            {
                var LimiteInferior = new SqlParameter
                {
                    ParameterName = "LimiteInferior",
                    Value = limiteInferior
                };

                var LimiteSuperior = new SqlParameter
                {
                    ParameterName = "LimiteSuperior",
                    Value = limiteSuperior
                };

                var Tabla = new SqlParameter
                {
                    ParameterName = "Tabla",
                    Value = propPk.Name
                };

                var IdTable = new SqlParameter
                {
                    ParameterName = "IdTable",
                    Value = "IdUsuario"
                };
                exec = ctx.Database.SqlQuery<TEntity>("exec PaginadorConsultas @LimiteInferior,@LimiteSuperior,@Tabla,@IdTable ", LimiteInferior, LimiteSuperior, Tabla, IdTable).ToList<TEntity>();

                //var EXEC = ctx.INSERTSOLicitud(IdSolicitudAntigua, IdSolicitudNueva);
            }
            return exec;
        }


        #endregion


    }
}