using FilmManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManager.DAL.Repositories.Interfaces
{
    public interface IFilmCategoryRepository
    {
        Task<int?> Create(FilmCategory entity);
        IEnumerable<Category> GetAvailableCategoriesForFilm(int filmId);
        IEnumerable<Category> GetSelectedCategoriesForFilm(int filmId);
        int? Remove(int filmId, int categoryId);
    }
}
