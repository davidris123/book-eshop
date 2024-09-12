using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eshop.DomainEntities.Domain;
using EshopWebApplication1.Data;
using Eshop.Service.Interface;

namespace EshopWebApplication1.Controllers
{
    public class FoodPartnersController : Controller
    {
        private readonly IFoodPartnerService _service;

        public FoodPartnersController(IFoodPartnerService service)
        {
            _service = service;
        }


        // GET: FoodPartners
        public IActionResult Index()
        {
            return View(_service.GetFoodPartnerList());
        }
    }
}
