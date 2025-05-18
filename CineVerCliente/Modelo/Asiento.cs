using CineVerCliente.ModeloVista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class Asiento : BaseModeloVista
    {
        private EstadoAsiento estado;
        public int IdAsiento { get; set; }
        public int IdFila { get; set; }
        public string LetraColumna { get; set; }
        public EstadoAsiento Estado
        {
            get => estado;
            set
            {
                if (estado != value)
                {
                    estado = value;
                    OnPropertyChanged(nameof(Estado));
                }
            }
        }
    }

    public enum EstadoAsiento
    {
        DISPONIBLE,
        OCUPADO,
        SELECCIONADO,
        MANTENIMIENTO
    }
}
