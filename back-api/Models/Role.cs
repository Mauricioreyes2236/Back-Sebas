using System;
using System.Collections.Generic;

namespace back_api.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
