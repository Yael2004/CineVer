//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CineVerEntidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gasto
    {
        public int idGasto { get; set; }
        public Nullable<int> idSucursal { get; set; }
        public Nullable<decimal> monto { get; set; }
        public string motivo { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> idEmpleado { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual Sucursal Sucursal { get; set; }
    }
}
