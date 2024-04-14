using FilmManager.DAL.Models;
using FilmManager.DAL.Repositories;
using FilmManager.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FilmManager.Web.Controllers;

public class FilmController : Controller
{
    private readonly ILogger<FilmController> _logger;
    private readonly IBaseRepository<Film> _filmRepository;

    public FilmController(ILogger<FilmController> logger, IBaseRepository<Film> filmRepository)
    {
        _logger = logger;
        _filmRepository = filmRepository;
    }

    public async Task<IActionResult> Index()
    {
        var films = await _filmRepository.GetAll();
        return View(films);
    }

    public IActionResult Create()
    {
        Film newFilm = new Film();
        return View(newFilm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Film newEntity)
    {
        if (ModelState.IsValid)
        {
            var result = await _filmRepository.Create(newEntity);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Update(int id)
    {
        var updatedFilm = await _filmRepository.GetById(id);
        return View(updatedFilm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Film updatedFilm)
    {
        if (ModelState.IsValid)
        {
            var result = await _filmRepository.Update(updatedFilm);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        return BadRequest();
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _filmRepository.Delete(id);
        if (result == null)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(Index));
    }
}
