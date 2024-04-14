using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManager.DAL.Models;

[Table(name: "films")]
public class Film
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string Director { get; set; }

    public DateTime Release { get; set; }

    [ValidateNever]
    public ICollection<FilmCategory> FilmCategories { get; set; }
}
