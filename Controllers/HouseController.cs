using HouseShop.Core.Dto;
using HouseShop.Core.ServiceInterface;
using HouseShop.Data;
using HouseShop.Models.House;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace House.Controllers
{
    public class HouseController : Controller
    {
        private readonly HouseDbContext _context;
        private readonly IHouseService _houseService;

        public HouseController
            (
            HouseDbContext context,
            IHouseService houseService

            )
        {
            _context = context;
            _houseService = houseService;

        }

        //ListItem
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.House
                .OrderByDescending(y => y.CreatedAT)
                .Select(x => new HouseListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Address = x.Address,
                    Size = x.Size,
                    Condition = x.Condition,
                    
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            HouseViewModel model = new HouseViewModel();

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseViewModel model)
        {
            var dto = new HouseDto()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                Size = model.Size,
                Condition = model.Condition,
                CreatedAT = model.CreatedAT,
                ModifiedAT = model.ModifiedAT,

            };

            var result = await _houseService.Add(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var house = await _houseService.Delete(id);
            if (house == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var house = await _houseService.GetAsync(id);

            if (house == null)
            {
                return NotFound();
            }

            var model = new HouseViewModel();

            model.Id = house.Id;
            model.Name = house.Name;
            model.Description = house.Description;
            model.Address = house.Address;
            model.Size = house.Size;
            model.Condition = house.Condition;
            model.ModifiedAT = house.ModifiedAT;
            model.CreatedAT = house.CreatedAT;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HouseViewModel model)
        {
            var dto = new HouseDto()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                Size = model.Size,
                Condition = model.Condition,
                CreatedAT = model.CreatedAT,
                ModifiedAT = model.ModifiedAT,

            };

            var result = await _houseService.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), model);
        }
    }
}