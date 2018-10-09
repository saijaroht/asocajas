using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asocajas.Common.Supertype
{
    interface ICamposAuditoria
    {

        System.DateTime FechaCreacion { get; set; }
        string UsuarioCreacion { get; set; }
        string MaquinaCreacion { get; set; }
        Nullable<System.DateTime> FechaActualizacion { get; set; }
        string UsuarioActualizacion { get; set; }
        string MaquinaActualizacion { get; set; }
        bool Activo { get; set; }
    }
}
