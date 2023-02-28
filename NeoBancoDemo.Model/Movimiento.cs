using System;
using System.Collections.Generic;

namespace NeoBancoDemo.Models;

public partial class Movimiento
{
    public int MovimientoId { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public decimal Valor { get; set; }

    public decimal Saldo { get; set; }

    public int CuentaId { get; set; }

    public virtual Cuenta Cuenta { get; set; } = null!;
}
