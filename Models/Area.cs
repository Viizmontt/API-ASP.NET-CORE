using System;
using System.Collections.Generic;

namespace WEBAPI002.Models;

public partial class Area{

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
