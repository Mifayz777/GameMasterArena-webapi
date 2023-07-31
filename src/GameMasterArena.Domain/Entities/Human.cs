using System;
using System.ComponentModel.DataAnnotations;

namespace GameMasterArena.Domain.Entities;

public abstract class Human : Auditable
{
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;


    public string Image { get; set; } = String.Empty;
}
