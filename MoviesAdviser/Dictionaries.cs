using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAdviser
{
    class Dictionaries
    {
        //Жанры tvigle.ru
        public static readonly Dictionary<string, int> tvigleGenres = new Dictionary<string, int>
        {
            {"Боевики", 4},
            {"Приключения", 14},
            {"Комедии", 10},
            {"Криминал", 12},
            {"Документальные", 7},
            {"Драмы", 8},
            {"Семейные", 15},
            {"Фантастика", 19},
            {"Исторические", 9},
            {"Ужасы", 18},
            {"Мелодрамы", 13},
            {"Военные", 5}
        };

        //Cтраны tvigle.ru
        public static readonly Dictionary<string, int> tvigleCountries = new Dictionary<string, int>
        {
            {"Россия",4},
            {"США",27},
            {"СССР",1},
            {"Австралия",30},
            {"Бельгия",36},
            {"Великобритания",23},
            {"Германия",6},
            {"Гонконг",34},
            {"Дания",31},
            {"Индия",20},
            {"Ирландия",35},
            {"Испания",32},
            {"Италия",3},
            {"Канада",37},
            {"Китай",33},
            {"Корея Южная",28},
            {"Франция",16},
            {"Швеция",12},
            {"Япония",11}
        };

        //Жанры TMDB
        public static readonly Dictionary<string, string> TMDBGenres = new Dictionary<string, string>()
        {
            { "Боевики", "28" },
            { "Приключения", "12" },
            { "Военные", "10752" },
            { "Криминал", "80" },
            { "Документальные", "99" },
            { "Драмы", "18" },
            { "Семейные", "10751" },
            { "Комедии", "35" },
            { "Ужасы", "27" },
            { "Фантастика", "14" },
            { "Исторические", "36" },
            { "Мелодрамы", "10749" },
            { "Триллер","53" }
        };
        
        //Методы сортировки TMDB
        public static readonly Dictionary<string, string> TMDBSortMethods = new Dictionary<string, string>()
        {
            { "По рейтингу","vote_average" },
            { "По количеству комментариев","popularity" }
        };
    }
}
