using System;
using System.Collections.Generic;

namespace back_api.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public int IdArea { get; set; }

    public int IdRole { get; set; }

    public byte? Status { get; set; }

    public virtual Area IdAreaNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;
}
