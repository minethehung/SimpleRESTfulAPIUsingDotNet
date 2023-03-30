using System;
using System.Collections.Generic;

namespace SimpleRESTfulAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
