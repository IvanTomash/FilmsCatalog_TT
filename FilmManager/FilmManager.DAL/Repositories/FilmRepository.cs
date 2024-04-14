using FilmManager.DAL.Models;
using FilmManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManager.DAL.Repositories;

public class FilmRepository : IBaseRepository<Film>
{
    private readonly ApplicationDbContext _dbContext;

    public FilmRepository(ApplicationDbContext dbContext) 
    {
        _dbContext = dbContext;
    }

    public async Task<int?> Create(Film entity)
    {
        var result = await _dbContext.Films.AddAsync(entity);
        _dbContext.SaveChanges();
        return result.Entity.Id;
    }

    public async Task<int?> Delete(int id)
    {
        var entity = await _dbContext.Films.FindAsync(id);
        if (entity != null)
        {
            var result = _dbContext.Films.Remove(entity);
            _dbContext.SaveChanges();
            return result.Entity.Id;
        }

        return null; ;
    }

    public async Task<IEnumerable<Film>> GetAll()
    {
        var result = await _dbContext.Films.Include(fi => fi.FilmCategories).ThenInclude(c => c.Category).ToListAsync();
        return result;
    }

    public async Task<Film> GetById(int id)
    {
        var result = await _dbContext.Films.FindAsync(id);
        return result!;
    }

    public async Task<int?> Update(Film entity)
    {
        var result = _dbContext.Films.Update(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity.Id;
    }
}
