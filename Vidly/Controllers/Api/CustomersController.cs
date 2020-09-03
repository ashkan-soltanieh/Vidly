using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Vidly.Domain;
using Vidly.Dtos;
using Vidly.Persistence;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly VidlyDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(VidlyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET /api/customers
        [HttpGet("/api/customers")]
        public IActionResult GetCustomers()
        {
            var customers =  
                _context.Customers.ToList()
                    .Select(_mapper.Map<Customer, CustomerDto>);

            return Ok(customers);
        }

        //GET /api/customers/{id}
        [HttpGet("/api/customers/{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();
            return Ok(_mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost("/api/customers")]
        public IActionResult CreateCustomer(CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.GetDisplayUrl() + customerDto.Id) , customerDto);
        }

        //PUT /api/customers/1
        [HttpPut("/api/customers/{id}")]
        public IActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            _mapper.Map<CustomerDto,Customer>(customerDto, customerInDb);

            _context.SaveChanges();

            return Ok(_mapper.Map<Customer, CustomerDto>(customerInDb));
        }

        //DELETE /api/customers/{id}
        [HttpDelete("/api/customers/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok(_mapper.Map<Customer, CustomerDto>(customerInDb));
        }
    }
}
