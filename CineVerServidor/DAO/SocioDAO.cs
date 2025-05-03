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
    public class SocioDAO
    {
        public SocioDAO() { }

        public Result<List<Socio>> ObtenerSocios()
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var socios = entities.Socio.ToList();

                    if (socios.Count == 0)
                    {
                        return Result<List<Socio>>.Fallo("No hay socios registrados");
                    }
                    return Result<List<Socio>>.Exito(socios);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Socio>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Socio>>.Fallo(sqlEx.Message);
                }
            }
        }

        public Result<Socio> BuscarSocioPorFolio(string folio)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var socio = entities.Socio.Where(e => e.folio.Equals(folio)).FirstOrDefault();

                    if (socio != null)
                    {
                        return Result<Socio>.Exito(socio);
                    }
                    else
                    {
                        return Result<Socio>.Fallo("Socio no encontrado");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<Socio>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<Socio>.Fallo(sqlEx.Message);
                }
            }
        }

        public Result<string> RegistrarSocio(Socio socio)
        {
            using (CineVerEntities entities = new CineVerEntities())

            {
                try
                {
                    entities.Socio.Add(socio);
                    entities.SaveChanges();
                    return Result<string>.Exito("Socio registrado exitosamente");
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

        public Result<string> ModificarSocio(Socio socio)
        {
            using (CineVerEntities entities = new CineVerEntities())

            {
                try
                {
                    var socioExistente = entities.Socio.Find(socio.idSocio);

                    if (socioExistente != null)
                    {
                        entities.Entry(socioExistente).CurrentValues.SetValues(socio);
                        entities.SaveChanges();
                        return Result<string>.Exito("Socio modificado exitosamente");
                    }
                    else
                    {
                        return Result<string>.Fallo("Socio no encontrado");
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

        public Result<string> InhabilitarCuentaSocio(int idSocio)
        {
            using (CineVerEntities entities = new CineVerEntities())

            {
                try
                {
                    var socio = entities.Socio.Find(idSocio);

                    if (socio != null)
                    {
                        entities.Socio.Remove(socio);
                        entities.SaveChanges();
                        return Result<string>.Exito("Cuenta de socio inhabilitada exitosamente");
                    }
                    else
                    {
                        return Result<string>.Fallo("Socio no encontrado");
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

        public Result<bool> ExisteSocio(string folio)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var socio = entities.Socio.Where(e => e.folio.Equals(folio)).FirstOrDefault();
                    if (socio != null)
                    {
                        return Result<bool>.Exito(true);
                    }
                    else
                    {
                        return Result<bool>.Exito(false);
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
