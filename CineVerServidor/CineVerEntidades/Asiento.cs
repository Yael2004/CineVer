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
    
    public partial class Asiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Asiento()
        {
            this.Boleto = new HashSet<Boleto>();
            this.AsientoFuncion = new HashSet<AsientoFuncion>();
        }
    
        public Nullable<int> idFila { get; set; }
        public string letraColumna { get; set; }
        public int idAsiento { get; set; }
        public string estado { get; set; }
    
        public virtual Fila Fila { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Boleto> Boleto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsientoFuncion> AsientoFuncion { get; set; }
    }
}
