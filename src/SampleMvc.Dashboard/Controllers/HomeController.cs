using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleMvc.Dashboard.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SampleMvc.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : Controller
    {
        private readonly ItemDbContext _context;

        public HomeController(ItemDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }
    }
}
