using FilmManager.DAL.Models;
using FilmManager.DAL.Repositories.Interfaces;
using FilmManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FilmManager.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ILogger<CategoryController> logger, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryRepository.GetAll();

        var categoryViewModels = categories.Select(c =>
        {
            return new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                NestingLevel = _categoryRepository.GetNestingLevel(c),
                ParentCategoryId = c.ParentCategoryId,
                ParentCategory = c.ParentCategory,
                MoviesNumber = _categoryRepository.GetMoviesNumber(c.Id)
            };
        });

        return View(categoryViewModels);
    }

    public async Task<IActionResult> Create()
    {
        Category newCategory = new Category();
        ViewBag.CategoryList = await _categoryRepository.GetAll();
        return View(newCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category newEntity)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryRepository.Create(newEntity);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Update(int id)
    {
        var updatedCategory = await _categoryRepository.GetById(id);
        ViewBag.CategoryList = await _categoryRepository.GetAll();
        return View(updatedCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Category updatedCategory)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryRepository.Update(updatedCategory);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryRepository.Delete(id);
        if (result == null)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(Index));    
    }
}
