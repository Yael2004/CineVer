using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace DAO
{
    public class CorteCajaDAO
    {
        public CorteCajaDAO() { }
        public Result<string> GuardarCorteCaja(CorteCaja corteCaja)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    entities.CorteCaja.Add(corteCaja);
                    entities.SaveChanges();
                    return Result<string>.Exito("Corte de caja guardado correctamente");
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

        public Result<decimal> ObtenerMontoInicioDia(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var fechaAyer = DateTime.Now.AddDays(-1).Date;
                    var corteCaja = entities.CorteCaja.FirstOrDefault(c => DbFunctions.TruncateTime(c.fechaCorte) == fechaAyer && c.idSucursal == idSucursal);
                    if (corteCaja != null)
                    {
                        return Result<decimal>.Exito((decimal)corteCaja.inicioDia);
                    }
                    else
                    {
                        return Result<decimal>.Fallo("No se encontró el corte de caja para la fecha especificada.");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<decimal>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<decimal>.Fallo(sqlEx.Message);
                }
            }
        }
    }
}
