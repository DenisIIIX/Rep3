namespace Example1Task2
{
    using System;
    using System.Collections.Generic;
    internal class Program
    {
        static void Main()
        {
            var cartService = new ShoppingCartService();
            // Тест 1: Обычный покупатель
            Console.WriteLine("Тест: With quantities");
            var items = new Dictionary<decimal, int>
            {
                { 100m, 2 }, // 2 товара по 100
                { 50m, 3 }   // 3 товара по 50
            };
            decimal result = cartService.CalculateTotalPrice("Regular", items);
            Console.WriteLine($"Ожидается: 332.5, Получено: {result}");
            Console.WriteLine();
        }

        public class ShoppingCartService
        {

            public decimal CalculateTotalPrice(string customerType, Dictionary<decimal, int> itemsWithQuantities)
            {
                decimal baseTotal = itemsWithQuantities.Sum(item => item.Key * item.Value);
                int discount = GetDiscountPercent(customerType);
                decimal finalPrice = baseTotal * (100 - discount) / 100;

                Console.WriteLine($"Base: {baseTotal}, Discount: {discount}, Final: {finalPrice}");
                return finalPrice;
            }
            private int GetDiscountPercent(string customerType)
            {
                return customerType?.ToLower() switch
                {
                    "regular" => 5,
                    _ => 0
                };
            }
        }
    }
}
