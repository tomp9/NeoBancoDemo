using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBancoDemo.Data.Repositories
{
    public interface IMovimientoRepository
    {
        Task<IEnumerable<IMovimientoRepository>> GetMovimiento();
        Task<IMovimientoRepository> GetMovimiento(int id);

        Task<IMovimientoRepository> InsertMovimiento(IMovimientoRepository cliente);

        Task<IMovimientoRepository> UpdateMovimiento(int id, IMovimientoRepository cliente);
        Task DeleteMovimiento(int id);
    }
}
