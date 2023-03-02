using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NeoBancoDemo.Models;

public partial class Cuenta
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CuentaId { get; set; }

    public string NumCuenta { get; set; } = null!;

    public string TipoCuenta { get; set; } = null!;

    public decimal SaldoInicial { get; set; }

    public bool Estado { get; set; }

    public int ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    [JsonIgnore]
    public virtual Cliente? Cliente { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
}
