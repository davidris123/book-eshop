using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eshop.DomainEntities.Domain;
using EshopWebApplication1.Data;

namespace EshopWebApplication1.Controllers
{
    public class FoodPartnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodPartnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FoodPartners
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodPartner.ToListAsync());
        }
    }
}
