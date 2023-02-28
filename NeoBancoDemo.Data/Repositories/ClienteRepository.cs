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
    public class ClienteRepository : IClienteRepository
    {

        private readonly NeoBancoDemoContext _context;

        public ClienteRepository(NeoBancoDemoContext _context)
        {
            this._context = _context;
        }

        public bool ClienteExist(int id)
        {
           return _context.Clientes.Any(e => e.ClienteId == id);
        }

        public async Task<int> DeleteCliente(Cliente cliente)
        {
             _context.Clientes.Remove(cliente);
            return await _context.SaveChangesAsync();
        }

        public async Task<Cliente> GetCliente(int id)
        {
            return await _context.Clientes.Include(c => c.Persona).FirstOrDefaultAsync(x=>x.ClienteId == id);
        }


        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.Include(c=>c.Persona).ToListAsync();
        }

        public async Task<int> InsertCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCliente(int id, Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }


    }
}
