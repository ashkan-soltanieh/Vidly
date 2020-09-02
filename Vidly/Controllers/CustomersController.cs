using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Domain;
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
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Customer customer)
        {
            if (!ModelState.IsValid) //if invalid customer
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = await _context.MembershipTypes.ToListAsync()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0) //add new valid customer
                await _context.Customers.AddAsync(customer);

            else //edit valid customer
            {
                var customerInDb = await _context.Customers.
                    SingleAsync(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Customers");
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

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.
                SingleOrDefaultAsync(c => c.Id == id);
            
            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = await _context.MembershipTypes.ToListAsync()
            };
            
            return View("CustomerForm", viewModel);
        }
    }
}
