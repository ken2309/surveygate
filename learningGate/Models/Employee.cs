using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ssSystem.Models;

public partial class Employee : IdentityUser
{
    public string? EmailAddress { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? ProfileImageUrl { get; set; }

    public string? Phone { get; set; }

    public string? Qualification { get; set; }

    public string? Department { get; set; }

    public bool? IsAdmin { get; set; }

    public string? Grade { get; set; }

    public bool? Status { get; set; }
}