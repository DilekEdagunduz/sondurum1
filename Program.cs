using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HelloCSharp.Models;

namespace HelloCSharp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            

            List<Product> products = new List<Product>();
            List<Category> categories = new List<Category>();
            List<Order> orders = new List<Order>();
            List<Supplier> suppliers = new List<Supplier>();
            List<Todo> todos = new List<Todo>();



            using (var httpClient = new HttpClient())
            {
            //    var streamTask = httpClient.GetStreamAsync("https://northwind.vercel.app/api/products");
            //    products = await JsonSerializer.DeserializeAsync<List<Product>>(await streamTask);


                var streamTaskCategories = httpClient.GetStreamAsync("https://northwind.vercel.app/api/categories");
                categories = await JsonSerializer.DeserializeAsync<List<Category>>(await streamTaskCategories);


                var streamTaskOrders = httpClient.GetStreamAsync("https://northwind.vercel.app/api/orders");
                orders = await JsonSerializer.DeserializeAsync<List<Order>>(await streamTaskOrders);

                var streamTaskSuppliers = httpClient.GetStreamAsync("https://northwind.vercel.app/api/suppliers");
                suppliers = await JsonSerializer.DeserializeAsync<List<Supplier>>(await streamTaskSuppliers);


                var streamTaskTodos = httpClient.GetStreamAsync("https://jsonplaceholder.typicode.com/todos");
                todos = await JsonSerializer.DeserializeAsync<List<Todo>>(await streamTaskTodos);


                foreach (var item in orders)
                {

                    string[] dates = item.orderDate.Split('-');
                    int year = Convert.ToInt32(dates[0]);
                    int month = Convert.ToInt32(dates[1]);
                    int day = Convert.ToInt32(dates[2].Substring(0,2));

                    DateTime dt = new DateTime(year, month, day);


                    if(item.shippedDate != "NULL")
                    {
                        string[] dates2 = item.shippedDate.Split('-');
                        int year2 = Convert.ToInt32(dates2[0]);
                        int month2 = Convert.ToInt32(dates2[1]);
                        int day2 = Convert.ToInt32(dates2[2].Substring(0, 2));
                        DateTime dt2 = new DateTime(year2, month2, day2);
                        item.ShippedDate = dt2;
                    }

                    if (item.requiredDate != "NULL")
                    {
                        string[] dates3 = item.orderDate.Split('-');
                        int year3 = Convert.ToInt32(dates3[0]);
                        int month3 = Convert.ToInt32(dates3[1]);
                        int day3 = Convert.ToInt32(dates3[2].Substring(0, 2));

                        DateTime dt3 = new DateTime(year3, month3, day3);

                        item.RequiredDate = dt3;

                    }

                    item.OrderDate = dt;

                }

            }





            // TODO servis soruları

            // UserId si 2 olan kaç TODO var?
            var UserID = todos.Where(q => q.userId == 2).Count();
            Console.WriteLine("UserId si 2 olan kaç TODO var?======>>>"+UserID);
            // Id si 1 olan TODO nun title ını ekrana yazdır
            var ID1 = todos.FirstOrDefault(q => q.id == 1);
            Console.WriteLine("Id si 1 olan TODO nun title ını ekrana yazdır======>>>" + ID1.title);
            // title f harfi ile başlayan kaç TODO var?
            var titleFharf = todos.Where(q => q.title.ToLower().StartsWith("f")).Count();
            Console.WriteLine("title f harfi ile başlayan kaç TODO var?======>>>" + titleFharf);
            // title a harfi ile biten kaç TODO var?
            var titleAharf = todos.Where(q => q.title.ToLower().EndsWith("a")).Count();
            Console.WriteLine("title f harfi ile başlayan kaç TODO var?======>>>" + titleAharf);


            Console.WriteLine(" _____________________________Supplier Ek Sorular_______________________________");

            // joponyoda kaç tedarikçim var
            var Supplier = suppliers.Where(q => q.address.country == "Japan").Count();
            Console.WriteLine("joponyoda kaç tedarikçim var======>>>" + Supplier);
            // joponyadaki tedarikçileri adlarını konsola yazdır
            List<Supplier> japanSuppliersName = suppliers.Where(x => x.address.country.ToLower() == "japan").ToList();

            foreach (var item in japanSuppliersName) Console.WriteLine("joponyadaki tedarikçileri adlarını konsola yazdır======>>>" + item.contactName);
            // Tokyada kaç tedarikçi var
            var SupplierTokyoCount = suppliers.Where(q => q.address.city == "Tokyo").Count();
            Console.WriteLine("Tokyada kaç tedarikçi var======>>>" + SupplierTokyoCount);
            // Japonyadaki tedarikçilerin içerisnde "a" harfi geçenlerin listesi
            //var JaponHarf = suppliers.Where(q => q.address.country.ToLower().Contains("a")=="Japan").ToList();
            List<Supplier> tokyoAchar = suppliers.Where(x => x.address.country != null && x.address.country.ToLower() == "japan" && x.contactName.Contains("a")).ToList();
            Console.WriteLine("Japonyadaki tedarikçilerin içerisnde \"a\" harfi geçenlerin listesi?======>>>" + tokyoAchar);
            // En çok hangi ülkede tedarikçi var (group by ile yapmışlar)
            var bestSupplier = suppliers.GroupBy(x => x.address.country).OrderByDescending(x => x.Count()).ToList();

            Console.WriteLine(bestSupplier[0].Key);

            // en fazla tedarikçim olduğu ülkedeki tedarikçilerin toplama oranı yüzdesi
            double bestCount = (bestSupplier[0].Count());
            double suppliersCount = suppliers.Count;

            double avr = bestCount / suppliersCount;

            Console.WriteLine("% " + avr * 100.0);
            // Almanyadaki son tedarikçinin adı nedir?
            //var almanyasontedarik = from sontedarik in suppliers
            //                        where sontedarik.address.country== "Germany"
            //                        select new
            //                        {
            //                            tedarik=sontedarik.address.country,


            //                        }
            var germanSuppliers = suppliers.Where(x => x.address.country != null && x.address.country.ToLower() == "germany").OrderByDescending(x => x.id).ToList();

            Console.WriteLine(germanSuppliers[0].contactName);
        

    }



    }
}

