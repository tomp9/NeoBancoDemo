using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoBancoDemo.Models;

namespace NeoBancoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly NeoBancoDemoContext _context;

        public CuentasController(NeoBancoDemoContext context)
        {
            _context = context;
        }

        // GET: api/Cuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuenta()
        {
            return await _context.Cuenta.ToListAsync();
        }

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuenta>> GetCuenta(int id)
        {
            var cuenta = await _context.Cuenta.FindAsync(id);

            if (cuenta == null)
            {
                return NotFound(new JsonResult(new { MensajeError = "No se encontró la cuenta con el Id " + id }));
            }

            return cuenta;
        }

        // PUT: api/Cuentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta(int id, Cuenta cuenta)
        {
            if (id != cuenta.CuentaId)
            {
                return BadRequest();
            }

            _context.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
                {
                    return NotFound(new JsonResult(new { MensajeError = "No se encontró la cuenta con el Id " + id }));
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(200, new JsonResult(new { CuentaActualizada = cuenta })); ;
        }

        // POST: api/Cuentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuenta>> PostCuenta(Cuenta cuenta)
        {
            _context.Cuenta.Add(cuenta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CuentaExists(cuenta.CuentaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCuenta", new { id = cuenta.CuentaId }, cuenta);
        }

        // DELETE: api/Cuentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _context.Cuenta.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound(new JsonResult(new { MensajeError = "No se encontró la cuenta con el Id " + id }));
            }

            _context.Cuenta.Remove(cuenta);
            await _context.SaveChangesAsync();

            return StatusCode(200, new JsonResult(new { CuentaEliminada = cuenta }));
        }

        private bool CuentaExists(int id)
        {
            return _context.Cuenta.Any(e => e.CuentaId == id);
        }
    }
}
