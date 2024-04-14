using FilmManager.DAL.Models;

namespace FilmManager.API.Models;

public class GetCategoriesResponse
{
    public IEnumerable<Category> AvailableCategories { get; set; } = null!;

    public IEnumerable<Category> SelectedCategories { get; set; } = null!;
}
