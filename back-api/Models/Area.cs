using System;
using System.Collections.Generic;

namespace back_api.Models;

public partial class Area
{
    public int IdArea { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
