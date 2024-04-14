using FilmManager.DAL.Models;
using FilmManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManager.DAL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int?> Create(Category entity)
    {
        var result = await _dbContext.Categories.AddAsync(entity);
        _dbContext.SaveChanges();
        return result.Entity.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var entity = await _dbContext.Categories.FindAsync(id);

        if (entity != null)
        {
            var result = _dbContext.Categories.Remove(entity);
            _dbContext.SaveChanges();
            return result.Entity.Id;
        }

        return null;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        var categories = await _dbContext.Categories.Include(c => c.ParentCategory).ToListAsync();
        return categories;
    }

    public async Task<Category> GetById(int id)
    {
        var result = await _dbContext.Categories.FindAsync(id);

        return result!;
    }

    public async Task<int?> Update(Category entity)
    {
        if (IsCircularReference(entity.Id, entity.ParentCategoryId))
        {
            return null;
        }

        var existingEntity = _dbContext.Categories.Local.FirstOrDefault(c => c.Id == entity.Id);
        if (existingEntity != null)
        {
            _dbContext.Entry(existingEntity).State = EntityState.Detached;
        }

        var result = _dbContext.Categories.Update(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity.Id;
    }

    public int GetNestingLevel(Category category, int level = 1)
    {
        if (category.ParentCategory != null)
        {
            return GetNestingLevel(category.ParentCategory, ++level);
        }

        return level;
    }

    public int GetMoviesNumber(int id)
    {
        var result = _dbContext.Categories.Include(c => c.FilmCategories).Where(c => c.Id == id).FirstOrDefault();
        if (result != null)
        {
            return result.FilmCategories.ToArray().Length;
        }

        return -1; // If there are no films in this category
    }

    private bool IsCircularReference(int id, int? changedParentId)
    {
        var category = _dbContext.Categories.Include(c => c.SubCategories).ThenInclude(c => c.SubCategories).Where(c => c.Id == id).FirstOrDefault();
        if (category == null)
        {
            return false;
        }

        if (category.Id == changedParentId)
        {
            return true; // Has circular reference
        }

        if (category.SubCategories != null)
        {
            foreach (var subCategory in category.SubCategories)
            {
                if (subCategory.Id == changedParentId)
                {
                    return true; // Has circular reference
                }
                if (subCategory.SubCategories != null)
                {
                    foreach (var sc in subCategory.SubCategories)
                    {
                        var result = IsCircularReference(sc.Id, changedParentId);
                        if (result == true)
                        {
                            return true; // Has circular reference
                        }
                    }
                }
            }
        }
        return false;
    }
}
