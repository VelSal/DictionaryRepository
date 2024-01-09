using DictionaryRepository.Models;
using System;

namespace DictionaryRepository
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager();
            Console.WriteLine(productManager.SelectAll()); 

            Console.ReadLine();
        }
    }
}
