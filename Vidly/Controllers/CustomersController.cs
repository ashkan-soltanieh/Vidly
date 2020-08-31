using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;
using Vidly.Persistence;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly VidlyDbContext _context;

        public CustomersController(VidlyDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public async Task<IActionResult> New()
        {
            var membershipTypes = await _context.MembershipTypes.ToListAsync();
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }

        public async Task <IActionResult> Index()
        {
            var customers =  await _context.Customers.
                Include(c => c.MembershipType).ToListAsync();

            return View(customers);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }
    }
}
