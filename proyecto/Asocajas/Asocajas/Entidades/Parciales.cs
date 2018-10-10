using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asocajas
{
    public partial class RCCF
    {

        [NotMapped]
        public string DescripcionReferencia
        {
            get;
            set;
        }
    }


    public partial class results
    {
        [NotMapped]
        public bool Ok { get; set; }
        [NotMapped]
        public string Message { get; set; }
        [NotMapped]
        public object Data { get; set; }
        [NotMapped]
        public object UFormularioSeccion { get; set; }
    }
}