using FilmManager.API.Models;
using FilmManager.DAL.Models;
using FilmManager.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmCategoriesController : ControllerBase
{
    private readonly ILogger<FilmCategoriesController> _logger;
    private readonly IFilmCategoryRepository _filmCategoryRepository;

    public FilmCategoriesController(ILogger<FilmCategoriesController> logger, IFilmCategoryRepository filmCategoryRepository)
    {
        _logger = logger;
        _filmCategoryRepository = filmCategoryRepository;
    }

    [HttpGet(nameof(GetCategories))]
    public ActionResult<GetCategoriesResponse> GetCategories(int filmId)
    {
        var availableCategories = _filmCategoryRepository.GetAvailableCategoriesForFilm(filmId).Select(c => new Category()
        {
            Id = c.Id,
            Name = c.Name,
            ParentCategoryId = c.ParentCategoryId,
        });
        var selectedCategories = _filmCategoryRepository.GetSelectedCategoriesForFilm(filmId).Select(c => new Category()
        {
            Id = c.Id,
            Name = c.Name,
            ParentCategoryId = c.ParentCategoryId,
        });

        var result = new GetCategoriesResponse()
        {
            AvailableCategories = availableCategories,
            SelectedCategories = selectedCategories
        };
        return Ok(result);
    }



    [HttpPost(nameof(CreateConnection), Name ="AddFilmCategory")]
    public async Task<IActionResult> CreateConnection(CreateConnectionRequest request)
    {
        var result = await _filmCategoryRepository.Create(new FilmCategory { FilmId = request.FilmId, CategoryId = request.CategoryId });
        return Ok(result);
    }

    [HttpPost(nameof(RemoveConnection))]
    public IActionResult RemoveConnection(RemoveConnectionRequest request)
    {
        var result =  _filmCategoryRepository.Remove(request.FilmId, request.CategoryId);
        return Ok(result);
    }
}
