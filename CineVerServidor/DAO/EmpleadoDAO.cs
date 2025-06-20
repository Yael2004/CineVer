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
    public class EmpleadoDAO
    {
        public EmpleadoDAO() { }

        public Result<List<Empleado>> ObtenerEmpleados()
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var empleados = entities.Empleado.Where(e => e.contratado == true && e.rol != "admin").ToList();

                    if (empleados.Count == 0)
                    {
                        return Result<List<Empleado>>.Fallo("No hay empleados registrados");
                    }
                    return Result<List<Empleado>>.Exito(empleados);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Empleado>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Empleado>>.Fallo(sqlEx.Message);
                }
            }
        }

        public Result<Empleado> BuscarEmpleadoPorMatricula(string matriculaEmpleado)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var empleado = entities.Empleado.Where(e => e.matriculaEmpleado.Equals(matriculaEmpleado) && e.contratado == true).FirstOrDefault();

                    if (empleado != null)
                    {
                        return Result<Empleado>.Exito(empleado);
                    }
                    else
                    {
                        return Result<Empleado>.Fallo("Empleado no encontrado");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<Empleado>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<Empleado>.Fallo(sqlEx.Message);
                }
            }
        }

        public Result<string> RegistrarEmpleado(Empleado empleado)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    entities.Empleado.Add(empleado);
                    entities.SaveChanges();
                    return Result<string>.Exito("Empleado registrado exitosamente");
                }
                catch (DbEntityValidationException ex)
                {
                    var errores = new StringBuilder();

                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            errores.AppendLine($"✧ Campo: {validationError.PropertyName} — Error: {validationError.ErrorMessage}");
                        }
                    }

                    return Result<string>.Fallo(errores.ToString());
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
            }
        }

        public Result<string> ModificarEmpleado(Empleado empleado)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var empleadoExistente = entities.Empleado.Find(empleado.idEmpleado);

                    if (empleadoExistente != null)
                    {
                        entities.Entry(empleadoExistente).CurrentValues.SetValues(empleado);
                        entities.SaveChanges();
                        return Result<string>.Exito("Empleado modificado exitosamente");
                    }
                    else
                    {
                        return Result<string>.Fallo("Empleado no encontrado");
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

        public Result<string> InhabilitarEmpleado(int idEmpleado)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var empleado = entities.Empleado.Find(idEmpleado);

                    if (empleado != null)
                    {
                        empleado.contratado = false;
                        entities.SaveChanges();
                        return Result<string>.Exito("Cuenta de empleado inhabilitada exitosamente");
                    }
                    else
                    {
                        return Result<string>.Fallo("Empleado no encontrado");
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

        public Result<bool> ExisteEmpleado(string matriculaEmpleado)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var empleado = entities.Empleado.Where(e => e.matriculaEmpleado.Equals(matriculaEmpleado) && e.contratado == true).FirstOrDefault();

                    if (empleado != null)
                    {
                        return Result<bool>.Exito(true);
                    }
                    else
                    {
                        return Result<bool>.Fallo("El empleado no existe");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<bool>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<bool>.Fallo(sqlEx.Message);
                }
            }
        }

        public Result<bool> VerificarInicioSesion(string matricula, byte[] contraseña)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var empleado = entities.Empleado.Where(e => e.matriculaEmpleado.Equals(matricula) && e.contratado == true).FirstOrDefault();

                    if (empleado != null)
                    {
                        if (empleado.contraseña.SequenceEqual(contraseña))
                        {
                            return Result<bool>.Exito(true);
                        }
                        else
                        {
                            return Result<bool>.Fallo("Empleado no encontrado");
                        }
                    }
                    else
                    {
                        return Result<bool>.Fallo("Empleado no encontrado");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<bool>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<bool>.Fallo(sqlEx.Message);
                }
            }
        }
    }
}
