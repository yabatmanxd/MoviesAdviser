using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAdviser
{
    class Dictionaries
    {
        //Жанры 2hdkino.vip
        public static Dictionary<string, int> hdkinoGenres = new Dictionary<string, int>
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

        //Cтраны 2hdkino.vip
        public static Dictionary<string, int> hdkinoCountries = new Dictionary<string, int>
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
    }
}
