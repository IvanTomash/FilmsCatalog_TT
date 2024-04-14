using FilmManager.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FilmManager.Web.Models;

public class CategoryViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int NestingLevel { get; set; }

    public int? ParentCategoryId { get; set; }

    public Category ParentCategory { get; set; }
    
    public int MoviesNumber { get; set; }
}
