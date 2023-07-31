using System;
namespace GameMasterArena.Domain.Entities;

public abstract class Auditable : BaseEntity
{
    public string Description { get; set; } = string.Empty;

    public DateTime Create_at { get; set; }

    public DateTime Update_at { get; set; }
}
