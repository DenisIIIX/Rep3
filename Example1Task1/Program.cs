namespace Example1Task1
{
    internal class Program
    {
        public class ShoppingCartService
        {
            public decimal CalculateTotalPrice(string customerType, List<decimal> itemPrices)
            {
                decimal baseTotal = 0;// Нарушение KISS: можно описать формулой 
                for (int i = 0; i < itemPrices.Count; i++)
                {
                    baseTotal += itemPrices[i];
                }

                decimal discount = 0; //Нарушение DRY: описать отдельным методом

                if (customerType == "Regular")
                {
                    discount = baseTotal * 0.05m;
                }
                //Нарушение YAGNI в строках 22-33: добавлены излишние условия (если учитывать условие рефакторинга)
                else if (customerType == "Premium")
                {
                    discount = baseTotal * 0.15m; // 15%
                    if (discount > 1000)
                    {
                        discount = 1000 + (discount - 1000) * 0.1m;
                    }
                }
                else if (customerType == "VIP")
                {
                    discount = baseTotal * 0.20m; // 20%
                }

                decimal finalPrice = baseTotal - discount;//// Нарушение KISS: можно описать формулой вместе с подсчетом скидки

                Console.WriteLine($"Base: {baseTotal}, Discount: {discount}, Final: {finalPrice}");
                return finalPrice;
            }

            public decimal CalculateTotalPriceWithQuantities(string customerType, Dictionary<decimal, int> itemsWithQuantities)
            {
                List<decimal> allPrices = new List<decimal>();//Нарушение YAGNI: метод можно ликвидировать, описать суммирование значений словаря в методе выше
                foreach (var item in itemsWithQuantities)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        allPrices.Add(item.Key);
                    }
                }
                return CalculateTotalPrice(customerType, allPrices);
            }
        }
    }
}
