using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmManager.DAL.Models;

[Table(name: "film_categories")]
public class FilmCategory
{
    [Key]
    public int Id { get; set; }

    [Column(name:"film_id")]
    public int FilmId { get; set; }

    [Column(name:"category_id")]
    public int CategoryId { get; set; }

    public Film Film { get; set; }

    public Category Category { get; set; }
}
