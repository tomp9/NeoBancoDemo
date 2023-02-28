using System;
using System.Collections.Generic;

namespace NeoBancoDemo.Models;

public partial class Persona
{
    public int PersonaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public short Edad { get; set; }

    public string Identificacion { get; set; } = null!;

    public string Telefono { get; set; } = null!;
    public string Direccion { get; set; } = null!;

    //public virtual Cliente? Cliente { get; set; }
}
