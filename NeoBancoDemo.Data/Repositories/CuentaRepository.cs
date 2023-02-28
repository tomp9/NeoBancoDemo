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
    internal class CuentaRepository : ICuentaRepository
    {
        private readonly NeoBancoDemoContext _context;

        public CuentaRepository(NeoBancoDemoContext _context)
        {
            this._context = _context;
        }

        public bool CuentaExist(int id)
        {
            return _context.Cuenta.Any(e => e.CuentaId == id);
        }

        public async Task<int> DeleteCuenta(Cuenta cuenta)
        {
            _context.Cuenta.Remove(cuenta);
            return await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuentas()
        {
            return await _context.Cuenta.ToListAsync();
        }

        public async Task<Cuenta> GetCuenta(int id)
        {
            return await _context.Cuenta.FindAsync(id);
        }

        public async Task<int> InsertCuenta(Cuenta cuenta)
        {
            _context.Cuenta.Add(cuenta);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCuenta(int id, Cuenta cuenta)
        {
            _context.Entry(cuenta).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
