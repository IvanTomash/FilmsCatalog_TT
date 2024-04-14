using FilmManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManager.DAL.Repositories.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    int GetNestingLevel(Category category, int level = 1);
    int GetMoviesNumber(int id);
}
