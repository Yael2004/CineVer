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
    public class CuentaFidelidadDAO
    {
        public CuentaFidelidadDAO() { }

        public Result<string> RegistrarCuentaFidelidad(CuentaFidelidad cuentaFidelidad)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    entities.CuentaFidelidad.Add(cuentaFidelidad);
                    entities.SaveChanges();
                    return Result<string>.Exito("Cuenta de fidelidad registrada con éxito");
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

        public Result<CuentaFidelidad> ObtenerCuentaFidelidadPorIdSocio(int idSocio)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var cuentaFidelidad = entities.CuentaFidelidad.Find(idSocio);
                    if (cuentaFidelidad != null)
                    {
                        return Result<CuentaFidelidad>.Exito(cuentaFidelidad);
                    }
                    else
                    {
                        return Result<CuentaFidelidad>.Fallo("No se encontró la cuenta de fidelidad");
                    }
                }
                catch (Exception ex)
                {
                    return Result<CuentaFidelidad>.Fallo(ex.Message);
                }
            }
        }

        public Result<string> ModificarCuentaFidelidad(CuentaFidelidad cuentaFidelidad)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var cuentaFidelidadExistente = entities.CuentaFidelidad.Find(cuentaFidelidad.idCuenta);

                    if (cuentaFidelidadExistente != null)
                    {
                        entities.Entry(cuentaFidelidadExistente).CurrentValues.SetValues(cuentaFidelidad);
                        entities.SaveChanges();
                        return Result<string>.Exito("Cuenta de fidelidad modificada con éxito");
                    }
                    else
                    {
                        return Result<string>.Fallo("No se encontró la cuenta de fidelidad");
                    }
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

        public Result<string> InhabilitarCuentaFidelidad(int idCuenta)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var cuentaFidelidad = entities.CuentaFidelidad.Find(idCuenta);

                    if (cuentaFidelidad != null)
                    {
                        entities.CuentaFidelidad.Remove(cuentaFidelidad);
                        entities.SaveChanges();
                        return Result<string>.Exito("Cuenta de fidelidad inhabilitada con éxito");
                    }
                    else
                    {
                        return Result<string>.Fallo("No se encontró la cuenta de fidelidad");
                    }
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
    }
}
