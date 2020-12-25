using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBase
{
    static class BusinessLogic
    {
        // вывод информации (select)

        public static List<List<string>> SelectCountries()
        {
            List<Country> countries = DataLogic.GetCountriesSet();
            List<List<string>> toRet = new List<List<string>>();

            toRet.Add(new List<string>() { "Id", "Name" });

            foreach(Country c in countries)
            {
                toRet.Add(new List<string>() { c.Id.ToString(), c.Name });
            }

            return toRet;
        }

        public static List<List<string>> SelectProducts()
        {
            List<Product> products = DataLogic.GetProductsSet();
            List<List<string>> toRet = new List<List<string>>();

            toRet.Add(new List<string>() { "Id", "Name" });

            foreach (Product p in products)
            {
                toRet.Add(new List<string>() { p.Id.ToString(), p.Name });
            }

            return toRet;
        }

        public static List<List<string>> SelectExports()
        {
            List<Export> exports = DataLogic.GetExportsSet();
            List<Country> countries = DataLogic.GetCountriesSet();
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Product> products = DataLogic.GetProductsSet();


            List<List<string>> toRet = new List<List<string>>();
            toRet.Add(new List<string>() { "Id", "Country", "positionsAmount" });

            int maxPos = 0;
            foreach(Export e in exports)
            {
                string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                toRet.Add(new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() });
                if(e.PositionSet.Count > maxPos)
                {
                    maxPos = e.PositionSet.Count;
                }

                foreach (int pos in e.PositionSet)
                {
                    Position position = positions.Find(pp => pp.Id == pos);
                    toRet.Last().Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                }

                
            }
            
            for(int i = 1; i <= maxPos; i++)
            {
                toRet.First().Add($"position#{i}");
            }

            return toRet;
        }

        public static List<List<string>> SelectPositions()
        {
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Product> products = DataLogic.GetProductsSet();


            List<List<string>> toRet = new List<List<string>>();
            toRet.Add(new List<string>() { "Id", "Product", "Amount" });

            foreach(Position p in positions)
            {
                string name = products.Find(pr => pr.Id == p.ProductId).Name;
                toRet.Add(new List<string>() { p.Id.ToString(), name, p.Amount.ToString() });
            }
            
            return toRet;
        }


        public static List<List<string>> SelectCountriesWithName(string condition, int x)
        {
            List<Country> countries = DataLogic.GetCountriesSet();
            List<List<string>> toRet = new List<List<string>>();

            toRet.Add(new List<string>() { "Id", "Name" });

            if (x == 0)     // 'is'
            {
                foreach (Country c in countries)
                {
                    if (c.Name.ToLower() == condition.ToLower())        // <--
                    {
                        toRet.Add(new List<string>() { c.Id.ToString(), c.Name });
                    }
                }
            }
            if (x == 1)     // 'start with'
            {
                foreach (Country c in countries)
                {
                    if (c.Name.ToLower().StartsWith(condition.ToLower()))       // <--
                    {
                        toRet.Add(new List<string>() { c.Id.ToString(), c.Name });
                    }
                }
            }
            if (x == 2)     // 'end with'
            {
                foreach (Country c in countries)
                {
                    if (c.Name.ToLower().EndsWith(condition.ToLower()))             // <--
                    {
                        toRet.Add(new List<string>() { c.Id.ToString(), c.Name });
                    }
                }
            }

            return toRet;
        }

        public static List<List<string>> SelectProductsWithName(string condition, int x)
        {
            List<Product> products = DataLogic.GetProductsSet();
            List<List<string>> toRet = new List<List<string>>();

            toRet.Add(new List<string>() { "Id", "Name" });

            if (x == 0)     // 'is'
            {
                foreach (Product p in products)
                {
                    if (p.Name.ToLower() == condition.ToLower())            // <--
                    {
                        toRet.Add(new List<string>() { p.Id.ToString(), p.Name });
                    }
                }
            }
            if (x == 1)     // 'start with'
            {
                foreach (Product p in products)
                {
                    if (p.Name.ToLower().StartsWith(condition.ToLower()))           // <--
                    {
                        toRet.Add(new List<string>() { p.Id.ToString(), p.Name });
                    }
                }
            }
            if (x == 2)     // 'end with'
            {
                foreach (Product p in products)
                {
                    if (p.Name.ToLower().EndsWith(condition.ToLower()))             // <--
                    {
                        toRet.Add(new List<string>() { p.Id.ToString(), p.Name });
                    }
                }
            }

            return toRet;
        }

        public static List<List<string>> SelectPositionsWithName(string condition, int x)
        {
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Product> products = DataLogic.GetProductsSet();
            List<List<string>> toRet = new List<List<string>>();
            List<int> id = new List<int>();

            toRet.Add(new List<string>() { "Id", "Product", "Amount" });

            if (x == 0)     // 'is'
            {
                foreach (Product p in products)
                {
                    if (p.Name.ToLower() == condition.ToLower())            // <--
                    {
                        id.Add(p.Id);
                    }
                }
            }
            if (x == 1)     // 'start with'
            {
                foreach (Product p in products)
                {
                    if (p.Name.ToLower().StartsWith(condition.ToLower()))           // <--
                    {
                        id.Add(p.Id);
                    }
                }
            }
            if (x == 2)     // 'end with'
            {
                foreach (Product p in products)
                {
                    if (p.Name.ToLower().EndsWith(condition.ToLower()))             // <--
                    {
                        id.Add(p.Id);
                    }
                }
            }
            
            foreach (Position p in positions)
            {
                if (id.Contains(p.ProductId))
                {
                    string name = products.Find(pr => pr.Id == p.ProductId).Name;
                    toRet.Add(new List<string>() { p.Id.ToString(), name, p.Amount.ToString() });
                }
            }

            return toRet;
        }

        public static List<List<string>> SelectPositionsWithAmount(int condition, int x)
        {
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Product> products = DataLogic.GetProductsSet();
            List<List<string>> toRet = new List<List<string>>();

            toRet.Add(new List<string>() { "Id", "Product", "Amount" });

            if (x == 0)     // '=='
            {
                foreach (Position p in positions)
                {
                    if (p.Amount == condition)                  // <--
                    {
                        string name = products.Find(pr => pr.Id == p.ProductId).Name;
                        toRet.Add(new List<string>() { p.Id.ToString(), name, p.Amount.ToString() });
                    }
                }
            }
            if (x == 1)     // '>='
            {
                foreach (Position p in positions)
                {
                    if (p.Amount >= condition)                  // <--
                    {
                        string name = products.Find(pr => pr.Id == p.ProductId).Name;
                        toRet.Add(new List<string>() { p.Id.ToString(), name, p.Amount.ToString() });
                    }
                }
            }
            if (x == 2)     // '<='
            {
                foreach (Position p in positions)
                {
                    if (p.Amount <= condition)                  // <--
                    {
                        string name = products.Find(pr => pr.Id == p.ProductId).Name;
                        toRet.Add(new List<string>() { p.Id.ToString(), name, p.Amount.ToString() });
                    }
                }
            }
            if (x == 3)     // '>'
            {
                foreach (Position p in positions)
                {
                    if (p.Amount > condition)                   // <--
                    {
                        string name = products.Find(pr => pr.Id == p.ProductId).Name;
                        toRet.Add(new List<string>() { p.Id.ToString(), name, p.Amount.ToString() });
                    }
                }
            }
            if (x == 4)     // '<'
            {
                foreach (Position p in positions)
                {
                    if (p.Amount < condition)                   // <--
                    {
                        string name = products.Find(pr => pr.Id == p.ProductId).Name;
                        toRet.Add(new List<string>() { p.Id.ToString(), name, p.Amount.ToString() });
                    }
                }
            }
            if (x == 5)     // '!='
            {
                foreach (Position p in positions)
                {
                    if (p.Amount != condition)                  // <--
                    {
                        string name = products.Find(pr => pr.Id == p.ProductId).Name;
                        toRet.Add(new List<string>() { p.Id.ToString(), name, p.Amount.ToString() });
                    }
                }
            }




            return toRet;
        }

        public static List<List<string>> SelectExportsWithCountry(string condition, int x)
        {
            List<Export> exports = DataLogic.GetExportsSet();
            List<Country> countries = DataLogic.GetCountriesSet();
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Product> products = DataLogic.GetProductsSet();


            List<List<string>> toRet = new List<List<string>>();
            toRet.Add(new List<string>() { "Id", "Country", "positionsAmount" });

            int maxPos = 0;

            if (x == 0)     // 'is'
            {
                foreach (Export e in exports)
                {
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    if (name.ToLower() == condition.ToLower())                       // <--
                    {
                        toRet.Add(new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() });
                        if (e.PositionSet.Count > maxPos)
                        {
                            maxPos = e.PositionSet.Count;
                        }

                        foreach (int pos in e.PositionSet)
                        {
                            Position position = positions.Find(pp => pp.Id == pos);
                            toRet.Last().Add($"{products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                        }
                    }

                }
            }
            if (x == 1)     // 'start with'
            {
                foreach (Export e in exports)
                {
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    if (name.ToLower().StartsWith(condition.ToLower()))                 // <--
                    {
                        toRet.Add(new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() });
                        if (e.PositionSet.Count > maxPos)
                        {
                            maxPos = e.PositionSet.Count;
                        }

                        foreach (int pos in e.PositionSet)
                        {
                            Position position = positions.Find(pp => pp.Id == pos);
                            toRet.Last().Add($"{products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                        }
                    }

                }
            }
            if (x == 2)     // 'end with'
            {
                foreach (Export e in exports)
                {
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    if (name.ToLower().EndsWith(condition.ToLower()))                   // <--
                    {
                        toRet.Add(new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() });
                        if (e.PositionSet.Count > maxPos)
                        {
                            maxPos = e.PositionSet.Count;
                        }

                        foreach (int pos in e.PositionSet)
                        {
                            Position position = positions.Find(pp => pp.Id == pos);
                            toRet.Last().Add($"{products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                        }
                    }

                }
            }


            for (int i = 1; i <= maxPos; i++)
            {
                toRet.First().Add($"position#{i}");
            }

            return toRet;
        }

        public static List<List<string>> SelectExportsWithProduct(string condition, int x, out string total)
        {
            List<Export> exports = DataLogic.GetExportsSet();
            List<Country> countries = DataLogic.GetCountriesSet();
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Product> products = DataLogic.GetProductsSet();

            int totalValue = 0;
            List<List<string>> toRet = new List<List<string>>();
            toRet.Add(new List<string>() { "Id", "Country", "positionsAmount" });

            int maxPos = 0;

            if (x == 0)     // 'is'
            {
                foreach (Export e in exports)
                {
                    int currPos = 0;
                    int totalToCountry = 0;
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };
                    bool isToAdd = false;
                    

                    foreach (int pos in e.PositionSet)
                    {
                        Position position = positions.Find(pp => pp.Id == pos);
                        string posName = products.Find(pr => pr.Id == position.ProductId).Name;
                        if (posName.ToLower() == condition.ToLower())                               // <--
                        {
                            totalValue += position.Amount;
                            totalToCountry += position.Amount;
                            toAdd.Add($"[{position.Id}] {posName} ({position.Amount})");
                            isToAdd = true;
                            currPos++;
                        }
                    }

                    if (isToAdd)
                    {
                        if (currPos > maxPos)
                        {
                            maxPos = currPos;
                        }
                        toAdd.Add($"total: ({totalToCountry})");
                        toRet.Add(toAdd);
                    }
                }
            }
            if (x == 1)     // 'start with'
            {
                foreach (Export e in exports)
                {
                    int currPos = 0;
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };
                    bool isToAdd = false;


                    foreach (int pos in e.PositionSet)
                    {
                        Position position = positions.Find(pp => pp.Id == pos);
                        string posName = products.Find(pr => pr.Id == position.ProductId).Name;
                        if (posName.ToLower().StartsWith(condition.ToLower()))                              // <--
                        {
                            toAdd.Add($"[{position.Id}] {posName} ({position.Amount})");
                            isToAdd = true;
                            currPos++;
                        }
                    }

                    if (isToAdd)
                    {
                        if (currPos > maxPos)
                        {
                            maxPos = currPos;
                        }
                        toRet.Add(toAdd);
                    }
                }
            }
            if (x == 2)     // 'end with'
            {
                foreach (Export e in exports)
                {
                    int currPos = 0;
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };
                    bool isToAdd = false;


                    foreach (int pos in e.PositionSet)
                    {
                        Position position = positions.Find(pp => pp.Id == pos);
                        string posName = products.Find(pr => pr.Id == position.ProductId).Name;
                        if (posName.ToLower().EndsWith(condition.ToLower()))                                // <--
                        {
                            toAdd.Add($"[{position.Id}] {posName} ({position.Amount})");
                            isToAdd = true;
                        }
                    }

                    if (isToAdd)
                    {
                        if (currPos > maxPos)
                        {
                            maxPos = currPos;
                        }
                        toRet.Add(toAdd);
                    }
                }
            }



            for (int i = 1; i <= maxPos; i++)
            {
                toRet.First().Add($"position#{i}");
            }
            if(x == 0)
            {
                toRet.First().Add("total to country");
            }

            total = totalValue.ToString();
            return toRet;
        }

        public static List<List<string>> SelectExportsWithCount(int condition, int x)
        {
            List<Export> exports = DataLogic.GetExportsSet();
            List<Country> countries = DataLogic.GetCountriesSet();
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Product> products = DataLogic.GetProductsSet();


            List<List<string>> toRet = new List<List<string>>();
            toRet.Add(new List<string>() { "Id", "Country", "positionsAmount" });

            int maxPos = 0;

            if (x == 0)     // '=='
            {
                foreach (Export e in exports)
                {
                    if (e.PositionSet.Count == condition)                               // <--
                    {
                        string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                        List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };


                        foreach (int pos in e.PositionSet)
                        {
                            Position position = positions.Find(pp => pp.Id == pos);
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");                            
                        }
                       
                        if (e.PositionSet.Count > maxPos)
                        {
                            maxPos = e.PositionSet.Count;
                        }
                        toRet.Add(toAdd);                       
                    }
                }
            }
            if (x == 1)     // '>='
            {
                foreach (Export e in exports)
                {
                    if (e.PositionSet.Count >= condition)                               // <--
                    {
                        string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                        List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };


                        foreach (int pos in e.PositionSet)
                        {
                            Position position = positions.Find(pp => pp.Id == pos);
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                        }

                        if (e.PositionSet.Count > maxPos)
                        {
                            maxPos = e.PositionSet.Count;
                        }
                        toRet.Add(toAdd);
                    }
                }
            }
            if (x == 2)     // '<='
            {
                foreach (Export e in exports)
                {
                    if (e.PositionSet.Count <= condition)                                   // <--
                    {
                        string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                        List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };


                        foreach (int pos in e.PositionSet)
                        {
                            Position position = positions.Find(pp => pp.Id == pos);
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                        }

                        if (e.PositionSet.Count > maxPos)
                        {
                            maxPos = e.PositionSet.Count;
                        }
                        toRet.Add(toAdd);
                    }
                }
            }
            if (x == 3)     // '>'
            {
                foreach (Export e in exports)
                {
                    if (e.PositionSet.Count > condition)                                    // <--
                    {
                        string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                        List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };


                        foreach (int pos in e.PositionSet)
                        {
                            Position position = positions.Find(pp => pp.Id == pos);
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                        }

                        if (e.PositionSet.Count > maxPos)
                        {
                            maxPos = e.PositionSet.Count;
                        }
                        toRet.Add(toAdd);
                    }
                }
            }
            if (x == 4)     // '<'
            {
                foreach (Export e in exports)
                {
                    if (e.PositionSet.Count < condition)                                    // <--
                    {
                        string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                        List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };


                        foreach (int pos in e.PositionSet)
                        {
                            Position position = positions.Find(pp => pp.Id == pos);
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                        }

                        if (e.PositionSet.Count > maxPos)
                        {
                            maxPos = e.PositionSet.Count;
                        }
                        toRet.Add(toAdd);
                    }
                }
            }
            if (x == 5)     // '!='
            {
                foreach (Export e in exports)
                {
                    if (e.PositionSet.Count != condition)                                   // <--
                    {
                        string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                        List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };


                        foreach (int pos in e.PositionSet)
                        {
                            Position position = positions.Find(pp => pp.Id == pos);
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                        }

                        if (e.PositionSet.Count > maxPos)
                        {
                            maxPos = e.PositionSet.Count;
                        }
                        toRet.Add(toAdd);
                    }
                }
            }


            for (int i = 1; i <= maxPos; i++)
            {
                toRet.First().Add($"position#{i}");
            }

            return toRet;
        }

        public static List<List<string>> SelectExportsWithAmount(int condition, int x)
        {
            List<Export> exports = DataLogic.GetExportsSet();
            List<Country> countries = DataLogic.GetCountriesSet();
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Product> products = DataLogic.GetProductsSet();


            List<List<string>> toRet = new List<List<string>>();
            toRet.Add(new List<string>() { "Id", "Country", "positionsAmount" });

            int maxPos = 0;

            if (x == 0)     // '=='
            {
                foreach (Export e in exports)
                {
                    bool isToAdd = false;
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };
                    int currPos = 0;

                    foreach (int pos in e.PositionSet)
                    {
                        Position position = positions.Find(pp => pp.Id == pos);
                        if (position.Amount == condition)                               // <--
                        {
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                            isToAdd = true;
                            currPos++;
                        }
                    }

                    if (currPos > maxPos)
                    {
                        maxPos = currPos;
                    }
                    if (isToAdd)
                    {
                        toRet.Add(toAdd);
                    }
                    
                }
            }
            if (x == 1)     // '>='
            {
                foreach (Export e in exports)
                {
                    bool isToAdd = false;
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };
                    int currPos = 0;

                    foreach (int pos in e.PositionSet)
                    {
                        Position position = positions.Find(pp => pp.Id == pos);
                        if (position.Amount >= condition)                               // <--
                        {
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                            isToAdd = true;
                            currPos++;
                        }
                    }

                    if (currPos > maxPos)
                    {
                        maxPos = currPos;
                    }
                    if (isToAdd)
                    {
                        toRet.Add(toAdd);
                    }

                }
            }
            if (x == 2)     // '<='
            {
                foreach (Export e in exports)
                {
                    bool isToAdd = false;
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };
                    int currPos = 0;

                    foreach (int pos in e.PositionSet)
                    {
                        Position position = positions.Find(pp => pp.Id == pos);
                        if (position.Amount <= condition)                               // <--
                        {
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                            isToAdd = true;
                            currPos++;
                        }
                    }

                    if (currPos > maxPos)
                    {
                        maxPos = currPos;
                    }
                    if (isToAdd)
                    {
                        toRet.Add(toAdd);
                    }

                }
            }
            if (x == 3)     // '>'
            {
                foreach (Export e in exports)
                {
                    bool isToAdd = false;
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };
                    int currPos = 0;

                    foreach (int pos in e.PositionSet)
                    {
                        Position position = positions.Find(pp => pp.Id == pos);
                        if (position.Amount > condition)                            // <--
                        {
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                            isToAdd = true;
                            currPos++;
                        }
                    }

                    if (currPos > maxPos)
                    {
                        maxPos = currPos;
                    }
                    if (isToAdd)
                    {
                        toRet.Add(toAdd);
                    }

                }
            }
            if (x == 4)     // '<'
            {
                foreach (Export e in exports)
                {
                    bool isToAdd = false;
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };
                    int currPos = 0;

                    foreach (int pos in e.PositionSet)
                    {
                        Position position = positions.Find(pp => pp.Id == pos);
                        if (position.Amount < condition)                            // <--
                        {
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                            isToAdd = true;
                            currPos++;
                        }
                    }

                    if (currPos > maxPos)
                    {
                        maxPos = currPos;
                    }
                    if (isToAdd)
                    {
                        toRet.Add(toAdd);
                    }

                }
            }
            if (x == 5)     // '!='
            {
                foreach (Export e in exports)
                {
                    bool isToAdd = false;
                    string name = countries.Find(nm => nm.Id == e.CountryId).Name;
                    List<string> toAdd = new List<string>() { e.Id.ToString(), name, e.PositionSet.Count.ToString() };
                    int currPos = 0;

                    foreach (int pos in e.PositionSet)
                    {
                        Position position = positions.Find(pp => pp.Id == pos);
                        if (position.Amount != condition)                               // <--
                        {
                            toAdd.Add($"[{position.Id}] {products.Find(pr => pr.Id == position.ProductId).Name} ({position.Amount})");
                            isToAdd = true;
                            currPos++;
                        }
                    }

                    if (currPos > maxPos)
                    {
                        maxPos = currPos;
                    }
                    if (isToAdd)
                    {
                        toRet.Add(toAdd);
                    }

                }
            }


            for (int i = 1; i <= maxPos; i++)
            {
                toRet.First().Add($"position#{i}");
            }

            return toRet;
        }

        // добавление новых строк (insert)

        public static bool InsertCountry(string name)
        {
            List<Country> countries = DataLogic.GetCountriesSet();
            foreach(Country c in countries)
            {
                if (c.Name.ToLower() == name.ToLower())     // если уже существует
                {
                    return false;
                }
            }
            countries.Add(new Country(countries.Count, name));
            DataLogic.SetCountries(countries);
            return true;
        }

        public static bool InsertProduct(string name)
        {
            List<Product> products = DataLogic.GetProductsSet();
            foreach (Product p in products)
            {
                if (p.Name.ToLower() == name.ToLower())     // если уже существует
                {
                    return false;
                }
            }
            products.Add(new Product(products.Count, name));
            DataLogic.SetProducts(products);
            return true;
        }

        public static bool InsertPosition(string product, int amount)
        {
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Product> products = DataLogic.GetProductsSet();
            int productId = -1;
            foreach(Product pr in products)
            {
                if(pr.Name.ToLower() == product.ToLower())
                {
                    productId = pr.Id;
                    break;
                }
            }
            if(productId == -1)             // нет такого продукта
            {
                return false;
            }
            positions.Add(new Position(positions.Count, productId, amount));
            DataLogic.SetPositions(positions);
            return true;

        }

        public static bool InsertExport(string country)
        {
            List<Country> countries = DataLogic.GetCountriesSet();
            List<Export> exports = DataLogic.GetExportsSet();
            int countryId = -1;
            foreach(Country c in countries)
            {
                if(c.Name.ToLower() == country.ToLower())
                {
                    countryId = c.Id;
                    break;
                }
            }   
            if(countryId == -1)         // нет страны
            {
                return false;   
            }
            foreach(Export e in exports)
            {
                if(e.CountryId == countryId)        // уже есть экспорт в эту страну
                {
                    return false;
                }
            }
            exports.Add(new Export(exports.Count, countryId, new List<int>()));
            DataLogic.SetExports(exports);
            return true;
        }

        // изменение строк (set)

        public static bool SetCountry(int id, string name)
        {
            List<Country> countries = DataLogic.GetCountriesSet();
            bool isRowExist = false;
            foreach (Country c in countries)
            {
                if(id == c.Id)
                {
                    isRowExist = true;
                }
                if (c.Name.ToLower() == name.ToLower() && id != c.Id)               // страна с таким названием уже есть 
                {
                    return false;                       
                }

            }
            if (!isRowExist)
            {
                return false;                   // нет такой страны
            }

            Country country = countries[countries.FindIndex(c => c.Id == id)];
            country.Name = name;
            countries[countries.FindIndex(c => c.Id == id)] = country;

            DataLogic.SetCountries(countries);
            return true;
        }

        public static bool SetProduct(int id, string name)
        {
            List<Product> products = DataLogic.GetProductsSet();
            bool isRowExist = false;
            foreach (Product p in products)
            {
                if (id == p.Id)
                {
                    isRowExist = true;
                }
                if (p.Name.ToLower() == name.ToLower() && id != p.Id)               // страна с таким названием уже есть 
                {
                    return false;
                }

            }
            if (!isRowExist)
            {
                return false;                   // нет такого товара
            }

            Product product = products[products.FindIndex(p => p.Id == id)];
            product.Name = name;
            products[products.FindIndex(p => p.Id == id)] = product;

            DataLogic.SetProducts(products);
            return true;
        }

        public static bool SetPositionAmount(int id, int amount)
        {
            List<Position> positions = DataLogic.GetPositionsSet();
            if (positions.FindIndex(p => p.Id == id) == -1)
            {
                return false;           // нет такой строки
            }

            Position position = positions[positions.FindIndex(p => p.Id == id)];
            position.Amount = amount;
            positions[positions.FindIndex(p => p.Id == id)] = position;

            DataLogic.SetPositions(positions);
            return true;
        }

        public static bool SetPositionName(int id, string name)
        {
            List<Product> products = DataLogic.GetProductsSet();
            List<Position> positions = DataLogic.GetPositionsSet();
            int productId = products.FindIndex(pr => pr.Name == name);
            if (productId == -1)
            {
                return false;               // нет такого продукта
            }
            productId = products[productId].Id;
            if(positions.FindIndex(p => p.Id == id) == -1)
            {
                return false;           // нет такой строки
            }
            

            Position position = positions[positions.FindIndex(p => p.Id == id)];
            position.ProductId = productId;
            positions[positions.FindIndex(p => p.Id == id)] = position;

            DataLogic.SetPositions(positions);
            return true;
        }

        public static bool SetExportCountry(int id, string name)
        {
            List<Export> exports = DataLogic.GetExportsSet();
            List<Country> countries = DataLogic.GetCountriesSet();
            int countryId = countries.FindIndex(c => c.Name == name);
            if (countryId == -1)
            {
                return false;               // нет такой страны
            }
            countryId = countries[countryId].Id;
            if (exports.FindIndex(e => e.Id == id) == -1)
            {
                return false;           // нет такой строки
            }
            if (exports.FindIndex(e => e.CountryId == countryId) != -1)
            {
                Export exportFrom = exports[exports.FindIndex(e => e.Id == id)];
                Export exportTo = exports[exports.FindIndex(e => e.CountryId == countryId)];
                foreach(int pos in exportFrom.PositionSet)
                {
                    exportTo.PositionSet.Add(pos);
                }
                exports[exports.FindIndex(e => e.CountryId == countryId)] = exportTo;
                exports.RemoveAt(exports.FindIndex(e => e.Id == id));
                for (int i = 0; i < exports.Count; i++)
                {
                    if (exports[i].Id > id)
                    {
                        Export e = exports[i];
                        e.Id -= 1;
                        exports[i] = e;
                    }
                }
                
            }
            else
            {
                Export export = exports[exports.FindIndex(e => e.Id == id)];
                export.CountryId = countryId;
                exports[exports.FindIndex(e => e.Id == id)] = export;
            }
            

            DataLogic.SetExports(exports);
            return true;
        }

        public static bool SetExportNewPosition(int id, int posId)
        {
            List<Export> exports = DataLogic.GetExportsSet();
            List<Position> positions = DataLogic.GetPositionsSet();

            if (exports.FindIndex(e => e.Id == id) == -1)
            {
                return false;           // нет такой строки
            }
            if (positions.FindIndex(p => p.Id == posId) == -1)
            {
                return false;           // нет такой позиции
            }
            foreach (Export e in exports)
            {
                if (e.PositionSet.Contains(posId))
                {
                    return false;       // позиция уже назначена
                }
            }

            Export export = exports[exports.FindIndex(e => e.Id == id)];
            export.PositionSet.Add(posId);
            exports[exports.FindIndex(e => e.Id == id)] = export;


            DataLogic.SetExports(exports);
            return true;
        }

        public static bool SetExportRemovePosition(int id, int posId)
        {
            List<Export> exports = DataLogic.GetExportsSet();

            if (exports.FindIndex(e => e.Id == id) == -1)
            {
                return false;           // нет такой строки
            }

            Export export = exports[exports.FindIndex(e => e.Id == id)];
            if (export.PositionSet.FindIndex(pos => pos == posId) == -1)
            {
                return false;           // нет такой позиции
            }

            export.PositionSet.Remove(posId);

            exports[exports.FindIndex(e => e.Id == id)] = export;


            DataLogic.SetExports(exports);
            return true;
        }

        // удаление строк (delete) 

        public static bool DeleteCountry(int id, out string result)
        {
            List<Country> countries = DataLogic.GetCountriesSet();
            List<Export> exports = DataLogic.GetExportsSet();
            result = "";
            if (countries.FindIndex(c => c.Id == id) == -1)
            {
                result = "no such string";
                return false;                   // нет такой строки
            }
            if (exports.FindIndex(e => e.CountryId == id) != -1)
            {
                result = "country is connected to export, change export first";
                return false;                   // нельзя удалять страну так как на неё ссылаетсыя экспорт
            }
            countries.RemoveAt(countries.FindIndex(c => c.Id == id));
            for (int i = 0; i < countries.Count; i++)
            {
                if (countries[i].Id > id)
                {
                    Country c = countries[i];
                    c.Id -= 1;
                    countries[i] = c;
                }
            }
            for (int i = 0; i < exports.Count; i++)
            {
                if (exports[i].CountryId > id)
                {
                    Export e = exports[i];
                    e.CountryId -= 1;
                    exports[i] = e;
                }
            }
            DataLogic.SetCountries(countries);
            DataLogic.SetExports(exports);
            return true;

        }

        public static bool DeleteProduct(int id, out string result)
        {
            List<Product> products = DataLogic.GetProductsSet();
            List<Position> positions = DataLogic.GetPositionsSet();
            result = "";
            if (products.FindIndex(p => p.Id == id) == -1)
            {
                result = "no such string";
                return false;                   // нет такой строки
            }
            if (positions.FindIndex(pos => pos.ProductId == id) != -1)
            {
                result = "product is connected to position, change position first";
                return false;                   // нельзя удалять страну так как на неё ссылаетсыя экспорт
            }
            products.RemoveAt(products.FindIndex(p => p.Id == id));
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id > id)
                {
                    Product p = products[i];
                    p.Id -= 1;
                    products[i] = p;
                }
            }
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i].ProductId > id)
                {
                    Position pos = positions[i];
                    pos.ProductId -= 1;
                    positions[i] = pos;
                }
            }
            DataLogic.SetProducts(products);
            DataLogic.SetPositions(positions);
            return true;
        }

        public static bool DeletePosition(int id, out string result)
        {
            List<Position> positions = DataLogic.GetPositionsSet();
            List<Export> exports = DataLogic.GetExportsSet();
            result = "";
            if (positions.FindIndex(p => p.Id == id) == -1)
            {
                result = "no such string";
                return false;                   // нет такой строки
            }
            foreach (Export e in exports)
            {
                if (e.PositionSet.FindIndex(Id => Id == id) != -1)
                {
                    result = "position is connected to export, change export first";
                    return false;               // нельзя удалять так как на позицию ссылается экспорт
                }
            }
            positions.RemoveAt(positions.FindIndex(pos => pos.Id == id));
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i].Id > id)
                {
                    Position p = positions[i];
                    p.Id -= 1;
                    positions[i] = p;
                }
            }
            for (int i = 0; i < exports.Count; i++)
            {
                Export export = exports[i];
                for (int j = 0; j < export.PositionSet.Count; j++)
                {
                    if (export.PositionSet[j] > id)
                    {
                        export.PositionSet[j] -= 1;
                    }
                }
                exports[i] = export;
            }


            DataLogic.SetExports(exports);
            DataLogic.SetPositions(positions);
            return true;
        }

        public static bool DeleteExport(int id)
        {
            List<Export> exports = DataLogic.GetExportsSet();
            if (exports.FindIndex(e => e.Id == id) == -1)
            {
                return false;                   // нет такой строки
            }
            exports.RemoveAt(exports.FindIndex(e => e.Id == id));
            for (int i = 0; i < exports.Count; i++)
            {
                if (exports[i].Id > id)
                {
                    Export e = exports[i];
                    e.Id -= 1;
                    exports[i] = e;
                }
            }
            DataLogic.SetExports(exports);
            return true; // todo check
        }
    }
}
