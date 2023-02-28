using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoBancoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBancoDemo.Data.Repositories
{
    public class MovimientoRepository : IMovimientoRepository
    {

        private readonly NeoBancoDemoContext _context;

        public MovimientoRepository(NeoBancoDemoContext _context)
        {
            this._context = _context;
        }
        public bool MovimientoExist(int id)
        {
            return _context.Movimientos.Any(e => e.MovimientoId == id);
        }

        public async Task<int> DeleteMovimiento(Movimiento movimiento)
        {
            _context.Movimientos.Remove(movimiento);
            return await _context.SaveChangesAsync();
        }

        public async Task<Movimiento> GetMovimiento(int id)
        {
            return await _context.Movimientos.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientos()
        {
            return await _context.Movimientos.ToListAsync();
        }

        public async Task<int> InsertMovimiento(Movimiento movimiento)
        {
            _context.Movimientos.Add(movimiento);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> UpdateMovimiento(int id, Movimiento movimiento)
        {
            _context.Entry(movimiento).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
