using FilmManager.DAL.Models;
using FilmManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManager.DAL.Repositories;

public class FilmCategoryRepository : IFilmCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public FilmCategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> Create(FilmCategory entity)
    {
        var result = await _dbContext.FilmCategories.AddAsync(entity);
        _dbContext.SaveChanges();
        return result.Entity.Id;
    }

    public int? Remove(int filmId, int categoryId)
    {
        var deletedEntity = _dbContext.FilmCategories.Where(fc => fc.FilmId==filmId && fc.CategoryId==categoryId).FirstOrDefault();
        if (deletedEntity != null)
        {
            var result = _dbContext.FilmCategories.Remove(deletedEntity);
            _dbContext.SaveChanges();
            return result.Entity.Id;
        }
        return null;
    }

    public IEnumerable<Category> GetAvailableCategoriesForFilm(int filmId)
    {
        var categories = _dbContext.Categories.Include(c => c.FilmCategories).ToList();
        var unavailableCategories = new List<Category>();

        foreach (var category in categories)
        {
            foreach (var filmCategory in category.FilmCategories)
            {
                if (filmCategory.FilmId == filmId)
                {
                    unavailableCategories.Add(category);
                }
            }
        }

        foreach (var category in unavailableCategories)
        {
            categories.Remove(category);
        }
       

        return categories;
    }

    public IEnumerable<Category> GetSelectedCategoriesForFilm(int filmId)
    {
        var categories = _dbContext.Categories.Include(c => c.FilmCategories).ToList();
       
        var selectedCategories = new List<Category>();
        foreach(var category in categories)
        {
            foreach (var filmCategory in category.FilmCategories)
            {
                if (filmCategory.FilmId == filmId)
                {
                    selectedCategories.Add(category);
                    break;
                }
            }
        }

        return selectedCategories;
    }
}
