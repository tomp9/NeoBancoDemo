using Microsoft.AspNetCore.Mvc;
using NeoBancoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBancoDemo.Data.Repositories
{
    public interface ICuentaRepository
    {
        bool CuentaExist(int id);
        Task<ActionResult<IEnumerable<Cuenta>>> GetCuentas();
        Task<Cuenta> GetCuenta(int id);

        Task<int> InsertCuenta(Cuenta cuenta);

        Task<int> UpdateCuenta(int id, Cuenta cuenta);
        Task<int> DeleteCuenta(Cuenta cuenta);
    }
}
