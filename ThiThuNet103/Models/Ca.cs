using System;
using System.Collections.Generic;

namespace ThiThuNet103.Models;

public partial class Ca
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public string? ThucAn { get; set; }

    public string? TapTinh { get; set; }

    public int? Idca { get; set; }

    public virtual Ca? IdcaNavigation { get; set; }

    public virtual ICollection<Ca> InverseIdcaNavigation { get; set; } = new List<Ca>();
}
