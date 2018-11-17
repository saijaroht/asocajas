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
    public partial class RUsuario : EntityBase
    {
        public RUsuario()
        {
            this.LTLogEventos = new HashSet<LTLogEventos>();
            this.LTLogEventos1 = new HashSet<LTLogEventos>();
            this.LTLogConsultasAni = new HashSet<LTLogConsultasAni>();
        }
    
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public System.DateTime Vigencia { get; set; }
        public string Estado { get; set; }
        public Nullable<int> IdCcf { get; set; }
        public int IdRole { get; set; }
        public Nullable<int> Intentos { get; set; }
        public bool CambioObligatorio { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public string UsuarioActualizacion { get; set; }
        public string MaquinaActualizacion { get; set; }
        public bool Activo { get; set; }
    
        public virtual ICollection<LTLogEventos> LTLogEventos { get; set; }
        public virtual ICollection<LTLogEventos> LTLogEventos1 { get; set; }
        public virtual RCCF RCCF { get; set; }
        public virtual RRole RRole { get; set; }
        public virtual ICollection<LTLogConsultasAni> LTLogConsultasAni { get; set; }
    }
}
