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
        public bool CambioObligatorio { get; set; }
    }

    public partial class CambioPassword
    {
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string Usuario { get; set; }
    }


    public partial class RecuperarPassword
    {
        [NotMapped]
        public string Usuario { get; set; }
    }

    public partial class ActivarBloquear
    {
        [NotMapped]
        public string Estado { get; set; }
        [NotMapped]
        public int IdUsuario { get; set; }
        [NotMapped]
        public string UsuarioLogueado { get; set; }
    }

    public partial class UpdateUser
    {
        [NotMapped]
        public int IdUsuario { get; set; }
        [NotMapped]
        public string Nombre { get; set; }
        [NotMapped]
        public string Apellido { get; set; }
        [NotMapped]
        public System.DateTime Vigencia { get; set; }
        [NotMapped]
        public int IdRole { get; set; }
    }

    public partial class RUsuario
    {

        [NotMapped]
        public string EstadoSTR
        {
            get { return this.TiposDeEstados.ToString(); }
            private set { var temp = value; }

        }
        [NotMapped]
        public Estados TiposDeEstados
        {
            get { return (Estados)Convert.ToInt32(this.Estado); }
            private set { var temp = value; }
        }

        [NotMapped]
        public string Nombres
        {
            get
            {
                return Nombre + " " + Apellido;
            }
            private set { var temp = value; }
        }
        [NotMapped]
        public string UsuarioLogueado { get; set; }
    }

    public partial class LTLogEventos
    {
        [NotMapped]
        public string CCF { get; set; }
    }

    public partial class RMenu
    {
        [NotMapped]
        public List<List<RMenu>> ListMenu { get; set; }
    }

    public partial class LTLogConsultasAni
    {
        [NotMapped]
        public RRptaAsocajas RRptaAsocajas { get; set; }
    }

    public partial class LTLogConsultasProcesadas
    {
        [NotMapped]
        public DateTime Fecha { get; set; }
        [NotMapped]
        public string Lapso { get; set; }
        [NotMapped]
        public int Cantidad { get; set; }
    }

    public partial class ConsultaDocumentos
    {
        [NotMapped]
        public List<string> Cedulas { get; set; }
    }

    #region DataTables
    public class DataTableParameters
    {
        public int Draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<List<string>> data { get; set; }
    }
    #endregion
}