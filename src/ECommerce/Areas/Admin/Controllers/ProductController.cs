using System.Linq;
using Microsoft.AspNet.Mvc;
using ECommerce.Models;
using ECommerce.Infrastructure;
using ECommerce.ApplicationServices;
using Microsoft.AspNet.Authorization;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNet.Http;
using System;
using System.IO;
using ECommerce.ViewModels.Product;
using ECommerce.ViewModels.SmartTable;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> productRepository;
        private readonly IMediaService mediaService;
        private readonly IProductService productService;
        private readonly IRepository<ProductCategory> productCategoryRepository;

        public ProductController(
            IRepository<Product> productRepository,
            IMediaService mediaService,
            IProductService productService,
            IRepository<ProductCategory> productCategoryRepository)
        {
            this.productRepository = productRepository;
            this.mediaService = mediaService;
            this.productService = productService;
            this.productCategoryRepository = productCategoryRepository;
        }

        public IActionResult Get(long id)
        {
            var product = productRepository.Get(id);

            var productVm = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                Specification = product.Specification,
                OldPrice = product.OldPrice,
                Price = product.Price,
                CategoryIds = product.Categories.Select(x => x.CategoryId).ToList(),
                ThumbnailImageUrl = mediaService.GetThumbnailUrl(product.ThumbnailImage),
                BrandId = product.BrandId
            };

            foreach (var productMedia in product.Medias)
            {
                productVm.ProductMedias.Add(new ProductMediaVm
                {
                    Id = productMedia.Id,
                    MediaUrl = mediaService.GetThumbnailUrl(productMedia.Media)
                });
            }

            return Json(productVm);
        }

        public IActionResult List([FromBody] SmartTableParam param)
        {
            var products = productRepository.Query().Where(x => !x.IsDeleted);
            var gridData = products.ToSmartTableResult(
                param,
                x => new ProductListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    IsPublished = x.IsPublished
                });

            return Json(gridData);
        }

        [HttpPost]
        public IActionResult Create(ProductForm model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var product = new Product
            {
                Name = model.Product.Name,
                SeoTitle = StringHelper.ToUrlFriendly(model.Product.Name),
                ShortDescription = model.Product.ShortDescription,
                Description = model.Product.Description,
                Specification = model.Product.Specification,
                Price = model.Product.Price,
                OldPrice = model.Product.OldPrice,
                IsPublished = model.Product.IsPublished,
                BrandId = model.Product.BrandId
            };

            foreach (var categoryId in model.Product.CategoryIds)
            {
                var productCategory = new ProductCategory
                {
                    CategoryId = categoryId
                };
                product.AddCategory(productCategory);
            }

            SaveProductImages(model, product);

            productService.Create(product);

            return Ok();
        }

        [HttpPost]
        public IActionResult Edit(long id, ProductForm model)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var product = productRepository.Get(id);
            product.Name = model.Product.Name;
            product.ShortDescription = model.Product.ShortDescription;
            product.Description = model.Product.Description;
            product.Specification = model.Product.Specification;
            product.Price = model.Product.Price;
            product.OldPrice = model.Product.OldPrice;
            product.BrandId = model.Product.BrandId;

            SaveProductImages(model, product);

            foreach (var productMediaId in model.Product.DeletedMediaIds)
            {
                var productMedia = product.Medias.First(x => x.Id == productMediaId);
                mediaService.DeleteMedia(productMedia.Media);
                product.RemoveMedia(productMedia);
            }

            AddOrDeleteCategories(model, product);

            productService.Update(product);

            return Ok();
        }

        private void AddOrDeleteCategories(ProductForm model, Product product)
        {
            foreach (var categoryId in model.Product.CategoryIds)
            {
                if (product.Categories.Any(x => x.CategoryId == categoryId))
                {
                    continue;
                }

                var productCategory = new ProductCategory
                {
                    CategoryId = categoryId
                };
                product.AddCategory(productCategory);
            }

            var deletedProductCategories =
                product.Categories.Where(productCategory => !model.Product.CategoryIds.Contains(productCategory.CategoryId))
                    .ToList();

            foreach (var deletedProductCategory in deletedProductCategories)
            {
                deletedProductCategory.Product = null;
                product.Categories.Remove(deletedProductCategory);
                productCategoryRepository.Remove(deletedProductCategory);
            }
        }

        private void SaveProductImages(ProductForm model, Product product)
        {
            if (model.ThumbnailImage != null)
            {
                var fileName = SaveFile(model.ThumbnailImage);
                if (product.ThumbnailImage != null)
                {
                    product.ThumbnailImage.FileName = fileName;
                }
                else
                {
                    product.ThumbnailImage = new Media { FileName = fileName };
                }
            }

            // Currently model binder cannot map the collection of file productImages[0], productImages[1]
            foreach (var file in Request.Form.Files)
            {
                if (file.ContentDisposition.Contains("productImages"))
                {
                    model.ProductImages.Add(file);
                }
            }

            foreach (var file in model.ProductImages)
            {
                var fileName = SaveFile(file);
                var productMedia = new ProductMedia
                {
                    Product = product,
                    Media = new Media { FileName = fileName }
                };
                product.AddMedia(productMedia);
            }
        }

        private string SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            mediaService.SaveMedia(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}