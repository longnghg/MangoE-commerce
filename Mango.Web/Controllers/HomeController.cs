﻿using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult>  Index()
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}