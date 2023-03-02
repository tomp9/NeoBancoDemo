using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoBancoDemo.Business;
using NeoBancoDemo.Data.Repositories;
using NeoBancoDemo.Models;

namespace NeoBancoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly NeoBancoDemoContext _context;

        public MovimientosController(NeoBancoDemoContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientos()
        {
            return await _context.Movimientos.ToListAsync();
        }

        // GET: api/Movimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound(new JsonResult(new { MensajeError = "No se encontró el movimiento con el Id " + id }));
            }

            return movimiento;
        }

        // PUT: api/Movimientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, Movimiento movimiento)
        {
            if (id != movimiento.MovimientoId)
            {
                return BadRequest();
            }
            var cuenta = _context.Cuenta.FirstOrDefault(x => x.CuentaId == movimiento.CuentaId);
            if (cuenta == null)
            {
                return NotFound(new JsonResult(new { MensajeError = "No se encontró la cuenta con el Id " + movimiento.CuentaId }));
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoExists(id))
                {
                    return NotFound(new JsonResult(new { MensajeError = "No se encontró el movimiento con el Id " + id })); ;
                }
                else
                {
                    throw;
                }
            }
            cuenta.SaldoInicial = movimiento.SaldoFinal;
            CuentaRepository cuentaRepository = new CuentaRepository(_context);
            await cuentaRepository.UpdateCuenta(cuenta.CuentaId, cuenta);

            return StatusCode(200, new JsonResult(new { movimientoActualizado = movimiento })); ;
        }

        // POST: api/Movimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimiento>> PostMovimiento(Movimiento movimiento)
        {
            var cuenta = _context.Cuenta.FirstOrDefault(x=>x.CuentaId == movimiento.CuentaId);
            if (cuenta == null)
            {
                return NotFound(new JsonResult(new { MensajeError = "No se encontró la cuenta con el Id " + movimiento.CuentaId }));
            }

            movimiento.SaldoInicial = cuenta.SaldoInicial;
            if (movimiento.TipoMovimiento.ToUpper() == "Credito".ToUpper())
            {
                try
                {
                    cuenta.SaldoInicial = MovimientoBusiness.RealizarOperacionCredito(cuenta.SaldoInicial , movimiento.Valor);

                }
                catch (Exception ex)
                {
                    return StatusCode(500, new JsonResult(new { Message = ex.Message }) );
                }
            }
            else if (movimiento.TipoMovimiento.ToUpper() == "Debito".ToUpper() ) {
                cuenta.SaldoInicial = MovimientoBusiness.RealizarOperacionDebito(cuenta.SaldoInicial, movimiento.Valor);
            }
            else
            {
                return StatusCode(500, new JsonResult(new { Message = "El tipo de movimiento debe se Credito o Debito" }));
            }

            CuentaRepository cuentaRepository = new CuentaRepository(_context);
            await cuentaRepository.UpdateCuenta(cuenta.CuentaId,cuenta);

            movimiento.SaldoFinal = cuenta.SaldoInicial;

            _context.Movimientos.Add(movimiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MovimientoExists(movimiento.MovimientoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovimiento", new { id = movimiento.MovimientoId }, movimiento);
        }

        // DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound(new JsonResult(new { MensajeError = "No se encontró el movimiento con el Id " + id }));
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return StatusCode(200, new JsonResult(new { MovimientoEliminada = movimiento }));
        }

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.MovimientoId == id);
        }
    }
}
