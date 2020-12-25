using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductBase
{
    static class UserLogic
    {
        public static string CurrentBaseName { get; set; } = "DataBase1";
        

        public static void StartWindow()
        {
            Form1 app = new Form1();
            Application.Run(app);
        }

        public static bool query(out List<List<string>> toRetBase, out string toRetName, out string result, string toQuery)
        {
            bool isQueried = false;
            result = "invalid command";

            toRetName = CurrentBaseName;
            toRetBase = null;


            toQuery = toQuery.Trim();
            // \e\ пустая строка
            if (toQuery == "")                      
            {
                result = "empty string";
                return false;
            }

            // работа с оператором 'select'
            else if (toQuery.StartsWith("SELECT ")) 
            {
                // \e\ еще один оператор 'select' внутри
                if (toQuery.LastIndexOf("SELECT") != 0)     
                {
                    result = "'select' into another 'select' operator";
                    return false;
                }
                toQuery = toQuery.Substring(7);

                // есть условный оператор 'where'
                if (toQuery.Contains(" WHERE "))     
                {
                    // \e\ в строке несколько операторов 'where'
                    if (toQuery.IndexOf(" WHERE ") != toQuery.LastIndexOf(" WHERE "))    
                    {
                        result = "multiple operators 'where'";
                        return false;
                    }

                    string condition = toQuery.Substring(toQuery.IndexOf("WHERE") + 5).Trim();
                    toQuery = toQuery.Substring(0, toQuery.IndexOf("WHERE")).Trim();

                    if (toQuery == "country")
                    {
                        if (condition.StartsWith("name IS "))
                        {
                            toRetBase = BusinessLogic.SelectCountriesWithName(condition.Substring(8).Trim(), 0);
                        }
                        else if (condition.StartsWith("name BEGIN WITH "))
                        {
                            toRetBase = BusinessLogic.SelectCountriesWithName(condition.Substring(16).Trim(), 1);
                        }
                        else if (condition.StartsWith("name END WITH "))
                        {
                            toRetBase = BusinessLogic.SelectCountriesWithName(condition.Substring(14).Trim(), 2);
                        }
                        

                        // \e\ неправильное условие
                        else
                        {
                            result = "wrong condition for 'where'";
                            return false;
                        }
                        result = "countries selected";
                        isQueried = true;
                    }
                    else if (toQuery == "product")
                    {
                        if (condition.StartsWith("name IS "))
                        {
                            // \e\ несколько несовместимых условий
                            if (condition.IndexOf("IS") != condition.LastIndexOf("IS"))
                            {
                                result = "cant take multiple conditions in 'where'";
                                return false;
                            }
                            toRetBase = BusinessLogic.SelectProductsWithName(condition.Substring(8).Trim(), 2);
                        }
                        else if (condition.StartsWith("name BEGIN WITH "))
                        {
                            // \e\ несколько несовместимых условий
                            if (condition.IndexOf("WITH") != condition.LastIndexOf("WITH"))
                            {
                                result = "cant take multiple conditions in 'where'";
                                return false;
                            }
                            toRetBase = BusinessLogic.SelectProductsWithName(condition.Substring(16).Trim(), 1);
                        }
                        else if (condition.StartsWith("name END WITH "))
                        {
                            // \e\ несколько несовместимых условий
                            if (condition.IndexOf("WITH") != condition.LastIndexOf("WITH"))
                            {
                                result = "cant take multiple conditions in 'where'";
                                return false;
                            }
                            toRetBase = BusinessLogic.SelectProductsWithName(condition.Substring(14).Trim(), 2);
                        }

                        // \e\ неправильное условие
                        else
                        {
                            result = "wrong condition for 'where'";
                            return false;
                        }
                        result = "products selected";
                        isQueried = true;
                    }
                    else if (toQuery == "export")
                    {
                        if (condition.StartsWith("country IS "))
                        {
                            toRetBase = BusinessLogic.SelectExportsWithCountry(condition.Substring(11).Trim(), 0);
                        }
                        else if (condition.StartsWith("country BEGIN WITH "))
                        {
                            toRetBase = BusinessLogic.SelectExportsWithCountry(condition.Substring(19).Trim(), 1);
                        }
                        else if (condition.StartsWith("country END WITH "))
                        {
                            toRetBase = BusinessLogic.SelectExportsWithCountry(condition.Substring(17).Trim(), 2);
                        }
                   
                        else if (condition.StartsWith("product IS "))
                        {
                            toRetBase = BusinessLogic.SelectExportsWithProduct(condition.Substring(11).Trim(), 0, out string total);
                            result = $"total amount of product '{condition.Substring(11).Trim()}' is {total}";
                        }
                        else if (condition.StartsWith("product BEGIN WITH "))
                        {
                            toRetBase = BusinessLogic.SelectExportsWithProduct(condition.Substring(19).Trim(), 1, out string total);
                        }
                        else if (condition.StartsWith("product END WITH "))
                        {
                            toRetBase = BusinessLogic.SelectExportsWithProduct(condition.Substring(17).Trim(), 2, out string total);
                        }
                        
                        else if (condition.StartsWith("positions count == "))
                        {
                            condition = condition.Substring(19).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithCount(num, 0);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("positions count >= "))
                        {
                            condition = condition.Substring(19).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithCount(num, 1);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("positions count <= "))
                        {
                            condition = condition.Substring(19).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithCount(num, 2);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("positions count > "))
                        {
                            condition = condition.Substring(18).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithCount(num, 3);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("positions count < "))
                        {
                            condition = condition.Substring(18).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithCount(num, 4);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("positions count != "))
                        {
                            condition = condition.Substring(19).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithCount(num, 5);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }

                        else if (condition.StartsWith("product amount == "))
                        {
                            condition = condition.Substring(18).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithAmount(num, 0);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("product amount >= "))
                        {
                            condition = condition.Substring(18).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithAmount(num, 1);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("product amount <= "))
                        {
                            condition = condition.Substring(18).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithAmount(num, 2);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("product amount > "))
                        {
                            condition = condition.Substring(17).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithAmount(num, 3);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("product amount < "))
                        {
                            condition = condition.Substring(17).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithAmount(num, 4);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("product amount != "))
                        {
                            condition = condition.Substring(18).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectExportsWithAmount(num, 5);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }

                        // \e\ неправильное условие
                        else
                        {
                            result = "wrong condition for 'where'";
                            return false;
                        }
                        if (!result.StartsWith("total"))
                        {
                            result = "export selected";
                        }
                        isQueried = true;
                    }
                    else if (toQuery == "position")
                    {
                        if (condition.StartsWith("name IS "))
                        {
                            toRetBase = BusinessLogic.SelectPositionsWithName(condition.Substring(8).Trim(), 0);
                        }
                        else if (condition.StartsWith("name BEGIN WITH "))
                        {
                            toRetBase = BusinessLogic.SelectPositionsWithName(condition.Substring(16).Trim(), 1);
                        }
                        else if (condition.StartsWith("name END WITH "))
                        {
                            toRetBase = BusinessLogic.SelectPositionsWithName(condition.Substring(14).Trim(), 2);
                        }


                        else if (condition.StartsWith("amount == "))
                        {
                            condition = condition.Substring(10).Trim();
                            if(int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectPositionsWithAmount(num, 0);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("amount >= "))
                        {
                            condition = condition.Substring(10).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectPositionsWithAmount(num, 1);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("amount <= "))
                        {
                            condition = condition.Substring(10).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectPositionsWithAmount(num, 2);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("amount > "))
                        {
                            condition = condition.Substring(9).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectPositionsWithAmount(num, 3);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("amount < "))
                        {
                            condition = condition.Substring(9).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectPositionsWithAmount(num, 4);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }
                        else if (condition.StartsWith("amount != "))
                        {
                            condition = condition.Substring(10).Trim();
                            if (int.TryParse(condition, out int num))
                            {
                                toRetBase = BusinessLogic.SelectPositionsWithAmount(num, 5);
                            }
                            // \e\ сравнение не с числом
                            else
                            {
                                result = "trying to compare amount with non-number";
                                return false;
                            }
                        }


                        // \e\ неправильное условие
                        else
                        {
                            result = "wrong condition for 'where'";
                            return false;
                        }
                        result = "position selected";
                        isQueried = true;
                    }

                }

                // нет условного оператора 'where'
                else
                {
                    toQuery = toQuery.Trim();
                    if (toQuery == "country")
                    {
                        toRetBase = BusinessLogic.SelectCountries();
                        result = "countries selected";
                        isQueried = true;
                    }
                    else if (toQuery == "product")
                    {
                        toRetBase = BusinessLogic.SelectProducts();
                        result = "products selected";
                        isQueried = true;
                    }
                    else if (toQuery == "export")
                    {
                        toRetBase = BusinessLogic.SelectExports();
                        result = "export selected";
                        isQueried = true;
                    }
                    else if (toQuery == "position")
                    {
                        toRetBase = BusinessLogic.SelectPositions();
                        result = "position selected";
                        isQueried = true;
                    }

                    // \e\ операнд оператора 'select' некорректный
                    else {
                        result = "wrong parameter into 'select'";
                        return false;
                    }
                }
            }

            else if (toQuery.StartsWith("INSERT "))
            {
                // \e\ еще один оператор 'select' внутри
                if (toQuery.LastIndexOf("INSERT") != 0)
                {
                    result = "'insert' into another 'insert' operator";
                    return false;
                }
                toQuery = toQuery.Substring(7).Trim();

                if (toQuery.StartsWith("country "))
                {
                    toQuery = toQuery.Substring(8).Trim();
                    // \e\ новое значение не в фигурных скобках
                    if(toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                    {
                        result = "cant recognize new country name inside '{}'";
                        return false;
                    }

                    toQuery = toQuery.Trim('{', '}', ' ');

                    // \e\ новое значение - пустая строка
                    if(toQuery == "")
                    {
                        result = "new value can't be empty";
                        return false;
                    }

                    // \e\ уже есть такая страна
                    if (!BusinessLogic.InsertCountry(toQuery))
                    {
                        result = "country with this name already exist";
                        return false;
                    }

                    toRetBase = BusinessLogic.SelectCountries();
                    result = "country added successfully";
                    isQueried = true;

                }
                else if (toQuery.StartsWith("product "))
                {
                    toQuery = toQuery.Substring(8).Trim();
                    // \e\ новое значение не в фигурных скобках
                    if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                    {
                        result = "cant recognize new product name inside '{}'";
                        return false;
                    }

                    toQuery = toQuery.Trim('{', '}', ' ');

                    // \e\ новое значение - пустая строка
                    if (toQuery == "")
                    {
                        result = "new value can't be empty";
                        return false;
                    }

                    // \e\ уже есть такая страна
                    if (!BusinessLogic.InsertProduct(toQuery))
                    {
                        result = "product with this name already exist";
                        return false;
                    }

                    toRetBase = BusinessLogic.SelectProducts();
                    result = "product added successfully";
                    isQueried = true;
                }
                else if (toQuery.StartsWith("export ")){
                    toQuery = toQuery.Substring(7).Trim();
                    // \e\ новое значение не в фигурных скобках
                    if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                    {
                        result = "cant recognize new export values inside '{}'";
                        return false;
                    }

                    toQuery = toQuery.Trim('{', '}', ' ');

                    // \e\ нет такой страны или экспорт туда уже есть
                    if (!BusinessLogic.InsertExport(toQuery))
                    {
                        result = "export to such country already exist or no such country";
                        return false;
                    }

                    result = "export added successfully";
                    toRetBase = BusinessLogic.SelectExports();
                    isQueried = true;
                }
                else if (toQuery.StartsWith("position "))
                {
                    toQuery = toQuery.Substring(9).Trim();
                    // \e\ новое значение не в фигурных скобках
                    if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                    {
                        result = "cant recognize new product name inside '{}'";
                        return false;
                    }

                    toQuery = toQuery.Trim('{', '}', ' ');

                    // \e\ больше двух параметров или один из них пустой
                    if(toQuery.IndexOf(',') != toQuery.LastIndexOf(',') || toQuery.IndexOf(',') == 0 || toQuery.IndexOf(',') == toQuery.Count())
                    {
                        result = "wrong parameters";
                        return false;
                    }
                    if (int.TryParse(toQuery.Substring(toQuery.IndexOf(',') + 2).Trim(), out int amount))
                    {
                        // \e\ нет такого товара
                        if(!BusinessLogic.InsertPosition(toQuery.Substring(0, toQuery.IndexOf(',')).Trim(), amount))
                        {
                            result = "no such product";
                            return false;
                        }
                    }
                    else
                    {
                        result = "amount is non-numeric";
                        return false;
                    }

                    result = "position added successfully";
                    toRetBase = BusinessLogic.SelectPositions();
                    isQueried = true;

                }

            }

            else if (toQuery.StartsWith("SET "))
            {
                // \e\ еще один оператор 'set' внутри
                if (toQuery.LastIndexOf("SET") != 0)
                {
                    result = "'set' into another 'set' operator";
                    return false;
                }
                toQuery = toQuery.Substring(4).Trim();

                if (toQuery.StartsWith("country "))
                {
                    toQuery = toQuery.Substring(8).Trim();
                    // \e\ нет индекса строки для изменения
                    if(!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                    {
                        result = "no id of row to change";
                        return false;
                    }

                    int id;
                    // \e\ внутри [] не индекс
                    if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                    {
                        result = "there is no number inside '[]'";
                        return false;
                    }

                    toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                    // \e\ новое значение не в фигурных скобках
                    if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                    {
                        result = "cant recognize new country name inside '{}'";
                        return false;
                    }

                    toQuery = toQuery.Trim('{', '}', ' ');

                    // \e\ новое значение - пустая строка
                    if (toQuery == "")
                    {
                        result = "new value can't be empty";
                        return false;
                    }

                    // \e\ уже есть такая страна
                    if (!BusinessLogic.SetCountry(id, toQuery))
                    {
                        result = "no such row or country with this name already exist";
                        return false;
                    }

                    toRetBase = BusinessLogic.SelectCountries();
                    result = "country changed successfully";
                    isQueried = true;

                }
                else if (toQuery.StartsWith("product "))
                {
                    toQuery = toQuery.Substring(8).Trim();
                    // \e\ нет индекса строки для изменения
                    if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                    {
                        result = "no id of row to change";
                        return false;
                    }

                    int id;
                    // \e\ внутри [] не индекс
                    if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                    {
                        result = "there is no number inside '[]'";
                        return false;
                    }

                    toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                    // \e\ новое значение не в фигурных скобках
                    if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                    {
                        result = "cant recognize new product name inside '{}'";
                        return false;
                    }

                    toQuery = toQuery.Trim('{', '}', ' ');

                    // \e\ новое значение - пустая строка
                    if (toQuery == "")
                    {
                        result = "new value can't be empty";
                        return false;
                    }

                    // \e\ уже есть такой продукт
                    if (!BusinessLogic.SetProduct(id, toQuery))
                    {
                        result = "no such row or product with this name already exist";
                        return false;
                    }

                    toRetBase = BusinessLogic.SelectProducts();
                    result = "product changed successfully";
                    isQueried = true;
                }
                else if (toQuery.StartsWith("export "))
                {
                    toQuery = toQuery.Substring(7).Trim();

                    if (toQuery.StartsWith("add position "))  
                    {
                        toQuery = toQuery.Substring(13).Trim();
                        // \e\ нет индекса строки для изменения
                        if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                        {
                            result = "no id of row to change";
                            return false;
                        }

                        int id;
                        // \e\ внутри [] не индекс
                        if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                        {
                            result = "there is no number inside '[]'";
                            return false;
                        }

                        toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                        // \e\ новое значение не в фигурных скобках
                        if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                        {
                            result = "cant recognize new amount inside '{}'";
                            return false;
                        }

                        toQuery = toQuery.Trim('{', '}', ' ');

                        int newId = 0;
                        // \e\ новое значение - не число
                        if (!int.TryParse(toQuery, out newId))
                        {
                            result = "id of position is non-number";
                            return false;
                        }

                        // \e\ нет нужной строки, нужной позиции или позиция уже назначена другому экспорту
                        if (!BusinessLogic.SetExportNewPosition(id, newId))
                        {
                            result = "no such row or such position or position is at other export";
                            return false;
                        }

                    }
                    else if (toQuery.StartsWith("remove position "))
                    {
                        toQuery = toQuery.Substring(16).Trim();
                        // \e\ нет индекса строки для изменения
                        if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                        {
                            result = "no id of row to change";
                            return false;
                        }

                        int id;
                        // \e\ внутри [] не индекс
                        if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                        {
                            result = "there is no number inside '[]'";
                            return false;
                        }

                        toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                        // \e\ новое значение не в фигурных скобках
                        if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                        {
                            result = "cant recognize new amount inside '{}'";
                            return false;
                        }

                        toQuery = toQuery.Trim('{', '}', ' ');

                        int newId = 0;
                        // \e\ новое значение - не число
                        if (!int.TryParse(toQuery, out newId))
                        {
                            result = "id of position is non-number";
                            return false;
                        }

                        // \e\ нет нужной строки, нужной позиции или позиция уже назначена другому экспорту
                        if (!BusinessLogic.SetExportRemovePosition(id, newId))
                        {
                            result = "no such row or such position at row";
                            return false;
                        }

                    }
                    else if (toQuery.StartsWith("country "))
                    {
                        toQuery = toQuery.Substring(8).Trim();
                        // \e\ нет индекса строки для изменения
                        if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                        {
                            result = "no id of row to change";
                            return false;
                        }

                        int id;
                        // \e\ внутри [] не индекс
                        if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                        {
                            result = "there is no number inside '[]'";
                            return false;
                        }

                        toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                        // \e\ новое значение не в фигурных скобках
                        if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                        {
                            result = "cant recognize new name inside '{}'";
                            return false;
                        }

                        toQuery = toQuery.Trim('{', '}', ' ');

                        // \e\ новое значение - пустая строка
                        if (toQuery == "")
                        {
                            result = "new name can't be empty";
                            return false;
                        }

                        // \e\ нет нужной строки или такой страны
                        if (!BusinessLogic.SetExportCountry(id, toQuery))
                        {
                            result = "no such row or country";
                            return false;
                        }
                    }

                    // \e\ неправильное уточнение что именно в экспорте менять
                    else
                    {
                        result = "wrong command";
                        return false;
                    }


                    toRetBase = BusinessLogic.SelectExports();
                    result = "exports changed successfully";
                    isQueried = true;
                }
                else if (toQuery.StartsWith("position "))
                {
                    toQuery = toQuery.Substring(9).Trim();

                    if (toQuery.StartsWith("amount "))
                    {
                        toQuery = toQuery.Substring(7).Trim();
                        // \e\ нет индекса строки для изменения
                        if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                        {
                            result = "no id of row to change";
                            return false;
                        }

                        int id;
                        // \e\ внутри [] не индекс
                        if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                        {
                            result = "there is no number inside '[]'";
                            return false;
                        }

                        toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                        // \e\ новое значение не в фигурных скобках
                        if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                        {
                            result = "cant recognize new amount inside '{}'";
                            return false;
                        }

                        toQuery = toQuery.Trim('{', '}', ' ');

                        int newAmount = 0;
                        // \e\ новое значение - не число
                        if (!int.TryParse(toQuery, out newAmount))
                        {
                            result = "new value can't be empty";
                            return false;
                        }

                        // \e\ нет нужной строки
                        if (!BusinessLogic.SetPositionAmount(id, newAmount))
                        {
                            result = "no such row";
                            return false;
                        }

                    }
                    else if (toQuery.StartsWith("name "))
                    {
                        toQuery = toQuery.Substring(5).Trim();
                        // \e\ нет индекса строки для изменения
                        if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                        {
                            result = "no id of row to change";
                            return false;
                        }

                        int id;
                        // \e\ внутри [] не индекс
                        if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                        {
                            result = "there is no number inside '[]'";
                            return false;
                        }

                        toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                        // \e\ новое значение не в фигурных скобках
                        if (toQuery.LastIndexOf('{') != 0 || toQuery.IndexOf('}') != (toQuery.Count() - 1))
                        {
                            result = "cant recognize new name inside '{}'";
                            return false;
                        }

                        toQuery = toQuery.Trim('{', '}', ' ');

                        // \e\ новое значение - пустая строка
                        if (toQuery == "")
                        {
                            result = "new name can't be empty";
                            return false;
                        }

                        // \e\ нет нужной строки или такого продукта
                        if (!BusinessLogic.SetPositionName(id, toQuery))
                        {
                            result = "no such row or product";
                            return false;
                        }
                    }

                    // \e\ неправильное уточнение что именно в позиции менять
                    else
                    {
                        result = "wrong command";
                        return false;
                    }


                    toRetBase = BusinessLogic.SelectPositions();
                    result = "position changed successfully";
                    isQueried = true;

                }

            }

            else if (toQuery.StartsWith("DELETE "))
            {
                // \e\ еще один оператор 'delete' внутри
                if (toQuery.LastIndexOf("DELETE") != 0)
                {
                    result = "'delete' into another 'delete' operator";
                    return false;
                }
                toQuery = toQuery.Substring(7).Trim();

                if (toQuery.StartsWith("country "))
                {
                    toQuery = toQuery.Substring(8).Trim();
                    // \e\ нет индекса строки для удаления
                    if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                    {
                        result = "no id of row to delete";
                        return false;
                    }

                    int id;
                    // \e\ внутри [] не индекс
                    if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                    {
                        result = "there is no number inside '[]'";
                        return false;
                    }

                    toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                    // \e\ нельзя удалить страну
                    if (!BusinessLogic.DeleteCountry(id, out string res))
                    {
                        result = res;
                        return false;
                    }

                    toRetBase = BusinessLogic.SelectCountries();
                    result = "country deleted successfully";
                    isQueried = true;
                }
                else if (toQuery.StartsWith("product "))
                {
                    toQuery = toQuery.Substring(8).Trim();
                    // \e\ нет индекса строки для удаления
                    if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                    {
                        result = "no id of row to delete";
                        return false;
                    }

                    int id;
                    // \e\ внутри [] не индекс
                    if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                    {
                        result = "there is no number inside '[]'";
                        return false;
                    }

                    toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                    // \e\ нельзя удалить продукт
                    if (!BusinessLogic.DeleteProduct(id, out string res))
                    {
                        result = res;
                        return false;
                    }

                    toRetBase = BusinessLogic.SelectProducts();
                    result = "product deleted successfully";
                    isQueried = true;
                }
                else if (toQuery.StartsWith("export "))
                {
                    toQuery = toQuery.Substring(7).Trim();
                    // \e\ нет индекса строки для удаления
                    if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                    {
                        result = "no id of row to delete";
                        return false;
                    }

                    int id;
                    // \e\ внутри [] не индекс
                    if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                    {
                        result = "there is no number inside '[]'";
                        return false;
                    }

                    toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                    // \e\ нельзя удалить экспорт
                    if (!BusinessLogic.DeleteExport(id))
                    {
                        result = "there is no such row";
                        return false;
                    }

                    toRetBase = BusinessLogic.SelectExports();
                    result = "export deleted successfully";
                    isQueried = true;
                }
                else if (toQuery.StartsWith("position "))
                {
                    toQuery = toQuery.Substring(9).Trim();
                    // \e\ нет индекса строки для удаления
                    if (!toQuery.StartsWith("[") || toQuery.IndexOf('[') != toQuery.LastIndexOf('[') || toQuery.LastIndexOf(']') != toQuery.IndexOf(']') || toQuery.IndexOf(']') == -1)
                    {
                        result = "no id of row to delete";
                        return false;
                    }

                    int id;
                    // \e\ внутри [] не индекс
                    if (!int.TryParse(toQuery.Substring(1, toQuery.IndexOf(']') - 1).Trim(), out id))
                    {
                        result = "there is no number inside '[]'";
                        return false;
                    }

                    toQuery = toQuery.Substring(toQuery.IndexOf(']') + 1).Trim();

                    // \e\ нельзя удалить позицию
                    if (!BusinessLogic.DeletePosition(id, out string res))
                    {
                        result = res;
                        return false;
                    }

                    toRetBase = BusinessLogic.SelectPositions();
                    result = "position deleted successfully";
                    isQueried = true;
                }

            }

            return isQueried;
        }
    }
}
