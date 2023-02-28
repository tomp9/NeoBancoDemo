    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBancoDemo.Data.Repositories
{
    public interface ICuentaRepository
    {
        Task<IEnumerable<ICuentaRepository>> GetCuenta();
        Task<ICuentaRepository> GetCuenta(int id);

        Task<ICuentaRepository> InsertCuenta(ICuentaRepository cliente);

        Task<ICuentaRepository> UpdateCuenta(int id, ICuentaRepository cliente);
        Task DeleteCuenta(int id);
    }
}
