using System;
using System.Collections.Generic;

namespace WEBAPI002.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public decimal Salario { get; set; }

    public DateOnly FechaIngreso { get; set; }

    public int? JefeInmediatoId { get; set; }

    public int? AreaId { get; set; }

    public virtual Area? Area { get; set; }
}
