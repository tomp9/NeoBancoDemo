using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NeoBancoDemo.Models;

public partial class Movimiento
{
    public int MovimientoId { get; set; }
    public string TipoMovimiento { get; set; }
    public decimal Valor { get; set; }
    public decimal SaldoInicial { get; set; }
    public int? CuentaId { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public decimal SaldoFinal { get; set; }

    public virtual Cuenta? Cuenta { get; set; }
}
