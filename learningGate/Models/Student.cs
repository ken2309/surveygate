using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace learningGate.Models;

public partial class Student
{
    [Key]
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public bool? Gender { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? Status { get; set; }

}
