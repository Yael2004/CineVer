using CineVerCliente.ModeloVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class ProductoDulceria : BaseModeloVista
    {
        public int Id { get; set; }
        public string CostoUnitario { get; set; }
        public string PrecioVentaUnitario { get; set; }
        public byte[] Imagen { get; set; }
        public string Nombre { get; set; }
        public string CantidadInventario { get; set; }
        public int IdSucursal { get; set; }
        private int _cantidadAVender;
        public int CantidadAVender
        {
            get => _cantidadAVender;
            set
            {
                if (int.TryParse(CantidadInventario, out int cantidadInventarioNumerica))
                {
                    if (value > cantidadInventarioNumerica)
                    {
                        _cantidadAVender = cantidadInventarioNumerica;
                    }
                    else if (value < 0)
                    {
                        _cantidadAVender = 0;
                    }
                    else
                    {
                        _cantidadAVender = value;
                    }
                }
                else
                {
                    _cantidadAVender = 0;
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalProducto));
            }
        }

        public double TotalProducto
        {
            get
            {
                if (double.TryParse(PrecioVentaUnitario, out double precioVentaNumerico))
                {
                    return CantidadAVender * precioVentaNumerico;
                }
                return 0;
            }
        }
    }
}
