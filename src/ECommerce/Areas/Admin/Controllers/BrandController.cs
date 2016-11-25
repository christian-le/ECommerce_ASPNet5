using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ECommerce.Infrastructure;
using ECommerce.Models;
using ECommerce.ApplicationServices;
using ECommerce.ViewModels.Brand;
using Microsoft.AspNet.Authorization;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "admin")]
    public class BrandController : Controller
    {
        private readonly IRepository<Brand> brandRepository;
        private readonly IBrandService brandService;

        public BrandController(IRepository<Brand> brandRepository, IBrandService brandService)
        {
            this.brandRepository = brandRepository;
            this.brandService = brandService;
        }

        public IActionResult List()
        {
            var brandList = brandRepository.Query().Where(x => !x.IsDeleted).ToList();

            return Json(brandList);
        }

        public IActionResult Get(long id)
        {
            var brand = brandRepository.Get(id);
            var model = new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                IsPublished = brand.IsPublished
            };

            return Json(model);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand
                {
                    Name = model.Name,
                    SeoTitle = StringHelper.ToUrlFriendly(model.Name),
                    IsPublished = model.IsPublished
                };

                brandService.Create(brand);

                return Ok();
            }
            return new BadRequestObjectResult(ModelState);
        }

        [HttpPost]
        public IActionResult Edit(long id, [FromBody] BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                var brand = brandRepository.Get(id);
                brand.Name = model.Name;
                brand.SeoTitle = StringHelper.ToUrlFriendly(model.Name);
                brand.IsPublished = model.IsPublished;

                brandService.Update(brand);

                return Ok();
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var brand = brandRepository.Get(id);
            if (brand == null)
            {
                return new HttpStatusCodeResult(400);
            }

            brandService.Delete(brand);
            return Json(true);
        }
    }
}