using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Text.Json;
using Newtonsoft.Json;


namespace ProductBase
{

    public struct Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Country(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public struct Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public struct Export
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public List<int> PositionSet { get; set; }

        public Export(int id, int country, List<int> positions)
        {
            Id = id;
            CountryId = country;
            PositionSet = positions;
        }

    }

    public struct Position
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public Position(int id, int product, int amount)
        {
            Id = id;
            ProductId = product;
            Amount = amount;
        }
    }

    static class DataLogic
    {
        private class DataBase
        {
            public List<Country> countries;
            public List<Product> products;
            public List<Export> exports;
            public List<Position> positions;

            public DataBase()
            {
            }

            public DataBase(List<Country> c, List<Product> pr, List<Export> e, List<Position> p)
            {
                countries = c;
                products = pr;
                exports = e;
                positions = p;
            }
        }

        
        public static string CurrentBaseName { get; set; }

        private static DataBase CurrentBase;

        

        static DataLogic()
        {
            CurrentBaseName = "DataBase1";

            CurrentBase = new DataBase();
        }
        

        // serialize-deserialize
        public static void Open(string name)
        {
            DirectoryInfo dir = Directory.CreateDirectory(@"Data");
            CurrentBaseName = name;

            using (StreamReader file = File.OpenText($"Data/{CurrentBaseName}.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                CurrentBase = (DataBase)serializer.Deserialize(file, typeof(DataBase));
            }
        }
        
        public static void Close()
        {
            DirectoryInfo dir = Directory.CreateDirectory(@"Data");
            
            using (StreamWriter file = File.CreateText($"Data/{CurrentBaseName}.json"))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented });
                serializer.Serialize(file, CurrentBase);
            }
        }


        // connection with bussiness logic layer
        public static List<Country> GetCountriesSet()
        {
            return CurrentBase.countries;
        }

        public static List<Product> GetProductsSet()
        {
            return CurrentBase.products;
        }

        public static List<Export> GetExportsSet()
        {
            return CurrentBase.exports;
        }

        public static List<Position> GetPositionsSet()
        {
            return CurrentBase.positions;
        }

        public static void SetCountries(List<Country> toSet)
        {
            CurrentBase.countries = toSet;
        }

        public static void SetProducts(List<Product> toSet)
        {
            CurrentBase.products = toSet;
        }

        public static void SetExports(List<Export> toSet)
        {
            CurrentBase.exports = toSet;
        }

        public static void SetPositions(List<Position> toSet)
        {
            CurrentBase.positions = toSet;
        }
    }
}
