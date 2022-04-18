using System;
namespace HelloCSharp
{

    //Entity, POCO
    //Bir class içerisinde sadece metot barındırabilir veya class içerisinde bir ŞABLON BARINDIRABİLİR. (DESIGN)
    public class Film
    {



        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public double Rating { get; set; }

        public decimal Price { get; set; }


    }
}
