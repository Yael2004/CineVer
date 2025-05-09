using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace DAO
{
    public class GastoDAO
    {
        public GastoDAO() { }

        public Result<string> RegistrarGasto(Gasto gasto)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    entities.Gasto.Add(gasto);
                    entities.SaveChanges();
                    return Result<string>.Exito("Gasto registrado con éxito");
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
            }
        }

        public Result<List<Gasto>> ObtenerGastosDelDia(DateTime fecha, int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var gastos = entities.Gasto.Where(e => e.fecha.Value.Day.Equals(fecha.Day) && e.idSucursal.Value.Equals(idSucursal)).ToList();

                    if (gastos.Count() == 0)
                    {
                        return Result<List<Gasto>>.Fallo("No hay gastos registrados el día de hoy");
                    }

                    return Result<List<Gasto>>.Exito(gastos);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Gasto>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Gasto>>.Fallo(sqlEx.Message);
                }
            }
        }
    }
}
