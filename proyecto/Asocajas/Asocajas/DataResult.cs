using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asocajas
{
    [Serializable]
    public class DataResult
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object UFormularioSeccion { get; set; }
    }
}