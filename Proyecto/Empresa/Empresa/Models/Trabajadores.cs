using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Empresa.Models;

public partial class Trabajadores
{
    public int Id { get; set; }

    public string? TipoDocumento { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? Nombres { get; set; }

    public string? Sexo { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdProvincia { get; set; }

    public int? IdDistrito { get; set; }

    [JsonIgnore]
    public virtual Departamento? IdDepartamentoNavigation { get; set; }
    [JsonIgnore]
    public virtual Distrito? IdDistritoNavigation { get; set; }
    [JsonIgnore]
    public virtual Provincia? IdProvinciaNavigation { get; set; }
}
