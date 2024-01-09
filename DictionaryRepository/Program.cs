using DictionaryRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace DictionaryRepository
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dictList = new ProductManager();
            var list = dictList.SelectAll();
            Show(list, "All products in original order");

            var mySortedList = list.ToList();

            //Sort mySortedList pair1 = dictionary, pair2 = dictionary
            mySortedList.Sort((pair1, pair2) => pair1.Value.Name.CompareTo(pair2.Value.Name));
            Show(mySortedList.ToDictionary(pair => pair.Key, pair => pair.Value), "All products sorted by name");

            //Delete record 2
            dictList.Delete(2);
            Show(dictList.SelectAll(), "Second product gone");

            //Add record
            Product p8 = new Product() { Id = 8, Category = "Computer", Name = "Packard Bell", Price = 449.99m };

            dictList.Insert(p8);
            Show(dictList.SelectAll(), "Added 8th record");

            //Update record
            Product p4 = new Product() { Id = 4, Category = "Computer", Name = "Laptop 2.0", Price = 999.00m };
            dictList.Update(p4);
            Show(dictList.SelectAll(), "Updated laptop to 2.0");

            //Select single record
            Product p5 = dictList.SelectSingle(5);
            Console.WriteLine($"Product with id 5 is {p5.ToString()}");

            //Create two objects of Product to test Equals and Hashcode
            Product p10 = new Product() { Id = 10, Category = "Telephone", Name = "GSM", Price = 400.00m };
            Product p11 = new Product() { Id = 11, Category = "Computer", Name = "Laptop", Price = 600.00m };
            //Product p11 = new Product() { Id = 10, Category = "Telephone", Name = "GSM", Price = 400.00m };

            var equal = p10.Equals(p11);    //utilise GetNameAndPrice, compare 2 objets et non un objet avec la db
            if (equal)
            {
                Console.WriteLine("Product already exists in database");
            }
            else
            {
                Product product = new Product();
                dictList.Insert(p11);
                Console.WriteLine("Product added in database");
            }

            Show(dictList.SelectAll(), "Product already in database or added in it");

            if (dictList.Find(10))
                Console.WriteLine("Product exist in database");
            else Console.WriteLine("Product doesn't exist in database");

            Console.ReadLine();
        }

        private static void Show(Dictionary<int, Product> dictList, string parameter)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(parameter);
            Console.WriteLine(new string('-', 50));

            foreach (var item in dictList) 
            {
                Console.WriteLine(item.Key.ToString().PadRight(5) + item.Value.Name.PadRight(15) + item.Value.Price.ToString().PadRight(10) + item.Value.Category.PadRight(25));
            }
            Console.WriteLine(new string('-', 50));
        }
    }
}
