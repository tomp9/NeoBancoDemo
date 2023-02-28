using System;
using System.Collections.Generic;

namespace NeoBancoDemo.Models;

public partial class Cliente 
{

    public int ClienteId { get; set; }

    public decimal Contrasena { get; set; }

    public bool Estado { get; set; }

    public int PersonaId { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; } = new List<Cuenta>();

    //public virtual Persona Persona { get; set; } = null!;


    public static explicit operator Cliente(Task<Cliente> v)
    {
        throw new NotImplementedException();
    }
}
