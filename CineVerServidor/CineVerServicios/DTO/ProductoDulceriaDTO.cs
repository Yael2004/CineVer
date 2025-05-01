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
    public class ProductoDulceriaDTO
    {
        [DataMember]
        public int IdProducto { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public int CantidadInventario { get; set; }
        [DataMember]
        public decimal CostoUnitario { get; set; }
        [DataMember]
        public decimal PrecioVentaUnitario { get; set; }
        [DataMember]
        public byte[] Imagen { get; set; }
        [DataMember]
        public int IdSucursal { get; set; }

        public ProductoDulceriaDTO()
        {
        }

        public ProductoDulceriaDTO(int idProducto, string nombre, int cantidadInventario, decimal costoUnitario, decimal precioVentaUnitario, byte[] imagen, int idSucursal)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            CantidadInventario = cantidadInventario;
            CostoUnitario = costoUnitario;
            PrecioVentaUnitario = precioVentaUnitario;
            Imagen = imagen;
            IdSucursal = idSucursal;
        }

    }
}
