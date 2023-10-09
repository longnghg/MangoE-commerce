using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
     
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProductDto>? list = new();

            ResponseDto? response = await _productService.GetAllProductsAsync();

            if (response != null && response.IsSuccess == true)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                TempData["success"] = "All data has been loaded";

            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int productId)
        {
            ProductDto? productDto = new();

            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess == true)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result)!);
                TempData["success"] = "All data has been loaded";

            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto ProductDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.CreateProductAsync(ProductDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Create success";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(ProductDto);
        }
    }
}
