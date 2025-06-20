using CineVerEntidades;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace DAO
{
    public class VentaDAO
    {
        public VentaDAO() { }

        public Result<string> CambiarEstadoAsiento(int idAsiento, int idFuncion, string nuevoEstado)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var asientoFuncion = entities.AsientoFuncion
                        .FirstOrDefault(af => af.idAsiento == idAsiento && af.idFuncion == idFuncion);

                    if (asientoFuncion == null)
                    {
                        return Result<string>.Fallo("Asiento no encontrado para esta función");
                    }

                    asientoFuncion.estado = nuevoEstado;
                    entities.SaveChanges();

                    return Result<string>.Exito("Estado del asiento actualizado correctamente");
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }


        public Result<List<Venta>> ObtenerVentasPorAnio(int anio, int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var ventas = entities.Venta
                        .Where(v => v.fecha.Value.Year == anio && v.idSucursal == idSucursal)
                        .ToList();

                    if (ventas.Count == 0)
                    {
                        return Result<List<Venta>>.Fallo("No se encontraron ventas para el año especificado");
                    }

                    return Result<List<Venta>>.Exito(ventas);
                }
                catch (Exception ex)
                {
                    return Result<List<Venta>>.Fallo("¡Error al consultar las ventas! " + ex.Message);
                }
            }
        }

        public Result<List<Venta>> ObtenerVentasPorMes(int mes, int anio, int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var ventas = entities.Venta
                        .Where(v => v.fecha.Value.Month == mes && v.fecha.Value.Year == anio && v.idSucursal == idSucursal)
                        .ToList();
                    if (ventas.Count == 0)
                    {
                        return Result<List<Venta>>.Fallo("No se encontraron ventas para el mes especificado");
                    }
                    return Result<List<Venta>>.Exito(ventas);
                }
                catch (Exception ex)
                {
                    return Result<List<Venta>>.Fallo("¡Error al consultar las ventas! " + ex.Message);
                }
            }
        }

        public Result<string> ObtenerVentaPorFolio(string folio)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var venta = entities.Venta
                        .FirstOrDefault(v => v.folioVenta == folio);
                    if (venta == null)
                    {
                        return Result<string>.Fallo("No se encontró la venta con el folio especificado");
                    }
                    return Result<string>.Exito("Se econtró la venta especificada");
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo("¡Error al consultar la venta! " + ex.Message);
                }
            }
        }

        public Result<string> VerificarFechaVentaParaDevolucion(string folio)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var venta = entities.Venta
                        .FirstOrDefault(v => v.folioVenta == folio);

                    var tiempoRestante = venta.fecha.Value - DateTime.Now.Date;

                    if (tiempoRestante.TotalMinutes < 60)
                    {
                        return Result<string>.Fallo("No es posible devolver el boleto");
                    }

                    return Result<string>.Exito("La devolucion es valida");
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo("¡Error al verificar la fecha de la venta! " + ex.Message);
                }
            }
        }

        public Result<decimal> ObtenerVentasDeBoletosDelDia(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var fechaHoy = DateTime.Now.Date;
                    var ventas = entities.Venta
                        .Where(v => DbFunctions.TruncateTime(v.fecha) == fechaHoy && v.idSucursal == idSucursal && v.tipoVenta == "Taquilla")
                        .ToList();

                    if (ventas.Count == 0)
                    {
                        return Result<decimal>.Fallo("No se encontraron ventas de boletos para el día especificado");
                    }

                    decimal totalVentas = (decimal)ventas.Sum(v => v.total);

                    return Result<decimal>.Exito(totalVentas);
                }
                catch (Exception ex)
                {
                    return Result<decimal>.Fallo("¡Error al consultar las ventas de boletos! " + ex.Message);
                }
            }
        }

        public Result<decimal> ObtenerVentasDeDulceriaDelDia(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var fechaHoy = DateTime.Now.Date;
                    var ventas = entities.Venta
                        .Where(v => DbFunctions.TruncateTime(v.fecha) == fechaHoy && v.idSucursal == idSucursal && v.tipoVenta == "Dulcería")
                        .ToList();

                    if (ventas.Count == 0)
                    {
                        return Result<decimal>.Fallo("No se encontraron ventas de dulcería para el día especificado");
                    }
                    decimal totalVentas = (decimal)ventas.Sum(v => v.total);

                    return Result<decimal>.Exito(totalVentas);
                }
                catch (Exception ex)
                {
                    return Result<decimal>.Fallo("¡Error al consultar las ventas de dulcería! " + ex.Message);
                }
            }
        }

        public Result<decimal> ObtenerVentasEnEfectivoDelDia(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var fechaHoy = DateTime.Now.Date;
                    var ventas = entities.Venta
                        .Where(v => DbFunctions.TruncateTime(v.fecha) == fechaHoy && v.idSucursal == idSucursal && v.metodoPago.Contains("Efectivo"))
                        .ToList();
                    if (ventas.Count == 0)
                    {
                        return Result<decimal>.Fallo("No se encontraron ventas en efectivo para el día especificado");
                    }
                    decimal totalVentas = (decimal)ventas.Sum(v => v.total);
                    return Result<decimal>.Exito(totalVentas);
                }
                catch (Exception ex)
                {
                    return Result<decimal>.Fallo("¡Error al consultar las ventas en efectivo! " + ex.Message);
                }
            }
        }

        public Result<string> RealizarPagoDulceria(Venta venta, Dictionary<int, int> productos, double puntosUsados)
        {
            using (CineVerEntities entities = new CineVerEntities())
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    venta.fecha = DateTime.Now;

                    if (venta.idSocio == 0)
                    {
                        venta.idSocio = null;
                    }

                    entities.Venta.Add(venta);
                    entities.SaveChanges();

                    int nuevoId = venta.idVenta;
                    string folioGenerado = nuevoId.ToString().PadLeft(8, '0');
                    venta.folioVenta = folioGenerado;

                    entities.SaveChanges();

                    if (venta.idSocio.HasValue)
                    {
                        var cuentaFidelidad = entities.CuentaFidelidad.FirstOrDefault(c => c.idSocio == venta.idSocio);
                        if (cuentaFidelidad != null)
                        {
                            decimal puntosGanados = venta.total.GetValueOrDefault() * 0.10m;
                            decimal puntosUsadosDecimal = Convert.ToDecimal(puntosUsados);

                            cuentaFidelidad.puntos = cuentaFidelidad.puntos.GetValueOrDefault()
                                                     - puntosUsadosDecimal
                                                     + puntosGanados;
                        }
                    }

                    foreach (var item in productos)
                    {
                        int idProducto = item.Key;
                        int cantidadVendida = item.Value;

                        var producto = entities.ProductoDulceria.Find(idProducto);
                        if (producto == null)
                        {
                            transaction.Rollback();
                            return Result<string>.Fallo($"Producto con ID {idProducto} no encontrado. ¡Ups!");
                        }

                        if (producto.cantidadInventario < cantidadVendida)
                        {
                            transaction.Rollback();
                            return Result<string>.Fallo($"Inventario insuficiente para el producto con ID {idProducto}. ¡Necesitamos más stock!");
                        }

                        producto.cantidadInventario -= cantidadVendida;
                    }

                    entities.SaveChanges();
                    transaction.Commit();

                    return Result<string>.Exito($"Venta registrada correctamente con folio {venta.folioVenta}");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result<string>.Fallo($"¡Error mágico! {ex.Message}");
                }
            }
        }

        public Result<string> RealizarPagoBoletos(Venta venta, List<int> asientosIds, double puntosUsados)
        {
            using (CineVerEntities entities = new CineVerEntities())
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    venta.fecha = DateTime.Now;

                    if (venta.idSocio == 0)
                    {
                        venta.idSocio = null;
                    }

                    entities.Venta.Add(venta);
                    entities.SaveChanges();

                    int nuevoId = venta.idVenta;
                    string folioGenerado = nuevoId.ToString().PadLeft(8, '0');
                    venta.folioVenta = folioGenerado;

                    entities.SaveChanges();

                    if (venta.idSocio.HasValue)
                    {
                        var cuentaFidelidad = entities.CuentaFidelidad
                            .FirstOrDefault(c => c.idSocio == venta.idSocio);
                        if (cuentaFidelidad != null)
                        {
                            decimal puntosGanados = venta.total.GetValueOrDefault() * 0.10m;
                            decimal puntosUsadosDecimal = Convert.ToDecimal(puntosUsados);

                            cuentaFidelidad.puntos = cuentaFidelidad.puntos.GetValueOrDefault()
                                                     - puntosUsadosDecimal
                                                     + puntosGanados;
                        }
                    }

                    List<Asiento> asientos = new List<Asiento>();

                    foreach (int idBoleto in asientosIds)
                    {
                        var asientoFuncion = entities.AsientoFuncion
                            .FirstOrDefault(af => af.idAsiento == idBoleto && af.idFuncion == venta.idFuncion);

                        if (asientoFuncion == null)
                        {
                            transaction.Rollback();
                            return Result<string>.Fallo($"El asiento con ID {idBoleto} no está registrado para esta función.");
                        }

                        if (asientoFuncion.estado == "OCUPADO")
                        {
                            transaction.Rollback();
                            return Result<string>.Fallo($"El asiento con ID {idBoleto} ya está ocupado. ¡Elige otro, senpai!");
                        }

                        asientoFuncion.estado = "OCUPADO";

                        var asiento = entities.Asiento.FirstOrDefault(a => a.idAsiento == idBoleto);
                        if (asiento != null)
                        {
                            asientos.Add(asiento);
                        }
                    }

                    entities.SaveChanges();
                    transaction.Commit();

                    var resultadoPDF = GenerarPDFBoletos(venta, asientos);

                    if (!resultadoPDF.EsExitoso)
                        return Result<string>.Fallo($"La venta se realizó pero no se pudo generar el PDF: {resultadoPDF.Error}");

                    return Result<string>.Exito($"Boletos pagados y registrados exitosamente con folio {venta.folioVenta} 🎉");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result<string>.Fallo($"¡Error mágico! {ex.Message}");
                }
            }
        }

        public Result<string> GenerarPDFBoletos(Venta venta, List<Asiento> asientos)
        {
            try
            {
                string documentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string carpeta = Path.Combine(documentos, "CineBoletos");
                Directory.CreateDirectory(carpeta);

                string nombreArchivo = $"Boleto_{venta.folioVenta}.pdf";
                string rutaArchivo = Path.Combine(carpeta, nombreArchivo);

                using (var entities = new CineVerEntities())
                using (FileStream fs = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None))
                using (Document doc = new Document(PageSize.A4, 50, 50, 60, 60))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                    doc.Open();

                    var fuenteTitulo = FontFactory.GetFont("Arial", 20, Font.BOLD, BaseColor.ORANGE);
                    var fuenteSeccion = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.DARK_GRAY);
                    var fuenteTexto = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                    var funcion = entities.Función
                        .Include("Película")
                        .Include("Sala")
                        .FirstOrDefault(f => f.idFuncion == venta.idFuncion);

                    string nombrePeli = funcion?.Película?.nombre ?? "Película desconocida";
                    string nombreSala = funcion?.Sala?.nombre ?? $"Sala #{funcion?.idSala}";
                    string fechaFuncion = funcion?.fecha?.ToString("dd/MM/yyyy") ?? "Fecha no disponible";
                    string horaFuncion = funcion?.horaInicio?.ToString(@"hh\:mm") ?? "Hora desconocida";
                    string fechaVenta = venta.fecha?.ToString("dd/MM/yyyy HH:mm") ?? "Fecha desconocida";

                    var titulo = new Paragraph("Boleto de CineVer", fuenteTitulo) { Alignment = Element.ALIGN_CENTER };
                    doc.Add(titulo);
                    doc.Add(new Paragraph("¡Gracias por tu compra, disfruta la función!", fuenteTexto) { Alignment = Element.ALIGN_CENTER });
                    doc.Add(new Paragraph("\n"));

                    doc.Add(new Paragraph("Detalles de la función", fuenteSeccion));
                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph($"Película: {nombrePeli}", fuenteTexto));
                    doc.Add(new Paragraph($"Sala: {nombreSala}", fuenteTexto));
                    doc.Add(new Paragraph($"Fecha: {fechaFuncion}", fuenteTexto));
                    doc.Add(new Paragraph($"Hora: {horaFuncion}", fuenteTexto));
                    doc.Add(new Paragraph($"Folio: {venta.folioVenta}", fuenteTexto));
                    doc.Add(new Paragraph($"Total pagado: ${venta.total:F2}", fuenteTexto));
                    doc.Add(new Paragraph($"Fecha de compra: {fechaVenta}", fuenteTexto));
                    doc.Add(new Paragraph(" "));

                    doc.Add(new Paragraph("Asientos asignados", fuenteSeccion));
                    foreach (var asiento in asientos)
                    {
                        int numeroFila = (int)asiento.Fila.númeroFila;
                        string asientoTexto = $"{asiento.letraColumna}{numeroFila}";
                        doc.Add(new Paragraph($"• Asiento: {asientoTexto}", fuenteTexto));
                    }

                    doc.Add(new Paragraph("\n──────────────────────────────", fuenteTexto));
                    doc.Add(new Paragraph("✨ ¡Te esperamos con palomitas y mucha emoción!", fuenteTexto) { Alignment = Element.ALIGN_CENTER });

                    doc.Close();
                }

                return Result<string>.Exito($"PDF generado en: {rutaArchivo}");
            }
            catch (Exception ex)
            {
                return Result<string>.Fallo($"Ocurrió un error al generar el PDF: {ex.Message}");
            }
        }

    }
}