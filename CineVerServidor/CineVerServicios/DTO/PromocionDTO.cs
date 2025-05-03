using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class PromocionDTO
    {
        [DataMember]
        public int IdPromocion { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public int IdSucursal { get; set; }
        [DataMember]
        public string Producto { get; set; }
        [DataMember]
        public int NumeroProductosNecesarios { get; set; }
        [DataMember]
        public int NumeroProductosPagar { get; set; }
        [DataMember]
        public bool LunesAplica { get; set; }
        [DataMember]
        public bool MartesAplica { get; set; }
        [DataMember]
        public bool MiercolesAplica { get; set; }
        [DataMember]
        public bool JuevesAplica { get; set; }
        [DataMember]
        public bool ViernesAplica { get; set; }
        [DataMember]
        public bool SabadoAplica { get; set; }
        [DataMember]
        public bool DomingoAplica { get; set; }
        [DataMember]
        public string Nombre { get; set; }

        public PromocionDTO()
        {
            IdPromocion = 0;
            Tipo = string.Empty;
            IdSucursal = 0;
            Producto = string.Empty;
            NumeroProductosNecesarios = 0;
            NumeroProductosPagar = 0;
            LunesAplica = false;
            MartesAplica = false;
            MiercolesAplica = false;
            JuevesAplica = false;
            ViernesAplica = false;
            SabadoAplica = false;
            DomingoAplica = false;
            Nombre = string.Empty;
        }
    }
}
