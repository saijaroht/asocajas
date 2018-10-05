using Supertype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asocajas
{
    [Serializable]
    public class BusinessBase<TEntity> : SuperType<TEntity> where TEntity : class
    {

        public BusinessBase(bool proxyCreationEnabled = false, bool lazyLoadingEnabled = false)
        {
            Context = new AsocajasBDEntities();
            Context.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
            Context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }


    }
}