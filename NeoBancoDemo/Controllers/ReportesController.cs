using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoBancoDemo.Models;

namespace NeoBancoDemo.Controllers
{
    public class ReportesController : Controller
    {

        private readonly NeoBancoDemoContext _context;

        public ReportesController(NeoBancoDemoContext context)
        {
            _context = context;
        }

        // GET: api/Reportes/?fechaInicial=10-12-2020&fechaFinal=10-11-2023
        [HttpGet("api/Reportes/")]
        public IActionResult Index(string fechaInicial, string fechaFinal, string numDocCliente)
        {
            DateTime fechaIni;
            DateTime fechaFin;
            try
            {
                fechaIni = DateTime.Parse(fechaInicial.ToString());
                fechaFin = DateTime.Parse(fechaFinal.ToString());
            }
            catch (Exception)
            {
                return BadRequest("fecha con formato incorrecto");
            }

            List<Reporte> reportes = new List<Reporte>();

            var persona = _context.Personas.FirstOrDefault(p => p.Identificacion == numDocCliente);

            var Cliente = _context.Clientes.Include(c => c.Cuenta).FirstOrDefault(c => c.PersonaId == persona.PersonaId);

            var cuentas = _context.Cuenta.Where(c => c.ClienteId == Cliente.ClienteId).Include(c => c.Movimientos).ToList();

            List<Movimiento> movimientos = new List<Movimiento>();

            foreach (Cuenta cuenta in cuentas)
            {
                movimientos.AddRange(cuenta.Movimientos.Where(m=>m.FechaMovimiento >= fechaIni && m.FechaMovimiento <= fechaFin).OrderByDescending(x=>x.MovimientoId));
            }

            foreach (Cuenta cuenta in cuentas)
            {
                foreach (var movimiento in movimientos)
                {
                    decimal signo = (movimiento.TipoMovimiento == "Credito") ? -1 : 1;
                    reportes.Add(new Reporte {Fecha = movimiento.FechaMovimiento,
                                              Cliente = persona.Nombre, 
                                              NumeroCuenta = cuenta.NumCuenta,
                                              Tipo = movimiento.TipoMovimiento ,
                                              SaldoInicial = movimiento.SaldoInicial ,
                                              estado = cuenta.Estado , 
                                              Movimiento= movimiento.Valor * signo ,
                                              SaldoDisponible = movimiento.SaldoFinal });
                }
            }

            return new JsonResult(new { fechaInicial = fechaInicial, fechaFin = fechaFinal, Reporte = reportes });

            
        }
    }
}
