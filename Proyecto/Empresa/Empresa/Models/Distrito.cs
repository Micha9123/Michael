using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Empresa.Models;

public partial class Distrito
{
    public int Id { get; set; }

    public int? IdProvincia { get; set; }

    public string? NombreDistrito { get; set; }
    [JsonIgnore]
    public virtual Provincia? IdProvinciaNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Trabajadores> Trabajadores { get; set; } = new List<Trabajadores>();
}
