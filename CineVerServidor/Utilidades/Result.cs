using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class Result<T>
    {
        public T Valor { get; private set; }
        public bool EsExitoso { get; private set; }
        public string Error { get; private set; }

        public Result(T valor, bool esExitoso, string error)
        {
            Valor = valor;
            EsExitoso = esExitoso;
            Error = esExitoso ? string.Empty : error;
        }

        public Result(bool esExitoso, string error)
        {
            EsExitoso = esExitoso;
            Error = esExitoso ? string.Empty : error;
        }

        public static Result<T> Exito(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result<T> Fallo(string error)
        {
            return new Result<T>(default, false, error);
        }

        public static Result<decimal> Exito(decimal? v)
        {
            throw new NotImplementedException();
        }
    }
}
