﻿using ECommerce.Infrastructure;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ApplicationServices
{
    public class ProductService : IProductService
    {
        private const string ProductEntityName = "Product";

        private IRepository<Product> productRepository;
        private IUrlSlugService urlSlugService;

        public ProductService(IRepository<Product> productRepository, IUrlSlugService urlSlugService)
        {
            this.productRepository = productRepository;
            this.urlSlugService = urlSlugService;
        }

        public void Create(Product product)
        {
            productRepository.Add(product);
            productRepository.SaveChange();

            urlSlugService.Add(product.SeoTitle, product.Id, ProductEntityName);
            productRepository.SaveChange();
        }

        public void Update(Product product)
        {
            urlSlugService.Update(product.SeoTitle, product.Id, ProductEntityName);
            productRepository.SaveChange();
        }

        public void Delete(Product product)
        {
            productRepository.Remove(product);
            urlSlugService.Remove(product.Id, ProductEntityName);
            productRepository.SaveChange();
        }
    }
}
