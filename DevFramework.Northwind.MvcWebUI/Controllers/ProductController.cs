﻿using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var model = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(model);
        }
        public string Add()
        {
            _productService.Add(new Product
            {
                CategoryID = 1,
                ProductName = "Gsm",
                QuantityPerUnit = "1",
                UnitPrice = 21
            });
            return "Added";
        }
        public string AddUpdate()
        {
            _productService.TransactionalOperation(new Product
            {
                CategoryID = 1,
                ProductName = "Computer",
                QuantityPerUnit = "1",
                UnitPrice = 12
            }, new Product
            {
                CategoryID = 1,
                ProductID=79,
                ProductName = "Computer 2",
                QuantityPerUnit = "1",
                UnitPrice = 23
            }
            );
            return "Done";
        }
    }
}