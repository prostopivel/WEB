using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Models
{
    public class Product
    {
        [MaybeNull]
        public int ID { get; private set; }

        [Required(ErrorMessage = "Введите название продукта!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите цену продукта!")]
        [Range(0, double.MaxValue, ErrorMessage = "Неверное значение!")]
        public double Price { get; set; }

        [DefaultValueIfEmpty(0)]
        [Range(0, double.MaxValue, ErrorMessage = "Неверное значение!")]
        public int? Count { get; set; }

        public Product()
        {
        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public Product(string name, double price, int count) : this(name, price)
        {
            Count = count;
        }

        public override string ToString()
        {
            return $"{ID}: {Name}, цена: {Price}, количество: {Count}";
        }
    }
}