//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asocajas
{
    using System;
    using System.Collections.Generic;
    using Asocajas.Common.Supertype;
    public partial class LTLogEventos : EntityBase
    {
        public long IdLogEvento { get; set; }
        public System.DateTime FechaEvento { get; set; }
        public string Evento { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
    }
}
