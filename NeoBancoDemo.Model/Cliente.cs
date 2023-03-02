using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NeoBancoDemo.Models;

public partial class Cliente 
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClienteId { get; set; }

    [Range(0001, 9999, ErrorMessage = "El valor de la contraseña debe estar entre 0001 y 9999")]
    public decimal Contrasena { get; set; }

    public bool Estado { get; set; }

    public int PersonaId { get; set; }

    [ForeignKey("PersonaId")]
    public virtual Persona? Persona { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Cuenta>? Cuenta { get; } = new List<Cuenta>();

    public static explicit operator Cliente(Task<Cliente> v)
    {
        throw new NotImplementedException();
    }
}
