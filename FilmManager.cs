using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloCSharp
{
    public class FilmManager
    {



        public List<Film> GetFilms()
        {
            List<Film> films = new List<Film>();


            using (var webClient = new System.Net.WebClient())
            {
                string result = webClient.DownloadString("https://www.imdb.com/chart/top/");

                string tBody = "<tbody class=\"lister-list\">";
                int tbodyStartIndex = result.IndexOf(tBody);

                int tBodyEndIndex = result.IndexOf("</tbody>");


                int tbodyLength = result.Length - tbodyStartIndex;

                string tBodyContent = result.Substring(tbodyStartIndex + tBody.Length, (tBodyEndIndex - tbodyStartIndex));

                string[] trList = tBodyContent.Split("</tr>");


                int id = 1;

                foreach (var item in trList)
                {
                    Film film = new Film();
                    film.Id = id;
                    Random rnd = new Random();
                    decimal price = Convert.ToDecimal(rnd.Next(100, 1000));

                    film.Price = price;

                    id++;
                    string[] tdList = item.Split("</td>");

                   
                    if (tdList.Length > 3)
                    {
                        for (int i = 0; i < tdList.Length; i++)
                        {
                            //i == 1 se yani 2. hücreye geçtiysem ( Film ismini oradan alacağım.)
                            if (i == 1)
                            {
                                string tdContent = tdList[i];

                                int parantezIndex = tdContent.LastIndexOf('(');

                                string year = tdContent.Substring(parantezIndex + 1, 4);

                                int aEnd = tdContent.IndexOf("</a>");

                                string filmSubContent = tdContent.Substring(0, aEnd);
                                int upperLastIndex = filmSubContent.LastIndexOf(">") + 1;


                                string filmTitle = filmSubContent.Substring(upperLastIndex, filmSubContent.Length - upperLastIndex);



                                film.Name = filmTitle;
                                film.Year = Convert.ToInt32(year);

                        
                            }

                            else if (i == 2)
                            {
                                string tdContent = tdList[i];

                                int startIndex = tdContent.IndexOf("ratings\">");

                                string rating = tdContent.Substring(startIndex + 9, 3);

                                film.Rating = Convert.ToDouble(rating);

                         

                            }
                        }

                        films.Add(film);
                    }
                }

            }

            return films;


        }

        //Dışarıdan bir film alan ve bu filmi mevcut listeye ekleyen metot.
        public void Add(Film film)
        {
            var films = GetFilms();
            films.Add(film);

            //Filmi 3 numaralı index e ekliyor.
            //films.Insert(3, film);
        }

        //Dışardan bir id alan ve bu id ye ait filmi listeden çıkaran metot
        public void RemoveFilmById(int id)
        {
            var films = GetFilms();

            Film removeFilm = films.FirstOrDefault(q => q.Id == id);

            films.Remove(removeFilm);
        }

        //Dışarıdan ID alan ve bu id ye göre FİLMİ bana veren metot
        public Film GetFilmById(int id)
        {
            var films = GetFilms();

            Film film = films.FirstOrDefault(q => q.Id == id);

            return film;
        }


        //Dışarıdan yıl alan vu bu yıldaki FİLMLERİ bana veren metot
        public List<Film> GetFilmsByYear(int year)
        {
            var films = GetFilms();

            var filteredFilms = films.Where(q => q.Year == year).ToList();

            return filteredFilms;

        }


        //Dışarıdan film adını alan ve İÇERİSİNDE bu film ismi geçen FİLMLERİ bana veren metot
        public List<Film> GetFilmsByName(string name)
        {
            var films = GetFilms();

            List<Film> filmList = films.Where(q => q.Name.ToLower().Contains(name)).ToList();


            return filmList;

        }


        //Dışarıdan yıl alan ve bu yıla ait KAÇ FİLM olduğunu bana söyleyen metot
        public int GetFilmsCountByYear(int year)
        {
            var films = GetFilms();
            int filmsCount =  films.Where(q => q.Year == year).Count();

            return filmsCount;
        }


        //Dışarıdan adet alan ve o miktarda film listesi dönen metot. Yani dışarıdan 20 aldığında 20 adet film dönecek.
        public List<Film> GetFilmsByCount(int count)
        {
            var films = GetFilms();
            var filmList = films.Take(count).ToList();

            return filmList;

        }


        //Dışarıdan yıl aralığı yani 2 yıl alacak ve bu aralıklarda kaç adet film çekilmiş bana dönsün
        public int GetFilmsCountByYearRange(int minYear, int maxYear)
        {

            var films = GetFilms();

            int count = films.Where(q => q.Year > minYear && q.Year < maxYear).Count();

            return count;
        }


        //Dışarıdan bir harf alacak ve o harfle başlayan filmleri bana dönecek
        public List<Film> GetFilmsByStartChar(char harf)
        {
            var films = GetFilms();

            List<Film> filteredFilms = films.Where(q => q.Name.StartsWith(harf)).ToList();

            return filteredFilms;
        }


        // Dışardan bir film adı alacak o FİLMİ bana dönecek metot
        public Film GetFilmByName(string name)
        {
            var films = GetFilms();

            Film film = films.FirstOrDefault(q => q.Name.ToLower() == name.ToLower());

            return film;
           
        }


        //Dışarıdan bir film adı alan ve bu film listede Var mı onu söyleyen metot
        public bool HasFilmInList(string name)
        {
            var films = GetFilms();

            // 1. yol
            //Film film = films.FirstOrDefault(q => q.Name.ToLower() == name.ToLower());

            //if (film == null)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}

            // 2.yol
            bool filmVarMi = films.Any(q => q.Name.ToLower() == name.ToLower());

            return filmVarMi;

        }


        //Filmleri ADA GÖRE sıralayıp bana filmleri dönen metot
        public List<Film> OrderByFilms()
        {
            var films = GetFilms();

            var orderedFilms = films.OrderBy(q => q.Name).ToList();

            return orderedFilms;
        }

        // Dışarıdan bir film adı alan ve bu filmin hangi yıla ait olduğunu bana dönen metot
        public int GetYearByFilmName(string name)
        {
            var films = GetFilms();

            Film film = films.FirstOrDefault(q => q.Name == name);

            return film.Year;
            
        }

        // Dışarıdan yıl alan ve o yıla ait filmleri bana veren metot
        public List<Film> GetFilmsByYear2(int year)
        {
            var films = GetFilms();
            List<Film> filteredFilms = films.Where(x => x.Year == year).ToList();

            return filteredFilms;
        }


        //Dışarıdan 2 yıl alan ve o 2 yıl arasındaki filmleri bana veren metot
        public List<Film> GetFilmsByYearDifference(int startYear, int endYear)
        {
            var films = GetFilms();

            List<Film> filteredFilms = films.Where(q => q.Year >= startYear && q.Year <= endYear).ToList();

            return filteredFilms;
        }

        // Filmlerin rating ortalaması bana veren metot?
        public double GetAvgRatingAllFilms()
        {
            var films = GetFilms();

            var avgRating = films.Average(q => q.Rating);

            return avgRating;

        }

        //İlk 10 filmin puan ortalaması nedir?
        public double GetAvgTopTenFilms()
        {
            var films = GetFilms();

            var avgRating = films.Take(10).Average(q => q.Rating);

            return avgRating;
        }


        //İlk 20 ile 30 arasındaki filmlerin ortalaması nedir?
        public double GetAvgFilms2()
        {
            var films = GetFilms();

            double avgRating = films.Skip(20).Take(10).Average(q => q.Rating);

            return avgRating;
        }

        //Puanı en düşük filmin ADINI veren metot
        public string GetFilmNameMinRating()
        {
            var films = GetFilms();

            var orderedFilms = films.OrderBy(q => q.Rating).ToList();
            Film film = orderedFilms[0];


            return film.Name;

        }

        // Dışarıdan aldığı yıla göre o YILIN EN BAŞARILI FİLMİNİ BANA DÖNEN METOT
        public Film GetTopFilmByYear(int year)
        {
            var films = GetFilms();

            Film film = films.Where(x => x.Year == year).OrderByDescending(x => x.Rating).ToArray()[0];

            return film;

        }


        //Son 5 senenin en iyi FİLMLERİNİ bana ver
        public List<Film> GetTopFilmsFiveYear()
        {
            var films = GetFilms();

            int nowYear = DateTime.Now.Year;

            List<Film> filteredFilms = films.Where(x => x.Year >= nowYear - 5).OrderByDescending(x => x.Rating).ToList();

            return filteredFilms;
        }




        public void PrintFilmName(string name)
        {
            Console.WriteLine(name);
        }

        void Delete(string name)
        {

        }

    }
}
