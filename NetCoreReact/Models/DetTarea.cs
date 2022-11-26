using System;
using System.Collections.Generic;

namespace NetCoreReact.Models;

public partial class DetTarea
{
    public int Id { get; set; }

    public int EstadoId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descrpcion { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }
}
