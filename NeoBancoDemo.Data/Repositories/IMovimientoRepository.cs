using Microsoft.AspNetCore.Mvc;
using NeoBancoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBancoDemo.Data.Repositories
{
    public interface IMovimientoRepository
    {
        Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientos();
        Task<Movimiento> GetMovimiento(int id);

        Task<int> InsertMovimiento(Movimiento movimiento);

        Task<int> UpdateMovimiento(int id, Movimiento movimiento);
        Task<int> DeleteMovimiento(Movimiento movimiento);

        bool MovimientoExist(int id);
    }
}
