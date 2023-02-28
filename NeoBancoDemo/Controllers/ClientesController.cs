using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeoBancoDemo.Data.Repositories;
using NeoBancoDemo.Models;

namespace NeoBancoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository _clienteRepository)
        {
            this._clienteRepository = _clienteRepository;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return Ok(await _clienteRepository.GetClientes());
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteRepository.GetCliente(id);    //await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }


            try
            {
                await _clienteRepository.UpdateCliente(id, cliente);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(NoContent());
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {

            try
            {
                await _clienteRepository.InsertCliente(cliente);
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.ClienteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(CreatedAtAction("GetCliente", new { id = cliente.ClienteId }, cliente));
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente =(Cliente) _clienteRepository.GetCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteRepository.DeleteCliente(cliente);
    
            return Ok(NoContent());
        }

        private bool ClienteExists(int id)
        {
            return _clienteRepository.ClienteExist(id);
        }
    }
}
