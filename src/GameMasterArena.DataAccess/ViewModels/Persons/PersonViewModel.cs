using GameMasterArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMasterArena.DataAccess.ViewModels.Persons;

public class PersonViewModel
{
    public long Id { get; set; }
    
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;

    public string Image { get; set; } = String.Empty;

    public string Email { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

}
