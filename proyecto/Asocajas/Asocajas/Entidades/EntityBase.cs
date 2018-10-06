using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asocajas
{
    //    text
    //number
    //select
    //date
    //CheckBox
    //Matriz
    //Radio
    //Button
    //File
    //Label

    [Serializable]
    public enum TipoControl
    {
        Ninguno = 0,
    }


    [Serializable]
    public enum TiposDatoBasico
    {
        TiposDeSolicitud = 1,
    }


    [Serializable]
    public class EntityBase
    {
        [NotMapped]
        public string PkName { get; set; }
    }
}