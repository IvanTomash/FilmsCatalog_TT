using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManager.DAL.Models;

[Table(name:"categories")]
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Column(name: "parent_category_id")]
    public int? ParentCategoryId { get; set; }

    [ValidateNever]
    public Category ParentCategory { get; set; }

    [ValidateNever]
    public ICollection<Category> SubCategories { get; set; }

    [ValidateNever]
    public ICollection<FilmCategory> FilmCategories { get; set; }
}
