using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System.Diagnostics;
using System.Globalization;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContent _content;

        public HomeController(AppDBContent content)
        {
            _content = content;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FindByName(string productName)
        {
            ViewBag.ProductName = productName;
            Product? product = await _content.FindByName(productName.ToUpper());
            return View("ResultFind", product);
        }

        [HttpGet]
        public async Task<IActionResult> FindByPrice(double price)
        {
            ViewBag.Price = price;
            List<Product> products = await _content.FindByPrice(price);
            return View("ResultFindByPrice", products);
        }

        [HttpGet]
        public async Task<IActionResult> ResultAll()
        {
            List<Product> products = await _content.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (double.TryParse(product.Price.ToString().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedPrice))
            {
                product.Price = parsedPrice;
            }
            if (ModelState.IsValid)
            {
                product.Name = product.Name.ToUpper();
                await _content.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(string productName)
        {
            await _content.DeleteProduct(productName.ToUpper());
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}