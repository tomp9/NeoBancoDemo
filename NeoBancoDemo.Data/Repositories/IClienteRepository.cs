using Microsoft.AspNetCore.Mvc;
using NeoBancoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBancoDemo.Data.Repositories
{
    public interface IClienteRepository
    {
        Task<ActionResult<IEnumerable<Cliente>>> GetClientes();
        Task<Cliente> GetCliente(int id);

        Task<int> InsertCliente(Cliente cliente);

        Task<int> UpdateCliente(int id, Cliente cliente);
        Task<int> DeleteCliente(Cliente cliente);

        bool ClienteExist(int id);

    }
}
