using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MoviesAdviser.Models
{
    public class Movie
    {       
        //Название
        public string Title { get; set; }

        //Постер фильма - картинка
        public string Poster { get; set; } 

        //Страна
        public string Country { get; set; }

        //Год выпуска
        public int Year { get; set; }

        //Средний рейтинг (будет получен от Analyzer, в конструкторе не инициализируется)
        public  double Rating { get; set; }

        public string Genre { get; set; }

        // ссылка на страницу с фильмом (из неё придётся вытягивать описание)
        public string Link { get; set; }

        public string Description { get; set; }
        
        public Movie()
        {

        }

        public Movie (string title, string country, int year)
        {
            Title = title;
            Country = country;
            Year = year;
        }

        public Movie(string title, string country, int year, string genre)
        {
            Title = title;
            Country = country;
            Year = year;
            Genre = genre;
        }

        public int getLevelRating
        {
            get
            {
                if (this.Rating < 4)
                {
                    return 0;
                }  
                else
                {
                    if (this.Rating >= 4 && this.Rating < 7.5)
                    {
                        return 1;
                    }    
                    else
                    {
                        return 2;
                    }
                }
            }
        }        
    }
}
